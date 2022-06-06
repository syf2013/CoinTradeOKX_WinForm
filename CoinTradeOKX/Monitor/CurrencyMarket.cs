using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Classes;
using CoinTradeOKX.Okex.Behavior;
using CoinTradeOKX.Event;
using Common;

using CoinTradeOKX.Manager;
using Common.Interface;

namespace CoinTradeOKX.Monitor
{ 
    public class CurrencyMarket:IDisposable,IDepthProvider
    {
        protected CTCTickerMonitor ctcTickerMonitor = null;
        protected IWalletMonitor actWalletMonitor = null;
        protected CTCWalletRESTMonitor ctcWalletRESTMonitor = null;
        protected OTCMarketMonitor otcMarketMonitor = null;
        protected List<CTCCandleMonitor> candleMonitorList = new List<CTCCandleMonitor>();
        Instrument instrument = null;
        public event Action OnMarketChanged = null;
        public event Action OnAmountChanged = null;
        MonitorManager monitorManager = null;

        private List<BehaviorBase> Behaviors = new List<BehaviorBase>();

        public MonitorManager MonitorManager
        {
            get
            {
                return monitorManager;
            }
        }

        public decimal AmountForCTCSell {
            get;
            set;
        }


        public bool HasCTCMarket
        {
            get;
            private set;
        }

        public string Currency
        {
            get; private set;
        }

        public bool Effective
        {
            get
            {
                return this.monitorManager.AllIsEffective();
            }
        }

        /*
        public decimal AvalibleInOtcMarket
        {
            get;
            private set;
        }

        public decimal HoldInOtcMarket
        {
            get; private set;
        }
        */

        public decimal AvalibleInCtcMarket
        {
            get;
            private set;
        }

        public decimal HoldInCtcMarket
        {
            get; private set;
        }

        public decimal AvalibleInAccount
        {
            get;
            private set;
        }

        public decimal HoldInAccount
        {
            get; private set;
        }

        public decimal CTCAsk
        {
            get; private set;
        }

        public decimal CTCBid
        {
            get; private set;
        }

        public virtual decimal OTCAsk
        {
            get; private set;
        }

        public decimal OTCKyc1Ask
        {
            get; private set;
        }

        public virtual decimal OTCBid
        {
            get;
            private set;
        }

        public Instrument Instrument
        {
            get
            {
                return this.instrument;
            }
        }

        public CurrencyMarket(string currency, OkxV5APIPublic<WebSocket> socket)
            : this(currency, true, true,  socket)
        {

        }

        public void EachSellOrder(Action<Order> callback, bool reverse = false)
        {
            this.otcMarketMonitor.EachSellOrder(callback,reverse);
        }



