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
using CoinTradeOKX.Manager;
using Common.Util;


namespace CoinTradeOKX.Control
{
    public partial class C2CMarketView : UserControl, IMarketView
    {
        CurrencyMarket market = null;

        public string Currency
        {
            get;
            private set;
        }


        public SpotStrategyEnum StrategyGroup
        {
            get; set;
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

        public C2CMarketView(string currency, SpotStrategyEnum strategy, OkxV5APIPublic<WebSocket> api)
        {
            InitializeComponent();

            this.StrategyGroup = strategy;
            market = new CurrencyMarket(currency, true, false, api);

            this.Currency = market.Currency;
            this.lblCurrency.Text = this.Currency;

            var currency2 = Config.Instance.Anchor;

            //CTCOrderManager.Instance.AddOrderMonitor(currency, currency2);


            switch (this.StrategyGroup)
            {
                case SpotStrategyEnum.Cell:
                    var ctcSellBehavios = new CTCSellBehavior(market, USDXMarket.Instance);
                    market.AddBehavior(ctcSellBehavios);
                    var ctcBuyBehavios = new CTCBuyBehavior(market, USDXMarket.Instance);
                    market.AddBehavior(ctcBuyBehavios);
                    break;
                case SpotStrategyEnum.MarketMaker:
                    //var mmBehavior = new CTCMarketMaker(market, USDXMarket.Instance);
                    var msBehavior = new CTCMakerSellBehavior(market, USDXMarket.Instance);
                    var mbBehavior = new CTCMakerBuyBehavior(market, USDXMarket.Instance);

                    market.AddBehavior(msBehavior);
                    market.AddBehavior(mbBehavior);
                    break;
            }

            var behaviors = market.GetBehaviorList();
            this.ShowBehaviorList(behaviors);
            this.depthView1.SetPriceDecimal(market.Instrument.TickSizeDigit);

            ShowMonitorList();
            this.timer1.Enabled = true;
            this.timer1.Start();
            CTCHistoryManager.Instance.LoadHistory(currency, currency2);
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

            var usdxMarket = USDXMarket.Instance;

            string strAsk = (ask * usdxMarket.OTCAsk).ToString(this.market.Instrument.PriceFormat);
            string strBid = (bid * usdxMarket.OTCBid).ToString(this.market.Instrument.PriceFormat);

            if (this.tabMain.SelectedTab == this.tabTrade)
            {
                this.tickView1.ShowTickerPrice(strAsk, strBid);
            }
            else if (this.tabMain.SelectedTab == this.tabQuick)
            {
                this.tickView2.ShowTickerPrice(strAsk, strBid);
            }


            needUpdate = false;
        }

        private void Market_OnAmountChanged()
        {
            decimal ask = market.CTCAsk;
            decimal bid = market.CTCBid;

            string formatter = market.Instrument.AmountFormat;

            decimal ctcTotalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;

            decimal price = USDXMarket.Instance.GetOTCBid() * market.CTCBid;
            decimal totalMoney = (ctcTotalAmount * price);
            if (this.tabMain.SelectedTab == this.tabTrade)
            {
                this.lblCTCAvalible.Text = market.AvalibleInCtcMarket.ToString(formatter);
                this.lblCTCHold.Text = market.HoldInCtcMarket.ToString(formatter);
                this.lblTotal.Text = totalMoney.ToString("0.00");
            }
            else if(this.tabMain.SelectedTab == this.tabQuick)
            {
                this.lblCTCAvalible2.Text = market.AvalibleInCtcMarket.ToString(formatter);
                this.lblCTCHold2.Text = market.HoldInCtcMarket.ToString(formatter);
                this.lblTotal2.Text = totalMoney.ToString("0.00");
            }
        }

        private long last_update = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (needUpdate)
            {
                Market_OnMarketChanged();
            }
            if (last_update == 0)
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
                    market.BuyFromCTCMarketWithUSDX(buyAmount/ usdxPrice);
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
            //this.Parent = null; 
            this.Parent.Controls.Remove(this);
            this.Dispose();

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

            CTCHistoryManager.Instance.ResyncHistory(this.Currency, Config.Instance.Anchor, 7);//重新同步历史数据避免数据丢失
            //CTCHistoryManager.Instance.ResyncHistory(currency1, currency2, 7);//重新同步历史数据避免数据丢失
            //CTCOrderManager.Instance.RemoveOrderMonitor(this.Currency, Config.Instance.Anchor);
            this.market.Dispose();

            base.Dispose(disposing);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedTab == this.tabDepth)
            {
                this.depthView1.SetProvider(this.market);
            }
            else
            {
                this.depthView1.SetProvider(null);
            }
        }


        private void btnStat_Click(object sender, EventArgs e)
        {
            var win = WindowManager.Instance.OpenWindow<WinCTCStat>();
            var balance = (market.HoldInCtcMarket + this.market.AvalibleInCtcMarket) * this.market.CTCBid;
            win.SetCurrency(this.Currency, Config.Instance.Anchor, balance);
            win.Show();
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            decimal buyAmount;
            if (decimal.TryParse(txtBuyAmount.Text, out buyAmount))
            {
                if (buyAmount > 0)
                {
                    decimal size = buyAmount / market.CTCAsk;
                    if (size >= market.Instrument.MinSize)
                    {
                        market.BuyFromCTCMarketWithAmountV5(buyAmount);
                    }
                    else
                    {
                        decimal minAmount = market.Instrument.MinSize * market.CTCBid;
                        WinMessage.Show(MessageType.Error, string.Format("买入金额不能小于 {0:0.00}", minAmount));
                    }
                }
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            decimal sellAmount;
            if (decimal.TryParse(txtSellAmount.Text, out sellAmount))
            {
                if (sellAmount > 0)
                {
                    decimal size = sellAmount / market.CTCBid;

                    if (size >= market.Instrument.MinSize)
                    {
                        market.SellToCTCMarketWithAmount(size, true);
                    }
                    else
                    {
                        decimal minAmount = market.Instrument.MinSize * market.CTCBid;
                        WinMessage.Show(MessageType.Error, string.Format("卖出金额不能小于 {0:0.00}", minAmount));
                    }
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要全部卖出吗？", "清仓", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

            }
        }

        private void btnAllIn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要全部买入吗？", "满仓", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

            }
        }
    }
}
