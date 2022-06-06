namespace CoinTradeOKX
{
    partial class OrderView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOperate = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblSide = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblKycLevel = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOperate
            // 
            this.btnOperate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOperate.Location = new System.Drawing.Point(439, 12);
            this.btnOperate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(67, 26);
            this.btnOperate.TabIndex = 0;
            this.btnOperate.Text = "撤消";
            this.btnOperate.UseVisualStyleBackColor = true;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(66, 21);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(63, 15);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "1000000";
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.ForeColor = System.Drawing.Color.Red;
            this.lblSide.Location = new System.Drawing.Point(140, 21);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(37, 15);
            this.lblSide.TabIndex = 2;
            this.lblSide.Text = "卖出";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(185, 21);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(15, 15);
            this.lblAmount.TabIndex = 3;
            this.lblAmount.Text = "5";
            // 
            // lblKycLevel
            // 
            this.lblKycLevel.AutoSize = true;
            this.lblKycLevel.Location = new System.Drawing.Point(347, 21);
            this.lblKycLevel.Name = "lblKycLevel";
            this.lblKycLevel.Size = new System.Drawing.Size(55, 15);
            this.lblKycLevel.TabIndex = 4;
            this.lblKycLevel.Text = "label4";
            this.lblKycLevel.Visible = false;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(275, 21);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(55, 15);
            this.lblCurrency.TabIndex = 5;
            this.lblCurrency.Text = "label1";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(6, 21);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(15, 15);
            this.lblIndex.TabIndex = 6;
            this.lblIndex.Text = "-";
            // 
            // OrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblKycLevel);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnOperate);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OrderView";
            this.Size = new System.Drawing.Size(516, 54);
            this.Load += new System.EventHandler(this.OrderView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblKycLevel;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblIndex;
    }
}
