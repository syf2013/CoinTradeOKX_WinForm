namespace CoinTradeOKX.Control
{
    partial class ReceiptAccountView
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.lblApplyType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(11, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 12);
            this.lblName.TabIndex = 0;
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(58, 7);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(0, 12);
            this.lblBank.TabIndex = 0;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(165, 7);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(119, 12);
            this.lblAccount.TabIndex = 1;
            this.lblAccount.Text = "8888888888888888888";
            // 
            // lblApplyType
            // 
            this.lblApplyType.AutoSize = true;
            this.lblApplyType.Location = new System.Drawing.Point(290, 7);
            this.lblApplyType.Name = "lblApplyType";
            this.lblApplyType.Size = new System.Drawing.Size(41, 12);
            this.lblApplyType.TabIndex = 2;
            this.lblApplyType.Text = "收付款";
            // 
            // ReceiptAccountView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblApplyType);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.lblName);
            this.Name = "ReceiptAccountView";
            this.Size = new System.Drawing.Size(337, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblApplyType;
    }
}
