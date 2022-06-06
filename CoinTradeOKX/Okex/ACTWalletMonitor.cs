using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{

    /**
    * 资金账户usdx监视器
    */
    [MonitorName(Name = "资金账户")]
    public class ACTWalletMonitor : MonitorBase, IWalletMonitor
    {

#if OKEX_API_V5
    private Okex_Rest_Api_Balance api = null;
#else
    private Okex_Rest_Api_Account api = null;
#endif

        public string Currency { get; private set; }

        public decimal Availible { get; private set; }

        public decimal Hold { get; private set; }

        public ACTWalletMonitor( string currency, uint interval = 500)
        {
#if !OKEX_API_V5
            this.api = new Okex_Rest_Api_Account(currency);
#else
            this.api = Okex_Rest_Api_Balance.Instance;

#endif
            this.Interval = interval;
            api.OnData += Api_OnData;
            this.Currency = currency.ToUpper();
        }

        private void Api_OnData(JToken obj)
        {
            if(obj == null)
            {
                return;
            }
#if OKEX_API_V5
            if (obj.Value<int>("code") == 0)
            {
                JArray arr = obj["data"] as JArray;
                bool isFind = false;
                foreach (JToken d in arr)
                {
                    if (string.Compare(this.Currency, d.Value<string>("ccy"), true) == 0)
                    {
                        string strValue = string.Empty;
                        strValue = d.Value<string>("availBal");

                        this.Availible = string.IsNullOrEmpty(strValue) ? 0 : d.Value<decimal>("availBal");

                        strValue = d.Value<string>("frozenBal");
                        this.Hold = string.IsNullOrEmpty(strValue) ? 0 : d.Value<decimal>("frozenBal");

                        isFind = true;
                        break;
                    }
                }

                if (!isFind)
                {
                    this.Availible = 0;
                    this.Hold = 0;
                }

                this.Feed();
            }
#else
            if (obj["code"].Value<int>() == 0)
            {
                this.Availible = api.Availible;
                this.Hold = api.Hold;
                this.Feed();
            }
            else
            {

            }
#endif
        }

        private void invokeApi()
        {
            this.api.execAsync();
        }

        protected override void RunInvoke()
        {

            if(Config.Instance.ApiInfo.IsSimulated) //模拟盘是无法获取资金账户
            {

                this.Hold = 0;
                this.Availible = 0;
                this.Feed();
            }
            else
            {
                this.invokeApi();
            }
            
        }

        public override void Destory()
        {
            this.api.OnData -= this.Api_OnData;
            base.Destory();
        }
    }
}
