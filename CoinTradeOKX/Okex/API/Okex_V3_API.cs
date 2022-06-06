using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace CoinTradeOKX.Okex
{
    /**
     * 其中 op 的取值为 1--subscribe 订阅； 2-- unsubscribe 取消订阅 ；3--login 登录
     */
    public enum APIOperate
    {
        Login       = 3,   //登录
        Subscribe   = 1,//订阅
        Unsubscribe = 2,//取消订阅
    }

    public static class WebsocketChannels
    {
        public static readonly string Channel_spot_ticker = "tickers";  //价格
        public static readonly string Channel_spot_account = "account"; //账户
        public static readonly string Channel_book_depth5 = "books5"; // 5档深度数据
        public static readonly string Channel_book_depth200 = "books"; // 200档深度数据
    }


    public class OkxV5APIPublic<T> where T:IWebSocket,new()
    {
        public static readonly string Table_table = "table";

        public event Action<bool> OnLogin = null;

        public event Action<string,string, JArray> OnData = null;
        IWebSocket socketProxy = null;

        public OkxV5APIPublic()
        {

            this.CreateSocket();
        }

        private void CreateSocket()
        {
            this.socketProxy = new T();
            socketProxy.OnClose     += this.OnClose;
            socketProxy.OnError     += this.OnError;
            socketProxy.OnMessage   += this.OnMessage;
            socketProxy.OnOpen      += this.OnOpen;
            this.Connect(Config.Instance.ApiInfo.ApiAddress);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="args">参数</param>
        public void Unsubscribe(params string[] args)
        {
            
            JObject json = GetCommand(APIOperate.Unsubscribe, args);
            this.Send(json);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="args">参数</param>
        public void Unsubscribe(string channel, string instId)
        {
            JObject token = new JObject();
            token["channel"] = channel;
            token["instId"] = instId;
            token["instType"] = "SPOT";

            JObject json = GetCommand(APIOperate.Unsubscribe, token);
            this.Send(json);
        }

        /**
         * 获取现货最新成交价、买一价、卖一价和24交易量
        *send示例
        *{"op": "subscribe", "args": ["spot/ticker:ETH-USDT"]}
        */

        public void Subscribe(params string[] args)
        {
            JObject json = GetCommand(APIOperate.Subscribe, args);
            this.Send(json);
        }

        /**
 * 获取现货最新成交价、买一价、卖一价和24交易量
*send示例
*{"op": "subscribe", "args": ["spot/ticker:ETH-USDT"]}
*/

        public void Subscribe(string channel,string instId)
        {
            JObject token = new JObject();
            token["channel"] = channel;
            token["instId"] = instId;
            token["instType"] = "SPOT";

            JObject json = GetCommand(APIOperate.Subscribe, token);
            this.Send(json);
        }


        /*
        /// <summary>
        /// 订阅市场深度数据
        /// </summary>
        /// <param name="currency"></param>
        public void SubscribeBook(string currency)
        {  
            this.Subscribe(string.Format("{0}:{1}-{2}", WebsocketChannels.Channel_book_depth200, currency.ToUpper(), Config.Instance.Anchor.ToUpper()));
        }
        public void SubscribeCurrency(string currency)
        {
            this.Subscribe(string.Format("{0}:{1}-{2}", WebsocketChannels.Channel_spot_ticker, currency.ToUpper(), Config.Instance.Anchor.ToUpper()));
        }
        /*
        /**
        * 获取币币账户信息，需用用户登录
        * send示例
        * {"op": "subscribe", "args": ["spot/account:BTC"]}
        * 
        */
        public void SubscribeAccount(string currency)
        {
            this.Subscribe(string.Format("{0}:{1}", WebsocketChannels.Channel_spot_account, currency.ToUpper()));
        }

        private void Connect(string address)
        {
            this.IsLogin = false;

            this.socketProxy.ConnectAsync(Config.Instance.ApiInfo.ApiAddress);

        }


        /**
         * 
         *解压缩数据流 
         */
        private static string Decompress(byte[] baseBytes)
        {
            using (var decompressedStream = new MemoryStream())
            using (var compressedStream = new MemoryStream(baseBytes))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private Queue<JObject> CommandPool = new Queue<JObject>();

        private JObject GetCommand(APIOperate operate, params object[] args)
        {
            JObject json = CommandPool.Count > 0 ? CommandPool.Dequeue() : new JObject();

            const string key_args = "args";
            const string key_op = "op";

            if (CommandPool.Count > 0)
            {
                json = CommandPool.Dequeue();
                (json[key_args] as JArray).Clear();
            }
            else
            {
                json = new JObject();
                json[key_args] = new JArray();
            }

            json[key_op] = operate.ToString().ToLower();
            JArray jargs = json[key_args] as JArray;

            foreach (object obj in args)
            {
                jargs.Add(obj);
            }
            return json;
        }

        private JObject GetCommand(APIOperate operate,params JToken[] args)
        {
            JObject json = CommandPool.Count > 0 ? CommandPool.Dequeue() : new JObject();

            const string key_args = "args";
            const string key_op = "op";

            if (CommandPool.Count > 0)
            {
                json = CommandPool.Dequeue();
                (json[key_args] as JArray).Clear();
            }
            else
            {
                json = new JObject();
                json[key_args] = new JArray();
            }

            json[key_op] = operate.ToString().ToLower();
            JArray jargs = json[key_args] as JArray;

            foreach(JToken a in args)
            {
                jargs.Add(a);
            }

            return json;
        }

#if !OKEX_API_V5
        /**
         * 
         *  发送登录请求
         */
        private void SendLogin()
        {
            long timestamp = DateUtil.GetServerTimestampSec();

            string sign = Crypt.GenareteSign(timestamp.ToString(), "GET", "/users/self/verify"); 
            JObject json = GetCommand(APIOperate.Login, Config.Instance.ApiInfo.GetKey(), Config.Instance.ApiInfo.GetPassphrase(), timestamp, sign);


            this.Send(json.ToString());

            // string json =  { "op":"login","args":["<api_key>","<passphrase>","<timestamp>","<sign>"]
        }
#else
       /**
   * 
   *  发送登录请求
   */
        private void SendLogin()
        {

            if (false)
            {
                long timestamp = DateUtil.GetServerTimestampSec();
                string sign = Crypt.GenareteSign(Config.Instance.ApiInfo.SecretKey, timestamp.ToString(), "GET", "/users/self/verify");
                JObject data = new JObject();


                data["apiKey"] = Config.Instance.ApiInfo.GetKey();
                data["passphrase"] = Config.Instance.ApiInfo.GetPassphrase();
                data["timestamp"] = timestamp;
                data["sign"] = sign;


                JObject json = GetCommand(APIOperate.Login, data);


                this.Send(json.ToString());
            }
            this.IsLogin = true;

            this.OnLogin?.Invoke(this.IsLogin);

            // string json =  { "op":"login","args":["<api_key>","<passphrase>","<timestamp>","<sign>"]
        }
#endif


 

        private void OnOpen()
        {
            SendLogin();
            isAlive = true;
        }

        private void OnError(string msg)
        {
            IsLogin = false;
            isAlive = false;
            this.Cleanup();

            Logger.Instance.Log(LogType.Error, msg);
            CreateSocket();
        }

        private void OnClose()
        {
            IsLogin = false;
            isAlive = false;

            Logger.Instance.LogError("WebSocket disconnected");

            this.Cleanup();

            if (closeSelf == false)
            {
                CreateSocket();
            }
        }

        /**
         * 发送心跳
         * 
         */
        public void SendPing()
        {
            this.socketProxy.SendAsync("ping");
        }

        public void Send(string data)
        {
            this.socketProxy.SendAsync(data);
        }

        public bool IsLogin
        {
            get;
            private set;
        }

        public bool isAlive = false;
        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }


        private void OnMessage(byte[] data, int count)
        {
            string str = Encoding.UTF8.GetString(data, 0, count).Trim();// Decompress(data);
            if (str == "pong")
            {
                return;
            }

            JObject json = null;
            try
            {
                json = JObject.Parse(str);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, ex.Message);
                return;
            }
            if (json.ContainsKey("event"))
            {
                string evt = json["event"].Value<string>();
                switch (evt)
                {
                    case "error":
                        Logger.Instance.Log(CoinTradeOKX.LogType.Error, string.Format("{0}:{1}", json["errorCode"].Value<string>(), json["message"].Value<string>()));
                        break;
                    case "login":
                        this.IsLogin = json["success"].Value<bool>();
                        this.OnLogin?.Invoke(this.IsLogin);
                        break;
                }
            }
            else // if (json.ContainsKey(Table_table))
            {
                JObject args = json["arg"] as JObject;
                JArray datas = json["data"] as JArray;

                string channel = args.Value<string>("channel");
                string instId = args.Value<string>("instId");
        
                this.OnData?.Invoke(channel, instId, datas);
            }
            //{"event":"error"," message":"","errorCode":""} //错误格式
        }
 
        public void Send(JObject json)
        {
            string str = json.ToString();
            this.Send(str);
            CommandPool.Enqueue(json);
        }

        private void Cleanup()
        {
            if (socketProxy != null)
            {
                socketProxy.OnClose -= this.OnClose;
                socketProxy.OnError -= this.OnError;
                socketProxy.OnMessage -= this.OnMessage;
                socketProxy.OnOpen -= this.OnOpen;
                this.socketProxy = null;
            }
        }

        private bool closeSelf = false;
        public void Close()
        {

            closeSelf = true;
            this.socketProxy.CloseAsync();
        }
    }
}
