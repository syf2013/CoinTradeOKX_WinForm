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
using CoinTradeOKX.Okex;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Monitor;
using Common.Classes;
using CoinTradeOKX.Manager;

namespace CoinTradeOKX
{
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        }

        private MarketTypeEnum marketType
        {
            get;set;
        }
        public event Action<long> OnCancelled;

        
        private void OrderView_Load(object sender, EventArgs e)
        {

        }

        public string InstrumentId
        {
            get;set;
        }

        public long OrderID
        {
            get;
            private set;
        }

        public void SetOrder(OrderBase order)
        {
            if(order == null)
            {
                this.OrderID = 0;
                return;
            }


            if (this.marketType == MarketTypeEnum.CTCMarket)
            {
                this.InstrumentId = order.Currency;
            }

            this.OrderID = order.PublicId;
            this.lblIndex.Text = order.Index > 0 ? order.Index.ToString() : "-";
            this.lblPrice.Text = order.MarketType == MarketTypeEnum.OTCMarket ? order.Price.ToString("0.00") : order.Price.ToString() ;
            this.lblSide.Text = order.Side == Okex.OrderOparete.Buy ? "买入" : "卖出";
            this.lblSide.ForeColor = order.Side == Okex.OrderOparete.Buy ? Color.Green : Color.Red;
            this.lblAmount.Text = (order.AvailableAmount * order.Price).ToString("0.00");
           // this.lblKycLevel.Text = string.Format("Kyc{0}", order.MinKycLevel);
            this.lblCurrency.Text = order.Currency.ToUpper();
            this.btnOperate.Enabled = true;
            this.marketType = order.MarketType;

            if(this.marketType == MarketTypeEnum.CTCMarket)
            {
                this.InstrumentId = order.Currency;
            }

        }

        async private void doCancel(long orderId)
        {
            if (this.marketType == MarketTypeEnum.CTCMarket)
            {
                CTCOrderManager.Instance.CancelOrder(this.InstrumentId, this.OrderID);
            }
            else
            {
                bool result = await Task.Run<bool>(() => { return OTCOrderManager.Instance.CancelOrder(orderId, false); });

                if (result)
                {
                    this.OnCancelled?.Invoke(orderId);
                }
                else
                {
                    Logger.Instance.Log(LogType.Error, "取消挂单错误");
                    this.btnOperate.Enabled = true;
                }
            }
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            if(this.OrderID != 0)
            {
                this.doCancel(this.OrderID);
                this.btnOperate.Enabled = false;   
            }
        }
    }
}
