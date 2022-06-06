using Common.Classes;
using CoinTradeOKX.Event;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX
{
    public class OTCOrderManager
    {
        private bool sellOrderPlacing = false;
        private bool buyOrderPlacing = false;

        public bool Effective
        {
            get
            {
                return monitor.Effective;
            }
        }


        private Dictionary<string, long> cancelSellOrderTimeMS = new Dictionary<string, long>();
        private Dictionary<string, long> cancelBuyOrderTimeMS = new Dictionary<string, long>();

        private long lastPlaceSellOrderTimeMs = 0;
        private long lastPlaceBuyOrderTimeMs = 0;
        OTCMyOrderMonitor monitor = null;
        private OTCOrderManager()
        {
            if (Config.Instance.Account.MarketType != MarketTypeEnum.CTCMarket)
            {
                monitor = new OTCMyOrderMonitor();
                monitor.OnData += Monitor_OnData;
                MonitorManager.Default.AddMonotor(monitor);
            }

            EventCenter.Instance.AddEventListener(EventNames.DisableBuyBehavior, this.CancelBuy);
            EventCenter.Instance.AddEventListener(EventNames.DisableSellBehavior, this.CancelSell);
        }

        private void CancelSell(object arg)
        {
            List<long> ids = new List<long>();
            this.EachSellOrder((order) => {
                ids.Add(order.PublicId);
            });

            foreach(var id in ids)
            {
                this.CancelOrder(id, false, true);
            }
        }

        private void CancelBuy(object arg)
        {
            List<long> ids = new List<long>();
            this.EachBuyOrder((order) => {
                ids.Add(order.PublicId);
            });

            foreach (var id in ids)
            {
                this.CancelOrder(id, false, true);
            }
        }

        private void OnDisableSellBehavior()
        {
            lock(this.SellLocker)
            {
                List<long> ids = new List<long>();

                EachSellOrder((order) => {
                    ids.Add(order.PublicId);
                });

                foreach(var id in ids)
                {
                    this.CancelOrder(id, false, true);
                    //this.monitor.RemoveOrder(id);
                }
            }
        }

        private void OnDisableBuyBehavior()
        {
            lock (this.BuyLocker)
            {
                List<long> ids = new List<long>();

                EachBuyOrder((order) => {
                    ids.Add(order.PublicId);
                });

                foreach (var id in ids)
                {
                    this.CancelOrder(id, false, true);
                    //this.monitor.RemoveOrder(id);
                }
            }
        }

        private object _SyncRoot = new object();


        private object PlaceOrderLocker = new object();        
        private object SellLocker = new object();
        private object BuyLocker = new object();

        public bool CanPlaceSellOrder(string currency, bool doPlace = false)
        {
            bool b = System.Threading.Monitor.TryEnter(SellLocker);

            if (!b) return false;

            bool can = true;

            long nowMS = DateUtil.GetTimestampMS();

            do
            {
                uint cdCommon = Config.Instance.PlatformConfig.OrderCountDownMS;
                cdCommon = (uint)RandomUtil.GetRandom((int)cdCommon, 2 * (int)cdCommon);

                if (nowMS - lastPlaceSellOrderTimeMs < cdCommon)
                {
                    can = false;
                    break;
                }

                currency = currency.ToUpper();
                long cancelTimeMS = cancelSellOrderTimeMS.ContainsKey(currency) ? cancelSellOrderTimeMS[currency] : 0;
                int reorderCd = RandomUtil.GetRandom((int)Config.Instance.PlatformConfig.ReorderCountDownMS, 2 * (int)Config.Instance.PlatformConfig.ReorderCountDownMS);

                if (nowMS - cancelTimeMS < reorderCd)
                {
                    can = false;
                    break;
                }
            } while (false);

            if (can && doPlace)
            {
                lastPlaceSellOrderTimeMs = nowMS;
                cancelSellOrderTimeMS[currency] = nowMS;
            }
            System.Threading.Monitor.Exit(SellLocker);

            return can;
        }

        public bool CanPlaceBuyOrder(string currency, bool doPlace = false)
        {
            bool b = System.Threading.Monitor.TryEnter(BuyLocker);

            if (!b) return false;

            bool can = true;

            long nowMS = DateUtil.GetTimestampMS();

            do
            {
                uint cdCommon = Config.Instance.PlatformConfig.OrderCountDownMS;
                cdCommon = (uint)RandomUtil.GetRandom((int)cdCommon, 2 * (int)cdCommon);

                if (nowMS - lastPlaceBuyOrderTimeMs < cdCommon)
                {
                    can = false;
                    break;
                }

                currency = currency.ToUpper();
                long cancelTimeMS = cancelBuyOrderTimeMS.ContainsKey(currency) ? cancelBuyOrderTimeMS[currency] : 0;
                int reorderCd = RandomUtil.GetRandom((int)Config.Instance.PlatformConfig.ReorderCountDownMS, 2 * (int)Config.Instance.PlatformConfig.ReorderCountDownMS);

                if (nowMS - cancelTimeMS < reorderCd)
                {
                    can = false;
                    break;
                }
            } while (false);

            if (can && doPlace)
            {
                lastPlaceBuyOrderTimeMs = nowMS;
                cancelBuyOrderTimeMS[currency] = nowMS;
            }

            System.Threading.Monitor.Exit(BuyLocker);

            return can;
        }
        /*
        public void CancelAllOrder()
        {
            return; //TODO

            List<Order> orders = new List<Order>();

            this.monitor.EachBuyOrder((order) => {
                orders.Add(order);
            });

            this.monitor.EachSellOrder((order) => {
                orders.Add(order);
            });
            
            bool b = this.sellOrderPlacing;
            this.sellOrderPlacing = false;
            foreach(var o in orders)
            {
                this.CancelOrder(o,false);
            }

            this.sellOrderPlacing = b;
        }
        */
        public void CancelOrder(string currency,string side)
        {
            return; //TODO

            /*
            if (side == Side.Sell)
            {
                this.EachSellOrder((order) =>
                {

                    if (string.Compare(order.Currency, currency, true) == 0)
                    {
                        this.CancelOrder(order);
                    }
                });
            }
            else if(side == Side.Buy)
            {
                this.EachBuyOrder((order) =>
                {
                    if (string.Compare(order.Currency, currency, true) == 0)
                    {
                        this.CancelOrder(order);
                    }
                });
            }

            */

        }

        public bool CancelOrder(long orderId, bool isNoProfit, bool syncToMonitor = true)
        {
            string currency = "";
            OrderOparete side = OrderOparete.Buy;
            bool isValidOrder = false;

         
                this.monitor.EachSellOrder((o) => {
                    if (o.PublicId == orderId)
                    {
                        side = o.Side;
                        currency = o.Currency;
                        isValidOrder = true;
                    }
                });


            if (!isValidOrder)
            {
                this.monitor.EachBuyOrder((o) =>
                {
                    if (o.PublicId == orderId)
                    {
                        side = o.Side;
                        currency = o.Currency;
                        isValidOrder = true;
                    }
                });
            }

            if(isValidOrder)
            {
                long nowMS = DateUtil.GetTimestampMS();

                 int noProfitPause = RandomUtil.GetRandom(7000,15000);

                if (side == OrderOparete.Sell)
                    this.cancelSellOrderTimeMS[currency.ToUpper()] = nowMS + (isNoProfit ? noProfitPause : 0);
                if (side == OrderOparete.Buy)
                    this.cancelBuyOrderTimeMS[currency.ToUpper()] = nowMS + (isNoProfit ? noProfitPause : 0);

                okex_Order_Cancel cancelInvoke = new okex_Order_Cancel(orderId.ToString());
                JToken result = cancelInvoke.execSync();
                bool ret = result["code"].Value<int>() == 0;

                if (ret && syncToMonitor)
                {
                    monitor.RemoveOrder(orderId);
                }

                return ret;
            }

            return false;
            
        }

        public bool CancelOrder(Order order, bool isNoProfit, bool syncToMonitor = true)
        {
            return CancelOrder(order.PublicId, isNoProfit, syncToMonitor);
            
        }

        public int OrderCount
        {
            get
            {
                return this.monitor.OrderCount;
            }
        }

        private void Monitor_OnData(MonitorBase obj)
        {
            
        }

        public Task<Order> OTCPlaceBuyOrder(string currency, decimal amount, PriceTypeEnum type, decimal price, double floatRate, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
            return this.OTCPlaceOrder(currency, amount, type, Side.Buy, price, floatRate, minAmountCNY,maxAmountCny, completeRate, completeQuality,0, userAge, kycLevel);
        }

        /// <summary>
        /// 往otc市场发布挂单
        /// </summary>
        /// <param name="currency">币种</param>
        /// <param name="amount">数量</param>
        /// <param name="type"></param>
        /// <param name="side"></param>
        /// <param name="price"></param>
        /// <param name="floatRate"></param>
        /// <param name="minAmountCNY"></param>
        /// <param name="maxAmountCny"></param>
        /// <param name="completeRate"></param>
        /// <param name="completeQuality"></param>
        /// <param name="sellCompleteQuality"></param>
        /// <param name="userAge"></param>
        /// <param name="kycLevel"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public Task<Order> OTCPlaceOrder(string currency, decimal amount, PriceTypeEnum type, string side, decimal price, double floatRate, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0, int sellCompleteQuality = 0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
            return Task.Run<Order>(() =>
            {
                if ( side == Side.Sell && !CanPlaceSellOrder(currency,true))
                    return null;

                if (side == Side.Buy && !CanPlaceBuyOrder(currency,true))
                    return null;

                long nowMs = DateUtil.GetTimestampMS();

                if (side == Side.Sell)
                {
                    this.lastPlaceSellOrderTimeMs = nowMs;
                    sellOrderPlacing = true;
                }
                else if (side == Side.Buy)
                {
                    this.lastPlaceBuyOrderTimeMs = nowMs;
                    buyOrderPlacing = true;
                }

                var userType = UserType.All;

                if (side == Side.Sell && !string.IsNullOrEmpty(Config.Instance.PlatformConfig.SellOrderUserType))
                    userType = Config.Instance.PlatformConfig.SellOrderUserType;

                if (side == Side.Buy && !string.IsNullOrEmpty(Config.Instance.PlatformConfig.BuyOrderUserType))
                    userType = Config.Instance.PlatformConfig.BuyOrderUserType;

                //法币创建挂单的ajax调用
                var otcOrderCreateApi = new okex_Order_Create();
                otcOrderCreateApi.baseCurrency = currency.ToLower();
                otcOrderCreateApi.side = side;
                //otcOrderCreateApi.minKycLevel = kycLevel;
                otcOrderCreateApi.minSellOrderQuantity = sellCompleteQuality;
                otcOrderCreateApi.userType = userType;
                otcOrderCreateApi.tradePassword = Config.Instance.Account.GetReleasePassword();

               
                if (minAmountCNY > 0)
                {
                    otcOrderCreateApi.quoteMinAmountPerOrder = minAmountCNY;
                }

                if(maxAmountCny > 0)
                {
                    otcOrderCreateApi.quoteMaxAmountPerOrder = maxAmountCny;
                }


                switch (type)
                {
                    case PriceTypeEnum.Float:
                        otcOrderCreateApi.floatRate = floatRate;
                        otcOrderCreateApi.type = PriceType.FloatMarket;
                        break;
                    case PriceTypeEnum.Fixed:
                        otcOrderCreateApi.price = price.ToString("0.00");
                        otcOrderCreateApi.type = PriceType.Limit;
                        break;
                }

                long maxUserDateStamp = DateUtil.GetServerTimestampMS() - (userAge * 86400000L);
                DateTime maxUserCreateDate = DateUtil.TimestampMSToDateTime(maxUserDateStamp);

                maxUserCreateDate = new DateTime(maxUserCreateDate.Year, maxUserCreateDate.Month, maxUserCreateDate.Day,0,0,0);
                maxUserDateStamp = DateUtil.GetTimestampMS(maxUserCreateDate);
                //otcOrderCreateApi.maxUserCreatedDate = maxUserDateStamp;
                otcOrderCreateApi.baseAmount = amount;
                otcOrderCreateApi.remark = remark;

                //if (Config.Instance.Account.IsBusiness)
                //{
                    //otcOrderCreateApi.minCompletedOrderQuantity = completeQuality;
                    otcOrderCreateApi.minCompletionRate = completeRate;
                //}

                JToken result = otcOrderCreateApi.execSync();

                if (side == Side.Sell)
                {
                    sellOrderPlacing = false;
                }
                else if (side == Side.Buy)
                {
                    buyOrderPlacing = false;
                }

                int code = result["code"].Value<int>();

                if(code == 13079)//需要密码
                {

                }

                if (code == 0)
                {
                    JToken orderJson = result["data"];

                    var order = Pool<Order>.GetPool().Get();
                    long publicId = orderJson["publicId"].Value<long>();
                    order.PublicId = publicId;

                    //okex_Order_Info orderInfoApi = new okex_Order_Info(publicId);

                    order.FloatRate = floatRate;
                    order.AvailableAmount = amount;
                    order.CreatedDate = DateUtil.GetServerDateTime();
                    order.Side = side == Side.Sell ?  OrderOparete.Sell : OrderOparete.Buy;
                    order.MinKycLevel = kycLevel;
                    order.Currency = currency;
                    order.PriceType = type;
                    order.Index = 99;
                    order.Price = price;
                    order.AmountRange = new Classes.Range<decimal>(minAmountCNY, maxAmountCny);
                    order.Owner = new Trader();
                    
                    //order.ParseFromJson(orderJson);

                    AddNewOrder(order);

                    return order;
                }
                else
                {
                    Logger.Instance.LogError("create order faled " + result["msg"].Value<string>());
                }

                return null;
            });
        }

        public Task<Order> OTCPlaceSellOrder(string currency, decimal amount, PriceTypeEnum type, decimal price, double floatRate, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0,int sellCompleteQuality = 0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
            return this.OTCPlaceOrder(currency, amount, type, Side.Sell, price, floatRate, minAmountCNY,maxAmountCny, completeRate, completeQuality, sellCompleteQuality, userAge, kycLevel);
        }

        public void AddNewOrder(Order order)
        {
            this.monitor.AddNewOrder(order);
        }

        public void EachSellOrder(Action<Order> callback)
        {
            this.monitor.EachSellOrder(callback);
        }

        public void EachBuyOrder(Action<Order> callback)
        {
            this.monitor.EachBuyOrder(callback);
        }

        private static OTCOrderManager _instance = null;
        public static OTCOrderManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OTCOrderManager();

                return _instance;
            }
        }
    }
}
