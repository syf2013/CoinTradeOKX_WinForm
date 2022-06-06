namespace CoinTradeOKX
{
    partial class WinReceiptAccount
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bankType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.account = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.applyType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.enabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.todayTotalTimes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.todayTotalAmounts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayLimitedTimes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayLimitedAmounts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeSpan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.recycle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.btnAccountRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.bankType,
            this.account,
            this.applyType,
            this.enabled,
            this.todayTotalTimes,
            this.todayTotalAmounts,
            this.dayLimitedTimes,
            this.dayLimitedAmounts,
            this.timeSpan,
            this.recycle});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(1, 2);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1857, 645);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // name
            // 
            this.name.Text = "姓名";
            this.name.Width = 86;
            // 
            // bankType
            // 
            this.bankType.Text = "类型";
            this.bankType.Width = 182;
            // 
            // account
            // 
            this.account.Text = "账号";
            this.account.Width = 276;
            // 
            // applyType
            // 
            this.applyType.Text = "收付款";
            // 
            // enabled
            // 
            this.enabled.Text = "启用";
            // 
            // todayTotalTimes
            // 
            this.todayTotalTimes.Text = "当日笔数";
            this.todayTotalTimes.Width = 82;
            // 
            // todayTotalAmounts
            // 
            this.todayTotalAmounts.Text = "当日总额";
            this.todayTotalAmounts.Width = 92;
            // 
            // dayLimitedTimes
            // 
            this.dayLimitedTimes.Text = "日笔数限制";
            this.dayLimitedTimes.Width = 91;
            // 
            // dayLimitedAmounts
            // 
            this.dayLimitedAmounts.Text = "日限额";
            this.dayLimitedAmounts.Width = 114;
            // 
            // timeSpan
            // 
            this.timeSpan.Text = "时段";
            this.timeSpan.Width = 111;
            // 
            // recycle
            // 
            this.recycle.Text = "轮换";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(831, 670);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "刷 新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAccountRefresh
            // 
            this.btnAccountRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccountRefresh.Location = new System.Drawing.Point(1739, 688);
            this.btnAccountRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAccountRefresh.Name = "btnAccountRefresh";
            this.btnAccountRefresh.Size = new System.Drawing.Size(104, 38);
            this.btnAccountRefresh.TabIndex = 2;
            this.btnAccountRefresh.Text = "刷新账号";
            this.btnAccountRefresh.UseVisualStyleBackColor = true;
            this.btnAccountRefresh.Click += new System.EventHandler(this.btnAccountRefresh_Click);
            // 
            // WinReceiptAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1861, 742);
            this.Controls.Add(this.btnAccountRefresh);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WinReceiptAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收付款账号管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinReceiptAccount_FormClosed);
            this.Load += new System.EventHandler(this.WinReceiptAccount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader bankType;
        private System.Windows.Forms.ColumnHeader account;
        private System.Windows.Forms.ColumnHeader applyType;
        private System.Windows.Forms.ColumnHeader enabled;
        private System.Windows.Forms.ColumnHeader todayTotalTimes;
        private System.Windows.Forms.ColumnHeader todayTotalAmounts;
        private System.Windows.Forms.ColumnHeader dayLimitedTimes;
        private System.Windows.Forms.ColumnHeader dayLimitedAmounts;
        private System.Windows.Forms.ColumnHeader timeSpan;
        private System.Windows.Forms.ColumnHeader recycle;
        private System.Windows.Forms.Button btnAccountRefresh;
    }
}