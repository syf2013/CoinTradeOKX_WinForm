namespace CoinTradeGecko.Control
{
    partial class C2CMarketViewV5
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtBuyAmount = new System.Windows.Forms.TextBox();
            this.btnCurrencyBuy = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCTCAvalible = new System.Windows.Forms.Label();
            this.lblCTCHold = new System.Windows.Forms.Label();
            this.lblCTCBid = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCTCAskPrice = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTrade = new System.Windows.Forms.TabPage();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.tabDepth = new System.Windows.Forms.TabPage();
            this.tabIndex = new System.Windows.Forms.TabPage();
            this.lblRSI14 = new System.Windows.Forms.Label();
            this.lblMA5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timerIndex = new System.Windows.Forms.Timer(this.components);
            this.depthView1 = new CoinTradeGecko.Control.DepthView();
            this.btnStat = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabTrade.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDepth.SuspendLayout();
            this.tabIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBuyAmount
            // 
            this.txtBuyAmount.Location = new System.Drawing.Point(44, 150);
            this.txtBuyAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuyAmount.Name = "txtBuyAmount";
            this.txtBuyAmount.Size = new System.Drawing.Size(89, 25);
            this.txtBuyAmount.TabIndex = 42;
            this.txtBuyAmount.Text = "1000";
            // 
            // btnCurrencyBuy
            // 
            this.btnCurrencyBuy.Location = new System.Drawing.Point(138, 150);
            this.btnCurrencyBuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCurrencyBuy.Name = "btnCurrencyBuy";
            this.btnCurrencyBuy.Size = new System.Drawing.Size(91, 24);
            this.btnCurrencyBuy.TabIndex = 41;
            this.btnCurrencyBuy.Tag = "";
            this.btnCurrencyBuy.Text = "补仓(元)";
            this.btnCurrencyBuy.UseVisualStyleBackColor = true;
            this.btnCurrencyBuy.Click += new System.EventHandler(this.btnCurrencyBuy_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.lblCTCAvalible);
            this.groupBox3.Controls.Add(this.lblCTCHold);
            this.groupBox3.Location = new System.Drawing.Point(44, 77);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(337, 59);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "币币账户";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(170, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 15);
            this.label15.TabIndex = 0;
            this.label15.Text = "冻结";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "剩余";
            // 
            // lblCTCAvalible
            // 
            this.lblCTCAvalible.AutoSize = true;
            this.lblCTCAvalible.Location = new System.Drawing.Point(60, 29);
            this.lblCTCAvalible.Name = "lblCTCAvalible";
            this.lblCTCAvalible.Size = new System.Drawing.Size(15, 15);
            this.lblCTCAvalible.TabIndex = 9;
            this.lblCTCAvalible.Text = "0";
            // 
            // lblCTCHold
            // 
            this.lblCTCHold.AutoSize = true;
            this.lblCTCHold.Location = new System.Drawing.Point(208, 29);
            this.lblCTCHold.Name = "lblCTCHold";
            this.lblCTCHold.Size = new System.Drawing.Size(15, 15);
            this.lblCTCHold.TabIndex = 9;
            this.lblCTCHold.Text = "0";
            // 
            // lblCTCBid
            // 
            this.lblCTCBid.AutoSize = true;
            this.lblCTCBid.Location = new System.Drawing.Point(355, 53);
            this.lblCTCBid.Name = "lblCTCBid";
            this.lblCTCBid.Size = new System.Drawing.Size(15, 15);
            this.lblCTCBid.TabIndex = 37;
            this.lblCTCBid.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "币币卖出";
            // 
            // lblCTCAskPrice
            // 
            this.lblCTCAskPrice.AutoSize = true;
            this.lblCTCAskPrice.Location = new System.Drawing.Point(356, 25);
            this.lblCTCAskPrice.Name = "lblCTCAskPrice";
            this.lblCTCAskPrice.Size = new System.Drawing.Size(15, 15);
            this.lblCTCAskPrice.TabIndex = 38;
            this.lblCTCAskPrice.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(289, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 15);
            this.label17.TabIndex = 34;
            this.label17.Text = "币币买入";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrency.Location = new System.Drawing.Point(39, 29);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(51, 25);
            this.lblCurrency.TabIndex = 44;
            this.lblCurrency.Text = "BTC";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(119, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 15);
            this.label20.TabIndex = 46;
            this.label20.Text = "估值";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(176, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(23, 15);
            this.lblTotal.TabIndex = 45;
            this.lblTotal.Text = "--";
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBehavior.Location = new System.Drawing.Point(3, 194);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(442, 80);
            this.pnlBehavior.TabIndex = 47;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabTrade);
            this.tabMain.Controls.Add(this.tabData);
            this.tabMain.Controls.Add(this.tabDepth);
            this.tabMain.Controls.Add(this.tabIndex);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(456, 305);
            this.tabMain.TabIndex = 48;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabTrade
            // 
            this.tabTrade.Controls.Add(this.btnStat);
            this.tabTrade.Controls.Add(this.lblMonitor);
            this.tabTrade.Controls.Add(this.label20);
            this.tabTrade.Controls.Add(this.pnlBehavior);
            this.tabTrade.Controls.Add(this.lblTotal);
            this.tabTrade.Controls.Add(this.lblCurrency);
            this.tabTrade.Controls.Add(this.txtBuyAmount);
            this.tabTrade.Controls.Add(this.label17);
            this.tabTrade.Controls.Add(this.btnCurrencyBuy);
            this.tabTrade.Controls.Add(this.lblCTCAskPrice);
            this.tabTrade.Controls.Add(this.groupBox3);
            this.tabTrade.Controls.Add(this.label7);
            this.tabTrade.Controls.Add(this.lblCTCBid);
            this.tabTrade.Controls.Add(this.label2);
            this.tabTrade.Location = new System.Drawing.Point(4, 25);
            this.tabTrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabTrade.Name = "tabTrade";
            this.tabTrade.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabTrade.Size = new System.Drawing.Size(448, 276);
            this.tabTrade.TabIndex = 0;
            this.tabTrade.Text = "交易";
            this.tabTrade.UseVisualStyleBackColor = true;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(17, 34);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(18, 15);
            this.lblMonitor.TabIndex = 48;
            this.lblMonitor.Text = "❤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(288, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "_____________________";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.pnlMonitor);
            this.tabData.Location = new System.Drawing.Point(4, 25);
            this.tabData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabData.Size = new System.Drawing.Size(448, 276);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "数据";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(3, 2);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(442, 272);
            this.pnlMonitor.TabIndex = 0;
            // 
            // tabDepth
            // 
            this.tabDepth.Controls.Add(this.depthView1);
            this.tabDepth.Location = new System.Drawing.Point(4, 25);
            this.tabDepth.Name = "tabDepth";
            this.tabDepth.Size = new System.Drawing.Size(448, 276);
            this.tabDepth.TabIndex = 2;
            this.tabDepth.Text = "深度";
            this.tabDepth.UseVisualStyleBackColor = true;
            // 
            // tabIndex
            // 
            this.tabIndex.Controls.Add(this.lblRSI14);
            this.tabIndex.Controls.Add(this.lblMA5);
            this.tabIndex.Location = new System.Drawing.Point(4, 25);
            this.tabIndex.Name = "tabIndex";
            this.tabIndex.Size = new System.Drawing.Size(448, 276);
            this.tabIndex.TabIndex = 3;
            this.tabIndex.Text = "指标";
            this.tabIndex.UseVisualStyleBackColor = true;
            // 
            // lblRSI14
            // 
            this.lblRSI14.AutoSize = true;
            this.lblRSI14.Location = new System.Drawing.Point(193, 25);
            this.lblRSI14.Name = "lblRSI14";
            this.lblRSI14.Size = new System.Drawing.Size(55, 15);
            this.lblRSI14.TabIndex = 1;
            this.lblRSI14.Text = "label1";
            // 
            // lblMA5
            // 
            this.lblMA5.AutoSize = true;
            this.lblMA5.Location = new System.Drawing.Point(38, 25);
            this.lblMA5.Name = "lblMA5";
            this.lblMA5.Size = new System.Drawing.Size(55, 15);
            this.lblMA5.TabIndex = 0;
            this.lblMA5.Text = "lblMA5";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(424, -1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 24);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timerIndex
            // 
            this.timerIndex.Interval = 1000;
            this.timerIndex.Tick += new System.EventHandler(this.timerIndex_Tick);
            // 
            // depthView1
            // 
            this.depthView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depthView1.Location = new System.Drawing.Point(0, 0);
            this.depthView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.depthView1.Name = "depthView1";
            this.depthView1.Size = new System.Drawing.Size(448, 276);
            this.depthView1.TabIndex = 0;
            // 
            // btnStat
            // 
            this.btnStat.Location = new System.Drawing.Point(389, 150);
            this.btnStat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(53, 24);
            this.btnStat.TabIndex = 50;
            this.btnStat.Tag = "";
            this.btnStat.Text = "统计";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // C2CMarketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "C2CMarketView";
            this.Size = new System.Drawing.Size(456, 305);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabTrade.ResumeLayout(false);
            this.tabTrade.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabDepth.ResumeLayout(false);
            this.tabIndex.ResumeLayout(false);
            this.tabIndex.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtBuyAmount;
        private System.Windows.Forms.Button btnCurrencyBuy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblCTCAvalible;
        private System.Windows.Forms.Label lblCTCHold;
        private System.Windows.Forms.Label lblCTCBid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCTCAskPrice;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTrade;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabDepth;
        private System.Windows.Forms.TabPage tabIndex;
        private DepthView depthView1;
        private System.Windows.Forms.Label lblRSI14;
        private System.Windows.Forms.Label lblMA5;
        private System.Windows.Forms.Timer timerIndex;
        private System.Windows.Forms.Button btnStat;
    }
}
