using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using CoinTradeOKX.Util;
using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using CoinTradeOKX.Database;
using System.Data;
using CoinTradeOKX.Okex.Const;
using System.Threading;
using Common.Util;

namespace CoinTradeOKX.Manager
{
    public class HistoryLoadResult
    {
        public bool Error = false;
        public int RowCount = 0;
        public string ErrorMessage = "";
    }

    public class OTCHistoryManager
    {
        private SqlDao dao = null;
        const int PageSize = 50;

        public int LoadingPage
        {
            get;
            private set;
        }

        public bool IsLoading
        {
            get;
            private set;
        }

        private string tableName = "";

        /// <summary>
        /// 
        /// </summary>
        public DateTime MaxHistoryTime
        {
            get;
            private set;
        }

        private static readonly string CreateHistoryTableScript = @"CREATE TABLE [{0}] (
    publicOrderId BIGINT   PRIMARY KEY,
    symbol             STRING NOT NULL,
    type STRING   NOT NULL,
    amount             DECIMAL NOT NULL,
    orderStatus STRING,
    orderTotal         DECIMAL NOT NULL,
    receiptAccountType STRING,
    exchangeRate       DECIMAL NOT NULL,
    counterPartyName STRING   NOT NULL,
    createdDate        DATETIME NOT NULL
);
";

        private OTCHistoryManager()
        {
            this.SetAccount(AccountManager.Current.UId.ToString());
        }

        public void SetAccount(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                tableName = "Account_Default";// "DefaultUser";
            }
            else
            {
                tableName = "Account_" + loginName;
            }

            dao = SqlDao.Instance;
            dao.DatabasePath = this.GetDatabasePath();

            if (!dao.TableIsExist( tableName))
            {
                dao.ExecuteNonQuery(string.Format(CreateHistoryTableScript, tableName));
            }

