using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Control;
using CoinTradeOKX.Okex.Behavior;
using System.Net;
using CoinTradeOKX.Util;
using System.Threading;
using CoinTradeOKX.Manager;
using Common;
using CoinTradeOKX.Event;
using CoinTradeOKX.Classes;
using Common.Classes;

namespace CoinTradeOKX
{
    public partial class WinMain : Form
    {
        private const string OTCPageUrl = "https://www.okx.com/otc";
        public OkxV5APIPublic<WebSocket> v5_api = null;
        List<BehaviorBase> behaviors = new List<BehaviorBase>();
        private List<System.Windows.Forms.Control> DataViews = new List<System.Windows.Forms.Control>();
        private List<OrderView> MyOrderViews = new List<OrderView>();

        string[] scripts = { "base.js" ,"proxy.js","okex.js", "monitor.js", "wss_proxy.js" };

        string title = "";

        public WinMain()
        {
            InitializeComponent();
            title = this.Text;
           
            this.GenerateTitle();
        }


        private void GenerateTitle()
        {
            var ver = Application.ProductVersion.ToString();
            string text = title + "   ver:" + ver + (this.IsOtcMode ? "(法币版)" : "(币币版)");

            if (Config.Instance.ApiInfo.IsSimulated)
                text += "   模拟盘";

            this.Text = text;
        }

        private void OnCurrencyMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.AddMarketView(item.Tag.ToString(), SpotStrategyEnum.Cell);
        }

        private void OnClickMarketMakerMenuItem(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.AddMarketView(item.Tag.ToString(), SpotStrategyEnum.MarketMaker);
        }

        private void ReloadCurrencyMenus()
        {
            this.mnCurrencies.DropDownItems.Clear();
 
            foreach(string s in Config.Instance.PlatformConfig.Currencies)
            {
                var item = new ToolStripMenuItem();
                item.Text = s.Trim().ToUpper();
                


                //子菜单类型
                var itemCell = new ToolStripMenuItem("网格策略");
                itemCell.Tag = item.Text;
                itemCell.Click += this.OnCurrencyMenuItemClick; 
                item.DropDownItems.Add(itemCell);

                var mmCell = new ToolStripMenuItem("做市商");
                mmCell.Tag = item.Text;
                mmCell.Click += this.OnClickMarketMakerMenuItem;
                item.DropDownItems.Add(mmCell);



                this.mnCurrencies.DropDownItems.Add(item);
            }
        }

        private void ShowBehaviorList()
        {
            foreach(System.Windows.Forms.Control c in this.pnlBehavior.Controls)
            {
                var view = c as BehaviorView;
                c.Visible = false;
            }

            for(int i = pnlBehavior.Controls.Count; i < this.behaviors.Count;i++)
            {
                var view = new BehaviorView();
                pnlBehavior.Controls.Add(view);
            }

            for (int i = 0; i < this.behaviors.Count; i++)
            {
                var view = pnlBehavior.Controls[i] as BehaviorView;
                view.SetBehavior(Config.Instance.Anchor,this.behaviors[i]);
                view.Visible = true;
            }
        }

