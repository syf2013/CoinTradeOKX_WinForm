using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "订单监视器")]
    public class OTCContractMonitor : JSMonitorBase
    {
        List<OTCContract> _contractList = new List<OTCContract>();

        private object locker = new object();

        public string Currency { get; private set; }

        public OTCContractMonitor(string currency)
            : this(currency, 1000)
        {
           
        }

        public OTCContractMonitor(string currency,uint interval)
     : base(new okex_contract_list(), interval)
        {
            this.Currency = currency.ToUpper();
        }

        protected override void OnDataUpdate(JToken data)
        {
            lock(locker)
            {
                var items = data["items"] as JArray;

                if (items == null)
                {
                    return;
                }

                var pool = Pool<OTCContract>.GetPool();

                pool.Put(_contractList);
                _contractList.Clear();

                foreach(var item in items)
                {
                    if (!string.IsNullOrEmpty(this.Currency) && string.Compare(this.Currency, item["baseCurrency"].Value<string>(), true) != 0)
                        break;

                    var contract = pool.Get();
                    contract.ParseFromJson(item);
                    _contractList.Add(contract);
                }

                this.Feed();
            }
        }

        public void RemoveContract(long publicId)
        {
            lock(this.locker)
            {
                OTCContract contract = null;
                foreach(var c in this._contractList)
                {
                    if(c.PublicOrderId == publicId)
                    {
                        contract = c;
                        break;
                    }
                }

                if(contract != null)
                {
                    this._contractList.Remove(contract);
                }
            }
        }

        public void EachContract(Action<OTCContract> callback)
        {
            lock(locker)
            {
                foreach(var c in _contractList)
                {
                    callback(c);
                }
            }
        }
    }
}
