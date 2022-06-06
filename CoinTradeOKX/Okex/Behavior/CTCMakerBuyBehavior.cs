using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Behavior
{
    [BehaviorName(Name = "做市商买策略")]
    public class CTCMakerBuyBehavior : SpotBehaviorBase
    {
        private OrderBase BuyOrder = null;

        public override string Message
        {
            get
            {
                return string.Format("HTTP延迟 {0:0.00000}", Http.Timeout);
            }
        }

        private decimal _maxHold = 0;

        [BehaviorParameter(Name = "最大持仓（个）", Min = 0, Max = double.MaxValue)]
        public decimal MaxHold
        {
            get
            {
                return _maxHold;
            }
            set
            {
                if (value < instrument.MinSize)
                    throw new Exception("最大持仓数量不能小于" + instrument.MinSize);

                _maxHold = value;
            }
        }

        /*

        [BehaviorParameter(Name ="上停止价格")]
        public decimal StopPriceUp
        {
            get;set;
        }

        [BehaviorParameter(Name = "下停止价格")]
        public decimal StopPriceDown
        {
            get;set;
        }
        */



     

        [BehaviorParameter(Name = "买单最大数量")]
        public decimal BuyMaxSize
        {
            get; set;
        }


        /// <summary>
        /// 买挂单利润
        /// </summary>
        [BehaviorParameter(Name = "买挂单利润", Min = 0, Max = 1)]
        public decimal BuyProfitMax
        {
            get; set;
        }

        /// <summary>
        /// 买撤单利润
        /// </summary>
        [BehaviorParameter(Name = "买撤单利润", Min = -0.1, Max = 1)]
        public decimal BuyProfitMin
        {
            get; set;
        }

        [BehaviorParameter(Name = "手续费率", Min = 0, Max = 1)]
        public decimal Fee
        {
            get; set;
        }

        private uint _orderCountDown = 200;
        [BehaviorParameter(Name = "挂单冷却", Intro = "两次挂单之间的时间间隔，单位毫秒，最小为1", Min = 1, Max = 1000000)]
        public uint OrderCountDown
        {
            get
            {
                return _orderCountDown;
            }
            set
            {
                this._orderCountDown = value;
                this.buyOrderCountDown.Interval = _orderCountDown;
            }
        }


        /*
           [BehaviorParameter(Name = "使用均线")]
           public bool UseMA
           {
               get; set;
           }


           CandleGranularity _candle;
           [BehaviorParameter(Name = "K线周期", Dependent = "UseMA", DependentValue = true)]
           public CandleGranularity Candle
           {
               get { return _candle; }
               set
               {
                   if (_candle != value)
                   {
                       this.market.UnloadCandle(this._candle);
                       _candle = value;
                       this.market.LoadCandle(value);
                   }
               }
           }


           int maPeriod = 5;
           [BehaviorParameter(Name = "均线周期", Dependent = "UseMA", DependentValue = true, Min = 5, Max = 1000)]
           public int MAPeriod
           {
               get { return maPeriod; }
               set { maPeriod = value; }
           }

           int maSample = 5;
           [BehaviorParameter(Name = "均线采样", Dependent = "UseMA", DependentValue = true, Min = 5, Max = 1000)]
           public int MASample
           {
               get { return maSample; }
               set { maSample = value; }
           }
           */


        Instrument instrument = null;


        CurrencyMarket market = null;
        USDXMarket anchorMarket = null;

        public CTCMakerBuyBehavior(CurrencyMarket market, USDXMarket anchorMarket)
            :base(market, anchorMarket)
        {

            this.Fee = 0.001m;

            this.market = market;
            this.anchorMarket = anchorMarket;
            //this._candle = CandleGranularity.M1;

            market.OnMarketChanged += this.MarketChange;

            instrument = market.Instrument;


            if (instrument == null)
            {
                Logger.Instance.LogError(market.Currency + "instrument 获取失败");

                return;
            }

            this.BuyProfitMin = 0.001m;
            this.BuyProfitMax = 0.003m;
            this.MaxHold = instrument.MinSize * 100;
            this.BuyMaxSize = instrument.MinSize;

            EventCenter.Instance.AddEventListener(EventNames.AmountAllocation, this.OnAmountAllocation);
            this.OrderCountDown = this._orderCountDown;
        }

        private void ResyncOrders()
        {
            CTCOrderManager.Instance.SyncOrders();
        }

        CountDown buyOrderCountDown = new CountDown(200, true);

        private object executeLocker = new object();
        bool canExecute = true;
        int volatility = 0;
        int second = 0;
        private void MarketChange()
        {

            int sec = DateTime.Now.Second;

            if(second == sec)
            {
                volatility++;
            }
            else
            {
                volatility = 0;
                second = sec;
            }

            
            if (instrument == null)
                return;

            lock (executeLocker)
            {
                if (!canExecute)
                    return;
                canExecute = false;
            }

            this.Executing = false;

            try
            {
                if (!(this.market.Effective) || !this.Enable)
                {
                    this.CancelAllOrders();
                    this.canExecute = true;
                    goto label_exit;
                }

                this.CheckAmounts();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }

            label_exit:
            this.Executing = false;

        }

        private string instrumentId = null;
        private string InstrumentId
        {
            get
            {
                if (string.IsNullOrEmpty(instrumentId))
                {
                    instrumentId = string.Format("{0}-{1}", this.market.Currency, anchorMarket.Currency).ToUpper(); ;
                }

                return this.instrumentId;
            }
        }

        private void CancelOrder(long id, string reason = null)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                Logger.Instance.LogDebug(string.Format("取消挂单{0} {1}", this.instrumentId, reason));
            }
            CTCOrderManager.Instance.CancelOrder(instrumentId, id);
        }

         decimal lastBid = 0;

        /// <summary>
        /// 检查持仓情况
        /// </summary>
        private async void CheckAmounts()
        {
            decimal askPrice = market.CTCAsk;
            decimal bidPrice = market.CTCBid;

            if (!this.Enable)
                goto label_exit;

            /*
            #region 触发上下停止价格
            if(StopPriceDown != 0 && bidPrice <= StopPriceDown)
            {
                goto label_exit;
            }

            if (StopPriceUp != 0 && bidPrice >= StopPriceUp)
            {
                goto label_exit;
            }
            #endregion
            */
            bool needResync = false;
            bool isFindOrder = false;
            string instrumentId = this.InstrumentId;

            #region 重新同步挂单数据

            CTCOrderManager.Instance.EachBuyOrder((order) =>
            {
                if (string.Compare(instrumentId, order.InstrumentId, true) == 0)
                {
                    if (BuyOrder != null && BuyOrder.PublicId == order.PublicId)
                    {
                        this.BuyOrder.CopyFrom(order);
                        isFindOrder = true;
                    }
                    else
                    {
                        CancelOrder(order.PublicId, "撤销不同步买单");
                        needResync = true;
                    }
                }
            });

            if (BuyOrder != null && !isFindOrder)
            {
                TimeSpan ts = DateTime.UtcNow - BuyOrder.CreatedDate;
                if (ts.TotalSeconds >= 5) //挂单同步可能有问题
                {
                    BuyOrder = null;
                    goto label_exit;
                }
            }


            #endregion

            if (lastBid > 0 && bidPrice / lastBid - 1 >= this.BuyProfitMax)//检测到价格向上跳变，取消挂单，并进行3次冷却
            {
                if (BuyOrder != null)
                {
                    CancelOrder(BuyOrder.PublicId, "价格向上跳变超过阈值");
                    BuyOrder = null;
                }

                this.buyOrderCountDown.Skip(3);
            }

            //decimal midPrice = (market.CTCAsk + market.CTCBid) * .5m;

            decimal avalibleAmount = this.market.AvalibleInCtcMarket;//没有挂单的金额
            decimal holdAmount = this.market.HoldInCtcMarket; //已挂单的额度
            decimal amountCount = avalibleAmount + holdAmount; //总持仓
            decimal minSize = instrument.MinSize;

            //decimal maDiff = 0;//根据当前均线算出来的均线头尾差价

            /*
            if (this.UseMA)
            {
                var maList = this.GetMA();
                decimal first = maList != null && maList.Count > 0 ? maList[0] : 0;
                decimal last = 0;

                foreach(decimal d in maList)
                {
                    if (d > 0)
                    {
                        last = d;
                    }
                    else
                    {
                        break;
                    }
                }
                if (first > 0)
                {
                    maDiff = (first - last) / first;
                }
            }
            */


            #region 检查买入挂单
            decimal buyPrice = Math.Round(bidPrice * (1.0m - Fee - BuyProfitMax), instrument.TickSizeDigit);
            decimal maxBuyPrice = Math.Round(bidPrice * (1.0m - Fee - BuyProfitMin), instrument.TickSizeDigit);
            decimal amountDiff = this.MaxHold - amountCount;
            OrderBase buyOrder = this.BuyOrder;
            decimal cashAvalible = anchorMarket.AvalibleInCtcMarket; //剩余的USDX 
            decimal canBuyAmount = cashAvalible / buyPrice + (buyOrder != null ? buyOrder.AvailableAmount : 0); //剩余USDT可购买数量

            amountDiff = Math.Min(BuyMaxSize, Math.Min(amountDiff, canBuyAmount));
            //Logger.Instance.LogDebug("Begin Check Buy Order " + instrumentId + " amount diff " + amountDiff);
            if (amountDiff > instrument.MinSize)
            {
                //需要挂单购买的条件
                if (buyOrder == null
                    || (buyOrder.Price - maxBuyPrice) > 0// instrument.TickSize
                    || (buyPrice - buyOrder.Price) > 0 //instrument.TickSize
                                                       //|| Math.Abs(buyOrder.AvailableAmount - avalibleAmount) > instrument.MinSize
                    )
                {
                    if (buyOrder != null)
                    {
                        bool ret = await CTCOrderManager.Instance.ChangeOrder(buyOrder.PublicId, this.market.Currency, amountDiff, buyPrice, true);

                        if (!ret)
                        {
                            this.BuyOrder = null;
                            buyOrder = null;
                        }
                        else
                        {
                            buyOrder = await CTCOrderManager.Instance.QueryOrder(buyOrder.PublicId, instrumentId, true);
                            this.BuyOrder = buyOrder;
                            if (buyOrder != null)
                                Assert(string.Compare(buyOrder.InstrumentId, instrumentId, true) == 0);
                            //needResync =buyOrder != null;
                        }
                    }

                    if (buyOrder == null && buyOrderCountDown.Check())
                    {
                        buyOrder = await CTCOrderManager.Instance.PlaceOrder(market.Currency, amountDiff, buyPrice, OrderOparete.Buy, true);

                        if (buyOrder != null)
                        {
                            Assert(string.Compare(buyOrder.InstrumentId, instrumentId, true) == 0);
                            BuyOrder = buyOrder;
                            needResync = true;
                        }
                    }
                }
            }
            else
            {
                //Logger.Instance.LogDebug("Amount diff  " + instrumentId + " " + amountDiff + " avalible cash " + cashAvalible);
                if (BuyOrder != null)
                {
                    CancelOrder(BuyOrder.PublicId, "已满仓，撤销买单");
                    BuyOrder = null;
                }
            }

            #endregion


            if (needResync)
            {
                this.Executing = true;
                this.market.SyncCTCAMounts();
                this.ResyncOrders();
                //Thread.Sleep(100);//等待数据同步
            }

            label_exit:
            lastBid = bidPrice;
            canExecute = true;

        }


        /*
        private IList<decimal> GetMA()
        {
            uint maCount = (uint)MAPeriod;
            uint maSize = (uint)MASample;
            var cm = this.market.GetCandleMonior(this.Candle);

            if (cm == null)
            {
                return null;
            }

            var maList = IndexUtil.MA(cm, maCount, maSize);
            return maList;
        }
        */


        private decimal GetAvg(IList<decimal> priceList)
        {
            decimal a = 0;

            foreach (var p in priceList)
            {
                a += p;
            }

            a /= priceList.Count;

            return a;
        }

        private void OnAmountAllocation(object obj)
        {
            if (string.Compare(this.market.Currency, obj.ToString(), true) == 0)
            {

            }
        }

        /// <summary>
        /// 撤销全部挂单
        /// </summary>
        public void CancelAllOrders()
        {
            #region 撤销全部挂单
            string instrumentId = string.Format("{0}-{1}", this.market.Currency, anchorMarket.Currency).ToUpper();

            CTCOrderManager.Instance.EachBuyOrder((order) =>
            {
                if (string.Compare(instrumentId, order.InstrumentId, true) == 0)
                {

                    CancelOrder(order.PublicId);
                }
            });


            BuyOrder = null;

            #endregion
        }


        public override void Dispose()
        {

            this.CancelAllOrders();
            market.OnMarketChanged -= this.MarketChange;

            //this.market.UnloadCandle(this._candle);

            EventCenter.Instance.RemoveListener(EventNames.AmountAllocation, this.OnAmountAllocation);
            base.Dispose();
        }
    }
}

