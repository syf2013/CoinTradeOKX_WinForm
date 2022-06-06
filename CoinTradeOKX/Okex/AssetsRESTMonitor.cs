using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "资产监视")]
    public class AssetsRESTMonitorBase : RESTMonitor
    {
        public decimal Availible { get; private set; }
        public decimal Hold { get; private set; }

        Action<string, decimal, decimal> balanceCallback = null;

        public Action OnBeforeUpdateBalance { get; set; }
        public Action OnAfterUpdateBalance { get; set; }

        public void SetBalanceCallback(Action<string, decimal, decimal> callback)
        {
            this.balanceCallback = callback;
        }

        public AssetsRESTMonitorBase(OkexRestApiBase api ) : base(api , 250)
        {
 
        }

        protected override void OnDataUpdate(JToken ret)
        {
            base.OnDataUpdate(ret);


            this.OnBeforeUpdateBalance?.Invoke();

            JArray data = ret as JArray;
            decimal avalible = 0;
            decimal frozen = 0;

            foreach (JToken d in data)
            {
                JArray details = d["details"] as JArray;
             
                foreach (JToken item in details)
                {
                    string currency = item.Value<string>("ccy");

                    string strValue = string.Empty;
                    strValue = item.Value<string>("availBal");

                    avalible = string.IsNullOrEmpty(strValue) ? 0 : item.Value<decimal>("availBal");

                    strValue = item.Value<string>("frozenBal");
                    frozen = string.IsNullOrEmpty(strValue) ? 0 : item.Value<decimal>("frozenBal");

                    this.balanceCallback?.Invoke(currency, avalible, frozen);
                }
            }

            this.Feed();

            this.OnAfterUpdateBalance?.Invoke();
        }

        public override void Destory()
        {
            this.balanceCallback = null;
            this.OnAfterUpdateBalance = null;
            this.OnBeforeUpdateBalance = null;
            base.Destory();
        }
    }

    [MonitorName(Name ="交易账户监视")]
    public class SpotMarketRESTMonitor:AssetsRESTMonitorBase
    {
        public SpotMarketRESTMonitor():base(RestWalletApi.Instance)
        {

        }
    }

    [MonitorName(Name = "账户资产监视")]
    public class AccountRESTMonitor : AssetsRESTMonitorBase
    {
        public AccountRESTMonitor() : base(Okex_Rest_Api_Balance.Instance)
        {

        }
    }
}
