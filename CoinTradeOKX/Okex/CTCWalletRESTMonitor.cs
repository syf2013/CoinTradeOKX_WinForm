using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    public class RestWalletApi : OkexRestApiBase
    {
        private RestWalletApi() : base(Okex_REST_API_V5.CTCAmount, Http.Method_Get)
        {
            //this.CacheTime = 300;
        }

        private static RestWalletApi _instance = null;
        public static RestWalletApi Instance
        {
            get
            {
                if (_instance == null)
                {
                    var s = new RestWalletApi();
                    _instance = s;
                }

                return _instance;
            }
        }
    }

    [MonitorName(Name ="币币账户")]
    public class CTCWalletRESTMonitor :RESTMonitor, IWalletMonitor
    {
        public long Id { get; private set; }
        public string Currency { get; private set; }

        public decimal Availible { get; private set; }
        public decimal Hold { get; private set; }

        private static long LastRequest = 0;

        const int FixedInterval = 500;

        private Random rnd = null;

        private static int instanceCount = 0;

        public CTCWalletRESTMonitor(string currency):base(RestWalletApi.Instance, FixedInterval)
        {
            this.Currency = currency.ToUpper();
            instanceCount++;
        }

        protected override void OnDataUpdate(JToken ret)
        {
            base.OnDataUpdate(ret);

#if OKEX_API_V5
            JArray data = ret as JArray;

            foreach (JToken d in data)
            {
                JArray details = d["details"] as JArray;
                bool isFind = false;
                foreach (JToken item in details)
                {
                    if (string.Compare(this.Currency, item.Value<string>("ccy"), true) == 0)
                    {
                        string strValue = string.Empty;
                        strValue = item.Value<string>("availBal"); //简单交易模式账户
                        if(string.IsNullOrEmpty(strValue))
                        {
                            strValue = item.Value<string>("availEq"); //保证金模式账户
                        }

                        if (string.IsNullOrEmpty(strValue))
                        {
                            strValue = "0";
                        }

                        this.Availible = string.IsNullOrEmpty(strValue) ? 0 : decimal.Parse( strValue);

                        strValue = item.Value<string>("frozenBal");
                        this.Hold = string.IsNullOrEmpty(strValue) ? 0 : decimal.Parse(strValue);  
                        isFind = true;
                        break;
                    }
                }

                if (!isFind)
                {
                    this.Availible = 0;
                    this.Hold = 0;
                }
            }

            if (rnd == null)
            {
                rnd = new Random();
            }

            this.Interval = (uint)(FixedInterval * (1 + 0.5 * rnd.NextDouble()));//随机触发，避免多实例同时访问造成的API频繁访问
            this.cd = (int)this.Interval;
#else
                JObject data = ret as JObject;
                this.Availible = data.Value<decimal>("available");// obj[""].Value<decimal>();
                this.Hold = data.Value<decimal>("hold");// () ["hold"].Value<decimal>();
#endif
            this.Feed();
        }

#if OKEX_API_V5
        protected override void RunInvoke()
        {
            long now = DateUtil.GetTimestampMS();

            if(now - LastRequest < FixedInterval)
            {
                return;
            }


            LastRequest = now;

            base.RunInvoke();
        }
#endif

        public override void Destory()
        {
           
            base.Destory();
        }
    }
}
