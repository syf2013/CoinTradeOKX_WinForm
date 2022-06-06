using Common.Classes;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "OTC市场")]
    /*
    public class OTCMarketMonitor:JSMonitorBase
    {
        List<Order> innerSell_Orders = new List<Order>();
        List<Order> innerBuy_Orders = new List<Order>();
        private object locker_sell_order = new object();
        private object locker_buy_order = new object();

        public List<Order> Sell_Orders
        {
            get
            {
                Order[] array;

                lock (locker_sell_order)
                {
                    array = new Order[innerSell_Orders.Count];
                    this.innerSell_Orders.CopyTo(array);
                }

                return new List<Order>(array);
            }
        }


        public List<Order> Buy_Orders
        {
            get
            {
                Order[] array;

                lock (locker_buy_order)
                {
                    array = new Order[innerBuy_Orders.Count];
                    this.innerBuy_Orders.CopyTo(array);
                }

                return new List<Order>(array);
            }
        }


        public decimal MinSellPrice { get; private set; }
        public    decimal MaxBuyPrice { get; private set; }  
        public    decimal MaxSellPrice { get; private set; }
        public    decimal MinKyc1CanBuyPrice { get; private set; }

        public bool MinPriceSellIsMyself { get; private set; }

        protected override void OnDataUpdate(JToken marketData)
        {
            {

                var sellList = marketData["sell"] as JArray;
                var buyList = marketData["buy"] as JArray;


                Pool<Order> orderPool = Pool<Order>.GetPool();
                MinSellPrice = decimal.MaxValue;
                MaxBuyPrice = decimal.MinValue;
                MaxSellPrice = decimal.MinValue;
                MinKyc1CanBuyPrice = decimal.MaxValue;
                Order order;

                lock (locker_sell_order)
                {
                    orderPool.Put(this.innerSell_Orders);
                    this.innerSell_Orders.Clear();
                    foreach (var o in sellList)
                    {
                        order = orderPool.Get();
                        order.ParseFromJson(o);

                        if (order.IsSelf)
                        {

                        }

                        MinSellPrice = Math.Min(order.Price, MinSellPrice);
                        MaxSellPrice = Math.Max(order.Price, MaxSellPrice);
                        if (order.MinKycLevel == 1)
                        {
                            MinKyc1CanBuyPrice = Math.Min(MinKyc1CanBuyPrice, order.Price);
                        }

                        this.innerSell_Orders.Add(order);
                    }
                }

                lock (locker_buy_order)
                {
                    orderPool.Put(this.innerBuy_Orders);
                    this.innerBuy_Orders.Clear();

                    foreach (var o in buyList)
                    {
                        order = orderPool.Get();
                        order.ParseFromJson(o);
                        this.innerBuy_Orders.Add(order);
                        MaxBuyPrice = Math.Max(order.Price, MaxBuyPrice);
                    }
                }


                MinKyc1CanBuyPrice = Math.Min(MinKyc1CanBuyPrice, MaxSellPrice);
                decimal diff = (MinSellPrice - MaxBuyPrice) / MinSellPrice;
            }
        }

        public OTCMarketMonitor(string currency)
            :base(new okex_Order_List_Update() { currency  = currency}, 1500)
        {

        }
    }
    */

    public class OTCMarketMonitor : RESTMonitor
    {
        List<Order> innerSell_Orders = new List<Order>();
        List<Order> innerBuy_Orders = new List<Order>();
        private object locker_sell_order = new object();
        private object locker_buy_order = new object();

        public decimal Ask { get; private set; }
        public decimal Bid { get; private set; }
        public string Currency { get; private set; }

        /*
        public List<Order> Sell_Orders
        {
            get
            {
                Order[] array;

                lock (locker_sell_order)
                {
                    array = new Order[innerSell_Orders.Count];
                    this.innerSell_Orders.CopyTo(array);
                }

                return new List<Order>(array);
            }
        }

        public List<Order> Buy_Orders
        {
            get
            {
                Order[] array;

                lock (locker_buy_order)
                {
                    array = new Order[innerBuy_Orders.Count];
                    this.innerBuy_Orders.CopyTo(array);
                }

                return new List<Order>(array);
            }
        }
        */

        public decimal MinSellPrice { get; private set; }
        public decimal MaxBuyPrice { get; private set; }
        public decimal MaxSellPrice { get; private set; }
  
        public bool MinPriceSellIsMyself { get; private set; }


        public void EachSellOrder(Action<Order> callback,bool reverse = false)
        {
            lock(locker_sell_order)
            {
                if (reverse)
                {
                    for(int i = this.innerSell_Orders.Count - 1; i >= 0;i --)
                    {
                        callback(this.innerSell_Orders[i]);
                    }
                }
                else
                {
                    foreach (var o in this.innerSell_Orders)
                    {
                        callback(o);
                    }
                }
            }
        }

        public void EachBuyOrder(Action<Order> callback, bool reverse = false)
        {
            lock(locker_buy_order)
            {
                foreach(var o in this.innerBuy_Orders)
                {
                    callback(o);
                }
            }
        }


        protected override void OnDataUpdate(JToken marketData)
        {
            var sellList = marketData["sell"] as JArray;
            var buyList = marketData["buy"] as JArray;

            Pool<Order> orderPool = Pool<Order>.GetPool();
            MinSellPrice    = decimal.MaxValue;
            MaxBuyPrice     = decimal.MinValue;
            MaxSellPrice    = decimal.MinValue;
           
            Order order;

            lock (locker_sell_order)
            {
                orderPool.Put(this.innerSell_Orders);
                this.innerSell_Orders.Clear();
                foreach (var o in sellList)
                {
                    order = orderPool.Get();
                    order.ParseFromJson(o);
                    if (!order.Hidden)
                    {
                        MinSellPrice = Math.Min(order.Price, MinSellPrice);
                        MaxSellPrice = Math.Max(order.Price, MaxSellPrice);
                    }

                    this.innerSell_Orders.Add(order);
                }

                this.Ask = MinSellPrice;
            }

            lock (locker_buy_order)
            {
                orderPool.Put(this.innerBuy_Orders);
                this.innerBuy_Orders.Clear();

                foreach (var o in buyList)
                {
                    order = orderPool.Get();
                    order.ParseFromJson(o);
                    this.innerBuy_Orders.Add(order);
                    if(!order.Hidden)
                        MaxBuyPrice = Math.Max(order.Price, MaxBuyPrice);
                }

                this.Bid = this.MaxBuyPrice;
            }

            if(this.innerBuy_Orders.Count >0 && this.innerSell_Orders.Count >0)
                this.Feed();
        }

        public OTCMarketMonitor(string currency)
#if !OKEX_API_V5
            :base(new Okex_Rest_Api_OTCMarket(currency), 2500)

#else
            : base(new Okex_Rest_Api_OTCMarketV5(currency), 2500)
#endif
        {
            this.Currency = currency.ToLower();
        }
    }
}
