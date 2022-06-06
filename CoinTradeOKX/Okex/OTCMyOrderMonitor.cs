using CoinTradeOKX.Okex.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Const;
using Common.Classes;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "OTC挂单")]
    public class OTCMyOrderMonitor : JSMonitorBase
    {
        private object locker = new object();
        private List<Order> _sell_orders = new List<Order>();
        private List<Order> _buy_orders = new List<Order>();
        private List<long> _removed_orders = new List<long>();

        /*
        public List<Order> Sell_Orders
        {
            get
            {
                lock (locker)
                {
                    var result =  new List<Order>(_sell_orders);

                    return result;
                }
            }
        }

        
        public List<Order> Buy_Orders
        {
            get
            {
                lock (locker)
                {
                    var result = new List<Order>(_buy_orders);

                    return result;
                }
            }
        }
        */
        public int OrderCount
        {
            get
            {
                lock (locker)
                {
                    return _buy_orders.Count + _sell_orders.Count;
                }
            }
        }

        public decimal MinSellPrice { get; private set; }
        public decimal MaxBuyPrice { get; private set; }
        public decimal MaxSellPrice { get; private set; }
        public decimal MinKyc1CanBuyPrice { get; private set; }

        public void EachSellOrder(Action<Order> callback)
        {
            lock(locker)
            {
                foreach(var o in _sell_orders)
                {
                    callback(o);
                }
            }
        }

        public void EachBuyOrder(Action<Order> callback)
        {
            lock (locker)
            {
                foreach (var o in _buy_orders)
                {
                    callback(o);
                }
            }
        }


        public void RemoveOrder(long publicId)
        {
            lock (_removed_orders)
            {
                this._removed_orders.Add(publicId);
            }

            lock (locker)
            {
                foreach (var o in _buy_orders)
                {
                    if(o.PublicId == publicId)
                    {
                        _buy_orders.Remove(o);
                        return;
                    }
                }

                foreach (var o in _sell_orders)
                {
                    if (o.PublicId == publicId)
                    {
                        _sell_orders.Remove(o);
                        return;
                    }
                }
            }
        }

        public OTCMyOrderMonitor()
            : base(new okex_My_Order_List() { }, 1800)
        {

        }

        public void AddNewOrder(Order order)
        {
            lock(locker)
            {
                var list = order.Side == OrderOparete.Buy ? _buy_orders : _sell_orders;
                list.Add(order);
            }
        }

        protected override void OnDataUpdate(JToken orderData)
        {
            Pool<Order> orderPool = Pool<Order>.GetPool();
            MinSellPrice = decimal.MaxValue;
            MaxBuyPrice = decimal.MinValue;
            MaxSellPrice = decimal.MinValue;
            MinKyc1CanBuyPrice = decimal.MaxValue;

            lock (locker)
            {
                orderPool.Put(this._sell_orders);
                orderPool.Put(this._buy_orders);

                this._sell_orders.Clear();
                this._buy_orders.Clear();

                if (orderData is JArray)
                {
                    foreach (JToken jt in orderData as JArray)
                    {
                        Order order = orderPool.Get();
                        order.ParseFromJson(jt);

                        lock (_removed_orders)
                        {
                            if (this._removed_orders.Contains(order.PublicId))
                                continue;
                        }

                        var list = order.Side == OrderOparete.Buy ? _buy_orders : _sell_orders;
                        list.Add(order);
                    }
                }

                this.Feed();
            }
        }
    }
}
