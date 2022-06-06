namespace CoinTradeOKX.Control
{
    partial class ContractView
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
            this.tabWeChatPay = new System.Windows.Forms.TabPage();
            this.btnWeChatQrCode = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWeChatAccount = new System.Windows.Forms.TextBox();
            this.lblSide = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnPaid = new System.Windows.Forms.Button();
            this.lblFee = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRealName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRegisterDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKycLevel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblComplete = new System.Windows.Forms.Label();
            this.txtBankAccount = new System.Windows.Forms.TextBox();
            this.txtBankNumber = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.tabPayInfo = new System.Windows.Forms.TabControl();
            this.tabBank = new System.Windows.Forms.TabPage();
            this.tabAlipay = new System.Windows.Forms.TabPage();
            this.btnAlipayQrCode = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAlipayAccount = new System.Windows.Forms.TextBox();
            this.lblPaymentType = new System.Windows.Forms.Label();
            this.lblAppealed = new System.Windows.Forms.Label();
            this.tabWeChatPay.SuspendLayout();
            this.tabPayInfo.SuspendLayout();
            this.tabBank.SuspendLayout();
            this.tabAlipay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabWeChatPay
            // 
            this.tabWeChatPay.Controls.Add(this.btnWeChatQrCode);
            this.tabWeChatPay.Controls.Add(this.label9);
            this.tabWeChatPay.Controls.Add(this.txtWeChatAccount);
            this.tabWeChatPay.Location = new System.Drawing.Point(4, 22);
            this.tabWeChatPay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabWeChatPay.Name = "tabWeChatPay";
            this.tabWeChatPay.Size = new System.Drawing.Size(230, 81);
            this.tabWeChatPay.TabIndex = 2;
            this.tabWeChatPay.Text = "微信支付";
            this.tabWeChatPay.UseVisualStyleBackColor = true;
            // 
            // btnWeChatQrCode
            // 
            this.btnWeChatQrCode.Location = new System.Drawing.Point(49, 43);
            this.btnWeChatQrCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnWeChatQrCode.Name = "btnWeChatQrCode";
            this.btnWeChatQrCode.Size = new System.Drawing.Size(123, 33);
            this.btnWeChatQrCode.TabIndex = 3;
            this.btnWeChatQrCode.Text = "扫码";
            this.btnWeChatQrCode.UseVisualStyleBackColor = true;
            this.btnWeChatQrCode.Click += new System.EventHandler(this.btnWeChatQrCode_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "微信号";
            // 
            // txtWeChatAccount
            // 
            this.txtWeChatAccount.Location = new System.Drawing.Point(49, 11);
            this.txtWeChatAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtWeChatAccount.Name = "txtWeChatAccount";
            this.txtWeChatAccount.ReadOnly = true;
            this.txtWeChatAccount.Size = new System.Drawing.Size(145, 21);
            this.txtWeChatAccount.TabIndex = 1;
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSide.ForeColor = System.Drawing.Color.Black;
            this.lblSide.Location = new System.Drawing.Point(20, 14);
            this.lblSide.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(47, 19);
            this.lblSide.TabIndex = 0;
            this.lblSide.Text = "卖出";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrency.Location = new System.Drawing.Point(69, 14);
            this.lblCurrency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(39, 19);
            this.lblCurrency.TabIndex = 1;
            this.lblCurrency.Text = "BTC";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSize.Location = new System.Drawing.Point(121, 14);
            this.lblSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(79, 19);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "0.00001";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMoney.Location = new System.Drawing.Point(241, 14);
            this.lblMoney.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(89, 19);
            this.lblMoney.TabIndex = 2;
            this.lblMoney.Text = "18000.00";
            // 
            // btnRelease
            // 
            this.btnRelease.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRelease.Location = new System.Drawing.Point(539, 79);
            this.btnRelease.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(106, 51);
            this.btnRelease.TabIndex = 3;
            this.btnRelease.Text = "放币";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnPaid
            // 
            this.btnPaid.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPaid.Location = new System.Drawing.Point(539, 78);
            this.btnPaid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPaid.Name = "btnPaid";
            this.btnPaid.Size = new System.Drawing.Size(106, 51);
            this.btnPaid.TabIndex = 3;
            this.btnPaid.Text = "已付款";
            this.btnPaid.UseVisualStyleBackColor = false;
            this.btnPaid.Click += new System.EventHandler(this.btnPaid_Click);
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Location = new System.Drawing.Point(521, 19);
            this.lblFee.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(11, 12);
            this.lblFee.TabIndex = 4;
            this.lblFee.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(35, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "对手";
            // 
            // lblRealName
            // 
            this.lblRealName.AutoSize = true;
            this.lblRealName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRealName.Location = new System.Drawing.Point(81, 51);
            this.lblRealName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRealName.Name = "lblRealName";
            this.lblRealName.Size = new System.Drawing.Size(32, 16);
            this.lblRealName.TabIndex = 6;
            this.lblRealName.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "注册日期";
            // 
            // lblRegisterDate
            // 
            this.lblRegisterDate.AutoSize = true;
            this.lblRegisterDate.Location = new System.Drawing.Point(80, 85);
            this.lblRegisterDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegisterDate.Name = "lblRegisterDate";
            this.lblRegisterDate.Size = new System.Drawing.Size(41, 12);
            this.lblRegisterDate.TabIndex = 8;
            this.lblRegisterDate.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(478, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "手续费";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "身份认证";
            // 
            // lblKycLevel
            // 
            this.lblKycLevel.AutoSize = true;
            this.lblKycLevel.Location = new System.Drawing.Point(80, 107);
            this.lblKycLevel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblKycLevel.Name = "lblKycLevel";
            this.lblKycLevel.Size = new System.Drawing.Size(53, 12);
            this.lblKycLevel.TabIndex = 10;
            this.lblKycLevel.Text = "身份认证";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 132);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "成交数";
            // 
            // lblComplete
            // 
            this.lblComplete.AutoSize = true;
            this.lblComplete.Location = new System.Drawing.Point(80, 132);
            this.lblComplete.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComplete.Name = "lblComplete";
            this.lblComplete.Size = new System.Drawing.Size(41, 12);
            this.lblComplete.TabIndex = 11;
            this.lblComplete.Text = "成交数";
            // 
            // txtBankAccount
            // 
            this.txtBankAccount.Location = new System.Drawing.Point(12, 11);
            this.txtBankAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.ReadOnly = true;
            this.txtBankAccount.Size = new System.Drawing.Size(157, 21);
            this.txtBankAccount.TabIndex = 12;
            // 
            // txtBankNumber
            // 
            this.txtBankNumber.Location = new System.Drawing.Point(12, 35);
            this.txtBankNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBankNumber.Name = "txtBankNumber";
            this.txtBankNumber.ReadOnly = true;
            this.txtBankNumber.Size = new System.Drawing.Size(157, 21);
            this.txtBankNumber.TabIndex = 13;
            this.txtBankNumber.Text = "1212 1212 1212 1212 121 ";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(12, 58);
            this.txtBankName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.ReadOnly = true;
            this.txtBankName.Size = new System.Drawing.Size(157, 21);
            this.txtBankName.TabIndex = 14;
            // 
            // tabPayInfo
            // 
            this.tabPayInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tabPayInfo.Controls.Add(this.tabBank);
            this.tabPayInfo.Controls.Add(this.tabAlipay);
            this.tabPayInfo.Controls.Add(this.tabWeChatPay);
            this.tabPayInfo.Location = new System.Drawing.Point(253, 50);
            this.tabPayInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPayInfo.Name = "tabPayInfo";
            this.tabPayInfo.SelectedIndex = 0;
            this.tabPayInfo.Size = new System.Drawing.Size(238, 107);
            this.tabPayInfo.TabIndex = 15;
            // 
            // tabBank
            // 
            this.tabBank.Controls.Add(this.txtBankAccount);
            this.tabBank.Controls.Add(this.txtBankName);
            this.tabBank.Controls.Add(this.txtBankNumber);
            this.tabBank.Location = new System.Drawing.Point(4, 22);
            this.tabBank.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabBank.Name = "tabBank";
            this.tabBank.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabBank.Size = new System.Drawing.Size(230, 81);
            this.tabBank.TabIndex = 0;
            this.tabBank.Text = "银行";
            this.tabBank.UseVisualStyleBackColor = true;
            // 
            // tabAlipay
            // 
            this.tabAlipay.Controls.Add(this.btnAlipayQrCode);
            this.tabAlipay.Controls.Add(this.label8);
            this.tabAlipay.Controls.Add(this.txtAlipayAccount);
            this.tabAlipay.Location = new System.Drawing.Point(4, 22);
            this.tabAlipay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabAlipay.Name = "tabAlipay";
            this.tabAlipay.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabAlipay.Size = new System.Drawing.Size(230, 81);
            this.tabAlipay.TabIndex = 1;
            this.tabAlipay.Text = "支付宝";
            this.tabAlipay.UseVisualStyleBackColor = true;
            // 
            // btnAlipayQrCode
            // 
            this.btnAlipayQrCode.Location = new System.Drawing.Point(49, 42);
            this.btnAlipayQrCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAlipayQrCode.Name = "btnAlipayQrCode";
            this.btnAlipayQrCode.Size = new System.Drawing.Size(117, 35);
            this.btnAlipayQrCode.TabIndex = 2;
            this.btnAlipayQrCode.Text = "扫码";
            this.btnAlipayQrCode.UseVisualStyleBackColor = true;
            this.btnAlipayQrCode.Click += new System.EventHandler(this.btnAlipayQrCode_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 14);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "支付宝";
            // 
            // txtAlipayAccount
            // 
            this.txtAlipayAccount.Location = new System.Drawing.Point(49, 11);
            this.txtAlipayAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAlipayAccount.Name = "txtAlipayAccount";
            this.txtAlipayAccount.ReadOnly = true;
            this.txtAlipayAccount.Size = new System.Drawing.Size(145, 21);
            this.txtAlipayAccount.TabIndex = 0;
            // 
            // lblPaymentType
            // 
            this.lblPaymentType.AutoSize = true;
            this.lblPaymentType.Location = new System.Drawing.Point(211, 105);
            this.lblPaymentType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Size = new System.Drawing.Size(41, 12);
            this.lblPaymentType.TabIndex = 16;
            this.lblPaymentType.Text = "支付宝";
            this.lblPaymentType.Click += new System.EventHandler(this.lblPaymentType_Click);
            // 
            // lblAppealed
            // 
            this.lblAppealed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAppealed.ForeColor = System.Drawing.Color.White;
            this.lblAppealed.Location = new System.Drawing.Point(613, 14);
            this.lblAppealed.Name = "lblAppealed";
            this.lblAppealed.Size = new System.Drawing.Size(47, 23);
            this.lblAppealed.TabIndex = 17;
            this.lblAppealed.Text = "申诉中";
            this.lblAppealed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContractView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblAppealed);
            this.Controls.Add(this.lblPaymentType);
            this.Controls.Add(this.tabPayInfo);
            this.Controls.Add(this.lblComplete);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblKycLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRegisterDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRealName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFee);
            this.Controls.Add(this.btnPaid);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblSide);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ContractView";
            this.Size = new System.Drawing.Size(663, 167);
            this.tabWeChatPay.ResumeLayout(false);
            this.tabWeChatPay.PerformLayout();
            this.tabPayInfo.ResumeLayout(false);
            this.tabBank.ResumeLayout(false);
            this.tabBank.PerformLayout();
            this.tabAlipay.ResumeLayout(false);
            this.tabAlipay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnPaid;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRealName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRegisterDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKycLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblComplete;
        private System.Windows.Forms.TextBox txtBankAccount;
        private System.Windows.Forms.TextBox txtBankNumber;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TabControl tabPayInfo;
        private System.Windows.Forms.TabPage tabBank;
        private System.Windows.Forms.TabPage tabAlipay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAlipayAccount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWeChatAccount;
        private System.Windows.Forms.Button btnAlipayQrCode;
        private System.Windows.Forms.Button btnWeChatQrCode;
        private System.Windows.Forms.TabPage tabWeChatPay;
        private System.Windows.Forms.Label lblPaymentType;
        private System.Windows.Forms.Label lblAppealed;
    }
}
