using Common.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    public static class Okex_REST_API_V5
    {
        public static readonly string UrlRoot = "https://www.okx.com";
        public static readonly string Account = UrlRoot + "/api/v5/account/config";

        public static readonly string Balance = UrlRoot + "/api/v5/asset/balances";////?ccy={0}"; //UrlRoot + "/api/account/v3/wallet/";//钱包
        public static readonly string Instruments = UrlRoot + "/api/v5/public/instruments?instType=SPOT"; //获取可交易币对的列表信息 instType=SPOT代表币币
        public static readonly string OTCMarket = UrlRoot + "/v3/c2c/tradingOrders/book?side=all&baseCurrency={0}&quoteCurrency=cny&userType=all&paymentMethod=all";
        //{"amount":0.0001,"currency":"eos","from":6,"to":5,"instrument_id":"eos-usdt"}

        public static readonly string CTCTicketAll = UrlRoot + "/api/spot/v3/instruments/ticker";
        public static readonly string CTCTicker = UrlRoot + "/api/spot/v3/instruments/{0}-{1}/ticker";//币对的ticker信息
       // public static readonly string CTCAmount = UrlRoot + "/api/v5/account/balance?ccy={0}";//获取账号余额  
        public static readonly string CTCAmount = UrlRoot + "/api/v5/account/balance";//获取账号余额  
        public static readonly string TimeService = UrlRoot + "/api/v5/public/time";
        public static readonly string Candle = UrlRoot + "/api/v5/market/candles?instId={0}-{1}&bar={2}&limit={3}";
        public static readonly string Transfer = UrlRoot + "/api/v5/asset/transfer"; //划转

        public static readonly string Orders = UrlRoot + "/api/v5/trade/orders-pending";

        // /api/v5/asset/transfer

        ///api/spot/v3/orders

        /// <summary>
        /// 提币api
        /// </summary>
        public static readonly string ACTWithdrawal = UrlRoot + "/api/account/v3/withdrawal";//提币 post{"amount":1,"fee":0.0005,"trade_pwd":"123456","destination":4,"currency":"btc","to_address":"17DKe3kkkkiiiiTvAKKi2vMPbm1Bz3CMKw"}
        public static readonly string CTCOrderCreate = UrlRoot + "/api/v5/trade/order";//创建订单
        public static readonly string CTCOrderModify = UrlRoot + "/api/v5/trade/amend-order"; //修改挂掉
        public static readonly string CTCOrderQuery = CTCOrderCreate;

        public static readonly string History = UrlRoot + "/api/v5/trade/orders-history-archive";//历史订单
        public static readonly string CTCCancelOrder = UrlRoot + "/api/v5/trade/cancel-order"; //{0} order id
        public static readonly string CTCDepth = "/api/spot/v3/instruments/{0}-{1}/book?size={2}&depth={3}"; //币对的深度信息 

        //{'type': 'limit', 'side': 'buy', 'instrument_id': 'BTC-USDT', 'size': 0.001, 'client_oid': 'oktspot79', 'price': '4638.51', 'funds': '', 'margin_trading': 1, 'order_type': '3'}

    }

    /*
    public class OkexRestApiBaseV5
    {
        protected bool NeedHead { get; set; }
        public static Dictionary<string, string> GenerateHeader(string url, string method, string body)
        {
            url = url.Replace(Okex_REST_API_V5.UrlRoot, "");
            Dictionary<string, string> result = new Dictionary<string, string>();

            string timestamp = DateUtil.GetServerTimeISO8601();
            result["OK-ACCESS-KEY"] = Config.Instance.ApiInfo.GetKey();
            result["OK-ACCESS-SIGN"] = Crypt.GenareteSign(timestamp, method, url, body != null ? body : string.Empty);
            result["OK-ACCESS-PASSPHRASE"] = Config.Instance.ApiInfo.GetPassphrase();
            result["OK-ACCESS-TIMESTAMP"] = timestamp.ToString();

            return result;
        }


        public JToken execSync()
        {
            Type t = this.GetType();

            string postData = null;

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

            Http.RequestEncoding = Encoding.ASCII; //这里要用ascii编码， 否则会出现签名错误的问题；
            Dictionary<string, string> head = this.NeedHead ? GenerateHeader(Address, Method, postData) : null;
            JObject response = Http.HttpSend(this.Address, this.Method, postData, head);

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

        protected string Method = Http.Method_Post;
        protected string Address { get; set; }

        public event Action<JToken> OnData;

        public OkexRestApiBaseV5()
        {

        }

        public OkexRestApiBaseV5(string address)
            : this(address, Http.Method_Get)
        {

        }

        public OkexRestApiBaseV5(string address, string method)
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
*/

    /// <summary>
    /// 服务器时间
    /// </summary>
    public class TimeServiceApi:OkexRestApiBase
    {
        public TimeServiceApi()
            :base(Okex_REST_API_V5.TimeService)
        {
            this.NeedHead = false;
        }
    }


    public class Okex_Rest_Api_CTCAmountV5 : OkexRestApiBase
    {

        public long Id { get; private set; }
        public string Currency { get; private set; }

        public decimal Availible { get; private set; }
        public decimal Hold { get; private set; }

        public Okex_Rest_Api_CTCAmountV5(string currency) : base(Okex_REST_API_V5.CTCAmount)
        {
            this.AutoBuildAddress = false;
            if (!string.IsNullOrEmpty(currency))
            {
                Address = Path.Combine(this.Address, currency);
            }
        }

        protected override void OnReceiveData(JToken data)
        {
            if (data.Value<int>("code") == 0)
            {
                JArray arr = data["data"] as JArray;
                bool isFind = false;
                foreach (JToken d in arr)
                {
                    if (string.Compare(this.Currency, d.Value<string>("ccy"), true) == 0)
                    {
                        this.Availible = d.Value<decimal>("availBal");
                        this.Hold = d.Value<decimal>("frozenBal");
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
            base.OnReceiveData(data);


            //this.Id = data["id"].Value<long>();

            base.OnReceiveData(data);
        }
    }

    public class CTCCandleRestApiV5 : OkexRestApiBase
    {
        private string currency1;
        private string currency2;
        private string granularity;

        private DateTime start;
        private DateTime end;

        public DateTime Start { get { return start; } set { start = value; this.UpdateUrl(); } }
        public DateTime End { get { return end; } set { end = value; this.UpdateUrl(); } }

        public string Granularity { get { return this.granularity; } set { granularity = value; this.UpdateUrl(); } }


        public CTCCandleRestApiV5(string currency, string currency2, string granularity)
        {
            this.currency1 = currency.ToUpper();
            this.currency2 = currency2.ToUpper();
            this.granularity = granularity;

            this.AutoBuildAddress = false;

            this.Method = Http.Method_Get;
            this.Address = this.BuildUrl();
        }

        private void UpdateUrl()
        {
            this.Address = this.BuildUrl();
        }

        private string BuildUrl()
        {
            return string.Format(Okex_REST_API_V5.Candle, currency1, currency2, granularity, 150);
        }
    }

    /// <summary>
    /// 提币接口
    /// </summary>
    public class Okex_Rest_Api_WithdrawalV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_WithdrawalV5()
            : base(Okex_REST_API_V5.ACTWithdrawal, Http.Method_Post)
        {
            //{"amount":1,"fee":0.0005,"trade_pwd":"123456","destination":4,"currency":"btc","to_address":"17DKe3kkkkiiiiTvAKKi2vMPbm1Bz3CMKw"}
            //3:OKEx; 4:数字货币地址;
            this.fee = "0";

        }

        /// <summary>
        /// 币
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public string destination { get; set; }

        /// <summary>
        /// 目标地址/账号
        /// </summary>
        public string to_address { get; set; }

        /// <summary>
        /// 交易密码
        /// </summary>
        public string trade_pwd { get; set; }

        /// <summary>
        /// 手续费 0
        /// </summary>
        public string fee { get; set; }

    }

    /// <summary>
    /// 资金划转
    /// </summary>
    public class Okex_Rest_Api_TransferV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_TransferV5(string currency, WalletType from, WalletType to) : base(Okex_REST_API_V5.Transfer, Http.Method_Post)
        {
            this.ccy = currency.ToUpper();
            this.from = (int)from;
            this.to = (int)to;
            this.amt = amt;
        }

        public string ccy { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public decimal amt { get; set; }

        protected override void OnReceiveData(JToken data)
        {
            base.OnReceiveData(data);
        }
    }

    //获取可交易的币对信息列表
    public class Okex_Rest_Api_InstrumentsV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_InstrumentsV5() : base(Okex_REST_API_V5.Instruments)
        {
            this.NeedHead = false;
        }
    }

    //获取交易历史订单数据的
    public class Okex_Rest_Api_HistoryV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_HistoryV5(string instrument_id, CTCHistoryOrderState state, long  beforeOrderId = 0, int size = 20) :
            base(Okex_REST_API_V5.History)
        {
            this.instType = "SPOT";
            this.instId = instrument_id;
            //this.after = afterOrderId.ToString();
            this.before = beforeOrderId.ToString();
            this.limit = size.ToString();
            //this.state = "filled";// state.ToString();

        }

        public string after { get; set; }
        public string before { get; set; }
        public string instId { get; set; }
        public string instType { get; set; }
        public string state { get; set; }
        public string limit { get; set; }
    }

    public class Okex_Rest_Api_OrderQuery:OkexRestApiBase
    {
        public Okex_Rest_Api_OrderQuery(string instId, long orderId) : base(Okex_REST_API_V5.CTCOrderQuery, Http.Method_Get)
        {
            this.instId = instId;
            this.ordId = orderId.ToString();
        }

        public Okex_Rest_Api_OrderQuery(string currency1, string currency2, long orderId)
            :this(string.Format("{0}-{1}", currency1.ToUpper(), currency2),orderId)
        {

        }
        public string instId { get; set; }
        public string ordId { get; set; }
    }

    //未成交订单查询
    public class Okex_Rest_Api_OrdersV5 : OkexRestApiBase
    {

        public Okex_Rest_Api_OrdersV5() : base(Okex_REST_API_V5.Orders)
        {
            //this.instId = string.Format("{0}-{1}", currency1, currency2).ToUpper();
            //this.instType = "SPOT";
            //this.ordType = "limit";
        }

        //public Okex_Rest_Api_OrdersV5(string currency1, string currency2) : base(Okex_REST_API_V5.Orders)
        //{
            //this.instId = string.Format("{0}-{1}", currency1, currency2).ToUpper();
            //this.instType = "SPOT";
            //this.ordType = "limit";
        //}

        //public string ordType { get; set; }
        //public string instId { get; set; }
        //public string instType { get; set; }


    }

    public class Okex_Rest_Api_Account : OkexRestApiBase
    {
        public Okex_Rest_Api_Account()
            : base(Okex_REST_API_V5.Account)
        {

        }
    }


    public class Okex_Rest_Api_Balance : OkexRestApiBase
    {
       
        private Okex_Rest_Api_Balance() 
            : base(Okex_REST_API_V5.Balance)
        {
            this.CacheTime = 250;
            ////if (!string.IsNullOrEmpty(currency))
            //{
            //    Address = Path.Combine(this.Address, currency);
            //}
        }

        private static Okex_Rest_Api_Balance _instance = null;
        private static object lockObj = new object();
        public static Okex_Rest_Api_Balance Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(lockObj)
                    {
                        var v = new Okex_Rest_Api_Balance();
                        _instance = v;
                    }
                }

                return _instance;
            }
        }
    }

    /// <summary>
    /// 获取资金账户的余额
    /// </summary>
     /*
    public class Okex_Rest_Api_WalletV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_WalletV5(string currency) : base(Okex_REST_API_V5.Account)
        {
            this.NeedHead = true;
            if (!string.IsNullOrEmpty(currency))
            {
                Address = Path.Combine(this.Address, currency);
            }
            else
            {
                throw new Exception("need currency");
            }
        }



        protected override void OnReceiveData(JToken data)
        {
            base.OnReceiveData(data);
        }
    }
    */

    public class Okex_Rest_Api_CTCOrderModify:OkexRestApiBase
    {
        public Okex_Rest_Api_CTCOrderModify(long id, string currency1, string currency2, decimal amount,decimal price) : base(Okex_REST_API_V5.CTCOrderModify, Http.Method_Post)
        {
            this.instId = string.Format("{0}-{1}", currency1, currency2).ToUpper();
            this.ordId = id.ToString();
            this.newSz = amount.ToString();
            this.newPx = price.ToString();
        }

        public string instId { get; set; }

        public string ordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool cxlOnFail { get; set; }

        public string newSz { get; set; }

        public string newPx { get; set; }


        //CTCOrderModify


    }

    public class Okex_Rest_Api_CTCOrderV5 : OkexRestApiBase
    {

        public Okex_Rest_Api_CTCOrderV5(string currency1,string currency2, OrderOparete side) 
            :this(string.Format("{0}-{1}", currency1.ToUpper(), currency2),side)
        {
           
        }

        public Okex_Rest_Api_CTCOrderV5(string instId, OrderOparete side)
            : base(Okex_REST_API_V5.CTCOrderCreate, Http.Method_Post)
        {
            this.ordType = "limit";

            switch (side)
            {
                case OrderOparete.Buy:
                    this.side = "buy";
                    break;
                case OrderOparete.Sell:
                    this.side = "sell";
                    break;
            }

            this.tdMode = "cash";
            this.instId = instId;

            /*
              {{
  "code": 0,
  "data": {
    "client_oid": "",
    "error_code": "",
    "error_message": "",
    "order_id": "2909852435156992",
    "result": true
  }
}}
             */
        }

        /**
         * 币对名称
         */
        public string instId { get; set; }

        /**
         * 买入 buy 卖出 sell
         */
        public string side { get; set; }

        /**
         * 	limit，market(默认是limit)，当以market（市价）下单，ordType只能选择0:普通委托
         */
        public string ordType { get; set; }

        /**
         *如果是挂单则必须传这个参数，表示挂单价 平台要求传string 
         */
        public string px { get; set; } // 如果是挂单则必须传这个参数，表示挂单价 平台要求传string 

        /**
         * 挂单数量
         */
        public decimal sz { get; set; }//平台要求传string

        /**
         * 自定义订单标识
         */
        public string clOrdId { get; set; }

        /**
         * 交易模式
保证金模式：isolated：逐仓 ；cross：全仓
非保证金模式：cash：非保证金
         */
        public string tdMode { get; set; }

        /**
         * 如果是市价立即成交则这个字段为买入金额（USDT）
         * 
         */
        //public string notional { get; set; }

        //{'type': 'limit', 'side': 'buy', 'instrument_id': 'BTC-USDT', 'size': 0.001, 'client_oid': 'oktspot79', 'price': '4638.51', 'funds': '', 'margin_trading': 1, 'order_type': '3'}
    }

    public class Okex_Rest_Api_CTCSellNowV5 : Okex_Rest_Api_CTCOrderV5
    {

        /**
         * 市价卖出
         * size 数量
         */
        public Okex_Rest_Api_CTCSellNowV5(string instId, decimal size) : base(instId, OrderOparete.Sell)
        {
            this.ordType = "market";
            this.sz = size;
        }
    }

    public class Okex_Rest_Api_CTCBuyNowV5 : Okex_Rest_Api_CTCOrderV5
    {
        /**
         * 市价买入
         * notional 金额（USDT）
         */
        public Okex_Rest_Api_CTCBuyNowV5(string instId, decimal size) : base(instId, OrderOparete.Buy)
        {
            this.ordType = "market";
            this.sz = size;
        }
    }

    public class Okex_RestApi_CancelOrderV5 : OkexRestApiBase
    {
        public Okex_RestApi_CancelOrderV5( string instrument_id, string id)
           : base(Okex_REST_API_V5.CTCCancelOrder, Http.Method_Post)
        {
            this.instId = instrument_id;
            this.ordId = id;
        }

        /**
         * 订单的币对名称
         */
        public string instId { get; set; }

/**
         * 订单的币对名称
         */
        public string ordId { get; set; }
    }

    public class Okex_Rest_Api_OTCMarketV5 : OkexRestApiBase
    {
        public Okex_Rest_Api_OTCMarketV5(string currency) : base(string.Format(Okex_REST_API_V5.OTCMarket, currency), Http.Method_Get)
        {
            this.NeedHead = false;
        }
    }
}
