namespace CoinTradeOKX
{
    partial class WinConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoCtc = new System.Windows.Forms.RadioButton();
            this.rdoOtc = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.txtCurrencies = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRealname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReleasePwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDeposit = new System.Windows.Forms.TextBox();
            this.txtAnchorOrder = new System.Windows.Forms.TextBox();
            this.txtAnchroSize = new System.Windows.Forms.TextBox();
            this.cbAnchor = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoSimulated = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtWebSocket = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtApiSecretKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtApiPassphrase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtOTCOrderCountDown = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDaySellAmountLimit = new System.Windows.Forms.NumericUpDown();
            this.chkAddCash = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMaxUnpaidBuyOrder = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbBuyUser = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbSellUser = new System.Windows.Forms.ComboBox();
            this.txtApiTimeout = new System.Windows.Forms.TextBox();
            this.txtReorderCountDown = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlOtcUsdx = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDaySellAmountLimit)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.pnlOtcUsdx.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoCtc);
            this.groupBox1.Controls.Add(this.rdoOtc);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtCurrencies);
            this.groupBox1.Controls.Add(this.txtLoginName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRealname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(33, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(541, 327);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账户设置";
            // 
            // rdoCtc
            // 
            this.rdoCtc.AutoSize = true;
            this.rdoCtc.Location = new System.Drawing.Point(343, 196);
            this.rdoCtc.Margin = new System.Windows.Forms.Padding(4);
            this.rdoCtc.Name = "rdoCtc";
            this.rdoCtc.Size = new System.Drawing.Size(98, 25);
            this.rdoCtc.TabIndex = 2;
            this.rdoCtc.Text = "币币版";
            this.rdoCtc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoCtc.UseVisualStyleBackColor = true;
            this.rdoCtc.CheckedChanged += new System.EventHandler(this.rdoCtc_CheckedChanged);
            this.rdoCtc.Click += new System.EventHandler(this.rdoCtc_Click);
            // 
            // rdoOtc
            // 
            this.rdoOtc.AutoSize = true;
            this.rdoOtc.Checked = true;
            this.rdoOtc.Location = new System.Drawing.Point(138, 196);
            this.rdoOtc.Margin = new System.Windows.Forms.Padding(4);
            this.rdoOtc.Name = "rdoOtc";
            this.rdoOtc.Size = new System.Drawing.Size(98, 25);
            this.rdoOtc.TabIndex = 2;
            this.rdoOtc.TabStop = true;
            this.rdoOtc.Text = "法币版";
            this.rdoOtc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoOtc.UseVisualStyleBackColor = true;
            this.rdoOtc.CheckedChanged += new System.EventHandler(this.rdoOtc_CheckedChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(50, 262);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(52, 21);
            this.label24.TabIndex = 6;
            this.label24.Text = "币种";
            // 
            // txtCurrencies
            // 
            this.txtCurrencies.Location = new System.Drawing.Point(138, 256);
            this.txtCurrencies.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurrencies.Name = "txtCurrencies";
            this.txtCurrencies.Size = new System.Drawing.Size(387, 31);
            this.txtCurrencies.TabIndex = 5;
            this.txtCurrencies.Text = "BTC;LTC;EOS;BCH;ETH;ETC;OKB";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(127, 124);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(398, 31);
            this.txtLoginName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "登录名";
            // 
            // txtRealname
            // 
            this.txtRealname.Location = new System.Drawing.Point(127, 63);
            this.txtRealname.Margin = new System.Windows.Forms.Padding(4);
            this.txtRealname.Name = "txtRealname";
            this.txtRealname.Size = new System.Drawing.Size(398, 31);
            this.txtRealname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "实名";
            // 
            // txtReleasePwd
            // 
            this.txtReleasePwd.Location = new System.Drawing.Point(196, 194);
            this.txtReleasePwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtReleasePwd.Name = "txtReleasePwd";
            this.txtReleasePwd.Size = new System.Drawing.Size(398, 31);
            this.txtReleasePwd.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 206);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "放币密码";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(293, 23);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 21);
            this.label15.TabIndex = 6;
            this.label15.Text = "押金";
            // 
            // txtDeposit
            // 
            this.txtDeposit.Location = new System.Drawing.Point(369, 16);
            this.txtDeposit.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.Size = new System.Drawing.Size(209, 31);
            this.txtDeposit.TabIndex = 5;
            // 
            // txtAnchorOrder
            // 
            this.txtAnchorOrder.Location = new System.Drawing.Point(108, 74);
            this.txtAnchorOrder.Margin = new System.Windows.Forms.Padding(4);
            this.txtAnchorOrder.Name = "txtAnchorOrder";
            this.txtAnchorOrder.Size = new System.Drawing.Size(149, 31);
            this.txtAnchorOrder.TabIndex = 4;
            this.txtAnchorOrder.Text = "3";
            // 
            // txtAnchroSize
            // 
            this.txtAnchroSize.Location = new System.Drawing.Point(108, 16);
            this.txtAnchroSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtAnchroSize.Name = "txtAnchroSize";
            this.txtAnchroSize.Size = new System.Drawing.Size(149, 31);
            this.txtAnchroSize.TabIndex = 4;
            this.txtAnchroSize.Text = "20000";
            // 
            // cbAnchor
            // 
            this.cbAnchor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnchor.FormattingEnabled = true;
            this.cbAnchor.Items.AddRange(new object[] {
            "USDT",
            "USDK"});
            this.cbAnchor.Location = new System.Drawing.Point(154, 44);
            this.cbAnchor.Margin = new System.Windows.Forms.Padding(4);
            this.cbAnchor.Name = "cbAnchor";
            this.cbAnchor.Size = new System.Drawing.Size(398, 29);
            this.cbAnchor.TabIndex = 2;
            this.cbAnchor.SelectedIndexChanged += new System.EventHandler(this.cbAnchor_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 80);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "锚定单数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "锚定数量";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "锚定币";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoSimulated);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.txtWebSocket);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtApiSecretKey);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtApiPassphrase);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtApiKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(600, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(655, 327);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "API设置";
            // 
            // rdoSimulated
            // 
            this.rdoSimulated.AutoSize = true;
            this.rdoSimulated.Location = new System.Drawing.Point(398, 256);
            this.rdoSimulated.Margin = new System.Windows.Forms.Padding(4);
            this.rdoSimulated.Name = "rdoSimulated";
            this.rdoSimulated.Size = new System.Drawing.Size(98, 25);
            this.rdoSimulated.TabIndex = 3;
            this.rdoSimulated.Text = "模拟盘";
            this.rdoSimulated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoSimulated.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(193, 256);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(77, 25);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "实盘";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // txtWebSocket
            // 
            this.txtWebSocket.Location = new System.Drawing.Point(193, 191);
            this.txtWebSocket.Margin = new System.Windows.Forms.Padding(4);
            this.txtWebSocket.Name = "txtWebSocket";
            this.txtWebSocket.Size = new System.Drawing.Size(398, 31);
            this.txtWebSocket.TabIndex = 1;
            this.txtWebSocket.Text = "wss://ws.okx.com:8443/ws/v5/public";
            this.txtWebSocket.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 196);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "WebSocket";
            this.label10.Visible = false;
            // 
            // txtApiSecretKey
            // 
            this.txtApiSecretKey.Location = new System.Drawing.Point(193, 135);
            this.txtApiSecretKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiSecretKey.Name = "txtApiSecretKey";
            this.txtApiSecretKey.Size = new System.Drawing.Size(398, 31);
            this.txtApiSecretKey.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 140);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "SecretKey";
            // 
            // txtApiPassphrase
            // 
            this.txtApiPassphrase.Location = new System.Drawing.Point(193, 86);
            this.txtApiPassphrase.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiPassphrase.Name = "txtApiPassphrase";
            this.txtApiPassphrase.Size = new System.Drawing.Size(398, 31);
            this.txtApiPassphrase.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "API密码";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(193, 32);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(398, 31);
            this.txtApiKey.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Key";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(772, 719);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(427, 108);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOTCOrderCountDown
            // 
            this.txtOTCOrderCountDown.Location = new System.Drawing.Point(196, 35);
            this.txtOTCOrderCountDown.Margin = new System.Windows.Forms.Padding(4);
            this.txtOTCOrderCountDown.Name = "txtOTCOrderCountDown";
            this.txtOTCOrderCountDown.Size = new System.Drawing.Size(160, 31);
            this.txtOTCOrderCountDown.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.nudDaySellAmountLimit);
            this.groupBox3.Controls.Add(this.chkAddCash);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txtReleasePwd);
            this.groupBox3.Controls.Add(this.txtMaxUnpaidBuyOrder);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cmbBuyUser);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cmbSellUser);
            this.groupBox3.Controls.Add(this.txtApiTimeout);
            this.groupBox3.Controls.Add(this.txtReorderCountDown);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtOTCOrderCountDown);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Location = new System.Drawing.Point(33, 385);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1901, 290);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "法币设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(974, 206);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "万元";
            // 
            // nudDaySellAmountLimit
            // 
            this.nudDaySellAmountLimit.Location = new System.Drawing.Point(810, 200);
            this.nudDaySellAmountLimit.Margin = new System.Windows.Forms.Padding(4);
            this.nudDaySellAmountLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDaySellAmountLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDaySellAmountLimit.Name = "nudDaySellAmountLimit";
            this.nudDaySellAmountLimit.Size = new System.Drawing.Size(152, 31);
            this.nudDaySellAmountLimit.TabIndex = 11;
            this.nudDaySellAmountLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkAddCash
            // 
            this.chkAddCash.AutoSize = true;
            this.chkAddCash.Location = new System.Drawing.Point(785, 89);
            this.chkAddCash.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.chkAddCash.Name = "chkAddCash";
            this.chkAddCash.Size = new System.Drawing.Size(309, 25);
            this.chkAddCash.TabIndex = 10;
            this.chkAddCash.Text = "卖单付款后自动增加现金储备";
            this.chkAddCash.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(920, 32);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(136, 21);
            this.label19.TabIndex = 9;
            this.label19.Text = "买单交易对象";
            // 
            // txtMaxUnpaidBuyOrder
            // 
            this.txtMaxUnpaidBuyOrder.Location = new System.Drawing.Point(810, 145);
            this.txtMaxUnpaidBuyOrder.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxUnpaidBuyOrder.Name = "txtMaxUnpaidBuyOrder";
            this.txtMaxUnpaidBuyOrder.Size = new System.Drawing.Size(149, 31);
            this.txtMaxUnpaidBuyOrder.TabIndex = 4;
            this.txtMaxUnpaidBuyOrder.Text = "3";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(363, 142);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 21);
            this.label17.TabIndex = 4;
            this.label17.Text = "秒";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(363, 93);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 21);
            this.label14.TabIndex = 4;
            this.label14.Text = "毫秒";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(596, 32);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(136, 21);
            this.label18.TabIndex = 9;
            this.label18.Text = "卖单交易对象";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(84, 147);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 21);
            this.label16.TabIndex = 4;
            this.label16.Text = "API超时";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(33, 94);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 21);
            this.label13.TabIndex = 4;
            this.label13.Text = "重新挂单冷却";
            // 
            // cmbBuyUser
            // 
            this.cmbBuyUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuyUser.FormattingEnabled = true;
            this.cmbBuyUser.Items.AddRange(new object[] {
            "所有",
            "商家",
            "非商家"});
            this.cmbBuyUser.Location = new System.Drawing.Point(1087, 24);
            this.cmbBuyUser.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBuyUser.Name = "cmbBuyUser";
            this.cmbBuyUser.Size = new System.Drawing.Size(121, 29);
            this.cmbBuyUser.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(363, 40);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 21);
            this.label12.TabIndex = 4;
            this.label12.Text = "毫秒";
            // 
            // cmbSellUser
            // 
            this.cmbSellUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSellUser.FormattingEnabled = true;
            this.cmbSellUser.Items.AddRange(new object[] {
            "所有",
            "商家",
            "非商家"});
            this.cmbSellUser.Location = new System.Drawing.Point(766, 24);
            this.cmbSellUser.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSellUser.Name = "cmbSellUser";
            this.cmbSellUser.Size = new System.Drawing.Size(121, 29);
            this.cmbSellUser.TabIndex = 7;
            // 
            // txtApiTimeout
            // 
            this.txtApiTimeout.Location = new System.Drawing.Point(196, 136);
            this.txtApiTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiTimeout.Name = "txtApiTimeout";
            this.txtApiTimeout.Size = new System.Drawing.Size(160, 31);
            this.txtApiTimeout.TabIndex = 3;
            // 
            // txtReorderCountDown
            // 
            this.txtReorderCountDown.Location = new System.Drawing.Point(196, 86);
            this.txtReorderCountDown.Margin = new System.Windows.Forms.Padding(4);
            this.txtReorderCountDown.Name = "txtReorderCountDown";
            this.txtReorderCountDown.Size = new System.Drawing.Size(160, 31);
            this.txtReorderCountDown.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(660, 206);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(136, 21);
            this.label22.TabIndex = 0;
            this.label22.Text = "日销售额限制";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(75, 44);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 21);
            this.label11.TabIndex = 4;
            this.label11.Text = "挂单冷却";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(642, 152);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(157, 21);
            this.label21.TabIndex = 0;
            this.label21.Text = "最大未处理买单";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pnlOtcUsdx);
            this.groupBox4.Controls.Add(this.cbAnchor);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(1280, 16);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox4.Size = new System.Drawing.Size(655, 290);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "锚定币设置";
            // 
            // pnlOtcUsdx
            // 
            this.pnlOtcUsdx.Controls.Add(this.txtDeposit);
            this.pnlOtcUsdx.Controls.Add(this.txtAnchorOrder);
            this.pnlOtcUsdx.Controls.Add(this.label20);
            this.pnlOtcUsdx.Controls.Add(this.label3);
            this.pnlOtcUsdx.Controls.Add(this.label15);
            this.pnlOtcUsdx.Controls.Add(this.txtAnchroSize);
            this.pnlOtcUsdx.Location = new System.Drawing.Point(42, 105);
            this.pnlOtcUsdx.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlOtcUsdx.Name = "pnlOtcUsdx";
            this.pnlOtcUsdx.Size = new System.Drawing.Size(592, 158);
            this.pnlOtcUsdx.TabIndex = 6;
            // 
            // WinConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1951, 868);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinConfig_FormClosing);
            this.Load += new System.EventHandler(this.WinConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDaySellAmountLimit)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.pnlOtcUsdx.ResumeLayout(false);
            this.pnlOtcUsdx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRealname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApiSecretKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtApiPassphrase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbAnchor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtReleasePwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWebSocket;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOTCOrderCountDown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReorderCountDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAnchroSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDeposit;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtApiTimeout;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbBuyUser;
        private System.Windows.Forms.ComboBox cmbSellUser;
        private System.Windows.Forms.TextBox txtAnchorOrder;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkAddCash;
        private System.Windows.Forms.TextBox txtMaxUnpaidBuyOrder;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudDaySellAmountLimit;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.RadioButton rdoCtc;
        private System.Windows.Forms.RadioButton rdoOtc;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCurrencies;
        private System.Windows.Forms.Panel pnlOtcUsdx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoSimulated;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}