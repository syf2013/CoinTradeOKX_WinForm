namespace CoinTradeOKX
{
    partial class WinHistory
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnUpdateHistory = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbSide = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblLoading = new System.Windows.Forms.Label();
            this.rdoUseDay = new System.Windows.Forms.RadioButton();
            this.rdoUseMonth = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoUnit10K = new System.Windows.Forms.RadioButton();
            this.rdoUnitK = new System.Windows.Forms.RadioButton();
            this.flpCheckbox = new System.Windows.Forms.FlowLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.llQueryExport = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQueryLastPage = new System.Windows.Forms.Button();
            this.btnQueryFirstPage = new System.Windows.Forms.Button();
            this.btnQueryPrePage = new System.Windows.Forms.Button();
            this.btnQueryNextPage = new System.Windows.Forms.Button();
            this.txtQueryName = new System.Windows.Forms.TextBox();
            this.cbQueryStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtQueryStart = new System.Windows.Forms.DateTimePicker();
            this.dtQueryEnd = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbCurrency = new System.Windows.Forms.ComboBox();
            this.cbQuerySide = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.pnlChart.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1835, 871);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(523, 252);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            // 
            // btnUpdateHistory
            // 
            this.btnUpdateHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateHistory.Location = new System.Drawing.Point(1972, 18);
            this.btnUpdateHistory.Name = "btnUpdateHistory";
            this.btnUpdateHistory.Size = new System.Drawing.Size(232, 53);
            this.btnUpdateHistory.TabIndex = 1;
            this.btnUpdateHistory.Text = "更新历史数据";
            this.btnUpdateHistory.UseVisualStyleBackColor = true;
            this.btnUpdateHistory.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(12, 29);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 31);
            this.dtpStart.TabIndex = 3;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(274, 30);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 31);
            this.dtpEnd.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1250, 26);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(103, 45);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbSide
            // 
            this.cbSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSide.FormattingEnabled = true;
            this.cbSide.Items.AddRange(new object[] {
            "卖出",
            "买入"});
            this.cbSide.Location = new System.Drawing.Point(491, 32);
            this.cbSide.Name = "cbSide";
            this.cbSide.Size = new System.Drawing.Size(121, 29);
            this.cbSide.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(1981, 77);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(54, 21);
            this.lblLoading.TabIndex = 8;
            this.lblLoading.Text = "2019";
            // 
            // rdoUseDay
            // 
            this.rdoUseDay.AutoSize = true;
            this.rdoUseDay.Checked = true;
            this.rdoUseDay.Location = new System.Drawing.Point(7, 8);
            this.rdoUseDay.Name = "rdoUseDay";
            this.rdoUseDay.Size = new System.Drawing.Size(119, 25);
            this.rdoUseDay.TabIndex = 9;
            this.rdoUseDay.TabStop = true;
            this.rdoUseDay.Text = "按日统计";
            this.rdoUseDay.UseVisualStyleBackColor = true;
            // 
            // rdoUseMonth
            // 
            this.rdoUseMonth.AutoSize = true;
            this.rdoUseMonth.Location = new System.Drawing.Point(144, 7);
            this.rdoUseMonth.Name = "rdoUseMonth";
            this.rdoUseMonth.Size = new System.Drawing.Size(119, 25);
            this.rdoUseMonth.TabIndex = 9;
            this.rdoUseMonth.Text = "按月统计";
            this.rdoUseMonth.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoUseDay);
            this.panel1.Controls.Add(this.rdoUseMonth);
            this.panel1.Location = new System.Drawing.Point(643, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 40);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "——";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTotal);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.flpCheckbox);
            this.panel2.Controls.Add(this.btnUpdateHistory);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblLoading);
            this.panel2.Controls.Add(this.dtpStart);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.dtpEnd);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.cbSide);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2387, 141);
            this.panel2.TabIndex = 12;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(1371, 40);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(85, 21);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "总计: 0";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.rdoUnit10K);
            this.panel3.Controls.Add(this.rdoUnitK);
            this.panel3.Location = new System.Drawing.Point(948, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(271, 55);
            this.panel3.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "金额单位";
            // 
            // rdoUnit10K
            // 
            this.rdoUnit10K.AutoSize = true;
            this.rdoUnit10K.Checked = true;
            this.rdoUnit10K.Location = new System.Drawing.Point(132, 15);
            this.rdoUnit10K.Name = "rdoUnit10K";
            this.rdoUnit10K.Size = new System.Drawing.Size(56, 25);
            this.rdoUnit10K.TabIndex = 9;
            this.rdoUnit10K.TabStop = true;
            this.rdoUnit10K.Text = "万";
            this.rdoUnit10K.UseVisualStyleBackColor = true;
            // 
            // rdoUnitK
            // 
            this.rdoUnitK.AutoSize = true;
            this.rdoUnitK.Location = new System.Drawing.Point(207, 15);
            this.rdoUnitK.Name = "rdoUnitK";
            this.rdoUnitK.Size = new System.Drawing.Size(46, 25);
            this.rdoUnitK.TabIndex = 9;
            this.rdoUnitK.Text = "K";
            this.rdoUnitK.UseVisualStyleBackColor = true;
            // 
            // flpCheckbox
            // 
            this.flpCheckbox.Location = new System.Drawing.Point(12, 77);
            this.flpCheckbox.Name = "flpCheckbox";
            this.flpCheckbox.Size = new System.Drawing.Size(1827, 44);
            this.flpCheckbox.TabIndex = 12;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(3, 6);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1994, 436);
            this.chart1.TabIndex = 13;
            this.chart1.Text = "chart1";
            // 
            // pnlChart
            // 
            this.pnlChart.AutoScroll = true;
            this.pnlChart.Controls.Add(this.chart1);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChart.Location = new System.Drawing.Point(0, 141);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(2387, 466);
            this.pnlChart.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblRecordCount);
            this.panel4.Controls.Add(this.llQueryExport);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnQueryLastPage);
            this.panel4.Controls.Add(this.btnQueryFirstPage);
            this.panel4.Controls.Add(this.btnQueryPrePage);
            this.panel4.Controls.Add(this.btnQueryNextPage);
            this.panel4.Controls.Add(this.txtQueryName);
            this.panel4.Controls.Add(this.cbQueryStatus);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dtQueryStart);
            this.panel4.Controls.Add(this.dtQueryEnd);
            this.panel4.Controls.Add(this.btnQuery);
            this.panel4.Controls.Add(this.cbCurrency);
            this.panel4.Controls.Add(this.cbQuerySide);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 607);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2387, 91);
            this.panel4.TabIndex = 15;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Location = new System.Drawing.Point(1763, 36);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(106, 21);
            this.lblRecordCount.TabIndex = 20;
            this.lblRecordCount.Text = "总记录: 0";
            // 
            // llQueryExport
            // 
            this.llQueryExport.AutoSize = true;
            this.llQueryExport.Location = new System.Drawing.Point(1654, 35);
            this.llQueryExport.Name = "llQueryExport";
            this.llQueryExport.Size = new System.Drawing.Size(94, 21);
            this.llQueryExport.TabIndex = 19;
            this.llQueryExport.TabStop = true;
            this.llQueryExport.Text = "导出记录";
            this.llQueryExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llQueryExport_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1167, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 18;
            this.label6.Text = "姓名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(944, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 17;
            this.label4.Text = "状态";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(522, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 21);
            this.label7.TabIndex = 16;
            this.label7.Text = "币种";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(717, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "类型";
            // 
            // btnQueryLastPage
            // 
            this.btnQueryLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryLastPage.Location = new System.Drawing.Point(2283, 29);
            this.btnQueryLastPage.Name = "btnQueryLastPage";
            this.btnQueryLastPage.Size = new System.Drawing.Size(75, 27);
            this.btnQueryLastPage.TabIndex = 15;
            this.btnQueryLastPage.Text = ">|";
            this.btnQueryLastPage.UseVisualStyleBackColor = true;
            this.btnQueryLastPage.Click += new System.EventHandler(this.btnQueryLastPage_Click);
            // 
            // btnQueryFirstPage
            // 
            this.btnQueryFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryFirstPage.Location = new System.Drawing.Point(2034, 29);
            this.btnQueryFirstPage.Name = "btnQueryFirstPage";
            this.btnQueryFirstPage.Size = new System.Drawing.Size(75, 27);
            this.btnQueryFirstPage.TabIndex = 14;
            this.btnQueryFirstPage.Text = "|<";
            this.btnQueryFirstPage.UseVisualStyleBackColor = true;
            this.btnQueryFirstPage.Click += new System.EventHandler(this.btnQueryFirstPage_Click);
            // 
            // btnQueryPrePage
            // 
            this.btnQueryPrePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryPrePage.Location = new System.Drawing.Point(2115, 29);
            this.btnQueryPrePage.Name = "btnQueryPrePage";
            this.btnQueryPrePage.Size = new System.Drawing.Size(75, 27);
            this.btnQueryPrePage.TabIndex = 14;
            this.btnQueryPrePage.Text = "<";
            this.btnQueryPrePage.UseVisualStyleBackColor = true;
            this.btnQueryPrePage.Click += new System.EventHandler(this.btnQueryPrePage_Click);
            // 
            // btnQueryNextPage
            // 
            this.btnQueryNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryNextPage.Location = new System.Drawing.Point(2202, 29);
            this.btnQueryNextPage.Name = "btnQueryNextPage";
            this.btnQueryNextPage.Size = new System.Drawing.Size(75, 27);
            this.btnQueryNextPage.TabIndex = 14;
            this.btnQueryNextPage.Text = ">";
            this.btnQueryNextPage.UseVisualStyleBackColor = true;
            this.btnQueryNextPage.Click += new System.EventHandler(this.btnQueryNextPage_Click);
            // 
            // txtQueryName
            // 
            this.txtQueryName.Location = new System.Drawing.Point(1225, 31);
            this.txtQueryName.Name = "txtQueryName";
            this.txtQueryName.Size = new System.Drawing.Size(227, 31);
            this.txtQueryName.TabIndex = 0;
            // 
            // cbQueryStatus
            // 
            this.cbQueryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryStatus.FormattingEnabled = true;
            this.cbQueryStatus.Items.AddRange(new object[] {
            "全部",
            "已完成",
            "已取消"});
            this.cbQueryStatus.Location = new System.Drawing.Point(1004, 32);
            this.cbQueryStatus.Name = "cbQueryStatus";
            this.cbQueryStatus.Size = new System.Drawing.Size(121, 29);
            this.cbQueryStatus.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "——";
            // 
            // dtQueryStart
            // 
            this.dtQueryStart.Location = new System.Drawing.Point(12, 29);
            this.dtQueryStart.Name = "dtQueryStart";
            this.dtQueryStart.Size = new System.Drawing.Size(200, 31);
            this.dtQueryStart.TabIndex = 3;
            // 
            // dtQueryEnd
            // 
            this.dtQueryEnd.Location = new System.Drawing.Point(274, 30);
            this.dtQueryEnd.Name = "dtQueryEnd";
            this.dtQueryEnd.Size = new System.Drawing.Size(200, 31);
            this.dtQueryEnd.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(1501, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(103, 45);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbCurrency
            // 
            this.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrency.FormattingEnabled = true;
            this.cbCurrency.Location = new System.Drawing.Point(580, 32);
            this.cbCurrency.Name = "cbCurrency";
            this.cbCurrency.Size = new System.Drawing.Size(121, 29);
            this.cbCurrency.TabIndex = 6;
            // 
            // cbQuerySide
            // 
            this.cbQuerySide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuerySide.FormattingEnabled = true;
            this.cbQuerySide.Items.AddRange(new object[] {
            "全部",
            "卖出",
            "买入"});
            this.cbQuerySide.Location = new System.Drawing.Point(785, 31);
            this.cbQuerySide.Name = "cbQuerySide";
            this.cbQuerySide.Size = new System.Drawing.Size(121, 29);
            this.cbQuerySide.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 698);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 72;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(4);
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(2387, 329);
            this.dataGridView1.TabIndex = 17;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "法币交易记录";
            this.saveFileDialog1.Filter = "html|*.html";
            // 
            // WinHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2387, 1027);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox1);
            this.Name = "WinHistory";
            this.Text = "数据统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MaximumSizeChanged += new System.EventHandler(this.WinHistory_MaximumSizeChanged);
            this.Load += new System.EventHandler(this.WinHistory_Load);
            this.ResizeEnd += new System.EventHandler(this.WinHistory_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.WinHistory_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.pnlChart.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnUpdateHistory;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbSide;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.RadioButton rdoUseDay;
        private System.Windows.Forms.RadioButton rdoUseMonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.FlowLayoutPanel flpCheckbox;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoUnit10K;
        private System.Windows.Forms.RadioButton rdoUnitK;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbQueryStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtQueryStart;
        private System.Windows.Forms.DateTimePicker dtQueryEnd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cbQuerySide;
        private System.Windows.Forms.TextBox txtQueryName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnQueryLastPage;
        private System.Windows.Forms.Button btnQueryFirstPage;
        private System.Windows.Forms.Button btnQueryPrePage;
        private System.Windows.Forms.Button btnQueryNextPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llQueryExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCurrency;
        private System.Windows.Forms.Label lblRecordCount;
    }
}