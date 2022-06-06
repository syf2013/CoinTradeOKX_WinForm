namespace CoinTradeOKX.Control
{
    partial class MarketView
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
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCTCAvalible = new System.Windows.Forms.Label();
            this.lblCTCCny = new System.Windows.Forms.Label();
            this.lblCTCHold = new System.Windows.Forms.Label();
            this.lblCellPrice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOTCAvalible = new System.Windows.Forms.Label();
            this.lblOTCCny = new System.Windows.Forms.Label();
            this.lblOTCHold = new System.Windows.Forms.Label();
            this.lblBuyPrice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCTCBid = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCTCAskPrice = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.lblContracts = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnMini = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBuyAmount
            // 
            this.txtBuyAmount.Location = new System.Drawing.Point(48, 432);
            this.txtBuyAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuyAmount.Name = "txtBuyAmount";
            this.txtBuyAmount.Size = new System.Drawing.Size(121, 31);
            this.txtBuyAmount.TabIndex = 42;
            this.txtBuyAmount.Text = "100";
            // 
            // btnCurrencyBuy
            // 
            this.btnCurrencyBuy.Location = new System.Drawing.Point(178, 430);
            this.btnCurrencyBuy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCurrencyBuy.Name = "btnCurrencyBuy";
            this.btnCurrencyBuy.Size = new System.Drawing.Size(138, 33);
            this.btnCurrencyBuy.TabIndex = 41;
            this.btnCurrencyBuy.Tag = "";
            this.btnCurrencyBuy.Text = "补仓(元)";
            this.btnCurrencyBuy.UseVisualStyleBackColor = true;
            this.btnCurrencyBuy.Click += new System.EventHandler(this.btnCurrencyBuy_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.lblCTCAvalible);
            this.groupBox3.Controls.Add(this.lblCTCCny);
            this.groupBox3.Controls.Add(this.lblCTCHold);
            this.groupBox3.Location = new System.Drawing.Point(48, 214);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(546, 82);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "币币账户";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(312, 44);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 21);
            this.label12.TabIndex = 10;
            this.label12.Text = "估值";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(165, 44);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "冻结";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 44);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "可用";
            // 
            // lblCTCAvalible
            // 
            this.lblCTCAvalible.AutoSize = true;
            this.lblCTCAvalible.Location = new System.Drawing.Point(73, 44);
            this.lblCTCAvalible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCAvalible.Name = "lblCTCAvalible";
            this.lblCTCAvalible.Size = new System.Drawing.Size(21, 21);
            this.lblCTCAvalible.TabIndex = 9;
            this.lblCTCAvalible.Text = "0";
            // 
            // lblCTCCny
            // 
            this.lblCTCCny.AutoSize = true;
            this.lblCTCCny.Location = new System.Drawing.Point(368, 44);
            this.lblCTCCny.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCCny.Name = "lblCTCCny";
            this.lblCTCCny.Size = new System.Drawing.Size(21, 21);
            this.lblCTCCny.TabIndex = 9;
            this.lblCTCCny.Text = "0";
            // 
            // lblCTCHold
            // 
            this.lblCTCHold.AutoSize = true;
            this.lblCTCHold.Location = new System.Drawing.Point(214, 44);
            this.lblCTCHold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCHold.Name = "lblCTCHold";
            this.lblCTCHold.Size = new System.Drawing.Size(21, 21);
            this.lblCTCHold.TabIndex = 9;
            this.lblCTCHold.Text = "0";
            // 
            // lblCellPrice
            // 
            this.lblCellPrice.AutoSize = true;
            this.lblCellPrice.Location = new System.Drawing.Point(150, 369);
            this.lblCellPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCellPrice.Name = "lblCellPrice";
            this.lblCellPrice.Size = new System.Drawing.Size(54, 21);
            this.lblCellPrice.TabIndex = 29;
            this.lblCellPrice.Text = "0.00";
            this.lblCellPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 332);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 21);
            this.label3.TabIndex = 30;
            this.label3.Text = "法币卖出";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblOTCAvalible);
            this.groupBox2.Controls.Add(this.lblOTCCny);
            this.groupBox2.Controls.Add(this.lblOTCHold);
            this.groupBox2.Location = new System.Drawing.Point(48, 107);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(546, 82);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "资金账户";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(312, 44);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 21);
            this.label13.TabIndex = 10;
            this.label13.Text = "估值";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "冻结";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "可用";
            // 
            // lblOTCAvalible
            // 
            this.lblOTCAvalible.AutoSize = true;
            this.lblOTCAvalible.Location = new System.Drawing.Point(73, 44);
            this.lblOTCAvalible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOTCAvalible.Name = "lblOTCAvalible";
            this.lblOTCAvalible.Size = new System.Drawing.Size(21, 21);
            this.lblOTCAvalible.TabIndex = 9;
            this.lblOTCAvalible.Text = "0";
            // 
            // lblOTCCny
            // 
            this.lblOTCCny.AutoSize = true;
            this.lblOTCCny.Location = new System.Drawing.Point(368, 44);
            this.lblOTCCny.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOTCCny.Name = "lblOTCCny";
            this.lblOTCCny.Size = new System.Drawing.Size(21, 21);
            this.lblOTCCny.TabIndex = 9;
            this.lblOTCCny.Text = "0";
            // 
            // lblOTCHold
            // 
            this.lblOTCHold.AutoSize = true;
            this.lblOTCHold.Location = new System.Drawing.Point(214, 44);
            this.lblOTCHold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOTCHold.Name = "lblOTCHold";
            this.lblOTCHold.Size = new System.Drawing.Size(21, 21);
            this.lblOTCHold.TabIndex = 9;
            this.lblOTCHold.Text = "0";
            // 
            // lblBuyPrice
            // 
            this.lblBuyPrice.AutoSize = true;
            this.lblBuyPrice.Location = new System.Drawing.Point(150, 332);
            this.lblBuyPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuyPrice.Name = "lblBuyPrice";
            this.lblBuyPrice.Size = new System.Drawing.Size(54, 21);
            this.lblBuyPrice.TabIndex = 31;
            this.lblBuyPrice.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 373);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 21);
            this.label4.TabIndex = 32;
            this.label4.Text = "法币买入";
            // 
            // lblCTCBid
            // 
            this.lblCTCBid.AutoSize = true;
            this.lblCTCBid.Location = new System.Drawing.Point(383, 373);
            this.lblCTCBid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCBid.Name = "lblCTCBid";
            this.lblCTCBid.Size = new System.Drawing.Size(21, 21);
            this.lblCTCBid.TabIndex = 37;
            this.lblCTCBid.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(279, 332);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 21);
            this.label7.TabIndex = 33;
            this.label7.Text = "币币卖出";
            // 
            // lblCTCAskPrice
            // 
            this.lblCTCAskPrice.AutoSize = true;
            this.lblCTCAskPrice.Location = new System.Drawing.Point(383, 332);
            this.lblCTCAskPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCAskPrice.Name = "lblCTCAskPrice";
            this.lblCTCAskPrice.Size = new System.Drawing.Size(21, 21);
            this.lblCTCAskPrice.TabIndex = 38;
            this.lblCTCAskPrice.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(279, 373);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 21);
            this.label17.TabIndex = 34;
            this.label17.Text = "币币买入";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrency.Location = new System.Drawing.Point(53, 40);
            this.lblCurrency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(69, 35);
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
            this.label20.Location = new System.Drawing.Point(163, 52);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 21);
            this.label20.TabIndex = 46;
            this.label20.Text = "总估值";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(251, 52);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(32, 21);
            this.lblTotal.TabIndex = 45;
            this.lblTotal.Text = "--";
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBehavior.Location = new System.Drawing.Point(4, 505);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(607, 264);
            this.pnlBehavior.TabIndex = 47;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 808);
            this.tabControl1.TabIndex = 48;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblMonitor);
            this.tabPage1.Controls.Add(this.lblContracts);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.pnlBehavior);
            this.tabPage1.Controls.Add(this.lblTotal);
            this.tabPage1.Controls.Add(this.lblCurrency);
            this.tabPage1.Controls.Add(this.txtBuyAmount);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.btnCurrencyBuy);
            this.tabPage1.Controls.Add(this.lblCTCAskPrice);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lblCellPrice);
            this.tabPage1.Controls.Add(this.lblCTCBid);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.lblBuyPrice);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(615, 773);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "交易";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(24, 47);
            this.lblMonitor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(31, 21);
            this.lblMonitor.TabIndex = 48;
            this.lblMonitor.Text = "❤";
            // 
            // lblContracts
            // 
            this.lblContracts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblContracts.Font = new System.Drawing.Font("宋体", 20.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContracts.ForeColor = System.Drawing.Color.White;
            this.lblContracts.Location = new System.Drawing.Point(519, 16);
            this.lblContracts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContracts.Name = "lblContracts";
            this.lblContracts.Size = new System.Drawing.Size(75, 65);
            this.lblContracts.TabIndex = 50;
            this.lblContracts.Text = "00";
            this.lblContracts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContracts.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(46, 341);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 21);
            this.label2.TabIndex = 49;
            this.label2.Text = "_________________________________________";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pnlMonitor);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(615, 773);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(4, 4);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(607, 765);
            this.pnlMonitor.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(579, -2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 33);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(480, -2);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 33);
            this.button1.TabIndex = 49;
            this.button1.Text = "L";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnMini
            // 
            this.btnMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMini.Location = new System.Drawing.Point(530, -2);
            this.btnMini.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(42, 33);
            this.btnMini.TabIndex = 50;
            this.btnMini.Text = "-";
            this.btnMini.UseVisualStyleBackColor = true;
            this.btnMini.Visible = false;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // MarketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MarketView";
            this.Size = new System.Drawing.Size(623, 808);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtBuyAmount;
        private System.Windows.Forms.Button btnCurrencyBuy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblCTCAvalible;
        private System.Windows.Forms.Label lblCTCCny;
        private System.Windows.Forms.Label lblCTCHold;
        private System.Windows.Forms.Label lblCellPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOTCAvalible;
        private System.Windows.Forms.Label lblOTCCny;
        private System.Windows.Forms.Label lblOTCHold;
        private System.Windows.Forms.Label lblBuyPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCTCBid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCTCAskPrice;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblContracts;
    }
}
