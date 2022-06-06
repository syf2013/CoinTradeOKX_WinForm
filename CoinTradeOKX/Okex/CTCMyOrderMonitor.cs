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
    [MonitorName(Name = "币币挂单")]
    public class CTCMyOrderMonitor : RESTMonitor
    {
        private object locker = new object();
        private List<OrderBase> _sell_orders = new List<OrderBase>();
        private List<OrderBase> _buy_orders = new List<OrderBase>();
        private List<long> _removed_orders = new List<long>();

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

        public bool UpdateOrder(OrderBase newOrderData)
        {
            bool isExist = false;
            Action<OrderBase> callback = (OrderBase order) => {
                if(order.PublicId == newOrderData.PublicId 
                && string.Compare(newOrderData.InstrumentId,order.InstrumentId,true) == 0 )
                {
                    order.CopyFrom(newOrderData);
                    isExist = true;
                }
            };

            switch(newOrderData.Side)
            {
                case OrderOparete.Buy:
                    this.EachBuyOrder(callback);
                    break;
                case OrderOparete.Sell:
                    this.EachSellOrder(callback);
                    break;
            }

            return isExist;
        }

        public void AddOrderFromExternal(OrderBase order)
        {
            if(!this.UpdateOrder(order))
            {
                lock(locker)
                {
                    List<OrderBase> list = null;

                    switch(order.Side)
                    {
                        case OrderOparete.Buy:
                            list = this._buy_orders;
                            break;
                        case OrderOparete.Sell:
                            list = this._sell_orders;
                            break;
                    }

                    list.Add(order);
                }
            }
        }

        public void EachSellOrder(Action<OrderBase> callback)
        {
            lock (locker)
            {
                foreach (var o in _sell_orders)
                {
                    callback(o);
                }
            }
        }

        public void EachBuyOrder(Action<OrderBase> callback)
        {
            lock (locker)
            {
                foreach (var o in _buy_orders)
                {
                    callback(o);
                }
            }
        }


        public CTCMyOrderMonitor()
            : base(new Okex_Rest_Api_OrdersV5(), 300)
        {

        }

        protected override void OnDataUpdate(JToken orderData)
        {
            Pool<OrderBase> orderPool = Pool<OrderBase>.GetPool();


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
                        OrderBase order = orderPool.Get();
                        /*
                        order.PublicId = jt["order_id"].Value<long>();
                        order.Price = jt["price"].Value<decimal>();
                        order.AvailableAmount = jt["size"].Value<decimal>()-jt["filled_size"].Value<decimal>();
                        order.Side = jt["side"].Value<string>()=="sell" ? OrderOparete.Sell : OrderOparete.Buy;
                        order.CreatedDate = DateTime.Parse(jt["timestamp"].Value<string>());
                        order.Currency = jt["instrument_id"].Value<string>();
                        */

                        try
                        {
                            order.ParseFromJson(jt);
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogError(" Order Data Error : \r\n" + orderData.ToString());
                            Logger.Instance.LogException(ex);
                            return;
                        }

                        order.MarketType = MarketTypeEnum.CTCMarket;

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
