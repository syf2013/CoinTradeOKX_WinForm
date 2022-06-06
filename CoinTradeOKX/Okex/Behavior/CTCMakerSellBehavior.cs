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

    [BehaviorName(Name = "做市商卖策略")]
    public class CTCMakerSellBehavior : SpotBehaviorBase
    {
        private OrderBase SellOrder = null;


        int volatility = 0; //表示价格的波动性/秒
        public override string Message
        {
            get
            {
                return string.Format("HTTP延迟 {0:0.00000}", Http.Timeout);
            }
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

        /// <summary>
        /// 撤单利润
        /// </summary>
        [BehaviorParameter(Name = "卖撤单利润", Min = -.1, Max = 1)]
        public decimal SellProfitMin
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

        public CTCMakerSellBehavior(CurrencyMarket market, USDXMarket anchorMarket)
            :base(market, anchorMarket)
        {
            
            this.Fee = 0.001m;

            this.market = market;
            this.anchorMarket = anchorMarket;
            //this._candle = CandleGranularity.M1;

            market.OnMarketChanged += this.MarketChange;

            instrument = market.Instrument;
            this.SellMaxSize =  instrument.MinSize;
            this.SellProfitMin = 0.001m;
            this.SellProfitMax = 0.003m;

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

 

            //decimal midPrice = (market.CTCAsk + market.CTCBid) * .5m;

            decimal avalibleAmount = this.market.AvalibleInCtcMarket;//没有挂单的金额
            decimal holdAmount = this.market.HoldInCtcMarket; //已挂单的额度
            decimal amountCount = avalibleAmount + holdAmount; //总持仓
            decimal minSize = instrument.MinSize;

            decimal maDiff = 0;//根据当前均线算出来的均线头尾差价

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

            #region 检查出售挂单
            //检查出售挂单
            OrderBase sellOrder = this.SellOrder;
            decimal sellPrice = Math.Round(askPrice * (1.0m + Fee + SellProfitMax + maDiff), instrument.TickSizeDigit);
            decimal minSellPrice = Math.Round(askPrice * (1.0m + Fee + SellProfitMin + maDiff), instrument.TickSizeDigit);

            if (sellOrder != null)
            {
                //价格超出范围,或有新增的持仓
                if ((minSellPrice - sellOrder.Price) > 0//instrument.TickSize 
                    || (sellOrder.Price - sellPrice) > 0//instrument.TickSize 
                                                        //|| (avalibleAmount > instrument.MinSize && sellOrder.AvailableAmount < SellMaxSize)
                    )
                {
                    decimal amountTotal = Math.Min(SellMaxSize, sellOrder.AvailableAmount + avalibleAmount);
                    bool ret = await CTCOrderManager.Instance.ChangeOrder(sellOrder.PublicId, this.market.Currency, amountTotal, sellPrice, true);

                    if (!ret)
                    {
                        this.SellOrder = null;
                        sellOrder = null;
                    }
                    else
                    {

                        sellOrder = await CTCOrderManager.Instance.QueryOrder(sellOrder.PublicId, instrumentId, true);
                        this.SellOrder = sellOrder;

                        if (sellOrder != null)
                            Assert(string.Compare(sellOrder.InstrumentId, instrumentId, true) == 0);
                        //needResync = sellOrder != null;
                    }
                }
            }

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

            if (needResync)
            {
                this.Executing = true;
                this.market.SyncCTCAMounts();
                this.ResyncOrders();
                //Thread.Sleep(100);//等待数据同步
            }

            label_exit:
            lastAsk = askPrice;
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

            CTCOrderManager.Instance.EachSellOrder((order) =>
            {
                if (string.Compare(instrumentId, order.InstrumentId, true) == 0)
                {
                    CancelOrder(order.PublicId);
                }
            });

            SellOrder = null;
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


