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

    [BehaviorName(Name = "套利策略")]
    public class CTCArbitrageBehavior:BehaviorBase
    {
        private void Assert(bool condition)
        {
            if (!condition)
            {
                throw new Exception(" condition is false ");
            }
        }

        private OrderBase SellOrder = null;
        private OrderBase BuyOrder = null;

        public override string Message
        {
            get
            {
                return string.Format("HTTP延迟 {0:0.000}", Http.Timeout);
            }
        }

        private decimal _maxHold = 0;

        [BehaviorParameter(Name = "持仓（个）", Min = 0, Max = double.MaxValue)]
        public decimal MaxHold
        {
            get
            {
                return _maxHold;
            }
            set
            {
                if (value < 0)
                    throw new Exception("invalide param MaxHold");

                _maxHold = value;
            }
        }

        [BehaviorParameter(Name = "基准价格")]
        public decimal BasePrice
        {
            get;set;
        }

        [BehaviorParameter(Name = "买入警戒价格上限")]
        public decimal AlertPriceUp
        {
            get;set;
        }

        [BehaviorParameter(Name = "买入警戒价格下限")]
        public decimal AlertPriceDown
        {
            get;set;
        }



        [BehaviorParameter(Name = "卖单最大数量")]
        public decimal SellMaxSize
        {
            get;
            set;
        }


        /// <summary>
        /// 挂单利润
        /// </summary>
        [BehaviorParameter(Name = "卖挂单利润", Min = 0, Max = 1)]
        public decimal SellProfitMax
        {
            get; set;
        }

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
                this.sellOrderCountDown.Interval = _orderCountDown;
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

        public CTCArbitrageBehavior(CurrencyMarket market, USDXMarket anchorMarket)
            : base()
        {
            this.BuyProfitMax = this.SellProfitMax = 0.002m;
            this.Fee = 0.001m;

            this.market = market;
            this.anchorMarket = anchorMarket;
            //this._candle = CandleGranularity.M1;

            market.OnMarketChanged += this.MarketChange;

            instrument = market.Instrument;
            this.SellMaxSize = this.BuyMaxSize = instrument.MinSize;

            if (instrument == null)
            {
                Logger.Instance.LogError(market.Currency + "instrument 获取失败");
            }


            EventCenter.Instance.AddEventListener(EventNames.AmountAllocation, this.OnAmountAllocation);
            this.OrderCountDown = this._orderCountDown;
        }

        private void ResyncOrders()
        {
            CTCOrderManager.Instance.SyncOrders();
        }

        CountDown buyOrderCountDown = new CountDown(200, true);
        CountDown sellOrderCountDown = new CountDown(200, true);

        private object executeLocker = new object();
        bool canExecute = true;
        private void MarketChange()
        {
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

        decimal lastAsk = 0;
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

            bool needResync = false;
            bool isFindOrder = false;
            string instrumentId = this.InstrumentId;

            #region 重新同步挂单数据


            CTCOrderManager.Instance.EachSellOrder((order) =>
            {
                if (string.Compare(instrumentId, order.InstrumentId, true) == 0)
                {
                    if (SellOrder != null && SellOrder.PublicId == order.PublicId)
                    {

                        this.SellOrder.CopyFrom(order);

                        Assert(string.Compare(order.InstrumentId, instrumentId, true) == 0);

                        isFindOrder = true;
                    }
                    else
                    {
                        CancelOrder(order.PublicId, "撤销不同步卖单");
                        needResync = true;
                    }
                }
            });

            if (SellOrder != null && !isFindOrder)
            {
                TimeSpan ts = DateTime.UtcNow - SellOrder.CreatedDate;
                if (ts.TotalSeconds >= 5) //挂单同步可能有问题
                {
                    SellOrder = null;
                    goto label_exit;
                }
            }

            isFindOrder = false;
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


            if (lastAsk > 0 && lastAsk / askPrice - 1 >= this.SellProfitMax)//检测到价格向下跳变，取消挂单，并进行3次冷却
            {
                if (SellOrder != null)
                {
                    CancelOrder(SellOrder.PublicId, "价格向下跳变超过阈值");
                    SellOrder = null;
                }
                this.sellOrderCountDown.Skip(3);
            }

            if (lastBid > 0 && bidPrice / lastBid - 1 >= this.BuyProfitMax)//检测到价格向上跳变，取消挂单，并进行3次冷却
            {
                if (BuyOrder != null)
                {
                    CancelOrder(BuyOrder.PublicId, "价格向上跳变超过阈值");
                    BuyOrder = null;
                }

                this.buyOrderCountDown.Skip(3);
            }

           
            decimal avalibleAmount = this.market.AvalibleInCtcMarket;//没有挂单的金额
            decimal holdAmount = this.market.HoldInCtcMarket; //已挂单的额度
            decimal amountCount = avalibleAmount + holdAmount; //总持仓
            decimal minSize = instrument.MinSize;

            #region 检查出售挂单
            //检查出售挂单
            OrderBase sellOrder = this.SellOrder;
            decimal sellPrice = Math.Round(BasePrice * (1.0m + Fee + SellProfitMax), instrument.TickSizeDigit);
           
            if (sellOrder == null && avalibleAmount > instrument.MinSize && sellOrderCountDown.Check())
            {
                sellOrder = await CTCOrderManager.Instance.PlaceOrder(this.market.Currency, Math.Min(SellMaxSize, avalibleAmount), sellPrice, OrderOparete.Sell, true);
                this.SellOrder = sellOrder;

                if (sellOrder != null)
                {
                    Assert(string.Compare(sellOrder.InstrumentId, instrumentId, true) == 0);
                    needResync = true;
                }
            }
            #endregion

            #region 检查买入挂单

            bool upAlert = bidPrice > this.AlertPriceUp;
            bool downAlert = bidPrice < this.AlertPriceDown;

            if (upAlert || downAlert)
            {
                if (BuyOrder != null)
                {
                    CancelOrder(BuyOrder.PublicId, string.Format( "触发{0}警戒价格，撤销买单",upAlert ? "上":"下"));
                    BuyOrder = null;
                }

                goto label_exit;
            }

            decimal buyPrice = Math.Round(BasePrice * (1.0m - Fee - BuyProfitMax), instrument.TickSizeDigit);
            decimal amountDiff = this.MaxHold - amountCount;
            OrderBase buyOrder = this.BuyOrder;
            decimal cashAvalible = anchorMarket.AvalibleInCtcMarket; //剩余的USDX 
            decimal canBuyAmount = cashAvalible / buyPrice + (buyOrder != null ? buyOrder.AvailableAmount : 0); //剩余USDT可购买数量

            amountDiff = Math.Min(BuyMaxSize, Math.Min(amountDiff, canBuyAmount));
            if (amountDiff > instrument.MinSize)
            {
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
            else
            {
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
            lastAsk = askPrice;
            lastBid = bidPrice;
            canExecute = true;

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

            CTCOrderManager.Instance.EachSellOrder((order) =>
            {
                if (string.Compare(instrumentId, order.InstrumentId, true) == 0)
                {
                    CancelOrder(order.PublicId);
                }
            });

            SellOrder = null;



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
