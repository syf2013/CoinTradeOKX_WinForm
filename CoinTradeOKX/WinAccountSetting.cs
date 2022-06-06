using CoinTradeOKX.Manager;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    public partial class WinAccountSetting : Form
    {
        private long id = 0;
        public WinAccountSetting()
        {
            InitializeComponent();
        }

        private async void SetAccountState(ReceiptAccountItem account,bool disable)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            var res = await mgr.SetAccountState(account.Id, disable);

            if (res.Code != 0)
            {
                MessageBox.Show(res.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            ReceiptAccountItem account = mgr.GetReceiptAccount(id);

            if (account != null)
            {
                this.id = account.Id;
                ReceiptAccountSetting setting = mgr.GetReceiptAccountSetting(id);

                setting.TotalDayReceiveAmount = this.numAmountLimit.Value * 10000;
                setting.TotalDayReceiveTimes = Convert.ToInt32( this.numTimesLimit.Value);
                setting.ForRecycle = chkRecycle.Checked;
                setting.AvalibleBeginHour = int.Parse(cmbBegin.Text);
                setting.AvalibleEndHour = int.Parse(cmbEnd.Text);

                AccountApplyType applyType = AccountApplyType.All;

                if (this.rdoAll.Checked)
                    applyType = AccountApplyType.All;
                if (this.rdoPayment.Checked)
                    applyType = AccountApplyType.Payment;
                if (this.rdoReceipt.Checked)
                    applyType = AccountApplyType.Receipt;

                if(applyType != account.ApplyType)
                {
                    var res = mgr.SetAccountApplyType(account.Id, applyType);
                    if(res.Code != 0)
                    {
                        MessageBox.Show(res.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    account.ApplyType = applyType;
                }

                bool disable = !this.chkEnable.Checked;

                if(disable != account.Disabled)
                {
                    SetAccountState(account, disable);
                }


                mgr.SaveSettings();

                this.Close();
            }
        }

        public void SetAccountID(long id)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            ReceiptAccountItem account = mgr.GetReceiptAccount(id);

            if(account != null)
            {
                this.id = account.Id;
                ReceiptAccountSetting setting = mgr.GetReceiptAccountSetting(id);
                this.lblName.Text = account.AccountName;
                this.lblBank.Text = account.BankName;
                this.lblAccount.Text = account.AccountNo;

                this.numAmountLimit.Value = Math.Floor( setting.TotalDayReceiveAmount / 10000);
                this.numTimesLimit.Value = setting.TotalDayReceiveTimes;

                this.chkEnable.Checked = !account.Disabled;
                this.chkRecycle.Checked = setting.ForRecycle;

                this.rdoAll.Checked = account.ApplyType == AccountApplyType.All;
                this.rdoPayment.Checked = account.ApplyType == AccountApplyType.Payment;
                this.rdoReceipt.Checked = account.ApplyType == AccountApplyType.Receipt;
                this.cmbBegin.SelectedIndex = setting.AvalibleBeginHour;
                this.cmbEnd.SelectedIndex = setting.AvalibleEndHour - 1;
            }
        }

        private void WinAccountSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
