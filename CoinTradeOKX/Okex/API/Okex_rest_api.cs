using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoinTradeOKX.Okex
{

    public static class Okex_REST_API
    {
        public static readonly string UrlRoot = "https://www.okx.com";

            #if !OKEX_API_V5
        public static readonly string Account = UrlRoot + "/api/account/v3/wallet/";//钱包
        public static readonly string Instruments = UrlRoot + "/api/spot/v3/instruments"; //获取可交易币对的列表信息
        public static readonly string Transfer = UrlRoot + "/api/account/v3/transfer"; //划转
        public static readonly string OTCMarket = UrlRoot+"/v3/c2c/tradingOrders/book?side=all&baseCurrency={0}&quoteCurrency=cny&userType=all&paymentMethod=all";
        //{"amount":0.0001,"currency":"eos","from":6,"to":5,"instrument_id":"eos-usdt"}

        public static readonly string CTCTicketAll = UrlRoot + "/api/spot/v3/instruments/ticker";
        public static readonly string CTCTicker = UrlRoot + "/api/spot/v3/instruments/{0}-{1}/ticker";//币对的ticker信息

        public static readonly string CTCAmount = UrlRoot + "/api/spot/v3/accounts";//获取币币账号余额

        public static readonly string TimeService = UrlRoot + "/api/general/v3/time";
        public static readonly string Orders = UrlRoot + "/api/spot/v3/orders_pending?limit=2&instrument_id={0}-{1}";//查询订单（挂单）
        public static readonly string Candle = UrlRoot + "/api/spot/v3/instruments/{0}-{1}/candles?granularity={2}"; //&start={3}&end={4}";
        public static readonly string History = UrlRoot + "/api/spot/v3/orders?instrument_id={0}-{1}&limit={2}&state={3}&after={4}"; //查询历史订单 &after=2500723297223680";//查询订单（挂单）
        ///api/spot/v3/orders

        /// <summary>
        /// 提币api
        /// </summary>
        public static readonly string ACTWithdrawal = UrlRoot + "/api/account/v3/withdrawal";//提币 post{"amount":1,"fee":0.0005,"trade_pwd":"123456","destination":4,"currency":"btc","to_address":"17DKe3kkkkiiiiTvAKKi2vMPbm1Bz3CMKw"}
        public static readonly string CTCOrder = UrlRoot + "/api/spot/v3/orders";//创建订单
        public static readonly string CTCCancelOrder = UrlRoot + "/api/spot/v3/cancel_orders/{0}"; //{0} order id
        public static readonly string CTCDepth = "/api/spot/v3/instruments/{0}-{1}/book?size={2}&depth={3}"; //币对的深度信息 

        //{'type': 'limit', 'side': 'buy', 'instrument_id': 'BTC-USDT', 'size': 0.001, 'client_oid': 'oktspot79', 'price': '4638.51', 'funds': '', 'margin_trading': 1, 'order_type': '3'}
#endif
    }

    public class OkexRestApiBase
    {
        protected int CacheTime { get; set; } //缓存时间， 有些API接口可能数据通用，但是访问限速低
        JToken cachedResult = null;
        long lastRequest = 0;
        string lastAddress = "";

        //public bool IsPublic { get; set; }

        private ApiKey apiInfo = null;


        public void SetApi(ApiKey apiInfo)
        {
            this.apiInfo = apiInfo;
        }

        protected bool NeedHead { get; set; }
        public static Dictionary<string, string> GenerateHeader(ApiKey apiInfo, string url, string method, string body)
        {
            url = url.Replace( Okex_REST_API.UrlRoot, "");
            Pool<Dictionary<string,string>> pool = Pool<Dictionary<string,string>>.GetPool();
            Dictionary<string, string> result = pool.Get();

            result.Clear();

            string timestamp = DateUtil.GetServerTimeISO8601();
            result["OK-ACCESS-KEY"] = apiInfo.GetKey();
            result["OK-ACCESS-SIGN"] = Crypt.GenareteSign(apiInfo.SecretKey, timestamp, method, url, body != null ? body : string.Empty);
            result["OK-ACCESS-PASSPHRASE"] = apiInfo.GetPassphrase();  
            result["OK-ACCESS-TIMESTAMP"] = timestamp.ToString();

            if (apiInfo.IsSimulated) //加入模拟盘参数
                result["x-simulated-trading"] = "1";

            return result;
        }


        private PropertyInfo[] queryProperties = null;

        public JToken execSync()
        {
            Type t = this.GetType();

            string postData = null;

            var address = this.Address;

            if (Method == Http.Method_Post)
            {
                var s = JsonSerializer.Create();

                using (StringWriter sw = new StringWriter())
                {
                    s.Serialize(sw, this);

                    sw.Flush();

                    var sb = sw.GetStringBuilder();
                    var args = sb.ToString();
                    sw.Close();

                    postData = args;
                }
            }
            else
            {
                if (this.AutoBuildAddress)
                {
                    if (queryProperties == null)
                    {
                        PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                        this.queryProperties = properties;
                    }

                    bool hasQuery = address.Contains("?");

                    foreach (PropertyInfo p in this.queryProperties)
                    {
   
                        object val = p.GetValue(this);

                        if (val != null && !string.IsNullOrEmpty(val.ToString()))
                        {
                            address += string.Format("{0}{1}={2}", (hasQuery ? "&" : "?"), HttpUtility.UrlEncode(p.Name), HttpUtility.UrlEncode(p.GetValue(this).ToString()));
                            hasQuery = true;
                        }
                    }
                }
            }

            long now = DateUtil.GetTimestampMS();

            if (this.CacheTime > 0 && this.cachedResult != null)
            {
                if(now - lastRequest < this.CacheTime)
                {
                    if(string.Compare(this.lastAddress, address) == 0)
                    {
                        return this.cachedResult;
                    }
                }
            }

            this.cachedResult = null;
            Http.RequestEncoding = Encoding.ASCII; //这里要用ascii编码， 否则会出现签名错误的问题；
            ApiKey api = this.apiInfo != null ? this.apiInfo : Config.Instance.ApiInfo;
            Dictionary<string, string> head = this.NeedHead ? GenerateHeader(api, address, Method, postData) : null;
            JObject response = Http.HttpSend(address, this.Method, postData, head);

            if(head != null)
            {
                Pool<Dictionary<string, string>> pool = Pool<Dictionary<string, string>>.GetPool();
                head.Clear();
                pool.Put(head);//  new Dictionary<string, string>();
            }

            if (this.CacheTime > 0)
            {
                now = DateUtil.GetTimestampMS();
                this.lastAddress = address;
                this.cachedResult = response;
                this.lastRequest = now;
            }

            return response;
        }

        public Task<JToken> exec()
        {
            return Task.Run(() => { return this.execSync(); });
        }

        public async void execAsync()
        {


            var result = await this.exec();
            this.OnReceiveData(result);
        }

        protected string Method     = Http.Method_Post;
        protected string Address { get; set; }
        protected bool AutoBuildAddress = true;

        public event Action<JToken> OnData;

        public OkexRestApiBase()
        {

        }

        public OkexRestApiBase(string address)
            : this(address, Http.Method_Get)
        {

        }

        public OkexRestApiBase(string address, string method)
        {
            this.NeedHead = true;
            this.Address = address;
            this.Method = method;
        }

        protected virtual void OnReceiveData(JToken data)
        {
            this.OnData?.Invoke(data);
        }
    }
}
