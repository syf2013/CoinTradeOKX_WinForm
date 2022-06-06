using CoinTradeOKX.Invoke;
using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTradeOKX.Okex;

namespace CoinTradeOKX
{
    public abstract  class MonitorBase : IMonitor
    {

        public DateTime LastUpdate
        {
            get;private set;
        }
        public MonitorBase()
        {
            this.Effective = false;
            this.CustomName = string.Empty;
        }

        public virtual string CustomName
        {
            get;
            private set;
        }

        protected uint _Interval = 2000;

        /// <summary>
        /// API调用间隔（毫秒）
        /// </summary>
        public uint Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }

        protected int cd = 0;
        private int feedCd = 0;
        

        protected bool isDestroy = false;

        public virtual bool Effective
        {
            get;private set;
        }
        public event Action<MonitorBase> OnData = null;
        public event Action<int, string> OnError = null;
        protected void Feed()
        {
            if (isDestroy)
                return;

            LastUpdate = DateTime.Now;
            feedCd = (int)Interval + 1000;
            this.Effective = true;
           

            this.OnData?.Invoke(this);
        }
        protected void InvokeError(int code,string msg)
        {
            if (isDestroy)
                return;
           


            this.OnError?.Invoke(code,msg);
        }

        protected void InvokeData()
        {
            this.OnData?.Invoke(this);
        }

        protected abstract void RunInvoke();

        public virtual void Update(int dt)
        {
            if(isDestroy)
                return;

            cd -= dt;
            if (cd <= 0)
            {
                RunInvoke();
                cd = (int)this.Interval;
            }

            if(feedCd <= 0)
            {
                this.Effective = false;
            }
            else
            {
                feedCd -= dt;
            }
        }

        public virtual void Destory()
        {
            this.Effective = false;
            isDestroy = true;
        }
    }
    public abstract class JSMonitorBase: MonitorBase
    {
        OTCInvoke invoke = null;

        //public event Action<JToken> OnData = null;

        public JSMonitorBase(OTCInvoke invoke, uint interval)
        {
            this.invoke = invoke;
            this.Interval = interval;
        }

        protected virtual void OnDataUpdate(JToken data)
        {
  
        }

        async protected override void RunInvoke()
        {
            JToken result = await invoke.execAsync();

            int code = result != null ? result["code"].Value<int>() : -1;
            if(code == 0)
            {
                this.OnDataUpdate(result["data"]);
            }
            else
            {
                string msg = result["msg"].Value<string>();
                InvokeError(code, msg);
            }
        }
    }

    /// <summary>
    /// okex V3版 Websocket接口
    /// </summary>
    public abstract class WebSocketMonitor: MonitorBase
    {
        protected string[] channels = null;
        protected string[] arguments = null;
        protected OkxV5APIPublic<WebSocket> api = null;
        private bool inited = false;
        private bool subscribed = false;


#if OKEX_API_V5
        protected virtual void ProcessData(string channel,string instId, JArray datas)
        {

        }
#else
                protected virtual void ProcessData(string channel, JArray datas) {
            
        }
#endif

        public WebSocketMonitor()
        {

        }

        protected void Init(OkxV5APIPublic<WebSocket> api, string[] channels, string[] args)
        {
            if(inited)
                return;

            this.inited = true;

            if (api == null)
                api = new OkxV5APIPublic<WebSocket>();

            this.channels = channels;
            api.OnData += Api_OnData;
            api.OnLogin += Api_OnLogin;
            this.arguments = args;

            this.api = api;

            Api_OnLogin(api.IsLogin);
        }

        private void Subscribe()
        {
            for (int i = 0; i < this.channels.Length; i++)
            {
#if OKEX_API_V5
                api.Subscribe(channels[i], arguments[i]);
#else
                api.Subscribe(string.Format("{0}:{1}", channels[i], arguments[i]));
#endif

            }

            this.subscribed = true;
        }

        private void Api_OnLogin(bool login)
        {
            if (login)
            {
                this.Subscribe();
            }
        }

        private void Unsubscribe()
        {
            for (int i = 0; i < this.channels.Length; i++)
            {
#if OKEX_API_V5
                api.Unsubscribe(channels[i], arguments[i]);
#else
                api.Unsubscribe(string.Format("{0}:{1}", channels[i], arguments[i]));
#endif
            }
        }

#if OKEX_API_V5
                private  void Api_OnData(string ch,string instId, JArray data)
        {
            //if(ch == this.channel)
            //{
                this.ProcessData(ch,instId, data);
            //}
        }
#else
        private void Api_OnData(string ch, JArray data)
        {
            //if(ch == this.channel)
            //{
            this.ProcessData(ch, data);
            //}
        }
#endif


        protected override void RunInvoke(){

        }

        public override void Destory()
        {
            if(this.api !=null)
            {
                if(this.api.IsLogin && this.subscribed)
                {
                    Unsubscribe();
                }

                this.api.OnData -= this.Api_OnData;
                this.api.OnLogin -= this.Api_OnLogin;
                this.api = null;
            }

            base.Destory();
        }
    }

    public abstract class RESTMonitor:MonitorBase
    {
        protected  OkexRestApiBase api = null;
        public RESTMonitor(OkexRestApiBase restApi,uint interval)
        {
            base.Interval = interval;
            this.api = restApi;
            this.api.OnData += Api_OnData;
        }

        private void Api_OnData(JToken obj)
        {
            int code = obj.Value<int>("code");
            if (code != 0)
            {
                string msg = obj["msg"].Value<string>();
                InvokeError(code, msg);
                Logger.Instance.LogError(string.Format("{0} {1} {2} {3}", code, msg, this.GetType().Name, api.GetType().Name));
            }
            else
            {
                this.OnDataUpdate(obj["data"]);

            }
        }

        protected virtual void OnDataUpdate(JToken data)
        {

        }

        protected override void RunInvoke()
        {
            api.execAsync();
        }

        public override void Destory()
        {
            this.api.OnData -= this.Api_OnData;
            base.Destory();
        }
    }
}
