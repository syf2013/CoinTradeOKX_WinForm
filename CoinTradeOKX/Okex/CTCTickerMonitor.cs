using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoinTradeOKX.Okex;
using Newtonsoft.Json.Linq;
using System.Threading;
using Common.Classes;
using Common.Interface;

namespace CoinTradeOKX.Monitor
{


    [MonitorNameAttribute(Name = "币币报价")]
    public class CTCTickerMonitor:WebSocketMonitor,IDepthProvider
    {
        public decimal Ask { get; private set; }
        public decimal Bid { get; private set; }

        string InstrumentId = "";

        private DepthBook depthBook = new DepthBook();

        public CTCTickerMonitor(OkxV5APIPublic<WebSocket> api, string currency)
        {
            this.InstrumentId = string.Format("{0}-{1}", currency.ToUpper(), Config.Instance.Anchor.ToUpper());
            string[] args = {this.InstrumentId,this.InstrumentId};
            string[] channels = {WebsocketChannels.Channel_spot_ticker ,WebsocketChannels.Channel_book_depth5};

            this.Init(api, channels, args);
            this.Interval = 3000;
        }


        public override void Destory()
        {
            this.isDestroy = true;

            base.Destory();
        }

        private void ParseTickerDataV5(JToken data)
        {
            var ask = data["askPx"].Value<decimal>();
            var bid = data["bidPx"].Value<decimal>();

            if (ask > 0 && bid > 0)
            {
                this.Ask = ask;
                this.Bid = bid;

                this.Feed();
                base.InvokeData();
            }
        }

        private void ParseTickerData(JToken data)
        {
            var ask = data["best_ask"].Value<decimal>();
            var bid = data["best_bid"].Value<decimal>();

            if (ask > 0 && bid > 0)
            {
                this.Ask = ask;
                this.Bid = bid;

                this.Feed();
                base.InvokeData();
            }
        }

        private void ParseDepthBook5(JToken data)
        {
            // "timestamp":"2019-04-16T11:03:03.712Z"
            string timestamp = data.Value<string>("timestamp");//时间戳

            Pool<DepthInfo> pool = Pool<DepthInfo>.GetPool();

            this.depthBook.Time = DateTime.Parse(timestamp);

            JArray askList = data["asks"] as JArray;
            JArray bidList = data["bids"] as JArray;

            List<DepthInfo> book = new List<DepthInfo>();

            foreach(var item in askList)
            {
                var depthInfo = pool.Get();

                var info = item as JArray;

                depthInfo.Price = decimal.Parse(info[0].Value<string>());
                depthInfo.Total = decimal.Parse(info[1].Value<string>());
                depthInfo.Orders = uint.Parse(info[2].Value<string>());

                book.Add(depthInfo);
            }

            this.depthBook.Update(SideEnum.Sell, book);
            book.Clear();

            foreach (var item in bidList)
            {
                var depthInfo = pool.Get();

                var info = item as JArray;

                depthInfo.Price = decimal.Parse(info[0].Value<string>());
                depthInfo.Total = decimal.Parse(info[1].Value<string>());
                depthInfo.Orders = uint.Parse(info[2].Value<string>());

                book.Add(depthInfo);
            }

            this.depthBook.Update(SideEnum.Buy, book);
            this.Feed();
        }

        private void ParseDepthBook5V5(JToken data)
        {
            // "timestamp":"2019-04-16T11:03:03.712Z"
            long timestamp = data.Value<long>("ts");//时间戳

            Pool<DepthInfo> pool = Pool<DepthInfo>.GetPool();

            this.depthBook.Time = DateUtil.TimestampMSToDateTime (timestamp);

            JArray askList = data["asks"] as JArray;
            JArray bidList = data["bids"] as JArray;

            List<DepthInfo> book = new List<DepthInfo>();

            foreach (var item in askList)
            {
                var depthInfo = pool.Get();

                var info = item as JArray;

                depthInfo.Price = decimal.Parse(info[0].Value<string>());
                depthInfo.Total = decimal.Parse(info[1].Value<string>());
                depthInfo.Orders = uint.Parse(info[3].Value<string>());

                book.Add(depthInfo);
            }

            this.depthBook.Update(SideEnum.Sell, book);
            book.Clear();

            foreach (var item in bidList)
            {
                var depthInfo = pool.Get();

                var info = item as JArray;

                depthInfo.Price = decimal.Parse(info[0].Value<string>());
                depthInfo.Total = decimal.Parse(info[1].Value<string>());
                depthInfo.Orders = uint.Parse(info[3].Value<string>());

                book.Add(depthInfo);
            }

            this.depthBook.Update(SideEnum.Buy, book);
            this.Feed();
        }

        protected override void ProcessData(string channel, string instId, JArray datas)
        {

            if (string.Compare(instId, this.InstrumentId, true) == 0)
            {
                foreach (JToken jt in datas)
                {
                    if (channel == WebsocketChannels.Channel_spot_ticker)
                    {
                        this.ParseTickerDataV5(jt);
                    }
                    else if (channel == WebsocketChannels.Channel_book_depth5)
                    {
                        this.ParseDepthBook5V5(jt);
                    }
                }
            }

            base.ProcessData(channel, instId, datas);
        }




        public void EachDeep(SideEnum side, Action<DepthInfo> callback)
        {
            this.depthBook.EachDeep(side, callback);
        }
    }
}
