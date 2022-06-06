namespace CoinTradeOKX
{
    partial class WinContract
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMatching = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPop3Port = new System.Windows.Forms.TextBox();
            this.txtBankEmail = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPop3Host = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDetails = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.lblUsdtCny = new System.Windows.Forms.Label();
            this.lblOTCUsdt = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCTCUsdt = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.deepView1 = new CoinTradeOKX.Control.DepthView();
            this.contractList1 = new CoinTradeOKX.Control.ContractList();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtMatching);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtPop3Port);
            this.panel2.Controls.Add(this.txtBankEmail);
            this.panel2.Controls.Add(this.txtUsername);
            this.panel2.Controls.Add(this.txtPop3Host);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1288, 10);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(445, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "开启自动对账";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMatching
            // 
            this.txtMatching.Location = new System.Drawing.Point(292, 52);
            this.txtMatching.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatching.Name = "txtMatching";
            this.txtMatching.Size = new System.Drawing.Size(356, 21);
            this.txtMatching.TabIndex = 3;
            this.txtMatching.Text = "您账户(?<account>\\d+)于\\d+月\\d+日.+转入人民币(?<money>\\d+\\.\\d+)，付方(?<name>.+?)($|，)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "密码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "内容正则";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "银行邮箱";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "邮箱";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "主机";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(277, 28);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(147, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "syzufvndxieecaig";
            // 
            // txtPop3Port
            // 
            this.txtPop3Port.Location = new System.Drawing.Point(68, 29);
            this.txtPop3Port.Margin = new System.Windows.Forms.Padding(2);
            this.txtPop3Port.Name = "txtPop3Port";
            this.txtPop3Port.Size = new System.Drawing.Size(147, 21);
            this.txtPop3Port.TabIndex = 1;
            this.txtPop3Port.Text = "995";
            // 
            // txtBankEmail
            // 
            this.txtBankEmail.Location = new System.Drawing.Point(76, 54);
            this.txtBankEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtBankEmail.Name = "txtBankEmail";
            this.txtBankEmail.Size = new System.Drawing.Size(147, 21);
            this.txtBankEmail.TabIndex = 0;
            this.txtBankEmail.Text = "95555@message.cmbchina.com";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(277, 7);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(147, 21);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "137150225@qq.com";
            // 
            // txtPop3Host
            // 
            this.txtPop3Host.Location = new System.Drawing.Point(68, 7);
            this.txtPop3Host.Margin = new System.Windows.Forms.Padding(2);
            this.txtPop3Host.Name = "txtPop3Host";
            this.txtPop3Host.Size = new System.Drawing.Size(147, 21);
            this.txtPop3Host.TabIndex = 0;
            this.txtPop3Host.Text = "pop.qq.com";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.deepView1);
            this.groupBox1.Location = new System.Drawing.Point(967, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 582);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "锚定币";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDetails);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.lblUsdtCny);
            this.groupBox2.Controls.Add(this.lblOTCUsdt);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.lblCTCUsdt);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(315, 146);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.Location = new System.Drawing.Point(230, 18);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(2);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(77, 39);
            this.btnDetails.TabIndex = 50;
            this.btnDetails.Text = "挂单详情";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 70);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 12);
            this.label19.TabIndex = 3;
            this.label19.Text = "RMB估值";
            // 
            // lblUsdtCny
            // 
            this.lblUsdtCny.AutoSize = true;
            this.lblUsdtCny.Location = new System.Drawing.Point(56, 70);
            this.lblUsdtCny.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsdtCny.Name = "lblUsdtCny";
            this.lblUsdtCny.Size = new System.Drawing.Size(11, 12);
            this.lblUsdtCny.TabIndex = 2;
            this.lblUsdtCny.Text = "0";
            // 
            // lblOTCUsdt
            // 
            this.lblOTCUsdt.AutoSize = true;
            this.lblOTCUsdt.Location = new System.Drawing.Point(56, 43);
            this.lblOTCUsdt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOTCUsdt.Name = "lblOTCUsdt";
            this.lblOTCUsdt.Size = new System.Drawing.Size(11, 12);
            this.lblOTCUsdt.TabIndex = 2;
            this.lblOTCUsdt.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 43);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "法币账户";
            // 
            // lblCTCUsdt
            // 
            this.lblCTCUsdt.AutoSize = true;
            this.lblCTCUsdt.Location = new System.Drawing.Point(56, 18);
            this.lblCTCUsdt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCTCUsdt.Name = "lblCTCUsdt";
            this.lblCTCUsdt.Size = new System.Drawing.Size(11, 12);
            this.lblCTCUsdt.TabIndex = 2;
            this.lblCTCUsdt.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "币币账户";
            // 
            // deepView1
            // 
            this.deepView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deepView1.Location = new System.Drawing.Point(3, 168);
            this.deepView1.Name = "deepView1";
            this.deepView1.Size = new System.Drawing.Size(315, 411);
            this.deepView1.TabIndex = 0;
            // 
            // contractList1
            // 
            this.contractList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contractList1.AutoScroll = true;
            this.contractList1.IsGlobalView = false;
            this.contractList1.Location = new System.Drawing.Point(1, 1);
            this.contractList1.Margin = new System.Windows.Forms.Padding(1);
            this.contractList1.Name = "contractList1";
            this.contractList1.Size = new System.Drawing.Size(959, 584);
            this.contractList1.TabIndex = 5;
            // 
            // WinContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 587);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.contractList1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WinContract";
            this.Text = "订单管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinContract_FormClosed);
            this.Load += new System.EventHandler(this.WinContract_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtMatching;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPop3Port;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPop3Host;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBankEmail;
        private System.Windows.Forms.Timer timer1;
        private Control.ContractList contractList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Control.DepthView deepView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblUsdtCny;
        private System.Windows.Forms.Label lblOTCUsdt;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCTCUsdt;
        private System.Windows.Forms.Label label14;
    }
}