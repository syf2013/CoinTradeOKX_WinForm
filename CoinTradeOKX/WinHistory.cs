using CoinTradeOKX.Manager;
using CoinTradeOKX.Okex.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    public partial class WinHistory : Form
    {
        public WinHistory()
        {
            InitializeComponent();
        }

        private void WinHistory_Load(object sender, EventArgs e)
        {
            this.dtpStart.Value = DateTime.Now.AddMonths(-1);
            this.dtpEnd.Value = DateTime.Now;
            this.cbSide.SelectedIndex = 0;
            this.cbQuerySide.SelectedIndex = 0;
            this.cbQueryStatus.SelectedIndex = 0;


            UpdateAllCurrencies();
            UpdateHistoryDate();
        }

        private int dataSize = 0;

        private async void UpdateHistory()
        {
            var result = await OTCHistoryManager.Instance.LoadHistory(this.textBox1.Text);

            if(result.Error)
            {
                MessageBox.Show(this,result.ErrorMessage);
            }
            else
            {
                MessageBox.Show("历史数据更新完成");
                this.UpdateAllCurrencies();
            }

            this.UpdateHistoryDate();
            this.timer1.Enabled = false;
            this.btnUpdateHistory.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.btnUpdateHistory.Enabled = false;
            this.timer1.Enabled = true;
            this.UpdateHistory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bool useDay = rdoUseDay.Checked;
            var startDate = this.dtpStart.Value;
            var endDate = this.dtpEnd.Value;

            const decimal totalDivide10K = 10000;
            const decimal totalDivideK = 1000;
            decimal total = 0;
            bool use10K = rdoUnit10K.Checked;

            decimal unit = use10K ? totalDivide10K : totalDivideK;

            

            if (startDate>endDate)
            {
                MessageBox.Show("日期无效，统计起始日期不能大于结束日期");
                return;
            }

            this.chart1.Series.Clear();

            List<string> dates = new List<string>();
            List<decimal> amounts = new List<decimal>();

            string side = this.cbSide.SelectedIndex == 0 ? Side.Sell : Side.Buy;
            List<string> currencies = new List<string>();

            foreach(var c in this.flpCheckbox.Controls)
            {
                CheckBox cb = c as CheckBox;

                if(cb!= null && cb.Checked)
                {
                    currencies.Add(cb.Text.Trim());
                }
            }

            if(useDay)
            {
                int days = (endDate - startDate).Days;
                var day = startDate;
                useDay = true;
                while(day <= endDate)
                {
                    dates.Add(day.ToString("yyyy-MM-dd"));
                    day = day.AddDays(1);
                }
            }
            else
            {
                var m = startDate;

                while(m <= endDate)
                {
                    dates.Add(m.ToString("yyyy-MM"));
                    m = m.AddMonths(1);
                }
            }

            DataTable table = null;

            if (useDay)
            {
                table = OTCHistoryManager.Instance.GetStatByDay(side, unit);
            }
            else
            {
                table = OTCHistoryManager.Instance.GetStatByMonth(side, unit);
            }

            foreach(DataRow dr in table.Rows)
            {
                Console.WriteLine(dr["time"]);
            }

            this.FillTotal(dates, amounts, table);

            foreach(decimal d in amounts)
            {
                total += d;
            }

            this.lblTotal.Text = string.Format("总计: {0:0.00}{1}",total, use10K ? "万" : "K");


            var serial =  this.chart1.Series.Add( string.Format( "总额(单位:{0})",use10K ? "万":"K"));
            serial.Points.DataBindXY(dates, amounts);
            serial.IsValueShownAsLabel = true;
            serial.LabelFormat = "0.00";

            foreach (string c in currencies)
            {
                if(useDay)
                {
                    table = OTCHistoryManager.Instance.GetStatByDayWithCurrency(side, c, unit);
                }
                else
                {
                    table = OTCHistoryManager.Instance.GetStatByMonthWithCurrency(side, c, unit);
                }

                if (table.Rows.Count > 0)
                {
                    this.FillTotal(dates, amounts, table);
                    serial = this.chart1.Series.Add(c);
                    //serial.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    serial.Points.DataBindXY(dates, amounts);
                }
            }

            this.dataSize = dates.Count;

            this.ResizeChart();
            var p = this.pnlChart.AutoScrollPosition;
            p.X = 10000;

            this.pnlChart.AutoScrollPosition = p;

            this.pnlChart.Height = this.chart1.Height + 50;
        }


        private void ResizeChart()
        {

            this.chart1.Width = Math.Max(dataSize * 40, pnlChart.Width-20);
        }

        private void UpdateHistoryDate()
        {
            var lastDate = OTCHistoryManager.Instance.MaxHistoryTime;
            this.lblLoading.Text = lastDate.ToString("最新数据yyyy年MM月dd日 HH:mm:ss");
        }

        private void UpdateAllCurrencies()
        {
            List<string> currencies = OTCHistoryManager.Instance.GetAllCurrencyTypes(string.Empty);

            this.flpCheckbox.Controls.Clear();
            this.cbCurrency.Items.Clear();
            this.cbCurrency.Items.Add("全部");
            foreach(string s in currencies)
            {
                CheckBox cb = new CheckBox();
                cb.Text = s + " ";
                this.flpCheckbox.Controls.Add(cb);
                this.cbCurrency.Items.Add(s);
            }

            this.cbCurrency.SelectedIndex = 0;
        }

        private void FillTotal(IList<string> dates, IList<decimal> totals, DataTable datas)
        {
            int count = dates.Count;
            totals.Clear();

            for(int i =0;i<count;i++)
            {
                totals.Add(0);
            }

            foreach (DataRow dr in datas.Rows)
            {
                string strTime = dr["time"].ToString();
                int index = -1;
                bool isFind = false;
                foreach (string s in dates)
                {
                    index++;
                    if (string.Compare(s, strTime) == 0)
                    {
                        isFind = true;
                        break;
                    }
                }

                if (isFind)
                {
                    decimal amount = Convert.ToDecimal(dr["total"]);
                    totals[index] = amount;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblLoading.Text = string.Format("正在下载第{0}页数据", OTCHistoryManager.Instance.LoadingPage);
        }

        private void WinHistory_ResizeEnd(object sender, EventArgs e)
        {
            this.ResizeChart();
        }

        private void WinHistory_MaximumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void WinHistory_SizeChanged(object sender, EventArgs e)
        {
            this.ResizeChart();
        }

        int pageIndex = 0;
        long pageCount = 0;

        int pageSize = 50;

        private DataTable DoQuery(bool resetPageIndex)
        {
            if (resetPageIndex)
                this.pageIndex = 0;

            string side = "";
            string status = "";
            switch (cbQuerySide.SelectedIndex)
            {
                case 1:
                    side = Side.Sell;
                    break;
                case 2:
                    side = Side.Buy;
                    break;
            }

            switch (cbQueryStatus.SelectedIndex)
            {
                case 1:
                    status = OrderStatus.Completed;
                    break;
                case 2:
                    status = OrderStatus.Canceled;
                    break;
            }



            long count = 0;

            List<string> currencies = null;

            if(this.cbCurrency.SelectedIndex != 0)
            {
                currencies = new List<string>();
                currencies.Add(this.cbCurrency.Text);
            }

            DateTime startTime = dtQueryStart.Value.Date;
            DateTime endTime = dtQueryEnd.Value.Date.AddDays(1);

            if (endTime <= startTime)
            {
                MessageBox.Show("查询订单开始日期不能大于结束日期");
                return null;
            }

            //startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day);
            //endTime = (new DateTime(endTime.Year, endTime.Month, endTime.Day)).AddDays(1);



            DataTable table = OTCHistoryManager.Instance.QueryOrders(this.txtQueryName.Text, side, startTime, endTime, currencies, status, pageIndex, pageSize, out count);


            table.Columns["publicOrderId"].ColumnName = "订单ID";
            table.Columns["symbol"].ColumnName = "币种";
            table.Columns["type"].ColumnName = "类型";
            table.Columns["amount"].ColumnName = "数量";
            table.Columns["orderStatus"].ColumnName = "状态";
            table.Columns["receiptAccountType"].ColumnName = "付款方式";
            table.Columns["exchangeRate"].ColumnName = "单价";
            table.Columns["counterPartyName"].ColumnName = "姓名";
            table.Columns["orderTotal"].ColumnName = "金额";
            table.Columns["createdDate"].ColumnName = "下单时间";

            pageIndex = Math.Min(pageIndex, (int)(count / pageSize) + ((count > 0 && count % pageSize > 0) ? 1 : 0));
            pageIndex = Math.Max(pageIndex, 0);

            this.lblRecordCount.Text = string.Format("总记录: {0}",count);

            return table;
        }


        private void BindToGridView(DataTable table)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; ;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.DataSource = table;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindToGridView( this.DoQuery(true));
        }

        private void btnQueryFirstPage_Click(object sender, EventArgs e)
        {
            this.pageIndex = 0;
            BindToGridView(this.DoQuery(false));
        }

        private void btnQueryPrePage_Click(object sender, EventArgs e)
        {
            this.pageIndex -= 1;
            BindToGridView(this.DoQuery(false));
        }

        private void btnQueryNextPage_Click(object sender, EventArgs e)
        {
            this.pageIndex += 1;
            BindToGridView(this.DoQuery(false));
        }

        private void btnQueryLastPage_Click(object sender, EventArgs e)
        {
            this.pageIndex = 1000000;//todo
            BindToGridView(this.DoQuery(false));
        }

        private void llQueryExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            var ps = this.pageSize;
            var pi = this.pageIndex;
            this.pageSize = int.MaxValue;

            DataTable table = this.DoQuery(true);

            if (table != null && table.Rows.Count > 0)
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = this.saveFileDialog1.FileName;

                    StringBuilder sb = new StringBuilder("<html><head><style>td,th{padding:2px 4px;border:solid 1px #ccc;}table { border-collapse:collapse; }</style></head><body><table>");
                    sb.Append("<thead><tr>");
                    foreach (DataColumn dc in table.Columns)
                    {
                        sb.Append("<th>");
                        sb.Append(dc.ColumnName);
                        sb.Append("</th>");
                    }
                    
                    sb.Append("</tr></thead>");
                    sb.Append("<tbody>");
                    foreach (DataRow dr in table.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn dc in table.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(dr[dc.ColumnName]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");

                    sb.Append("</table></body></html>");
                    File.WriteAllText(fileName, sb.ToString());
                }
            }
            else
            {
                MessageBox.Show("没有任何记录");
            }
            
            this.pageIndex = pi;
            this.pageSize = ps;
        }


    }
}