        public CTCCandleMonitor GetCandleMonior(CandleGranularity granularity)
        {
            lock(this.candleMonitorList)
            {
                foreach(var s in this.candleMonitorList)
                {
                    if(s.Granularity == granularity)
                    {
                        return s;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 加载指定粒度K线
        /// </summary>
        /// <param name="granularity"></param>
        public void LoadCandle(CandleGranularity granularity)
        {
            var monitor = this.GetCandleMonior(granularity);

            if(monitor == null)
            {
                monitor = new CTCCandleMonitor(this.Currency, USDXMarket.Instance.Currency, granularity);
                lock (this.candleMonitorList)
                {
                    this.monitorManager.AddMonotor(monitor);
                    this.candleMonitorList.Add(monitor);
                }
            }
        }

        public void UnloadCandle(CandleGranularity granularity)
        {
            var monitor = this.GetCandleMonior(granularity);

            if (monitor != null)
            {
      
                lock (this.candleMonitorList)
                {
                    this.monitorManager.RemoveMonitor(monitor);
                    this.candleMonitorList.Remove(monitor);
                }
            }
        }

        public void EachBuyOrder(Action<Order> callback)
        {
            this.otcMarketMonitor.EachBuyOrder(callback);
        }

        public CurrencyMarket(string currency, bool hasCTCMarket,bool hasOTCMarket, OkxV5APIPublic<WebSocket> socket)
        {
            this.Currency = currency;

            this.instrument = InstrumentTableVsUSDT.Instance.GetInstrument(currency);

            //if (this.instrument == null)
            //{
            //    throw new Exception("invalid currency");
            //}

            this.monitorManager = new MonitorManager();


            if (hasCTCMarket)
            {
                ctcTickerMonitor = new CTCTickerMonitor(socket, currency);
                ctcTickerMonitor.OnData += CtcTickerMonitor_OnData;
                monitorManager.AddMonotor(ctcTickerMonitor);
            }

            this.HasCTCMarket = hasCTCMarket;

            if (hasOTCMarket)
            {
                otcMarketMonitor = new OTCMarketMonitor(currency);
                otcMarketMonitor.OnData += OtcMarketMonitorOnData; ;
                monitorManager.AddMonotor(otcMarketMonitor);
            }

            var marketType = Config.Instance.Account.MarketType;
            bool isOtc = marketType == MarketTypeEnum.None || marketType == MarketTypeEnum.OTCMarket;


            if (isOtc)
                actWalletMonitor = new OTCWalletMonitor(currency);
            else if(Config.Instance.Account.MarketType == MarketTypeEnum.CTCMarket)
                actWalletMonitor = new ACTWalletMonitor(currency);

            if (actWalletMonitor != null)
            {
                (actWalletMonitor as MonitorBase).OnData += ActWalletMonitor_OnData;
                monitorManager.AddMonotor(actWalletMonitor as MonitorBase);
            }

#if OKEX_API_V5
            ctcWalletRESTMonitor = new CTCWalletRESTMonitor(currency);
            ctcWalletRESTMonitor.OnData += CtcWalletRESTMonitor_OnData;

            monitorManager.AddMonotor(ctcWalletRESTMonitor);

            this.ctcSellCountDown = new CountDown(this.ctcWalletRESTMonitor.Interval + 300, false);
#else
            ctcWalletRESTMonitor = new CTCWalletRESTMonitor(currency);
            ctcWalletRESTMonitor.OnData += CtcWalletRESTMonitor_OnData;

            monitorManager.AddMonotor(ctcWalletRESTMonitor);

            this.ctcSellCountDown = new CountDown(this.ctcWalletRESTMonitor.Interval + 300, false);
#endif

        }

        private void CtcTickerMonitor_OnData(MonitorBase obj)
        {
            this.CTCAsk = ctcTickerMonitor.Ask;
            this.CTCBid = ctcTickerMonitor.Bid;

            lock(this.candleMonitorList)
            {
                foreach(var m in this.candleMonitorList)
                {
                    m.UpdateLastPrice((this.CTCAsk + this.CTCBid) * 0.5m, DateUtil.GetServerUTCDateTime());
                }
            }

            this.Market_Change();
        }

        private object transferLocker = new object();

        private Okex_Rest_Api_TransferV5 transferApi = null;

        public  void CurrencyTrensfer(WalletType from, WalletType to,decimal amount)
        {
            lock(transferLocker)
            {
                if (from == to)
                    return;

                if (amount <= 0)
                    return;

                if (transferApi == null)
                {
                    transferApi = new Okex_Rest_Api_TransferV5(Currency, from, to);
                }
                else
                {
                    transferApi.from = (int)from;
                    transferApi.to = (int)to;
                }

                decimal transferAmount = amount;
                EventCenter.Instance.Emit(EventNames.AmountAllocation, this.Currency.ToUpper());
                switch(from)
                {
                    case WalletType.Account:
                        transferAmount = Math.Min(this.AvalibleInAccount, transferAmount);
                        this.AvalibleInAccount -= transferAmount;
                        break;
                    case WalletType.Unified:
                        transferAmount = Math.Min(this.AvalibleInCtcMarket, transferAmount);
                        this.AvalibleInCtcMarket -= transferAmount;
                        break;
                }

                if (transferAmount > 0)
                {
                    transferApi.amt = transferAmount;
                    transferApi.execAsync();
                }
            }
        }


        public void AddBehavior<T>(T behavior) where T:BehaviorBase
        {
            foreach(var t in this.Behaviors)
            {
                if( t is T)
                {
                    return;
                }
            }
  
            BehaviorConfig. LoadBehaviorConfig(this.Currency.ToLower(), behavior);

            this.Behaviors.Add(behavior);
        }

        public void EnableAllBehavior()
        {
            foreach(var t in this.Behaviors)
            {
                t.Enable = true;
            }
        }

        public void DisbaleAllBehavior()
        {
            foreach(var t in this.Behaviors)
            {
                t.Enable = false;
            }
        }

        public T GetBehavior<T>() where T:BehaviorBase
        {
            foreach(var t in this.Behaviors)
            {
                if( t is T)
                {
                    return t as T;
                }
            }

            return null;
        }

        public List<BehaviorBase> GetBehaviorList()
        {
            return this.Behaviors;
        }

        private void ActWalletMonitor_OnData(MonitorBase obj)
        {
            bool isChange = this.AvalibleInAccount != this.actWalletMonitor.Availible;

            this.AvalibleInAccount = actWalletMonitor.Availible;
            this.HoldInAccount = actWalletMonitor.Hold;

            if (isChange)
                this.AvalibleChange();
        }

        private void CtcWalletRESTMonitor_OnData(MonitorBase obj)
        {
            bool isChange = this.AvalibleInCtcMarket != this.ctcWalletRESTMonitor.Availible;
            this.AvalibleInCtcMarket = this.ctcWalletRESTMonitor.Availible;
            this.HoldInCtcMarket = this.ctcWalletRESTMonitor.Hold;

           if(isChange)
                this.AvalibleChange();
        }

        /// <summary>
        /// 重新同步交易账户持仓
        /// </summary>
        public void SyncCTCAMounts()
        {
            this.ctcWalletRESTMonitor.Update((int)(this.ctcWalletRESTMonitor.Interval + 1u));
        }

        public void Update(int dt)
        {
            this.monitorManager.Update(dt);
        }

        protected virtual void OtcMarketMonitorOnData(MonitorBase obj)
        {
            this.OTCAsk = this.otcMarketMonitor.Ask;
            this.OTCBid = this.otcMarketMonitor.Bid;
            this.Market_Change();
        }

        private void AvalibleChange()
        {
            this.OnAmountChanged?.Invoke();
        }

        private void Market_Change()
        {

            this.CheckForSell();

            this.OnMarketChanged?.Invoke();
        }


        public decimal TotalMoney
        {
            get
            {
                return (AvalibleInCtcMarket + HoldInCtcMarket + HoldInAccount + AvalibleInAccount) * this.OTCBid;
            }
        }

        
        public bool CanPlaceSellOrder()
        {
            return OTCOrderManager.Instance.CanPlaceSellOrder(this.Currency);
        }


        public Task<Order> OTCPlaceSellOrder(decimal amount,  PriceDetail priceDetail, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0, int sellCompleteQuality = 0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
            return this.OTCPlaceSellOrder(amount,priceDetail.Type , priceDetail.Price, priceDetail.FloatRate , minAmountCNY, maxAmountCny, completeRate, completeQuality, sellCompleteQuality, userAge, kycLevel, remark);
        }


        public Task<Order> OTCPlaceSellLimitOrder(decimal amount, decimal price, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float  completeRate = 0.0f, int completeQuality = 0,int sellCompleteQuality=0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
           return  this.OTCPlaceSellOrder(amount, PriceTypeEnum.Fixed, price, 1.0,  minAmountCNY,maxAmountCny, completeRate, completeQuality, sellCompleteQuality, userAge, kycLevel, remark);
        }



        public Task<Order> OTCPlaceSellOrder(decimal amount, PriceTypeEnum type, decimal price, double floatRate, decimal minAmountCNY = 0,decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0, int sellCompleteQuality=0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
            return OTCOrderManager.Instance.OTCPlaceSellOrder(this.Currency, amount, type, price, floatRate, minAmountCNY,maxAmountCny, completeRate, completeQuality,sellCompleteQuality, userAge, kycLevel, remark);
        }


        public Task<Order> OTCPlaceSellFloatOrder(decimal amount, float floatRate, decimal minAmountCNY = 0, decimal maxAmountCny = 2000000, float completeRate = 0.0f, int completeQuality = 0,int sellCompleteQuality =0, int userAge = 0, int kycLevel = 1, string remark = "")
        {
           return this.OTCPlaceSellOrder(amount, PriceTypeEnum.Float, 0, floatRate, minAmountCNY,maxAmountCny, completeRate, completeQuality, sellCompleteQuality, userAge, kycLevel, remark);
        }


        public void OTCPlaceBuyOrder(decimal mount, decimal price,string remark)
        {

        }

        async public void BuyFromCTCMarketWithAmountV5(decimal amount)
        {
            string instId = string.Format("{0}-{1}", this.Currency, Config.Instance.Anchor).ToUpper();
            Okex_Rest_Api_CTCBuyNowV5 api = new Okex_Rest_Api_CTCBuyNowV5(instId, amount);
            JToken result = await api.exec();
        }

        /// <summary>
        /// 加入抛出到币币市场的数量
        /// </summary>
        /// <param name="amount"></param>
        public void AddAmountForCTCSell(decimal amount)
        {
            lock (AmountForCTCSellLocker)
            {
                this.AmountForCTCSell += amount;
            }
        }

        private object AmountForCTCSellLocker = new object();
        private CountDown ctcSellCountDown = null;

        /// <summary>
        /// 检查是否有需要抛出币币市场的币，以及当前币币钱包是否有足够的数量
        /// </summary>
        private void CheckForSell()
        {
            if (this.HasCTCMarket)
            {
                lock (AmountForCTCSellLocker)
                {
                    if (ctcSellCountDown.Check())
                    {
                        var instrument = Instrument;
                        lock (AmountForCTCSellLocker)
                        {
                            if (this.AmountForCTCSell > instrument.MinSize && AvalibleInCtcMarket > instrument.MinSize)
                            {
                                decimal sellAmount = Math.Min(AmountForCTCSell, AvalibleInCtcMarket);
                                this.AmountForCTCSell = Math.Max(0, this.AmountForCTCSell - sellAmount);
                                SellToCTCMarketWithAmount(sellAmount,true);
                            }
                        }
                    }
                }
            }
        }

        async public void BuyFromCTCMarketWithAmount(decimal amount, decimal anchorAvalible, bool immediately)
        {

            string instId = string.Format("{0}-{1}", this.Currency, Config.Instance.Anchor).ToUpper();

            //检查是否刚好有需要抛出到币币市场的量，如果有的话扣除
            lock (AmountForCTCSellLocker)
            {
                amount -= this.AmountForCTCSell;

                this.AmountForCTCSell = Math.Max(0, this.AmountForCTCSell - amount);

                if (amount < Instrument.MinSize)
                {
                    return;
                }
            }

            var coinAmount = amount;

            //

            Okex_Rest_Api_CTCOrderV5 api;
            amount = amount * this.CTCAsk;//转为USDT数量

            //币币市场挂单
            if (immediately)//马上成交、否则是挂单方式
            {
                api = new Okex_Rest_Api_CTCBuyNowV5(instId, amount);
            }
            else
            {
                api = new Okex_Rest_Api_CTCOrderV5(instId, OrderOparete.Buy);
                api.px = ctcTickerMonitor.Ask.ToString();
                api.sz = amount;
            }
 
            var result = await api.exec();

            if (result["code"].Value<int>() != 0)
            {
                Logger.Instance.LogError("币币下单失败 " + result.ToString());
            }
            else
            {
                if (immediately)
                {
                    this.AvalibleInCtcMarket += coinAmount;
                }
            }
        }

        //币币市场抛出
        async public void SellToCTCMarketWithAmount(decimal amount,bool immediately)
        {
            amount = Math.Min(amount, this.AvalibleInCtcMarket);

            string instId = string.Format("{0}-{1}", this.Currency, Config.Instance.Anchor).ToUpper();

            var coinAmount = amount;

            if (amount > this.instrument.MinSize)
            {
                Okex_Rest_Api_CTCOrderV5 api = null;


                if (immediately)
                {
                    api = new Okex_Rest_Api_CTCSellNowV5(instId, amount);

                }
                else
                {
                    api = new Okex_Rest_Api_CTCOrderV5(instId, OrderOparete.Sell);
                    api.sz = amount;
                    api.px = this.CTCBid.ToString();
                }

                JToken result = await api.exec();
                this.AvalibleInCtcMarket -= coinAmount;
            }
        }

        public void Dispose()
        {
            this.DisbaleAllBehavior();

            foreach(var t in this.Behaviors)
            {
                t.Dispose();
            }
            this.monitorManager.DestoryAllMonitors();
        }

        public virtual void EachDeep(SideEnum side, Action<DepthInfo> callback)
        {
            if(this.HasCTCMarket)
            {
                this.ctcTickerMonitor.EachDeep(side, callback);
            }
        }
    }

    public class USDXMarket : CurrencyMarket
    {
        public decimal BidAvg
        {
            get
            {
                decimal totalAmount = 0;
                decimal totalMoney = 0;
                this.otcMarketMonitor.EachBuyOrder((order) =>
                {
                    totalAmount += order.AvailableAmount;
                    totalMoney += (order.AvailableAmount * order.Price);
                });

                return totalAmount != 0 ? totalMoney / totalAmount : 0;
            }
        }

        public decimal AskAvg
        {
            get
            {
                decimal totalAmount = 0;
                decimal totalMoney = 0;
                this.otcMarketMonitor.EachSellOrder((order) =>
                {
                    totalAmount += order.AvailableAmount;
                    totalMoney += (order.AvailableAmount * order.Price);
                });

                return totalAmount != 0 ? totalMoney / totalAmount : 0;
            }
        }

        public decimal TotalBidAmount
        {
            get
            {
                decimal totalAmount = 0;
                this.otcMarketMonitor.EachBuyOrder((order) =>
                {
                    totalAmount += order.AvailableAmount;
                });

                return totalAmount;
            }
        }

        public override decimal OTCAsk
        {
            get
            {
                if (Config.Instance.Account.MarketType == MarketTypeEnum.CTCMarket)
                    return Config.Instance.ExchangeRate;
                return base.OTCAsk;
            }
        }

        public override decimal OTCBid
        {
            get
            {
                if (Config.Instance.Account.MarketType == MarketTypeEnum.CTCMarket)
                    return Config.Instance.ExchangeRate;
                return base.OTCBid;
            }
        }


        public decimal TotalAskAmount
        {
            get
            {
                decimal totalAmount = 0;
                this.otcMarketMonitor.EachSellOrder((order) =>
                {
                    totalAmount += order.AvailableAmount;
                });

                return totalAmount;
            }
        }

        public decimal BidAmount
        {
            get
            {

                decimal totalAmount = 0;
                this.otcMarketMonitor.EachBuyOrder((order) =>
                {
                    if (order.Price == OTCBid)
                        totalAmount += order.AvailableAmount;
                });

                return totalAmount;
            }

        }

        public decimal AskAmount
        {
            get
            {
                decimal totalAmount = 0;
                this.otcMarketMonitor.EachSellOrder((order) =>
                {
                    if(order.Price == OTCAsk)
                        totalAmount += order.AvailableAmount;
                });

                return totalAmount;
            }
        }

        private const decimal USDX_Trim = (decimal)0.01; //USDX价格修正

        private decimal GetValidPriceWithAmountAndOrderCount(decimal anchorSize,uint orderCount, string side)
        {
            decimal moneyTotal = 0;
            decimal amountTotal = 0;
            decimal samplePrice = 0;
            int orderTotal = 0;

            Action<Order> orderCallback = (order) => {
                if (orderTotal >= orderCount && amountTotal >= anchorSize)
                    return;

                if(orderTotal < orderCount && order.Price != samplePrice)//单数不足价格跳变， 重新采样
                {
                    samplePrice = order.Price;
                    moneyTotal = 0;
                    amountTotal = 0;
                    orderTotal = 0;
                }
                moneyTotal += order.AvailableAmount * order.Price;
                orderTotal++;
                amountTotal += order.AvailableAmount;
            };

            if (side == Side.Sell)
            {
                this.otcMarketMonitor.EachSellOrder(orderCallback, true);
            }
            else if(side == Side.Buy)
            {
                this.otcMarketMonitor.EachBuyOrder(orderCallback,false);
            }

            if(amountTotal >= anchorSize && orderTotal >= orderCount)
            {
                return Math.Round(moneyTotal / amountTotal, 2);
            }

            decimal ret = side == Side.Sell ? this.AskAvg : this.BidAvg;

            if(ret <= 0)
            {
                throw new Exception("错误的锚定币价格");
            }

            return ret;
        }

        private static bool IsOtc()
        {
            return Config.Instance.Account.MarketType == MarketTypeEnum.OTCMarket || Config.Instance.Account.MarketType == MarketTypeEnum.None;
        }
    
        private USDXMarket(string currency)
            : base(currency,false, IsOtc(), null)
        {

        }

        public decimal GetPriceFromHistory(DateTime time)
        {
            OTCHistoryManager.Instance.GetPriceWithTime(this.Currency, time);
            return 0;
        }

        private static USDXMarket _instance = null;

        private static object locker = new object();
        /**
         * 锚定币市场
         */ 
        public static void CreateMarketInstance(string anchorCurrency)
        {
            if (_instance == null)
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new USDXMarket(anchorCurrency);
                    }
                }
            }
        }

        private List< DepthInfo> sellDeep = new List<DepthInfo>();
        private List< DepthInfo> buyDeep = new List<DepthInfo>();


        /// <summary>
        /// 循环深度列表
        /// </summary>
        /// <param name="side">方向</param>
        /// <param name="callback">回调</param>
        public override void EachDeep(SideEnum side, Action<DepthInfo> callback)
        {
            var list = side == SideEnum.Sell ? sellDeep : buyDeep;
            lock (list)
            {
                foreach (var s in list)
                {
                    callback(s);
                }
            }
        }

        /// <summary>
        /// 获取卖出价
        /// </summary>
        /// <param name="minAmount"></param>
        /// <returns></returns>
        public decimal GetOTCAsk()
        {
            //如果是币币版采用固定汇率的方式， 计算USDX的深度和价格
            if(Config.Instance.Account.MarketType == MarketTypeEnum.CTCMarket)
            {
                return Config.Instance.ExchangeRate;
            }

            decimal ask = GetValidPriceWithAmountAndOrderCount(Config.Instance.AnchorSize, Config.Instance.AnchorOrder , Side.Sell);
            decimal bid = GetValidPriceWithAmountAndOrderCount(Config.Instance.AnchorSize, Config.Instance.AnchorOrder, Side.Buy);
            return  Math.Max(ask, bid);
        }

        protected override void OtcMarketMonitorOnData(MonitorBase obj)
        {
            base.OtcMarketMonitorOnData(obj);

            var pool = Pool<DepthInfo>.GetPool();

            Func<Order, IList<DepthInfo>, DepthInfo> addOrder = (Order order, IList<DepthInfo> list) =>
            {
                int index = list.Count;
                DepthInfo dp = null;
                foreach (var d in list)
                {
                    if (d.Price == order.Price)
                    {
                        dp = d;
                        break;
                    }

                    if (d.Price > order.Price)
                    {
                        index = Math.Min(index, list.IndexOf(d));
                    }
                }

                if (dp == null)
                {
                    dp = pool.Get();
                    dp.Price = order.Price;
                    dp.Orders = 0;
                    dp.Total = 0;

                    if (index > list.Count)
                        list.Add(dp);
                    else
                        list.Insert(index, dp);
                }

                dp.Total += order.AvailableAmount;
                dp.Orders++;
                return dp;
            };

            lock (this.sellDeep)
            {
                pool.Put(sellDeep);
                this.sellDeep.Clear();

                EachSellOrder((order) =>
                {
                    addOrder(order, sellDeep);
                });
            }

            lock (this.buyDeep)
            {

                pool.Put(buyDeep);
                this.buyDeep.Clear();

                EachBuyOrder((order) =>
                {
                    addOrder(order, buyDeep);
                });

                buyDeep.Reverse();
            }
        }


        /// <summary>
        /// 获取收购价
        /// </summary>
        /// <param name="minAmount"></param>
        /// <returns></returns>
        public decimal GetOTCBid()
        {
            //如果是币币版采用固定汇率的方式， 计算USDX的深度和价格
            if (Config.Instance.Account.MarketType == MarketTypeEnum.CTCMarket)
            {
                return Config.Instance.ExchangeRate;
            }

            decimal bid = GetValidPriceWithAmountAndOrderCount(Config.Instance.AnchorSize, Config.Instance.AnchorOrder, Side.Buy);
            decimal ask = GetValidPriceWithAmountAndOrderCount(Config.Instance.AnchorSize, Config.Instance.AnchorOrder, Side.Sell);
            return Math.Min(bid, ask);
        }

        public static USDXMarket Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