        private void ChangeMarketType()
        {
            bool isOtc = this.IsOtcMode;

            this.lblContracts.Visible = isOtc;
            this.groupBox2.Visible = isOtc;
            this.groupBox3.Visible = isOtc;
            this.lblContracts.Visible = isOtc;
            this.nudTotalMoney.Visible = isOtc;
            this.label3.Visible = isOtc;
            this.menuWindow.Visible = isOtc;
            this.menuTeam.Visible = isOtc;
            this.pnlUsdxMarket.Visible = isOtc;
            this.mniBank.Visible = isOtc;
            this.mniBrowser.Visible = isOtc;

            if (isOtc)
                this.tabPage1.Show();
            else
                this.tabPage1.Hide();

        }
        private void ShowMonitorList()
        {
            if (IsOtcMode)
            {   var a = OTCOrderManager.Instance;
                var b = OTCContractManager.Instance;
                var _c = OTCReceiptAccountManager.Instance;
            }
            else
            {
               var a = CTCOrderManager.Instance;
            }

            foreach (System.Windows.Forms.Control c in this.pnlMonitor.Controls)
            {
                c.Visible = false;
                (c as MonitorView).monitor = null;
            }

            int mindex = 0;
            IEnumerable<MonitorBase> monitors =  USDXMarket.Instance.MonitorManager.GetAllMonitor().Concat( MonitorManager.Default.GetAllMonitor());

            foreach (var m in monitors)
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




        private void timer_render_Tick(object sender, EventArgs e)
        {

        }


        private OrderView GetOrderView(int index)
        {
            var views = this.pnlMyOrders.Controls;


            OrderView view = views.Count > index ? views[index] as OrderView : null; // new OrderView();

            /*
            foreach (var control in this.pnlMyOrders.Controls)
            {
                if(!(control as OrderView).Visible)
                {
                    view = control as OrderView;
                    view.Visible = true;
                    break;
                }
            }
            */

            if (view == null)
            {
                view = new OrderView();
                view.OnCancelled += View_OnCancelled;
                this.pnlMyOrders.Controls.Add(view);
                MyOrderViews.Add(view);
            }

            view.Visible = true;

            return view;
        }

        /**
         *我的订单监视
         */
        private void ShowMyOrders()
        {

            int index = 0;
            int used = 0;

            if (IsOtcMode)
            {

                OTCOrderManager manager = OTCOrderManager.Instance;

                manager.EachSellOrder((o) =>
                {
                    var view = this.GetOrderView(index ++);
                    view.SetOrder(o);
                    used++;
                });

                manager.EachBuyOrder((o) =>
                {
                    var view = this.GetOrderView(index ++);
                    view.SetOrder(o);
                    used++;
                });
            }
            else
            {
                CTCOrderManager manager = CTCOrderManager.Instance;

                manager.EachSellOrder((o) =>
                {
                    var view = this.GetOrderView(index ++);
                    view.SetOrder(o);
                    used++;
                });

                manager.EachBuyOrder((o) =>
                {
                    var view = this.GetOrderView(index ++);
                    view.SetOrder(o);
                    used++;
                });
            }

            var views = this.pnlMyOrders.Controls;

            for(int i = 0; i < views.Count;i++)
            {
                var v = views[i];
                v.Visible = index > i;
            }

            /*
            foreach (var control in this.pnlMyOrders.Controls)
            {
                if (used == 0)
                {
                    (control as OrderView).Visible = false;
                }
                else
                {
                    used--;
                }
            }
            */
        }

        private void View_OnCancelled(long id)
        {
            foreach(OrderView view in this.MyOrderViews)
            {
                if(view.OrderID == id)
                {
                    view.Visible = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.Instance.NewLog += this.ShowLog;

            this.groupBox1.Text = string.Format("稳定币{0}" , Config.Instance.Anchor);
            
            ReloadCurrencyMenus();

            DataViews.Add(pnlMyOrders);
            DataViews.Add(pnlBehavior);
            DataViews.Add(pnlMonitor);
            MonitorManager.Default.AddMonotor(new TimeMonitor());

            EventCenter.Instance.AddEventListener(EventNames.ConfigChanged,this.OnConfigChanged);

            if(!IsOtcMode)
            {
                this.OnInitializa(null);
            }
        }


        private void OnConfigChanged(object obj)
        {
            ReloadCurrencyMenus();

            this.GenerateTitle();
        }

        private bool isInited = false;
        private void OnInitializa(JObject data)
        {
            if (IsOtcMode)
            {

                this.timer_account.Enabled = true;
            }

            if (isInited)
                return;

            this.pnlMarketViews.Visible = true;
            lblLoginName.Text = Config.Instance.Account.GetLoginName();
            isInited = true;
            USDXMarket.CreateMarketInstance(Config.Instance.Anchor);
            USDXMarket.Instance.OnAmountChanged += UpdateUsdxAmountInfo;
            USDXMarket.Instance.OnMarketChanged += UpdateUsdxMarketInfo;

            
   

            v5_api = new OkxV5APIPublic<WebSocket>();// new Okex_V3_API(new WebSocketProxy(proxy));
            v5_api.OnLogin += V3_api_OnLogin;

            USDXMarket.Instance.AddBehavior(new StoreBehavior(USDXMarket.Instance));

            if (IsOtcMode)
            {
                USDXMarket.Instance.AddBehavior(new OTCAccountBehavior());
            }

            //USDXMarket.Instance.AddBehavior(new ACT2CTCTransferBehavior(USDXMarket.Instance));

            this.behaviors = new List<BehaviorBase>(USDXMarket.Instance.GetBehaviorList());

            ShowBehaviorList();
            ShowMonitorList(); //加载显示所有监视器
            ChangeMarketType();

            this.timer_monitor.Enabled = true;

        }

        private void UpdateUsdxAmountInfo()
        {
            var market = USDXMarket.Instance;
            this.lblCTCUsdt.Text = market.AvalibleInCtcMarket.ToString("0.00");
            this.lblOTCUsdt.Text = market.AvalibleInAccount.ToString("0.00");
            decimal totalAmount = 0;

            totalAmount = market.AvalibleInAccount + market.HoldInAccount - Config.Instance.Account.Deposit;
            totalAmount += market.AvalibleInCtcMarket + market.HoldInCtcMarket;

            decimal cny = (market.OTCAsk * totalAmount);
            lblUsdtCny.Text = cny.ToString("0.00");
        }


        private void AddMarketView(string currency, SpotStrategyEnum strategy )
        {
            if (USDXMarket.Instance == null)
            {
                MessageBox.Show("请先初始化");
                return;
            }

            // System.Activator.CreateInstance(behaviors[0],)

            //behaviors[0].

            OTCAccount accountSetting = Config.Instance.Account;

            if (IsOtcMode)
            {

                foreach (var c in this.pnlMarketViews.Controls)
                {
                    var v = c as MarketView;
                    if (v != null)
                    {
                        if (string.Compare(currency, v.Currency, true) == 0)
                        {
                            return;
                        }
                    }
                }

                MarketView view = new MarketView(currency, this.v5_api);
                this.pnlMarketViews.Controls.Add(view);
            }
            else if (accountSetting.MarketType == MarketTypeEnum.CTCMarket)
            {
                foreach (var c in this.pnlMarketViews.Controls)
                {
#if OKEX_API_V5
                    var v = c as C2CMarketView;
#else
                    var v = c as C2CMarketView;
#endif
                    if (v != null)
                    {
                        if (string.Compare(currency, v.Currency, true) == 0)
                        {
                            return;
                        }
                    }
                }

#if OKEX_API_V5

                C2CMarketView view = new C2CMarketView(currency,strategy, this.v5_api);
#else
                C2CMarketView view = new C2CMarketView(currency);//, this.v3_api);
#endif
                this.pnlMarketViews.Controls.Add(view);
            }
        }
            
        

        private void UpdateUsdxMarketInfo()
        {
            var market = USDXMarket.Instance;
            this.lblUsdtPrice.Text = market.AskAvg.ToString("0.00");
            this.lblUsdtAmount.Text = market.TotalAskAmount.ToString("0.0");

            this.lblUsdtMinPrice.Text = market.OTCAsk.ToString("0.00");
            this.lblUsdtAmountMin.Text =  market.AskAmount.ToString("0.0");
        }


        private void V3_api_OnLogin(bool login)
        {
            Logger.Instance.Log(LogType.Info, "websocket login success");
        }


        private delegate void ShowLogDelegate(Log log);
        private void ShowLog(Log log)
        {
            if (txtConsole.InvokeRequired)
            {
                ShowLogDelegate methon = new ShowLogDelegate(ShowLog);//委托的方法参数应和SetCalResult一致
                IAsyncResult syncResult = txtConsole.BeginInvoke(methon, new object[] { log }); //此方法第二参数用于传入方法,代替形参result
                txtConsole.EndInvoke(syncResult);
            }
            else
            {
                if (this.tabControl1.SelectedTab != this.tabLog || this.txtConsole.Focused)
                    return;

                if (log.Type != LogType.Error)
                    return;

                var t = this.txtConsole.Text;
                t += "\r\n" + log.ToString();
                this.txtConsole.Text = t;// Logger.Instance.LogContent(true);
                this.txtConsole.SelectionStart = this.txtConsole.Text.Length;
                this.txtConsole.SelectionLength = 0;
                this.txtConsole.ScrollToCaret();
            }
        }

        //发送socket心跳
        private void timer_api_ping_Tick(object sender, EventArgs e)
        {
            if(this.v5_api != null && this.v5_api.IsLogin)
            {
                this.v5_api.SendPing();
            }
        }

        private void timer_state_scan_Tick(object sender, EventArgs e)
        {
            decimal money = USDXMarket.Instance != null ? USDXMarket.Instance.TotalMoney : 0;
            foreach(var c in this.pnlMarketViews.Controls)
            {
                var mv = c as IMarketView;
                money += mv.TotalAmount;
            }

           // if (USDXMarket.Instance != null)
            //{
              //  this.Text = string.Format("{0} - {1} : {2} | {3:0.00}", title, USDXMarket.Instance.Currency, USDXMarket.Instance.OTCAsk, USDXMarket.Instance.OTCAsk * USDXMarket.Instance.AvalibleInCtcMarket);
           // }

            this.lblTotalMoney.Text = money.ToString("0.00");
            ShowMyOrders();
            if (IsOtcMode)
            {
                
                uint cc = OTCContractManager.Instance.ContractCount;

                this.lblContracts.Text = cc.ToString();
                this.lblContracts.ForeColor = cc > 0 ? Color.White : Color.Black;
            }
        }

        long time_monitor = -1; 
        private void timer_monitor_Tick(object sender, EventArgs e)
        {
            if(time_monitor == -1)
            {
                time_monitor = DateUtil.GetTimestampMS();
                return;
            }

            long now = DateUtil.GetTimestampMS();
            int dt =  (int)(now - time_monitor);
            time_monitor = now;

            USDXMarket.Instance.Update(dt);

            MonitorManager.Default.Update(dt);

            decimal cash = OTCCashManager.Instance.TotalCash;

            if (cash != this.nudTotalMoney.Value)
            {
                this.nudTotalMoney.Value = cash;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DataView_DBClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private bool IsOtcMode
        {
            get
            {
                var marketType = Config.Instance.Account.MarketType;
                return marketType  == MarketTypeEnum.OTCMarket || marketType == MarketTypeEnum.None;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.isClosed)
            {
                return;
            }

            if(IsOtcMode && OTCOrderManager.Instance != null && (OTCOrderManager.Instance.OrderCount > 0))
            {
                if(MessageBox.Show("当前存在未成交的挂单，是否继续退出？", "挂单提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (this.v5_api != null)
            {
                this.v5_api.OnLogin -= V3_api_OnLogin;
                this.v5_api.Close();
                this.v5_api = null;
            }

            
            foreach(var t in this.behaviors)
            {
                t.Enable = false;
                t.Dispose();
            }

            MonitorManager.Default.DestoryAllMonitors();
            EventCenter.Instance.RemoveListener(EventNames.ConfigChanged, this.OnConfigChanged);
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 账号设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var win = new WinCTCConfig();// new WinConfig();
            win.Show();
        }


        private void 重启ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
 
        }


        
        private void 订单管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowManager.Instance.OpenWindow<WinContract>();
        }

 

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void nudTotalMoney_ValueChanged(object sender, EventArgs e)
        {
             OTCCashManager.Instance.ResetCash( this.nudTotalMoney.Value);
        }

        private void timer_timeCorrecting_Tick(object sender, EventArgs e)
        {
#if !OKEX_API_V5
            Action t = async ()=>{ await DateUtil.CorrectingServerTime(Okex_REST_API.TimeService); };
            t.Invoke();
#endif
        }

        private void lblContracts_DoubleClick(object sender, EventArgs e)
        {
            WindowManager.Instance.OpenWindow<WinContract>();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            WinMarket win = new WinMarket();
            win.SetMarket(USDXMarket.Instance);
            win.Show();
        }

        private void 销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var win = new WinHistory();
            win.Show();
        }

        private void btnStopBuy_Click(object sender, EventArgs e)
        {
            EventCenter.Instance.Emit(EventNames.DisableBuyBehavior);
        }

        private void btnStopSell_Click(object sender, EventArgs e)
        {
            EventCenter.Instance.Emit(EventNames.DisableSellBehavior);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void OnClickBuyProfitMenuItem(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if(item != null)
            {
                double profit = 0;

                if(double.TryParse(item.Text, out profit))
                {
                    EventCenter.Instance.Emit(EventNames.Set_Buy_Profit, profit);
                }
            }
        }

        private void OnClickSellProfitMenuItem(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item != null)
            {
                double profit = 0;

                if (double.TryParse(item.Text, out profit))
                {
                    EventCenter.Instance.Emit(EventNames.Set_Sell_Profit, profit);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            (new WinPassword()).ShowDialog();
        }

        private void 浏览器设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void timer_account_Tick(object sender, EventArgs e)
        {
            var items = this.flyReceiptAccount.Controls;

            foreach(UserControl c in items)
            {
                c.Visible = false;
            }

            OTCReceiptAccountManager mgr = OTCReceiptAccountManager.Instance;
            int i = 0;
            mgr.EachAccount((account) =>{

                if (!account.Disabled)
                {
                    ReceiptAccountView view = null;

                    if (items.Count > i)
                    {
                        view = items[i] as ReceiptAccountView;
                        view.Visible = true;
                    }
                    else
                    {
                        view = new ReceiptAccountView();
                        this.flyReceiptAccount.Controls.Add(view);
                    }

                    view.SetAccount(account);
                    i++;
                }
                
            }, AccountApplyType.All );
        }

        private void 收付款设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowManager.Instance.OpenWindow<WinReceiptAccount>();
        }

        private void 团队管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowManager.Instance.OpenWindow<WinTeam>();
        }

        private void mnCurrencies_Click(object sender, EventArgs e)
        {

        }


        bool isClosed = false;
        private void WinMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!this.isClosed)
            {
                this.isClosed = true;
                WindowManager.Instance.CloseAll();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.tabControl1.SelectedTab == this.tabPage2)
            {
                this.ShowMonitorList();
            }
        }
    }
}