            this.UpdateMaxHistoryTime();
        }

        /// <summary>
        /// 获取所有有交易记录的数字货币类型
        /// </summary>
        /// <param name="side">买入或卖出</param>
        /// <returns></returns>
        public List<string> GetAllCurrencyTypes(string side)
        {
            string symbol = "symbol";
            string sql = "select distinct "+ symbol +" from " + this.tableName + " where 1 = 1 "; 

            if(side == Side.Buy || side == Side.Sell)
            {
                sql += "and type = '"+ side +"'";
            }
            

            DataTable dt = dao.ExecuteDatatable(sql);
            List<string> currencies = new List<string>();
            foreach(DataRow r in dt.Rows)
            {
                currencies.Add( r[symbol].ToString());
            }

            return currencies;
        }

        public DataTable GetStatByMonth(string side, decimal totalDivide)
        {
            string sql =  GenerateSql(false, side,totalDivide);

            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
     
        }

        public DataTable GetStatByMonthWithCurrency(string side, string currency, decimal totalDivide)
        {
            string sql = this.GenerateSqlWithSymbol(currency, false, side,totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }

        public DataTable QueryOrders(string name, string side,DateTime startDate,DateTime endData, IList<string> currencies, string status,int pageIndex,int pageSize, out long count)
        {
            StringBuilder conditions = new StringBuilder( " where 1 = 1 ");

            if(!string.IsNullOrEmpty(name))
            {
                conditions.AppendFormat(" and counterPartyName like '{0}%'", name.Trim());
            }

            if(!string.IsNullOrEmpty(side))
            {
                conditions.AppendFormat(" and type = '{0}'", side.Trim());
            }

            if (!string.IsNullOrEmpty(status))
            {
                conditions.AppendFormat(" and orderStatus = '{0}'", status.Trim());
            }

            if(currencies != null && currencies.Count > 0)
            {
                conditions.AppendFormat( " and symbol in('{0}')", string.Join("','", currencies));
            }

            conditions.AppendFormat(" and createdDate >='{0:yyyy-MM-dd HH:mm:ss}' and createdDate <'{1:yyyy-MM-dd HH:mm:ss}'", startDate, endData);
            string sqlForCount = string.Format("select count(*) from {0} {1}", tableName, conditions);
            count = dao.ExecuteScalar<long>(sqlForCount);
            pageIndex = Math.Min(pageIndex, (int)(count / pageSize) + ((count >0 && count % pageSize > 0) ? 1 : 0));
            pageIndex = Math.Max(pageIndex, 0);
            string sql = string.Format("select * from {0} {1} order by createdDate desc limit {2},{3} ", tableName, conditions,pageIndex * pageSize,pageSize);
          

            return dao.ExecuteDatatable(sql);
        }

        public DataTable GetStatByDay(string side,decimal totalDivide)
        {
            string sql = GenerateSql(true, side,totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }

        public DataTable GetStatByDayWithCurrency(string side, string currency, decimal totalDivide)
        {
            string sql = this.GenerateSqlWithSymbol( currency, true, side,totalDivide);
            DataTable dt = dao.ExecuteDatatable(sql);

            return dt;
        }


        private string GenerateSql(bool isDay,string side, decimal totalDivide)
        {
            string sql = string.Format("select type,orderStatus,strftime('{0}', createdDate) as 'time',  sum(orderTotal) /{1} as total from {2} group by type,orderStatus, time having type = '{3}' and orderStatus='{4}';"
                , isDay ? "%Y-%m-%d" : "%Y-%m"
                , totalDivide
                , tableName
                , side
                , OrderStatus.Completed
                );

            return sql;
        }

        private string GenerateSqlWithSymbol( string currency, bool isDay, string side,decimal totalDivide)
        {
            string sql = string.Format("select type,orderStatus,symbol,strftime('{0}', createdDate) as 'time',sum(amount) as amounts, sum(orderTotal)/{1} as total from {2} group by type,orderStatus,symbol, time having type = '{3}' and orderStatus='{4}' and symbol = '{5}';"
            , isDay ? "%Y-%m-%d" : "%Y-%m"
            , totalDivide
            , tableName
            , side
            , OrderStatus.Completed
            , currency
            );

            return sql;
        }

        public void UpdateMaxHistoryTime()
        {
            string strDate = dao.ExecuteScalar<string>("select max(createdDate) as mt from  " + tableName);
            MaxHistoryTime = Convert.ToDateTime(strDate);
        }

        private string GetDatabasePath()
        {
            return Path.Combine(Application.StartupPath, "history.db");
        }

        public Task<HistoryLoadResult> LoadHistory(string testdata = null)
        {
            this.IsLoading = true;
            return Task.Run<HistoryLoadResult>(() =>
            {
                HistoryLoadResult ret   = new HistoryLoadResult();
                int pageIndex           = 1;
                string currency         = "0";
                DateTime currentMaxOrderTime = DateTime.MinValue;
   
                StringBuilder sb = new StringBuilder();

                while (true)
                {
                    JToken result = null;
                    if (string.IsNullOrEmpty(testdata))
                    {
                        this.LoadingPage = pageIndex;
                        okex_api_history api = new okex_api_history(currency, PageSize, pageIndex, HistoryOrderStateEnum.All, HistoryOrderStateEnum.All);
                        result = api.execSync();
                    }
                    else
                    {
                        result = JObject.Parse(testdata);
                    }

                    if (result["code"].Value<int>() == 0)
                    {
                        JToken data = result["data"];
                        JToken orderPageVO = data["orderPageVoList"];
                        int pageCount = orderPageVO["pageCount"].Value<int>();


                        JToken pageInfo = orderPageVO["pageInfo"];
                        int itemCount = pageInfo["totalItemCount"].Value<int>();//总条目数
                        JArray items = orderPageVO["items"] as JArray;

                        List<HistoryOrder> pageDatas = new List<HistoryOrder>();

                        bool isOldData = false;

                        foreach (JObject jo in items)
                        {
                            DateTime createdDate = DateTime.Parse(jo["createdDate"].Value<string>());

                            currentMaxOrderTime = currentMaxOrderTime > createdDate ? currentMaxOrderTime : createdDate;

                            if (createdDate <= MaxHistoryTime)
                            {
                                isOldData = true;
                                break;
                            }


                            string sql = string.Format(@"insert into {0} (publicOrderId,symbol, type,amount,orderStatus,orderTotal,receiptAccountType,exchangeRate,counterPartyName,createdDate) values(
                            {1},""{2}"",""{3}"",{4},""{5}"",{6},""{7}"",{8},""{9}"",""{10}"");"
                            , tableName
                            , jo["publicOrderId"].Value<string>()
                            , jo["symbol"].Value<string>()
                            , jo["type"].Value<string>()
                            , jo["amount"].Value<string>()
                            , jo["orderStatus"].Value<string>()
                            , jo["orderTotal"].Value<string>().Replace("CNY", "")
                            , jo["receiptAccountType"].Value<string>()
                            , jo["exchangeRate"].Value<string>().Replace("CNY", "")
                            , jo["counterPartyName"].Value<string>()
                            , jo["createdDate"].Value<string>()
                            );

                            sb.Append(sql);
                        }


                        if (isOldData || pageCount <= pageIndex ||  !string.IsNullOrEmpty( testdata))
                        {
                            break;
                        }


                        Thread.Sleep(RandomUtil.GetRandom(1000,2000));
                        pageIndex++;
                    }
                    else
                    {
                        ret.Error = true;
                        ret.ErrorMessage = result["msg"].Value<string>();
                        break;
                    }
                }

                if (!ret.Error)
                {
                    ret.RowCount = dao.ExecuteNonQuery(sb.ToString(),true);
                    this.UpdateMaxHistoryTime();
                }

                this.IsLoading = false;

                return ret;
            });
        }

        /// <summary>
        /// 获取指定时间之前的最后历史价格
        /// </summary>
        /// <param name="currency">币种</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public decimal GetPriceWithTime( string currency, DateTime time)
        {
            string sql = string.Format(@"select exchangeRate from {0} where symbol = ""{1}"" and type=""buy"" and createdDate < ""{2}"" order by publicOrderId desc limit 1 ",tableName, currency, time.ToString("yyyy-MM-dd HH:mm:ss"));

            return dao.ExecuteScalar<decimal>(sql);
        }

        private static OTCHistoryManager _instance = null;
        public static OTCHistoryManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OTCHistoryManager();

                return _instance;
            }
        }
    }
}
