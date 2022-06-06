using CoinTradeOKX.Manager;
using CoinTradeOKX.Monitor;
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
    public partial class WinMarket : Form
    {
        public WinMarket()
        {
            InitializeComponent();

            //listView1.SetStyle(ControlStyles.DoubleBuffer |
            // ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.AllPaintingInWmPaint, true);
            // listView1.UpdateStyles();
            this.ShowNumber = 100;
            this.cmbCount.SelectedIndex = 2;

        }

        int ShowNumber
        {
            get;
            set;
        }

        string Side
        {
            get;
            set;
        }

        private CurrencyMarket market = null;


        public void SetMarket(CurrencyMarket market)
        {
            this.market = market;
            this.UpdateOrders();
            this.Text = string.Format("{0}挂单详情", market.Currency.ToUpper());
        }


        private void UpdateOrders()
        {
            string[] columns = {"index","price", "amount", "money", "range", "name", "KYC", "rate", "orderCount", "userAge" ,"visible", "pricefloat" , "payment","t1" };
            Queue<ListViewItem> items = new Queue<ListViewItem>();
            int index = 0;
            foreach (ListViewItem i in this.listView1.Items)
                items.Enqueue(i);

            listView1.Items.Clear();

            int count = ShowNumber;

            Action<Order> listOrders = (order) => {
                index++;

                if (index > count)
                    return;

                ListViewItem item = null;
               

                if (items.Count > 0)
                {
                    item = items.Dequeue();
                }
                else
                {
                    item = new ListViewItem();

                    for (int i = 1; i < this.listView1.Columns.Count; i++)
                    {
                        var s = new ListViewItem.ListViewSubItem();
                        s.Name = columns[i];
                        item.SubItems.Add(s);
                    }
                }

                item.Text = index.ToString();

                string[] payType = new string[3];

                if ((order.PayType & Okex.PayType.Bank) > 0)
                    payType[0]= "卡";
                if ((order.PayType & Okex.PayType.Alipay) > 0)
                    payType[1]= "支";
                if ((order.PayType & Okex.PayType.WechatPay) > 0)
                    payType[2]= "微";

                var subItems = item.SubItems;

                item.SubItems["price"].Text = order.Price.ToString("0.00");
                item.SubItems["amount"].Text = order.AvailableAmount.ToString();
                item.SubItems["money"].Text = (order.Price * order.AvailableAmount).ToString("0.00");
                item.SubItems["range"].Text = string.Format("{0:0.00}", order.AmountRange.Min);

                bool isTeamMamber = OTCTeamManager.Instance.IsTeamMember(order.Owner.Nickname);
                var sItemName = item.SubItems["name"];
                sItemName.Text = order.Owner.Nickname;
                sItemName.ForeColor = isTeamMamber ? Color.Blue : Color.Black;
              
                item.SubItems["KYC"].Text = order.MinKycLevel.ToString();
                item.SubItems["rate"].Text = order.CompletionRates.ToString("p2");
                item.SubItems["orderCount"].Text = string.Format("{0}-{1},{2}", order.MinCompletedOrderQuantity, order.MaxCompletedOrderQuantity, order.MinSellOrderQuantity);
                item.SubItems["userAge"].Text = (DateTime.Now - order.MaxUserCreatedDate).Days + "天";
                item.SubItems["visible"].Text = order.Hidden ? string.Format("否({0:0.00})", order.HiddenPrice) : "是";
                item.SubItems["pricefloat"].Text = order.PriceType == PriceTypeEnum.Fixed ? "固定" : order.FloatRate.ToString("p2");
                item.SubItems["payment"].Text = String.Join(",", payType.Where(s => !String.IsNullOrEmpty(s))); // string.Join("," ,payType,);
                item.SubItems["t1"].Text = order.SafetyLimit ? "是" : "否";
                //if (isNew)
                this.listView1.Items.Add(item);

                
                
            };

            index = 0;
            if(rdoSellOrders.Checked)
                market.EachSellOrder(listOrders,true);
            if(rdoBuyOrders.Checked)
                market.EachBuyOrder(listOrders);

            //foreach(var a in items)
            //{
            //    listView1.Items.Remove(a);
            //}

            //this.listView1.RedrawItems(0, this.listView1.Items.Count - 1, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.market != null)
            {
                this.UpdateOrders();
            }
        }

        private void rdoSellOrders_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateOrders();
        }

        private void cmbCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] counts = {100, 35, 50, 75};
            int index = this.cmbCount.SelectedIndex;
            
            index = Math.Min(index, counts.Length - 1);
            this.ShowNumber = counts[index]; 
        }
    }
}
