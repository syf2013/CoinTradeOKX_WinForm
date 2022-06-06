using CoinTradeOKX.Manager;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using Newtonsoft.Json.Linq;
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
    public partial class WinCTCStat : Form
    {

        private Pool<CTCOrder> pool = Pool<CTCOrder>.GetPool();
        public List<CTCOrder> orders = new List<CTCOrder>();
        string currency1, currency2;
        decimal balance = 0;
        Instrument instrument = null;

        public WinCTCStat()
        {
            InitializeComponent();

            this.cmbResyncDays.SelectedIndex = 0;
        }

        private void WinCTCStat_Load(object sender, EventArgs e)
        {
            //this.SetCurrency("eos", "usdt");//. Config.Instance.Anchor);
            var now = DateTime.Now;
            this.dtpStart.Value = now.AddMonths(-1);
            this.dtpEnd.Value = now;

            this.timer1.Enabled = true;
        }

        public void SetCurrency(string currency1, string currency2, decimal balance)
        {
            this.currency1 = currency1;
            this.currency2 = currency2;
            this.balance = balance;
            this.timer1.Enabled = true;

            instrument = InstrumentTableVsUSDT.Instance.GetInstrument(currency1);

            CTCHistoryManager.Instance.LoadHistory(currency1, currency2);
        }


        int pageSize = 100;
        bool isLoading = false;

        private void LoadData()//(DateTime start, DateTime end)
        {
            isLoading = true;
            var pool = Pool<CTCOrder>.GetPool();
            pool.Put(this.orders);
            this.orders.Clear();
            var startTime = dtpStart.Value.Date;
            var endTime = dtpEnd.Value.Date.AddDays(1);
            this.orders = CTCHistoryManager.Instance.GetHistoryOrders(this.currency1,this.currency2, startTime,endTime);

            this.cmbPage.Items.Clear();

            int pageCount = orders.Count / pageSize + (orders.Count % pageSize > 0 ? 1 : 0);

            for(int i = 1; i <= pageCount;i++)
            {
                this.cmbPage.Items.Add(i);
            }

            if(orders.Count > 0)
            {
                this.cmbPage.SelectedIndex = 0;
            }

            this.Stat(this.orders);
            this.ShowData();

            isLoading = false;
        }

        private void Stat(IList<CTCOrder> orders)
        {
            IEnumerable<CTCOrder> historyList = orders.Reverse();
            decimal totalBuyAmount = 0;
            decimal totalSellAmount = 0;
            decimal totalBuyMoney = 0;
            decimal totalSellMoney = 0;
            decimal balabceMoney = this.balance;
            int maxBuyTimes = 0;  //最大连续买入次数
            int maxSellTimes = 0; //最大连续卖出次数

            decimal curBuyAmount = 0; //最大连续买入额度
            decimal maxBuyAmount = 0; //最大连续买入额度

            int buyTimes = 0;
            int sellTimes = 0;

            decimal totalFee = 0;
            int totalSellOrder = 0;
            int totalBuyOrder = 0;

            

            foreach (CTCOrder o in historyList)
            {
                decimal amount = o.FilledSize * o.PriceAvg;
                switch (o.Side)
                {
                    case OrderOparete.Buy:
                        totalBuyAmount += o.FilledSize;
                        totalBuyMoney += amount;
                        totalBuyOrder++;
                        buyTimes++;
                        sellTimes--;
                        curBuyAmount += amount;
                        break;
                    case OrderOparete.Sell:
                        totalSellAmount += o.FilledSize;
                        totalSellMoney += amount;
                        totalSellOrder++;
                        sellTimes++;
                        buyTimes--;
                        curBuyAmount -= amount;
                        break;
                }

                buyTimes = Math.Max(0, buyTimes);
                sellTimes = Math.Max(0, sellTimes);
                curBuyAmount = Math.Max(0, curBuyAmount);

                maxBuyTimes = Math.Max(maxBuyTimes, buyTimes);
                maxSellTimes = Math.Max(maxSellTimes, sellTimes);
                maxBuyAmount = Math.Max(maxBuyAmount, curBuyAmount);


                decimal fee = o.Fee;

                if (string.Compare(o.FeeCurrency, this.currency1, true) == 0)
                {
                    fee = o.Fee * o.PriceAvg;
                }

                totalFee += fee;
            }

            this.lblMaxBuy.Text = maxBuyAmount.ToString("0.00") + string.Format("({0}单)", maxBuyTimes);
            this.lblTotalBuy.Text = totalBuyMoney.ToString("0.00") + string.Format("({0}单)", totalBuyOrder);
            this.lblTotalSell.Text = totalSellMoney.ToString("0.00") + string.Format("({0}单)", totalSellOrder);
            this.lblTotalProfit.Text = (totalSellMoney - totalBuyMoney + balabceMoney + totalFee).ToString("0.00");
            this.lblBalance.Text = balabceMoney.ToString("0.00");
            this.lblFee.Text = totalFee.ToString("0.00");

            this.lblBuyCount.Text = Math.Round(totalBuyAmount, instrument.MinSizeDigit).ToString();
            this.lblSellCount.Text = Math.Round(totalSellAmount, instrument.MinSizeDigit).ToString();


            totalBuyAmount = Math.Max(totalBuyAmount, instrument.MinSize);
            totalSellAmount = Math.Max(totalSellAmount, instrument.MinSize);
           

            this.lblBuyAvg.Text = (totalBuyMoney / totalBuyAmount).ToString(instrument.PriceFormat);
            this.lblSellAvg.Text = (totalSellMoney / totalSellAmount).ToString(instrument.PriceFormat);

        }
        
        static Queue<ListViewItem> listPool = new Queue<ListViewItem>();
        private void ShowData()
        {
            int pageIndex = cmbPage.SelectedIndex;
            
            foreach(ListViewItem item in listView1.Items)
            {
                listPool.Enqueue(item);
            }

            this.listView1.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            string[] columns = { "datetime", "currency", "side", "size", "price", "fillSize", "priceAvg" };

            
            for(int index = 0;index < orders.Count;index ++)
            {
                CTCOrder o = this.orders[index];
                if (index < pageIndex * pageSize || index >= pageSize * (pageIndex + 1))
                {
                    continue;
                }

                ListViewItem item = null;

                if (listPool.Count > 0)
                {
                    item = listPool.Dequeue();
                    item.Text = o.PublicId.ToString();
                    item.Checked = false;
                    item.Selected = false;
                    this.listView1.Items.Add(item);
                }
                else
                {
                   item = this.listView1.Items.Add(o.PublicId.ToString());

                    for (int i = 0; i < columns.Length; i++)
                    {
                        var s = new ListViewItem.ListViewSubItem();
                        s.Name = columns[i];// columns[i];
                        item.SubItems.Add(s);
                    }
                }

                

                
                item.SubItems["datetime"].Text = DateUtil.UtcToLocalTime( o.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems["currency"].Text = o.Currency;
                item.SubItems["side"].Text = o.Side == OrderOparete.Buy ? "买入" : "卖出"; 
                item.SubItems["size"].Text = o.Size.ToString();
                item.SubItems["price"].Text = o.Price.ToString();
                item.SubItems["fillSize"].Text = o.FilledSize.ToString();
                item.SubItems["priceAvg"].Text = o.PriceAvg.ToString();

                item.Tag = o;

            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!CTCHistoryManager.Instance.IsLoading(currency1,currency2))
            {
                this.LoadData();
                this.timer1.Enabled = false;
                this.lblWaiting.Visible = false;
                panel2.Enabled = true;
                /*
                btnRefresh.Enabled = true;
                btnResync.Enabled = true;
                cmbPage.Enabled = true;
                */
            }
            else
            {
                panel2.Enabled = false;
                this.lblWaiting.Visible = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void btnSelOnly_Click(object sender, EventArgs e)
        {
            List<CTCOrder> orders = new List<CTCOrder>();

            foreach(ListViewItem item  in this.listView1.Items)
            {
                if(item.Checked)
                {
                    orders.Add(item.Tag as CTCOrder);
                }
            }

            this.Stat(orders);
        }

        private void btnResync_Click(object sender, EventArgs e)
        {
            int days = 0;

            switch (cmbResyncDays.SelectedIndex)
            {
                case 0:
                    days = 3;
                    break;
                case 1:
                    days = 7;
                    break;
                case 2:
                    days = 14;
                    break;
                case 3:
                    days = 30;
                    break;
                case 4:
                    days = 60;
                    break;
            }

            this.timer1.Enabled = true;
            CTCHistoryManager.Instance.ResyncHistory(currency1, currency2, days);
            if (!CTCHistoryManager.Instance.IsLoading(currency1, currency2))
            {
                this.LoadData();
                this.timer1.Enabled = false;
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                this.ShowData();
            }
        }

        private void WinCTCStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                listPool.Enqueue(item);
            }

            this.listView1.Items.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listView1.SelectedItems.Count > 0)
            {
                var order = (CTCOrder)this.listView1.SelectedItems[0].Tag;
                this.txtId.Text = order.PublicId.ToString();
            }
        }

        private void btnFromId_Click(object sender, EventArgs e)
        {
            long id = 0;
            if(long.TryParse(txtId.Text, out id))
            {
                List<CTCOrder> orderList = new List<CTCOrder>();

                foreach(var order in this.orders)
                {
                    if(order.PublicId >= id)
                    {
                        orderList.Add(order);
                    }
                }

                this.Stat(orderList);
            }
            else
            {
                WinMessage.Show(MessageType.Alert, "请选中要开始统计的项目");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.pool.Put(this.orders);
            base.OnClosing(e);
        }
    }
}
