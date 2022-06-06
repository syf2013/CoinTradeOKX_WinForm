using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using CoinTradeOKX.Mail;
using CoinTradeOKX.Control;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Monitor;

namespace CoinTradeOKX
{
    public partial class WinContract : Form
    {
        public WinContract()
        {
            InitializeComponent();
        }

        private void Monitor_OnData(MonitorBase obj)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        enum MailTestResult
        {
            Success = 0,
            ConnectFailed = 1,
            LoginFailed = 2
        }

        private void StartAutoMatching()
        {
            string host = this.txtPop3Host.Text;
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Text;
            int port = int.Parse(this.txtPop3Port.Text); ;


            //transrecordParser = new TransferRecordParser(host, port, username, password);
            //transrecordParser.BankMailAddress = this.txtBankEmail.Text;
            //transrecordParser.Matching = this.txtMatching.Text;

            //transrecordParser.OnNewTransferRecord += TransrecordParser_OnNewTransferRecord;

            this.btnSave.Enabled = false;
        }

        delegate void ShowTransfer(TransferRecord record);

        private void ShowTransferRecord(TransferRecord record)
        {
            /*
            if(this.pnlTransferRecord.InvokeRequired)
            {
                ShowTransfer methon = new ShowTransfer(ShowTransferRecord);//委托的方法参数应和SetCalResult一致
                IAsyncResult syncResult = pnlTransferRecord.BeginInvoke(methon, new object[] { record }); //此方法第二参数用于传入方法,代替形参result
                pnlTransferRecord.EndInvoke(syncResult);

                return;
            }


            TransferView view = null;
            var rows = this.pnlTransferRecord.Controls;
            if (rows.Count >= 50)
            {
                view = rows[rows.Count - 1] as TransferView;
                rows.RemoveAt(rows.Count - 1);
            }
            else
            {
                view = new TransferView();
            }

            this.pnlTransferRecord.Controls.Add(view);
            view.SetRecord(record);
            this.pnlTransferRecord.Controls.SetChildIndex(view, 0);
            */
        }

        private void TransrecordParser_OnNewTransferRecord(TransferRecord obj)
        {
           // this.ShowTransferRecord(obj);
        }

        private void WinContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if(this.transrecordParser != null)
            //{
            //    this.transrecordParser.Dispose();
            //    this.transrecordParser = null;
            //}

            this.contractList1.Destroy();
        }

        bool releasing = false;
        private async void ReleaseContract(long contract, TransferRecord transferRecord)
        {
            releasing = true;

            //this.transrecordParser.RemoveRecord(transferRecord);
            bool result = await OTCContractManager.Instance.ReleaseContract(contract);

            if (result)
            {
                this.contractList1.RemoveContractViewByContract(contract);
            }

            releasing = false;
        }


        long serverTimeDiffMS = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {

            this.UpdateUsdxAmountInfo();

            /*
            if (this.transrecordParser == null)
                return;
            ContractManager manager = ContractManager.Instance;

            if (manager == null)
                return;

           // if(serverTimeDiffMS == -1)
            {
                long serverNowMs = DateUtil.GetServerTimestampMS();
                long serverUtcMs = DateUtil.GetServerUTCTimestampMS();

                serverTimeDiffMS = serverNowMs - serverUtcMs;
            }

            manager.EachContract("", (contract) =>
            {

                if (releasing)
                    return;

                if (contract.Side == Side.Sell)
                {
                    if (contract.PaymentStatus == PaymentStatus.Paid)
                    {
                        DateTime contractTimeUtc = contract.CreatedDate;
                        contractTimeUtc = contractTimeUtc.AddMilliseconds(-serverTimeDiffMS);
                        var record = this.transrecordParser.FindRecord(contract.RealName, contract.QuoteAmount, contractTimeUtc,(decimal)0.0001);

                        if(record != null)
                        {
                            this.ReleaseContract(contract.PublicOrderId, record);
                        }
                    }
                }
            });*/
        }

        private void contractList1_Load(object sender, EventArgs e)
        {
            
        }

        private void WinContract_Load(object sender, EventArgs e)
        {
            this.deepView1.SetProvider(USDXMarket.Instance);
        }

        private void UpdateUsdxAmountInfo()
        {
            var market = USDXMarket.Instance;
            this.lblCTCUsdt.Text = market.AvalibleInCtcMarket.ToString("0.00");
            this.lblOTCUsdt.Text = market.AvalibleInAccount.ToString("0.00");


            var totalAmount = market.AvalibleInAccount + market.HoldInAccount;
            totalAmount += market.AvalibleInCtcMarket + market.HoldInCtcMarket;

            decimal cny = (market.OTCAsk * totalAmount);
            lblUsdtCny.Text = cny.ToString("0.00");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            WinMarket win = new WinMarket();
            win.SetMarket(USDXMarket.Instance);
            win.Show();
        }

        private void lblUsdtAmountMin_Click(object sender, EventArgs e)
        {

        }
    }
}
