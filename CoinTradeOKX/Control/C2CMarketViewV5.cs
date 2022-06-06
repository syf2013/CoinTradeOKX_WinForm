using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeGecko.Monitor;
using CoinTradeGecko.Okex;
using CoinTradeGecko.Okex.Entity;
using CoinTradeGecko.Okex.Behavior;
using Common;
using CoinTradeGecko.Event;
using CoinTradeGecko.Manager;
using Common.Util;

namespace CoinTradeGecko.Control
{
    public partial class C2CMarketViewV5 : UserControl,IMarketView
    {
        CurrencyMarket market = null;
 

        Okex_V3_API<WebSocket> Socket = null;

 
        public string Currency
        { get;
            private set;
        }


        public decimal TotalAmount
        {
            get
            {
                decimal bid = market.CTCBid;
                decimal ctcTotalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;
                return bid * USDXMarket.Instance.GetOTCBid() * ctcTotalAmount;
            }

             set
            {

            }
        }

        public C2CMarketViewV5(string currency)
        {
            InitializeComponent();

            var api = this.Socket = new Okex_V3_API<WebSocket>();
           
            market = new CurrencyMarket(currency,true,false, api);

            this.Currency = market.Currency;
            this.lblCurrency.Text = this.Currency;

            var currency2 = Config.Instance.Anchor;

            CTCOrderManager.Instance.AddOrderMonitor(this.Currency, currency2);

            var ctcCellBehavios = new CTCCellBehavior(market, USDXMarket.Instance);
            market.AddBehavior(ctcCellBehavios);

            /*
            var otcBuyBehavior = new OTCBuyBehavior(market, USDXMarket.Instance);
            market.AddBehavior(otcBuyBehavior);

            StoreControlBehavior storeBehavior = new StoreControlBehavior(market);
            market.AddBehavior(storeBehavior);*/


            var behaviors = market.GetBehaviorList();
            this.ShowBehaviorList(behaviors);
            this.depthView1.SetPriceDecimal(market.Instrument.TickSizeDigit);

            ShowMonitorList();
            this.timer1.Enabled = true;
            this.timer1.Start();

            EventCenter.Instance.AddEventListener(EventNames.DisableBuyBehavior, this.CancelBuy);
            EventCenter.Instance.AddEventListener(EventNames.DisableSellBehavior, this.CancelSell);

            CTCHistoryManager.Instance.LoadHistory(currency, currency2);
        }

        private void CancelBuy(object arg)
        {
            foreach (System.Windows.Forms.Control c in this.pnlBehavior.Controls)
            {
                var view = c as BehaviorView;
               if(view.Behavior is OTCBuyBehavior)
                {
                    view.DisableBehavior();
                }
            }
        }

        private void CancelSell(object arg)
        {
            foreach (System.Windows.Forms.Control c in this.pnlBehavior.Controls)
            {
                var view = c as BehaviorView;
                if (view.Behavior is OTCSellBehavior)
                {
                    view.DisableBehavior();
                }
            }
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


            needUpdate = false;
        }

        private void Market_OnAmountChanged()
        {
            decimal ask = market.CTCAsk;
            decimal bid = market.CTCBid;

            string formatter = market.Instrument.AmountFormat;

           // var otcUsdtMarketMonitor = MonitorManager.Instance.OTCUSDXMarketMonitor;

            this.lblCTCAvalible.Text = market.AvalibleInCtcMarket.ToString(formatter);
            this.lblCTCHold.Text = market.HoldInCtcMarket.ToString(formatter);

            decimal otcTotalAmount = market.AvalibleInAccount + market.HoldInAccount;
            decimal ctcTotalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;

            decimal price = USDXMarket.Instance.GetOTCBid() * market.CTCBid;
            decimal totalMoney = (ctcTotalAmount * price);
            this.lblTotal.Text = totalMoney.ToString("0.00");
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

           if(decimal.TryParse(txtBuyAmount.Text, out buyAmount))
            {
                if (buyAmount > 0)
                {
#if OKEX_API_V5
 
                        market.BuyFromCTCMarketWithAmountV5(buyAmount);
                    
#else
                    decimal usdxPrice = USDXMarket.Instance.GetOTCAsk();
                    if (usdxPrice > 0)
                    {
                        market.BuyFromCTCMarketWithUSDX(buyAmount/ usdxPrice);
                    }
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
            if(this.Socket !=null)
            {
                this.Socket.Close();
                this.Socket = null;
            }

           

            this.market.Dispose();
            //this.Parent = null; 
            this.Parent.Controls.Remove(this);
            this.Dispose();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Socket != null)
            {
                this.Socket.SendPing();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Market_OnMarketChanged();
            this.Market_OnAmountChanged();
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
            CTCOrderManager.Instance.RemoveOrderMonitor(this.Currency, Config.Instance.Anchor);
            this.market.Dispose();

            base.Dispose(disposing);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( this.tabMain.SelectedTab == this.tabDepth)
            {
                this.depthView1.SetProvider(this.market);
            }
            else
            {
                this.depthView1.SetProvider(null);
            }

            if (this.tabMain.SelectedTab == this.tabIndex)
            {
                this.timerIndex.Enabled = true;
            }
            else
            {
                this.timerIndex.Enabled = false;
            }
        }

        private void timerIndex_Tick(object sender, EventArgs e)
        {
            uint maCount = 5;
            uint maSize = 5;
            var maList = IndexUtil.MA(this.market, maCount, maSize);
            decimal maVal = maList!= null && maList.Count > 0 ? maList[0] : 0;

            this.lblMA5.Text = string.Format("MA{0}:{1}", maCount, maVal);
            

        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            var win = new WinCTCStat();
            var balance = this.market.AvalibleInCtcMarket * this.market.CTCBid;
            win.SetCurrency(this.Currency, Config.Instance.Anchor,balance);
            win.Show();
        }
    }
}
