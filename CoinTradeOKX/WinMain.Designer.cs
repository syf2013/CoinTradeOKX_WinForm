namespace CoinTradeOKX
{
    partial class WinMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMain));
            this.timer_api_ping = new System.Windows.Forms.Timer(this.components);
            this.timer_state_scan = new System.Windows.Forms.Timer(this.components);
            this.timer_monitor = new System.Windows.Forms.Timer(this.components);
            this.timer_render = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlMyOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMonitor = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flyReceiptAccount = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopSell = new System.Windows.Forms.Button();
            this.btnStopBuy = new System.Windows.Forms.Button();
            this.lblContracts = new System.Windows.Forms.Label();
            this.nudTotalMoney = new System.Windows.Forms.NumericUpDown();
            this.lblTotalMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblCTCUsdt = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblOTCUsdt = new System.Windows.Forms.Label();
            this.lblUsdtCny = new System.Windows.Forms.Label();
            this.pnlUsdxMarket = new System.Windows.Forms.Panel();
            this.lblUsdtPrice = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUsdxDetails = new System.Windows.Forms.Button();
            this.lblUsdtAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblUsdtMinPrice = new System.Windows.Forms.Label();
            this.lblUsdtAmountMin = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账号设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mniBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.mniBank = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCurrencies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miContactMgr = new System.Windows.Forms.ToolStripMenuItem();
            this.销售统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.团队管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMarketViews = new System.Windows.Forms.FlowLayoutPanel();
            this.timer_timeCorrecting = new System.Windows.Forms.Timer(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.timer_account = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabMonitor.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalMoney)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlUsdxMarket.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_api_ping
            // 
            this.timer_api_ping.Enabled = true;
            this.timer_api_ping.Interval = 5000;
            this.timer_api_ping.Tick += new System.EventHandler(this.timer_api_ping_Tick);
            // 
            // timer_state_scan
            // 
            this.timer_state_scan.Enabled = true;
            this.timer_state_scan.Interval = 1000;
            this.timer_state_scan.Tick += new System.EventHandler(this.timer_state_scan_Tick);
            // 
            // timer_monitor
            // 
            this.timer_monitor.Interval = 10;
            this.timer_monitor.Tick += new System.EventHandler(this.timer_monitor_Tick);
            // 
            // timer_render
            // 
            this.timer_render.Enabled = true;
            this.timer_render.Interval = 17;
            this.timer_render.Tick += new System.EventHandler(this.timer_render_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.lblContracts);
            this.panel2.Controls.Add(this.nudTotalMoney);
            this.panel2.Controls.Add(this.lblTotalMoney);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblLoginName);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1579, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(808, 1479);
            this.panel2.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabMonitor);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 782);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(808, 697);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlMyOrders);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(800, 662);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "挂单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlMyOrders
            // 
            this.pnlMyOrders.AutoScroll = true;
            this.pnlMyOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMyOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMyOrders.Location = new System.Drawing.Point(4, 3);
            this.pnlMyOrders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMyOrders.Name = "pnlMyOrders";
            this.pnlMyOrders.Size = new System.Drawing.Size(792, 656);
            this.pnlMyOrders.TabIndex = 16;
            this.pnlMyOrders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pnlBehavior);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(800, 662);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "交易程序";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBehavior.Location = new System.Drawing.Point(4, 3);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(792, 656);
            this.pnlBehavior.TabIndex = 18;
            this.pnlBehavior.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabMonitor
            // 
            this.tabMonitor.Controls.Add(this.pnlMonitor);
            this.tabMonitor.Location = new System.Drawing.Point(4, 31);
            this.tabMonitor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabMonitor.Name = "tabMonitor";
            this.tabMonitor.Size = new System.Drawing.Size(800, 662);
            this.tabMonitor.TabIndex = 2;
            this.tabMonitor.Text = "数据";
            this.tabMonitor.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.AutoScroll = true;
            this.pnlMonitor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(0, 0);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(800, 662);
            this.pnlMonitor.TabIndex = 17;
            this.pnlMonitor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtConsole);
            this.tabLog.Location = new System.Drawing.Point(4, 31);
            this.tabLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(800, 662);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "日志";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(800, 662);
            this.txtConsole.TabIndex = 6;
            this.txtConsole.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flyReceiptAccount);
            this.groupBox3.Location = new System.Drawing.Point(7, 517);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(782, 251);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结算通道";
            // 
            // flyReceiptAccount
            // 
            this.flyReceiptAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyReceiptAccount.Location = new System.Drawing.Point(6, 30);
            this.flyReceiptAccount.Margin = new System.Windows.Forms.Padding(6);
            this.flyReceiptAccount.Name = "flyReceiptAccount";
            this.flyReceiptAccount.Size = new System.Drawing.Size(770, 215);
            this.flyReceiptAccount.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopSell);
            this.groupBox2.Controls.Add(this.btnStopBuy);
            this.groupBox2.Location = new System.Drawing.Point(11, 379);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(782, 126);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "快捷操作";
            // 
            // btnStopSell
            // 
            this.btnStopSell.Location = new System.Drawing.Point(186, 41);
            this.btnStopSell.Margin = new System.Windows.Forms.Padding(6);
            this.btnStopSell.Name = "btnStopSell";
            this.btnStopSell.Size = new System.Drawing.Size(164, 59);
            this.btnStopSell.TabIndex = 0;
            this.btnStopSell.Text = "停止卖单";
            this.btnStopSell.UseVisualStyleBackColor = true;
            this.btnStopSell.Click += new System.EventHandler(this.btnStopSell_Click);
            // 
            // btnStopBuy
            // 
            this.btnStopBuy.Location = new System.Drawing.Point(11, 41);
            this.btnStopBuy.Margin = new System.Windows.Forms.Padding(6);
            this.btnStopBuy.Name = "btnStopBuy";
            this.btnStopBuy.Size = new System.Drawing.Size(164, 59);
            this.btnStopBuy.TabIndex = 0;
            this.btnStopBuy.Text = "停止买单";
            this.btnStopBuy.UseVisualStyleBackColor = true;
            this.btnStopBuy.Click += new System.EventHandler(this.btnStopBuy_Click);
            // 
            // lblContracts
            // 
            this.lblContracts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblContracts.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContracts.ForeColor = System.Drawing.Color.White;
            this.lblContracts.Location = new System.Drawing.Point(654, 11);
            this.lblContracts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContracts.Name = "lblContracts";
            this.lblContracts.Size = new System.Drawing.Size(143, 129);
            this.lblContracts.TabIndex = 32;
            this.lblContracts.Text = "00";
            this.lblContracts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContracts.DoubleClick += new System.EventHandler(this.lblContracts_DoubleClick);
            // 
            // nudTotalMoney
            // 
            this.nudTotalMoney.DecimalPlaces = 2;
            this.nudTotalMoney.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudTotalMoney.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTotalMoney.Location = new System.Drawing.Point(177, 70);
            this.nudTotalMoney.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudTotalMoney.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTotalMoney.Name = "nudTotalMoney";
            this.nudTotalMoney.Size = new System.Drawing.Size(358, 50);
            this.nudTotalMoney.TabIndex = 31;
            this.nudTotalMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTotalMoney.ValueChanged += new System.EventHandler(this.nudTotalMoney_ValueChanged);
            // 
            // lblTotalMoney
            // 
            this.lblTotalMoney.AutoSize = true;
            this.lblTotalMoney.Location = new System.Drawing.Point(534, 29);
            this.lblTotalMoney.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalMoney.Name = "lblTotalMoney";
            this.lblTotalMoney.Size = new System.Drawing.Size(115, 21);
            this.lblTotalMoney.TabIndex = 28;
            this.lblTotalMoney.Text = "当前总估值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 38);
            this.label3.TabIndex = 27;
            this.label3.Text = "现金储备";
            this.label3.Click += new System.EventHandler(this.label2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 27;
            this.label2.Text = "当前总估值";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 35);
            this.label1.TabIndex = 26;
            this.label1.Text = "账号";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginName.Location = new System.Drawing.Point(95, 20);
            this.lblLoginName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(69, 35);
            this.lblLoginName.TabIndex = 22;
            this.lblLoginName.Text = "---";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.pnlUsdxMarket);
            this.groupBox1.Location = new System.Drawing.Point(7, 153);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(786, 218);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USDT";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lblCTCUsdt);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblOTCUsdt);
            this.panel1.Controls.Add(this.lblUsdtCny);
            this.panel1.Location = new System.Drawing.Point(15, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 166);
            this.panel1.TabIndex = 52;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 61);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 21);
            this.label18.TabIndex = 2;
            this.label18.Text = "资金账户";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 21);
            this.label14.TabIndex = 2;
            this.label14.Text = "币币账户";
            // 
            // lblCTCUsdt
            // 
            this.lblCTCUsdt.AutoSize = true;
            this.lblCTCUsdt.Location = new System.Drawing.Point(112, 21);
            this.lblCTCUsdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCUsdt.Name = "lblCTCUsdt";
            this.lblCTCUsdt.Size = new System.Drawing.Size(21, 21);
            this.lblCTCUsdt.TabIndex = 2;
            this.lblCTCUsdt.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 134);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 21);
            this.label19.TabIndex = 3;
            this.label19.Text = "总估值";
            // 
            // lblOTCUsdt
            // 
            this.lblOTCUsdt.AutoSize = true;
            this.lblOTCUsdt.Location = new System.Drawing.Point(112, 61);
            this.lblOTCUsdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOTCUsdt.Name = "lblOTCUsdt";
            this.lblOTCUsdt.Size = new System.Drawing.Size(21, 21);
            this.lblOTCUsdt.TabIndex = 2;
            this.lblOTCUsdt.Text = "0";
            // 
            // lblUsdtCny
            // 
            this.lblUsdtCny.AutoSize = true;
            this.lblUsdtCny.Location = new System.Drawing.Point(112, 134);
            this.lblUsdtCny.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtCny.Name = "lblUsdtCny";
            this.lblUsdtCny.Size = new System.Drawing.Size(21, 21);
            this.lblUsdtCny.TabIndex = 2;
            this.lblUsdtCny.Text = "0";
            // 
            // pnlUsdxMarket
            // 
            this.pnlUsdxMarket.Controls.Add(this.lblUsdtPrice);
            this.pnlUsdxMarket.Controls.Add(this.label8);
            this.pnlUsdxMarket.Controls.Add(this.btnUsdxDetails);
            this.pnlUsdxMarket.Controls.Add(this.lblUsdtAmount);
            this.pnlUsdxMarket.Controls.Add(this.label10);
            this.pnlUsdxMarket.Controls.Add(this.lblUsdtMinPrice);
            this.pnlUsdxMarket.Controls.Add(this.lblUsdtAmountMin);
            this.pnlUsdxMarket.Controls.Add(this.label9);
            this.pnlUsdxMarket.Controls.Add(this.label11);
            this.pnlUsdxMarket.Location = new System.Drawing.Point(356, 29);
            this.pnlUsdxMarket.Margin = new System.Windows.Forms.Padding(4);
            this.pnlUsdxMarket.Name = "pnlUsdxMarket";
            this.pnlUsdxMarket.Size = new System.Drawing.Size(422, 167);
            this.pnlUsdxMarket.TabIndex = 51;
            // 
            // lblUsdtPrice
            // 
            this.lblUsdtPrice.AutoSize = true;
            this.lblUsdtPrice.Location = new System.Drawing.Point(111, 13);
            this.lblUsdtPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtPrice.Name = "lblUsdtPrice";
            this.lblUsdtPrice.Size = new System.Drawing.Size(54, 21);
            this.lblUsdtPrice.TabIndex = 1;
            this.lblUsdtPrice.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 13);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "买入均价";
            // 
            // btnUsdxDetails
            // 
            this.btnUsdxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUsdxDetails.Location = new System.Drawing.Point(258, 13);
            this.btnUsdxDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUsdxDetails.Name = "btnUsdxDetails";
            this.btnUsdxDetails.Size = new System.Drawing.Size(142, 49);
            this.btnUsdxDetails.TabIndex = 50;
            this.btnUsdxDetails.Text = "挂单详情";
            this.btnUsdxDetails.UseVisualStyleBackColor = true;
            this.btnUsdxDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // lblUsdtAmount
            // 
            this.lblUsdtAmount.AutoSize = true;
            this.lblUsdtAmount.Location = new System.Drawing.Point(111, 55);
            this.lblUsdtAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtAmount.Name = "lblUsdtAmount";
            this.lblUsdtAmount.Size = new System.Drawing.Size(54, 21);
            this.lblUsdtAmount.TabIndex = 1;
            this.lblUsdtAmount.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 55);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 21);
            this.label10.TabIndex = 1;
            this.label10.Text = "挂单总量";
            // 
            // lblUsdtMinPrice
            // 
            this.lblUsdtMinPrice.AutoSize = true;
            this.lblUsdtMinPrice.Location = new System.Drawing.Point(111, 95);
            this.lblUsdtMinPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtMinPrice.Name = "lblUsdtMinPrice";
            this.lblUsdtMinPrice.Size = new System.Drawing.Size(54, 21);
            this.lblUsdtMinPrice.TabIndex = 1;
            this.lblUsdtMinPrice.Text = "0.00";
            // 
            // lblUsdtAmountMin
            // 
            this.lblUsdtAmountMin.AutoSize = true;
            this.lblUsdtAmountMin.Location = new System.Drawing.Point(111, 132);
            this.lblUsdtAmountMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtAmountMin.Name = "lblUsdtAmountMin";
            this.lblUsdtAmountMin.Size = new System.Drawing.Size(54, 21);
            this.lblUsdtAmountMin.TabIndex = 1;
            this.lblUsdtAmountMin.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 95);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 21);
            this.label9.TabIndex = 1;
            this.label9.Text = "最低卖价";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 132);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 21);
            this.label11.TabIndex = 1;
            this.label11.Text = "最低价量";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.mnCurrencies,
            this.menuWindow,
            this.menuTeam});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(2387, 34);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.账号设置ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.mniBrowser,
            this.mniBank});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(72, 32);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 账号设置ToolStripMenuItem
            // 
            this.账号设置ToolStripMenuItem.Name = "账号设置ToolStripMenuItem";
            this.账号设置ToolStripMenuItem.Size = new System.Drawing.Size(234, 40);
            this.账号设置ToolStripMenuItem.Text = "账号设置";
            this.账号设置ToolStripMenuItem.Click += new System.EventHandler(this.账号设置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(234, 40);
            this.toolStripMenuItem2.Text = "登录密码";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // mniBrowser
            // 
            this.mniBrowser.Name = "mniBrowser";
            this.mniBrowser.Size = new System.Drawing.Size(234, 40);
            this.mniBrowser.Text = "浏览器设置";
            this.mniBrowser.Click += new System.EventHandler(this.浏览器设置ToolStripMenuItem_Click);
            // 
            // mniBank
            // 
            this.mniBank.Name = "mniBank";
            this.mniBank.Size = new System.Drawing.Size(234, 40);
            this.mniBank.Text = "收付款设置";
            this.mniBank.Click += new System.EventHandler(this.收付款设置ToolStripMenuItem_Click);
            // 
            // mnCurrencies
            // 
            this.mnCurrencies.Name = "mnCurrencies";
            this.mnCurrencies.Size = new System.Drawing.Size(72, 32);
            this.mnCurrencies.Text = "币种";
            this.mnCurrencies.Click += new System.EventHandler(this.mnCurrencies_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miContactMgr,
            this.销售统计ToolStripMenuItem});
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Size = new System.Drawing.Size(72, 32);
            this.menuWindow.Text = "窗口";
            // 
            // miContactMgr
            // 
            this.miContactMgr.Name = "miContactMgr";
            this.miContactMgr.Size = new System.Drawing.Size(213, 40);
            this.miContactMgr.Text = "订单管理";
            this.miContactMgr.Click += new System.EventHandler(this.订单管理ToolStripMenuItem_Click);
            // 
            // 销售统计ToolStripMenuItem
            // 
            this.销售统计ToolStripMenuItem.Name = "销售统计ToolStripMenuItem";
            this.销售统计ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.销售统计ToolStripMenuItem.Text = "销售数据";
            this.销售统计ToolStripMenuItem.Click += new System.EventHandler(this.销售统计ToolStripMenuItem_Click);
            // 
            // menuTeam
            // 
            this.menuTeam.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.团队管理ToolStripMenuItem1});
            this.menuTeam.Name = "menuTeam";
            this.menuTeam.Size = new System.Drawing.Size(114, 32);
            this.menuTeam.Text = "团队管理";
            // 
            // 团队管理ToolStripMenuItem1
            // 
            this.团队管理ToolStripMenuItem1.Name = "团队管理ToolStripMenuItem1";
            this.团队管理ToolStripMenuItem1.Size = new System.Drawing.Size(213, 40);
            this.团队管理ToolStripMenuItem1.Text = "团队管理";
            this.团队管理ToolStripMenuItem1.Click += new System.EventHandler(this.团队管理ToolStripMenuItem1_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.pnlMarketViews);
            this.pnlMain.Location = new System.Drawing.Point(0, 39);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1580, 1723);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlMarketViews
            // 
            this.pnlMarketViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMarketViews.AutoScroll = true;
            this.pnlMarketViews.Location = new System.Drawing.Point(0, 0);
            this.pnlMarketViews.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMarketViews.Name = "pnlMarketViews";
            this.pnlMarketViews.Size = new System.Drawing.Size(1580, 1476);
            this.pnlMarketViews.TabIndex = 3;
            this.pnlMarketViews.Visible = false;
            // 
            // timer_timeCorrecting
            // 
            this.timer_timeCorrecting.Enabled = true;
            this.timer_timeCorrecting.Interval = 120000;
            this.timer_timeCorrecting.Tick += new System.EventHandler(this.timer_timeCorrecting_Tick);
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer_account
            // 
            this.timer_account.Interval = 30000;
            this.timer_account.Tick += new System.EventHandler(this.timer_account_Tick);
            // 
            // WinMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2387, 1513);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WinMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数字资管";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabMonitor.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalMoney)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlUsdxMarket.ResumeLayout(false);
            this.pnlUsdxMarket.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer_api_ping;
        private System.Windows.Forms.Timer timer_state_scan;
        private System.Windows.Forms.Timer timer_monitor;
        private System.Windows.Forms.Timer timer_render;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel pnlMyOrders;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.TabPage tabMonitor;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblUsdtCny;
        private System.Windows.Forms.Label lblOTCUsdt;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCTCUsdt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblUsdtAmountMin;
        private System.Windows.Forms.Label lblUsdtMinPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblUsdtAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblUsdtPrice;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账号设置ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripMenuItem menuWindow;
        private System.Windows.Forms.ToolStripMenuItem miContactMgr;
        private System.Windows.Forms.FlowLayoutPanel pnlMarketViews;
        private System.Windows.Forms.ToolStripMenuItem mnCurrencies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalMoney;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudTotalMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer_timeCorrecting;
        private System.Windows.Forms.Label lblContracts;
        private System.Windows.Forms.Button btnUsdxDetails;
        private System.Windows.Forms.ToolStripMenuItem 销售统计ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStopSell;
        private System.Windows.Forms.Button btnStopBuy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mniBrowser;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flyReceiptAccount;
        private System.Windows.Forms.Timer timer_account;
        private System.Windows.Forms.ToolStripMenuItem mniBank;
        private System.Windows.Forms.ToolStripMenuItem menuTeam;
        private System.Windows.Forms.ToolStripMenuItem 团队管理ToolStripMenuItem1;
        private System.Windows.Forms.Panel pnlUsdxMarket;
        private System.Windows.Forms.Panel panel1;
    }
}

