using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using CoinTradeOKX.Monitor;


namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "结算通道监视")]
     public class ReceiptAccountMonitor: JSMonitorBase
    {
        private object list_lock = new object();
        List<ReceiptAccountItem> accounts = new List<ReceiptAccountItem>();
        public ReceiptAccountMonitor()
            :base(new okex_receipetAccount_list(), 5 * 60 * 1000)
        {

        }

        /// <summary>
        /// 遍历账户
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="type"></param>
        public void EachAccount(Action<ReceiptAccountItem> callback, AccountApplyType type = AccountApplyType.All)
        {
            lock (this.list_lock)
            {
                foreach (var a in this.accounts)
                {
                    if (a.ApplyType == AccountApplyType.All || type == AccountApplyType.All || a.ApplyType == type)
                    {
                        callback(a);
                    }
                }
            }
        }

        protected override void OnDataUpdate(JToken data)
        {
            lock (list_lock)
            {
                var pool = Pool<ReceiptAccountItem>.GetPool();
                pool.Put(this.accounts);
                this.accounts.Clear();

                JArray arr = data as JArray;

                foreach (var a in arr)
                {
                    var account = pool.Get();
                    account.ParseFromJson(a);

                    this.accounts.Add(account);
                }
            }

            this.Feed();
        }
    }
}
