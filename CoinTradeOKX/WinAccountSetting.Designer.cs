namespace CoinTradeOKX
{
    partial class WinAccountSetting
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoPayment = new System.Windows.Forms.RadioButton();
            this.rdoReceipt = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numAmountLimit = new System.Windows.Forms.NumericUpDown();
            this.numTimesLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkRecycle = new System.Windows.Forms.CheckBox();
            this.cmbBegin = new System.Windows.Forms.ComboBox();
            this.cmbEnd = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(34, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 12);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "text";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(108, 23);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(41, 12);
            this.lblBank.TabIndex = 1;
            this.lblBank.Text = "label2";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(239, 23);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(41, 12);
            this.lblAccount.TabIndex = 1;
            this.lblAccount.Text = "label2";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(3, 3);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(59, 16);
            this.rdoAll.TabIndex = 2;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "收付款";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoPayment);
            this.panel1.Controls.Add(this.rdoReceipt);
            this.panel1.Controls.Add(this.rdoAll);
            this.panel1.Location = new System.Drawing.Point(36, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 29);
            this.panel1.TabIndex = 3;
            // 
            // rdoPayment
            // 
            this.rdoPayment.AutoSize = true;
            this.rdoPayment.Location = new System.Drawing.Point(214, 3);
            this.rdoPayment.Name = "rdoPayment";
            this.rdoPayment.Size = new System.Drawing.Size(47, 16);
            this.rdoPayment.TabIndex = 2;
            this.rdoPayment.TabStop = true;
            this.rdoPayment.Text = "付款";
            this.rdoPayment.UseVisualStyleBackColor = true;
            // 
            // rdoReceipt
            // 
            this.rdoReceipt.AutoSize = true;
            this.rdoReceipt.Location = new System.Drawing.Point(106, 3);
            this.rdoReceipt.Name = "rdoReceipt";
            this.rdoReceipt.Size = new System.Drawing.Size(47, 16);
            this.rdoReceipt.TabIndex = 2;
            this.rdoReceipt.TabStop = true;
            this.rdoReceipt.Text = "收款";
            this.rdoReceipt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "每日收款限额(万)";
            // 
            // numAmountLimit
            // 
            this.numAmountLimit.Location = new System.Drawing.Point(143, 131);
            this.numAmountLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAmountLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmountLimit.Name = "numAmountLimit";
            this.numAmountLimit.Size = new System.Drawing.Size(65, 21);
            this.numAmountLimit.TabIndex = 5;
            this.numAmountLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTimesLimit
            // 
            this.numTimesLimit.Location = new System.Drawing.Point(325, 128);
            this.numTimesLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTimesLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimesLimit.Name = "numTimesLimit";
            this.numTimesLimit.Size = new System.Drawing.Size(65, 21);
            this.numTimesLimit.TabIndex = 7;
            this.numTimesLimit.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "每日收款笔数";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(39, 188);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(48, 16);
            this.chkEnable.TabIndex = 8;
            this.chkEnable.Text = "启用";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(159, 236);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 25);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkRecycle
            // 
            this.chkRecycle.AutoSize = true;
            this.chkRecycle.Location = new System.Drawing.Point(110, 188);
            this.chkRecycle.Name = "chkRecycle";
            this.chkRecycle.Size = new System.Drawing.Size(72, 16);
            this.chkRecycle.TabIndex = 10;
            this.chkRecycle.Text = "开启轮换";
            this.chkRecycle.UseVisualStyleBackColor = true;
            // 
            // cmbBegin
            // 
            this.cmbBegin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBegin.FormattingEnabled = true;
            this.cmbBegin.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cmbBegin.Location = new System.Drawing.Point(259, 183);
            this.cmbBegin.Name = "cmbBegin";
            this.cmbBegin.Size = new System.Drawing.Size(49, 20);
            this.cmbBegin.TabIndex = 11;
            // 
            // cmbEnd
            // 
            this.cmbEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnd.FormattingEnabled = true;
            this.cmbEnd.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbEnd.Location = new System.Drawing.Point(345, 183);
            this.cmbEnd.Name = "cmbEnd";
            this.cmbEnd.Size = new System.Drawing.Size(49, 20);
            this.cmbEnd.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "可用时段";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(312, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "点到";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(400, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "点";
            // 
            // WinAccountSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 291);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbEnd);
            this.Controls.Add(this.cmbBegin);
            this.Controls.Add(this.chkRecycle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkEnable);
            this.Controls.Add(this.numTimesLimit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numAmountLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinAccountSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账号设置";
            this.Load += new System.EventHandler(this.WinAccountSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimesLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoPayment;
        private System.Windows.Forms.RadioButton rdoReceipt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numAmountLimit;
        private System.Windows.Forms.NumericUpDown numTimesLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkRecycle;
        private System.Windows.Forms.ComboBox cmbBegin;
        private System.Windows.Forms.ComboBox cmbEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}