using CoinTradeOKX.Event;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Manager
{
    public class CTCOrderManager
    {

        private readonly string ErrCodeField = "code";
        private readonly string DataField = "data";
        private readonly string CodeField = "sCode";
        private readonly string MsgField = "sMsg";


        MonitorManager monitorManager = null;
        CTCMyOrderMonitor monitor = null;
        public CTCOrderManager()
        {
            this.monitorManager = MonitorManager.Default;
            this.AddOrderMonitor();
        }


        public void EachSellOrder(Action<OrderBase> callback)
        {
            if (monitor != null)
                monitor.EachSellOrder(callback);
        }

        private bool ApiExecuteResult(JToken result, out JToken data)
        {
            if (result.Value<int>(ErrCodeField) == 0)
            {
                result = result[DataField];

                if (result is JArray)
                    result = result[0];

                if (result.Value<int>(CodeField) == 0)
                {
                    data = result;
                    return true;
                }
            }
            data = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="instId"></param>
        /// <returns></returns>
        public Task<OrderBase> QueryOrder(long orderId, string instId)
        {
            return this.QueryOrder(orderId, instId, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="instId"></param>
        /// <param name="updateMonitorBuffer"></param>
        /// <returns></returns>
        public Task<OrderBase> QueryOrder(long orderId, string instId, bool updateMonitorBuffer)
        {
            return Task.Run<OrderBase>(() => {

                Okex_Rest_Api_OrderQuery api = new Okex_Rest_Api_OrderQuery(instId, orderId);

                JToken result = api.execSync();

                if (ApiExecuteResult(result, out result))
                {
                    OrderBase order = Pool<OrderBase>.GetPool().Get();

                    order.ParseFromJson(result);

                    if (updateMonitorBuffer)
                    {
                        this.monitor.UpdateOrder(order);
                    }

                    return order;
                }

                return null;
            });
        }

        /*
        public Task<OrderBase> QueryOrder(long orderId,string currency1, string currency2,bool updateMonitorBuffer)
        {
            return this.QueryOrder(orderId, string.Format("{0}-{1}", currency1, currency2).ToUpper(),updateMonitorBuffer);
        }
        */

        /// <summary>
        /// 修改挂单
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="currency">币种</param>
        /// <param name="amount">新数量</param>
        /// <param name="price">新价格</param>
        /// <param name="cancelOrderWhenFailed">如果修改失败则撤销订单</param>
        /// <returns>如果成功返回true</returns>
        public Task<bool> ChangeOrder(long id, string currency, decimal amount, decimal price, bool cancelOrderWhenFailed)
        {
            //if (side == OrderOparete.Buy) //如果是购买模式的话需要转换为USDT数量
            //{
            //    amount = amount * price;//转为USDT数量
            //}

            string currency2 = Config.Instance.Anchor.ToUpper();
            Okex_Rest_Api_CTCOrderModify api = new Okex_Rest_Api_CTCOrderModify(id, currency, currency2, amount, price);
            api.cxlOnFail = cancelOrderWhenFailed;

            return Task.Run<bool>(() => {

                JToken result = api.execSync();

                if (this.ApiExecuteResult(result, out result))
                {
                    return true;
                }
                return false;
            });
        }

        public Task<OrderBase> PlaceOrder(string currency, decimal amount, decimal price, OrderOparete side, bool addToMonitorBuffer)
        {
            //decimal coinAmount = amount;

            if (side == OrderOparete.Buy) //如果是购买模式的话需要转换为USDT数量
            {
                //    amount = amount * price;//转为USDT数量
            }

            string currency2 = Config.Instance.Anchor.ToUpper();

            string instId = string.Format("{0}-{1}", currency, currency2).ToUpper();

            Okex_Rest_Api_CTCOrderV5 api = new Okex_Rest_Api_CTCOrderV5(instId, side);
            api.sz = amount;
            api.px = price.ToString();

            return Task.Run<OrderBase>(async () =>
            {
                long orderId = 0;
                JToken result = await api.exec();

                if (this.ApiExecuteResult(result, out result))
                {
                    orderId = result.Value<long>("ordId");
                    OrderBase order = await this.QueryOrder(orderId, instId);
                    if(addToMonitorBuffer && order != null)
                    {
                        OrderBase ret = Pool<OrderBase>.GetPool().Get();
                        ret.CopyFrom(order);
                        this.monitor.AddOrderFromExternal(order);
                        order = ret;
                    }
                    return order;
                }

                return null;
            });
        }

        public void EachBuyOrder(Action<OrderBase> callback)
        {
            if(this.monitor != null) this.monitor.EachBuyOrder(callback);
        }

        public void CancelOrder(OrderBase order)
        {
            this.CancelOrder(order.InstrumentId, order.PublicId);
        }

        public void CancelOrder(string instrumentId, long id)
        {
            var api = new Okex_RestApi_CancelOrderV5(instrumentId, id.ToString());
            api.execAsync();
        }

        public bool ExistOrder(string currency1, string currency2, OrderOparete side)
        {
            bool hasOrder = false;
            string instId = string.Format("{0}-{1}", currency1, currency2);

            switch (side)
            {
                case OrderOparete.Buy:
                    this.EachBuyOrder((order) =>
                    {
                        if (string.Compare(instId, order.Currency, true) == 0)
                        {
                            hasOrder = true;
                        }
                    });
                    break;
                case OrderOparete.Sell:
                    this.EachSellOrder((order) =>
                    {
                        if (string.Compare(instId, order.Currency, true) == 0)
                        {
                            hasOrder = true;
                        }
                    });
                    break;
            }

            return hasOrder;
        }

        public void CancelOrder(string currency1, string currency2, OrderOparete side)
        {
            List<long> ids = null;
            string ins = string.Format("{0}-{1}", currency1, currency2);

            switch (side)
            {
                case OrderOparete.Buy:
                    this.EachBuyOrder((order) => {
                        if (string.Compare(ins, order.Currency, true) == 0)
                        {
                            if (ids == null)
                                ids = new List<long>();
                            ids.Add(order.PublicId);
                        }
                    });
                    break;
                case OrderOparete.Sell:
                    this.EachSellOrder((order) => {
                        if (string.Compare(ins, order.Currency, true) == 0)
                        {
                            if (ids == null)
                                ids = new List<long>();
                            ids.Add(order.PublicId);
                        }
                    });
                    break;
            }

            if (ids != null && ids.Count > 0)
            {
                foreach (long id in ids)
                {
#if OKEX_API_V5
                    var api = new Okex_RestApi_CancelOrderV5(string.Format("{0}-{1}", currency1, currency2).ToUpper(), id.ToString());
#else
                    var api = new Okex_RestApi_CancelOrder(currency1, currency2, id.ToString());
#endif
                    api.execAsync();
                }
            }
        }

        public void SyncOrders()
        {
            monitor.Update((int)monitor.Interval + 1);
        }

        private void AddOrderMonitor()
        {
            if (this.monitor == null)
            {
                var monitor = new CTCMyOrderMonitor();
                this.monitorManager.AddMonotor(monitor);
                monitor.OnData += Monitor_OnData;
                this.monitor = monitor;
            }

        }

        private void Monitor_OnData(MonitorBase obj)
        {

        }

        private void RemoveOrderMonitor()
        {
            if (this.monitor != null)
            {
                this.monitorManager.RemoveMonitor(monitor);
                this.monitor = null;
            }
        }
    

        //public void 

        private static CTCOrderManager _instance = null;
        public static CTCOrderManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CTCOrderManager();

                return _instance;
            }
        }
    }
}
