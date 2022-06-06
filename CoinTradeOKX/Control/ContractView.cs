using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeOKX.Okex.Entity;
using CoinTradeOKX.Okex.Const;
using Newtonsoft.Json.Linq;

namespace CoinTradeOKX.Control
{
    public partial class ContractView : UserControl
    {
        public ContractView()
        {
            InitializeComponent();

        }

        public event Action<ContractView> OnContractComplete = null;

        private OTCContract _contract = null;
        public long ContractId
        {
            get;private set;
        }
        public OTCContract Contract
        {
            get { return _contract; }

            set {

                bool isSameContract = false;
                if (_contract != null && value.PublicOrderId == _contract.PublicOrderId)
                {
                    isSameContract = true;
                    value.CopyTo(_contract);
                }
                else
                {
                    _contract = value.Clone() as OTCContract;
                }

                this.ContractId = value.PublicOrderId;

                if (!isSameContract)
                {

                    var userVo = value.OrderDetailUserVo;

                    this.lblRealName.Text = value.RealName;
                    this.lblSide.Text = value.Side == Side.Sell ? "卖出" : "买入";
                    this.lblSize.Text = value.BaseAmount.ToString();
                    this.lblMoney.Text = string.Format("{0:0.00}(单价:{1:0.00})", value.QuoteAmount, value.Price);
                    this.lblCurrency.Text = value.BaseCurrency.ToUpper();
                    this.lblRegisterDate.Text = userVo.CreatedDate.ToShortDateString();
                    this.lblFee.Text = (value.PlatformInAmount * value.Price).ToString("0.00");
                    this.lblKycLevel.Text = string.Format("{0}级", userVo.KycLevel);

                    float completeQualityPersent = 1.0f * userVo.CompletedOrderQuantity / (userVo.CompletedOrderQuantity + userVo.CancelledOrderQuantity);

                    this.lblComplete.Text = string.Format("{0}/{1:0.00%}", userVo.CompletedOrderQuantity, completeQualityPersent);

                    this.tabBank.Parent = null;
                    this.tabAlipay.Parent = null;
                    this.tabWeChatPay.Parent = null;

                    if (_contract.Side == Side.Buy)
                    {
                        foreach (var p in value.SellerAllReceiptAccountList)
                        {
                            if (p.Type == PayType.Bank)
                            {
                                this.txtBankAccount.Text = p.AccountName;
                                this.txtBankNumber.Text = p.AccountNo;
                                this.txtBankName.Text = p.BankName;
                                this.tabBank.Parent = this.tabPayInfo;
                            }
                            else if (p.Type == PayType.Aliypay)
                            {
                                this.txtAlipayAccount.Text = p.AccountNo;
                                this.btnAlipayQrCode.Visible = !string.IsNullOrEmpty(p.AccountQrCodeUrl);
                                this.tabAlipay.Parent = this.tabPayInfo;
                            }
                            else if (p.Type == PayType.WeChatPay)
                            {
                                this.txtWeChatAccount.Text = p.AccountNo;
                                this.btnWeChatQrCode.Visible = !string.IsNullOrEmpty(p.AccountQrCodeUrl);
                                this.tabWeChatPay.Parent = this.tabPayInfo;
                            }
                        }
                    }

                    this.tabPayInfo.Visible = value.Side == Side.Buy;
                }

                this.btnPaid.Visible = value.Side == Side.Buy && value.PaymentStatus == PaymentStatus.Unpaid;
                this.btnRelease.Visible = value.Side == Side.Sell && value.PaymentStatus == PaymentStatus.Paid;
                this.lblPaymentType.Visible = value.Side == Side.Sell && value.PaymentStatus == PaymentStatus.Paid;

                this.lblAppealed.Visible = value.OrderStatus == ContractStatus.Appealed;

                if(lblPaymentType.Visible)
                {
                    string paymentTypeName = "";

                    if (string.Compare(value.SellerReceiptAccount.Type , PayType.Aliypay, true) == 0)
                    {
                        paymentTypeName = "支付宝";
                    }
                    else if (string.Compare(value.SellerReceiptAccount.Type,PayType.Aliypay,true) == 0)
                    {
                        paymentTypeName = "银行卡";
                    }
                    else if( string.Compare(value.SellerReceiptAccount.Type, PayType.WeChatPay,true) == 0)
                    {
                        paymentTypeName = "微信";
                    }

                    this.lblPaymentType.Text = string.Format("支付方式: {0} {1}", value.SellerReceiptAccount.AccountName, paymentTypeName);
                }

                this.btnRelease.Enabled = true;// this.btnRelease.Visible;
                this.btnPaid.Enabled = true;// this.btnPaid.Visible;
            }
        }

        async public void Release()
        {
           bool result = await OTCContractManager.Instance.ReleaseContract(this.Contract.PublicOrderId);

            if(result)
            {
                this.btnRelease.Visible = false;
                this.OnContractComplete?.Invoke(this);
            }
            else
            {
                this.btnRelease.Enabled = true;
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            string msg = string.Format("确定收到{0}的款项{1:0.00}元，并放币",this.lblRealName.Text,Contract.QuoteAmount);
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Release();
                this.btnRelease.Enabled = false;
            }
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
           // ContractHelper.ContractPaid(this.Contract);//TODO
        }

        private ReceiptAccount GetPaymentAccount(string type)
        {
            foreach(var v in this.Contract.SellerAllReceiptAccountList)
            {
                if (v.Type == type)
                    return v;
            }

            return null;
        }

        private void btnAlipayQrCode_Click(object sender, EventArgs e)
        {
            var alipayAccount = this.GetPaymentAccount(PayType.Aliypay);
            ShowQrCodeForm(alipayAccount);
        }

        private void ShowQrCodeForm(ReceiptAccount account)
        {
            if (account != null)
            {
                if (!string.IsNullOrEmpty(account.AccountQrCodeUrl))
                {
                    (new WinQrCode(account.Id, account.AccountName, account.AccountQrCodeUrl)).Show();
                }
            }
        }

        private void btnWeChatQrCode_Click(object sender, EventArgs e)
        {
            var wechatAccount = this.GetPaymentAccount(PayType.WeChatPay);
            ShowQrCodeForm(wechatAccount);
        }

        private void lblPaymentType_Click(object sender, EventArgs e)
        {

        }
    }
}
