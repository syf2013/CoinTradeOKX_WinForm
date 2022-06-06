using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using CoinTradeOKX.Okex.Behavior;
using Common;
using CoinTradeOKX.Event;

namespace CoinTradeOKX.Control
{
    public partial class MarketView : UserControl,IMarketView
    {
        CurrencyMarket market = null;
 
        public string Currency
        { get;
            private set;
        }


        public decimal TotalAmount
        {
            get
            {
                return this.market.TotalMoney;
            }
            set
            {

            }
        }

        public MarketView(string currency, OkxV5APIPublic<WebSocket>  api)
        {
            InitializeComponent();
           
            market = new CurrencyMarket(currency, api);

            this.Currency = market.Currency;
            this.lblCurrency.Text = this.Currency;

 
 

            StoreControlBehavior storeBehavior = new StoreControlBehavior(market);
            market.AddBehavior(storeBehavior);
            var behaviors = market.GetBehaviorList();
            this.ShowBehaviorList(behaviors);

            ShowMonitorList();
            this.timer1.Enabled = true;
            this.timer1.Start();

            EventCenter.Instance.AddEventListener(EventNames.DisableBuyBehavior, this.CancelBuy);
            EventCenter.Instance.AddEventListener(EventNames.DisableSellBehavior, this.CancelSell);
        }

        private void CancelBuy(object arg)
        {
         
        }

        private void CancelSell(object arg)
        {
             
        }

        private void ShowBehaviorList(List<BehaviorBase> behaviors)
        {
            foreach (System.Windows.Forms.Control c in this.pnlBehavior.Controls)
            {
                var view = c as BehaviorView;
                c.Visible = false;
            }

            for (int i = pnlBehavior.Controls.Count; i < behaviors.Count; i++)
            {
                var view = new BehaviorView();
                pnlBehavior.Controls.Add(view);
            }

            for (int i = 0; i < behaviors.Count; i++)
            {
                var view = pnlBehavior.Controls[i] as BehaviorView;
                view.SetBehavior(Currency, behaviors[i]);
                view.Visible = true;
            }
        }

        private bool needUpdate = false;
        private void Market_OnMarketChanged()
        {
            if (this.InvokeRequired)
            {
                needUpdate = true;
                return;
            }

            decimal ask = market.CTCAsk; 
            decimal bid = market.CTCBid; 

            string priceAskUsd = ask.ToString("$0.00");
            string priceBidUsd = bid.ToString("$0.00");

            var usdxMarket = USDXMarket.Instance;

            this.lblCTCAskPrice.Text = string.Format("￥{0:0.00}", ask * usdxMarket.OTCAsk);
            this.lblCTCBid.Text = string.Format("￥{0:0.00}", bid * usdxMarket.OTCBid);

            this.lblBuyPrice.Text = market.OTCAsk.ToString();
            this.lblCellPrice.Text = market.OTCBid.ToString();

            needUpdate = false;
        }

        private void Market_OnAmountChanged()
        {
            decimal ask = market.CTCAsk;
            decimal bid = market.CTCBid;

            string formatter = market.Instrument.AmountFormat;

           // var otcUsdtMarketMonitor = MonitorManager.Instance.OTCUSDXMarketMonitor;

            this.lblOTCAvalible.Text = market.AvalibleInAccount.ToString(formatter);
            this.lblOTCHold.Text = market.HoldInAccount.ToString(formatter);
            this.lblCTCAvalible.Text = market.AvalibleInCtcMarket.ToString(formatter);
            this.lblCTCHold.Text = market.HoldInCtcMarket.ToString(formatter);

            decimal otcTotalAmount = market.AvalibleInAccount + market.HoldInAccount;
            decimal ctcTotalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;

            this.lblOTCCny.Text = (otcTotalAmount * market.OTCBid).ToString("0.00");
            this.lblCTCCny.Text = (ctcTotalAmount * market.OTCBid).ToString("0.00");
            this.lblTotal.Text = TotalAmount.ToString("0.00");
        }

        private long last_update = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(needUpdate)
            {
                Market_OnMarketChanged();
            }
            if(last_update == 0)
            {
                last_update = DateUtil.GetTimestampMS();
            }

            long now = DateUtil.GetTimestampMS();

            this.market.Update((int)(now - last_update));

            last_update = now;

            this.lblMonitor.ForeColor = this.market.Effective ? Color.Red : Color.Black;
        }

        private void btnCurrencyBuy_Click(object sender, EventArgs e)
        {
            decimal buyAmount = 0;

            if (decimal.TryParse(txtBuyAmount.Text, out buyAmount))
            {
                if (buyAmount > 0)
                {

                    decimal usdxPrice = USDXMarket.Instance.GetOTCAsk();
                    if (usdxPrice <= 0)
                    {
                        return;
                    }
#if OKEX_API_V5
 
                    market.BuyFromCTCMarketWithAmountV5(buyAmount / usdxPrice);
#else
                    market.BuyFromCTCMarketWithUSDX(buyAmount / usdxPrice);
#endif
                }
            }
            else
            {

            }
        }

        private void ShowMonitorList()
        {
            foreach (System.Windows.Forms.Control c in this.pnlMonitor.Controls)
            {
                c.Visible = false;
                (c as MonitorView).monitor = null;
            }

            int mindex = 0;
            foreach (var m in market.MonitorManager.GetAllMonitor())
            {
                MonitorView mv;
                if (this.pnlMonitor.Controls.Count > mindex)
                {
                    mv = this.pnlMonitor.Controls[mindex] as MonitorView;
                    mv.Visible = true;
                }
                else
                {
                    mv = new MonitorView();
                    this.pnlMonitor.Controls.Add(mv);
                }

                mv.monitor = m;

                mindex++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.market.Dispose();
            this.Parent = null;
            this.Dispose();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Market_OnMarketChanged();
            this.Market_OnAmountChanged();

            int contracts = 0;

            OTCContractManager.Instance.EachContract(this.Currency, (contract) => {
                contracts += 1;
            });

            if(contracts > 0)
            {
                this.lblContracts.Visible = true;
                this.lblContracts.Text = contracts.ToString();
            }
            else
            {
                this.lblContracts.Visible = false;
            }
        }

        public void EnableAllBehavior()
        {
            this.EnableAllBehavior(true);
        }

        private void EnableAllBehavior(bool enable)
        {
            market.DisbaleAllBehavior();

            foreach (System.Windows.Forms.Control c in this.pnlBehavior.Controls)
            {
                var view = c as BehaviorView;
                if (enable)
                    view.EnableBehavior();
                else
                    view.DisableBehavior();
            }
        }

        public void DisableAllBehavior()
        {
            this.EnableAllBehavior(false);
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            WinMarket win = new WinMarket();
            win.SetMarket(this.market);
            win.Show();
        }

        bool isMini = false;
        private void btnMini_Click(object sender, EventArgs e)
        {
            isMini = !isMini;
            foreach(var c in   tabPage1.Controls)
            {
                (c as System.Windows.Forms.Control).Visible = !isMini;
            }

            this.Height = isMini ? 200 : 460;

         
        }


        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            EventCenter.Instance.RemoveListener(EventNames.DisableBuyBehavior, this.CancelBuy);
            EventCenter.Instance.RemoveListener(EventNames.DisableSellBehavior, this.CancelSell);

            this.market.Dispose();

            base.Dispose(disposing);
        }
    }
}
