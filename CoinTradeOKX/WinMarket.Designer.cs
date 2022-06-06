namespace CoinTradeOKX
{
    partial class WinMarket
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.amount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.money = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.range = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.KYC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.visible = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pricefloat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.payment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rdoSellOrders = new System.Windows.Forms.RadioButton();
            this.rdoBuyOrders = new System.Windows.Forms.RadioButton();
            this.cmbCount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.t1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.index,
            this.price,
            this.amount,
            this.money,
            this.range,
            this.name,
            this.KYC,
            this.rate,
            this.orderCount,
            this.userAge,
            this.visible,
            this.pricefloat,
            this.payment,
            this.t1});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(-1, 58);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1728, 618);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // index
            // 
            this.index.Text = "序号";
            // 
            // price
            // 
            this.price.Text = "单价";
            this.price.Width = 126;
            // 
            // amount
            // 
            this.amount.Text = "数量";
            this.amount.Width = 122;
            // 
            // money
            // 
            this.money.Text = "金额";
            this.money.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.money.Width = 178;
            // 
            // range
            // 
            this.range.Text = "限额";
            this.range.Width = 137;
            // 
            // name
            // 
            this.name.Text = "名称";
            this.name.Width = 123;
            // 
            // KYC
            // 
            this.KYC.Text = "kyc";
            this.KYC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KYC.Width = 46;
            // 
            // rate
            // 
            this.rate.Text = "完成率";
            this.rate.Width = 55;
            // 
            // orderCount
            // 
            this.orderCount.Text = "对手订单数";
            this.orderCount.Width = 77;
            // 
            // userAge
            // 
            this.userAge.Text = "对手天数";
            this.userAge.Width = 73;
            // 
            // visible
            // 
            this.visible.Text = "可见";
            this.visible.Width = 42;
            // 
            // pricefloat
            // 
            this.pricefloat.Text = "浮动比例";
            this.pricefloat.Width = 106;
            // 
            // payment
            // 
            this.payment.Text = "支付通道";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rdoSellOrders
            // 
            this.rdoSellOrders.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoSellOrders.Checked = true;
            this.rdoSellOrders.Location = new System.Drawing.Point(19, 16);
            this.rdoSellOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoSellOrders.Name = "rdoSellOrders";
            this.rdoSellOrders.Size = new System.Drawing.Size(123, 31);
            this.rdoSellOrders.TabIndex = 1;
            this.rdoSellOrders.TabStop = true;
            this.rdoSellOrders.Text = "卖单列表";
            this.rdoSellOrders.UseVisualStyleBackColor = true;
            this.rdoSellOrders.CheckedChanged += new System.EventHandler(this.rdoSellOrders_CheckedChanged);
            // 
            // rdoBuyOrders
            // 
            this.rdoBuyOrders.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoBuyOrders.Location = new System.Drawing.Point(164, 15);
            this.rdoBuyOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBuyOrders.Name = "rdoBuyOrders";
            this.rdoBuyOrders.Size = new System.Drawing.Size(121, 31);
            this.rdoBuyOrders.TabIndex = 1;
            this.rdoBuyOrders.Text = "买单列表";
            this.rdoBuyOrders.UseVisualStyleBackColor = true;
            this.rdoBuyOrders.CheckedChanged += new System.EventHandler(this.rdoSellOrders_CheckedChanged);
            // 
            // cmbCount
            // 
            this.cmbCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCount.FormattingEnabled = true;
            this.cmbCount.Items.AddRange(new object[] {
            "全部",
            "35",
            "50",
            "75"});
            this.cmbCount.Location = new System.Drawing.Point(369, 21);
            this.cmbCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCount.Name = "cmbCount";
            this.cmbCount.Size = new System.Drawing.Size(103, 23);
            this.cmbCount.TabIndex = 2;
            this.cmbCount.SelectedIndexChanged += new System.EventHandler(this.cmbCount_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(327, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "数量";
            // 
            // t1
            // 
            this.t1.Text = "T+1";
            // 
            // WinMarket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1727, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCount);
            this.Controls.Add(this.rdoBuyOrders);
            this.Controls.Add(this.rdoSellOrders);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "WinMarket";
            this.Text = "WinMarket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader amount;
        private System.Windows.Forms.ColumnHeader money;
        private System.Windows.Forms.ColumnHeader range;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader KYC;
        private System.Windows.Forms.ColumnHeader rate;
        private System.Windows.Forms.ColumnHeader orderCount;
        private System.Windows.Forms.ColumnHeader userAge;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton rdoSellOrders;
        private System.Windows.Forms.RadioButton rdoBuyOrders;
        private System.Windows.Forms.ColumnHeader index;
        private System.Windows.Forms.ColumnHeader visible;
        private System.Windows.Forms.ColumnHeader pricefloat;
        private System.Windows.Forms.ComboBox cmbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader payment;
        private System.Windows.Forms.ColumnHeader t1;
    }
}