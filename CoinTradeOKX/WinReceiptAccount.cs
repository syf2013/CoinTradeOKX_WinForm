using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using Common;
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
    public partial class WinReceiptAccount : Form
    {
        public WinReceiptAccount()
        {
            InitializeComponent();
        }

        private void RefreshList()
        {
            var mgr = OTCReceiptAccountManager.Instance;
            var items = this.listView1.Items;

            foreach (ListViewItem item in items)
            {
                item.Tag = 0;
                foreach (ListViewItem.ListViewSubItem s in item.SubItems)
                {
                    s.Text = "";
                    s.ResetStyle();
                }
            }

            int i = 0;
            mgr.EachAccount((account) =>
            {
                var stats = mgr.GetReceiptCount(account.Id);
                var setting = mgr.GetReceiptAccountSetting(account.Id);

                ListViewItem item = items.Count > i ? items[i] : null;

                if (item == null)
                {
                    item = new ListViewItem();

                    for (int c = 1; c < listView1.Columns.Count; c++)
                    {
                        var s = new ListViewItem.ListViewSubItem();
                        s.Name = this.listView1.Columns[c].Name;
                        item.SubItems.Add(s);
                    }

                    this.listView1.Items.Add(item);
                }

                item.Tag = account.Id;
                item.Text = account.AccountName;

                string applyType = "";

                switch (account.ApplyType)
                {
                    case AccountApplyType.All:
                        applyType = "收付款";
                        break;
                    case AccountApplyType.Payment:
                        applyType = "付款";
                        break;
                    case AccountApplyType.Receipt:
                        applyType = "收款";
                        break;
                }

                this.SetSubItemText(item, 1, account.BankName);
                this.SetSubItemText(item, 2, account.AccountNo);
                this.SetSubItemText(item, 3, applyType);
                this.SetSubItemText(item, 4, account.Disabled ? "否" : "是");
                this.SetSubItemText(item, 5, stats.Times.ToString());
                this.SetSubItemText(item, 6,stats.Amount.ToString());
                this.SetSubItemText(item, 7, setting.TotalDayReceiveTimes.ToString());
                this.SetSubItemText(item, 8, setting.TotalDayReceiveAmount.ToString());
                this.SetSubItemText(item, 9, string.Format("{0}-{1}", setting.AvalibleBeginHour, setting.AvalibleEndHour));
                this.SetSubItemText(item, 10, setting.ForRecycle ? "是" : "否");
                i++;
            });
        }

        private void SetSubItemText(ListViewItem item,int index, string value)
        {
            if(item.SubItems.Count > index)
            {
                item.SubItems[index].Text = value;
            }
        }

        private void WinReceiptAccount_Load(object sender, EventArgs e)
        {
            this.RefreshList();
            EventCenter.Instance.AddEventListener(EventNames.ReceiptAccountStateChanged, this.OnReceiptAccountStateChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RefreshList();
        }

        private void OnReceiptAccountStateChanged(object obj)
        {
            this.RefreshList();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var items = listView1.SelectedItems;

            if(items.Count > 0)
            {
                var item = items[0];

                long id = Convert.ToInt64(item.Tag);

               var win = WindowManager.Instance.OpenWindow<WinAccountSetting>();
                if (id > 0)
                {
                    win.SetAccountID(id);
                }
            }
        }

        private void btnAccountRefresh_Click(object sender, EventArgs e)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            mgr.UpdateAccountList(); //强制刷新
        }

        private void WinReceiptAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventCenter.Instance.RemoveListener(EventNames.ReceiptAccountStateChanged, this.OnReceiptAccountStateChanged);
        }
    }
}
