using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Behavior;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using Common.Util;
using System;
using System.Collections.Generic;

namespace CoinTradeOKX.Okex
{

    [BehaviorName(Name = "币币卖策略")]
    public class CTCSellBehavior : SpotBehaviorBase
    {
        private CountDown autoCheckAmountCD = new CountDown(2500, false);

        public override string Message
        {
            get
            {
                return string.Format("仓位:{0:P}%", this.GetAmountRate());
            }
        }

        private decimal _maxHold = 0;
     
        [BehaviorParameter(Name = "持仓（USD）", Min = 0, Max = 100000000)]
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

        private decimal _amountSellOutDiff = 0.2m;

        [BehaviorParameter(Name = "溢价抛售(倍)", Min = 0.01,Max = 10000)]
        public  decimal AmountSellOutDiff
        {
            get { return this._amountSellOutDiff; }
            set { this._amountSellOutDiff = value; }
        }


        [BehaviorParameter(Name = "停止价格(USD)", Min = 0, Max = 999999999)]
        public decimal StopPrice
        {
            get;set;
        }

        [BehaviorParameter(Name = "清仓价格(USD)", Min = 0, Max = 999999999)]
        public decimal CleanupPrice
        {
            get;set;
        }

        [BehaviorParameter(Name = "止损价格(USD)", Min = 0, Max = 999999999)]
        public decimal StopLoss
        {
            get; set;
        }

        [BehaviorParameter(Name = "限价模式")]
        public bool PriceLimit
        {
            get;set;
        }

        [BehaviorParameter(Name = "使用均线")]
        public bool UseMA
        {
            get;set;
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
        [BehaviorParameter(Name = "均线周期",Dependent ="UseMA",DependentValue = true,Min = 5, Max = 1000)]
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



        //[BehaviorParameter(Name = "锚定价格类型", Min = 0.01, Max = 10)]
        public PriceTypeEnum AnchorType
        {
            get;set;
        }

        /*
        [BehaviorParameter(Name = "锚定价格", Min = 0.01, Max = 100000000,Dependent = "AnchorType", DependentValue = PriceTypeEnum.Fixed)]
        public decimal AnchorPrice
        {
            get;set;
        }*/

        Instrument instrument = null;


        CurrencyMarket market = null;
        USDXMarket anchorMarket = null;

        public CTCSellBehavior(CurrencyMarket market, USDXMarket anchorMarket)
            : base(market, anchorMarket)
        {
            this.market = market;
            this.anchorMarket = anchorMarket;
            this._candle = CandleGranularity.M1;

            market.OnMarketChanged += this.MarketChange;

            instrument = market.Instrument;

            if(instrument == null)
            {
                Logger.Instance.LogError( market.Currency + "instrument 获取失败");
            }


            EventCenter.Instance.AddEventListener(EventNames.AmountAllocation, this.OnAmountAllocation);
        }

        /**
         *获取市场上usdx的价格
         */
        private decimal GetOTCUsdtAsk()
        {
            return Config.Instance.ExchangeRate;
        }

        private void BuyFromCTCMarket(decimal amount)
        {
            this.CheckAndCancelOrder(OrderOparete.Buy);
            market.BuyFromCTCMarketWithAmount(amount,anchorMarket.AvalibleInCtcMarket, !this.PriceLimit);
        }

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
                if (!(this.market.Effective))
                {
                    goto label_exit;
                }

                //自动仓位检查
  
                    this.CheckAmounts();
                
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }

            label_exit:

            canExecute = true;

        }
        /// <summary>
        /// 检查持仓情况
        /// </summary>
        private void CheckAmounts()
        {
            if (!this.Enable)
                return;

            this.autoCheckAmountCD.Interval = this.PriceLimit ? 2000u : 1000u;


            DateTime serverTime = DateUtil.GetServerDateTime();
            var hour = serverTime.Hour;

            lock (this.autoCheckAmountCD)
            {
                
                decimal totalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;

                if (totalAmount < this.instrument.MinSize) //可能数据有错,或已经清仓
                {
                    return;
                }

                decimal priceCNY = this.GetBasePrice();

                if (priceCNY <= 0) return;




                //全部抛售清仓， 认为无法到达更高的价格了
                if (this.CleanupPrice > 0 && this.market.CTCBid >= this.CleanupPrice)
                {
                    this.SellByAmount(totalAmount);
                    return;
                }

                //全部抛售清仓,止损
                if (this.StopLoss > 0 && this.market.CTCBid <= this.StopLoss)
                {
                    this.SellByAmount(totalAmount);
                    return;
                }

                //达到停止价格， 向上突破
                if (this.StopPrice > 0 && this.market.CTCAsk >= this.StopPrice)
                {
                    return;
                }

                decimal totalMoney = GetAskPrice() * totalAmount;
                decimal amountRate = totalMoney / this.MaxHold;//


                totalMoney = GetBidPrice() * totalAmount;
                amountRate = totalMoney / this.MaxHold;//
                if (amountRate - 1.0m >= AmountSellOutDiff && this.CanSellOut())
                {
                    decimal sellCNY = totalMoney - this.MaxHold;
                    if (sellCNY > 0 && this.autoCheckAmountCD.Check() && (!this.PriceLimit || !this.ExistOrder(OrderOparete.Sell)))
                    {
                        this.Executing = true;
                        Logger.Instance.LogDebug(string.Format("抛售{0}{1}, 当前持有:{2},{3}", market.Currency, sellCNY, market.AvalibleInCtcMarket, market.HoldInCtcMarket));
                        this.SellByCNY(sellCNY);
                    }
                }
                else
                {
                    this.CheckAndCancelOrder(OrderOparete.Sell);
                }
            }
        }

        /// <summary>
        /// 判断当前价格是否可以卖出
        /// </summary>
        /// <returns></returns>
        private bool CanSellOut()
        {
            if (!UseMA)
                return true;
            //TODO

            bool can = false;

            var maList = this.GetMA();
            //TODO 进行是否可以补仓的逻辑判断

            if (maList != null)
            {
                decimal maAvg = GetAvg(maList);
                decimal last = maList[0];
                can = last < maAvg;
            }
            return can;

        }

        private decimal GetAvg(IList<decimal> priceList)
        {
            decimal a = 0;

            foreach(var p in priceList)
            {
                a += p;
            }

            a /= priceList.Count;

            return a;
        }

        /*
        private bool Candle59(bool checkUp,bool checkDown)
        {
            bool avalile = false;
            int priceUpCount = 0;
            bool uFlag = true;
            bool dFlag = true;
            int priceDownCount = 0;
            decimal price = (market.CTCAsk + market.CTCBid) * 0.5m;
            int index = 0;
            this.market.EachCandle((candle) => {
                if(index > 0)
                {
                    if (checkDown)
                    {
                        if (candle.Low > price && dFlag)
                        {
                            priceDownCount++;
                        }
                        else
                        {
                            dFlag = false;
                        }
                    }

                    if (checkUp)
                    {
                        if (candle.High < price && uFlag)
                        {
                            priceUpCount++;
                        }
                        else
                        {
                            uFlag = false;
                        }
                    }
                }

                index++;

            });

            avalile = (priceUpCount >= 5 && priceUpCount <= 9) || (priceDownCount >= 5 && priceDownCount <= 9);

            return avalile;
        }
        */

        private IList<decimal> GetMA()
        {
            uint maCount =(uint) MAPeriod;
            uint maSize = (uint) MASample;
            var cm = this.market.GetCandleMonior(this.Candle);

            if(cm == null)
            {
                return null;
            }

            var maList = IndexUtil.MA(cm, maCount, maSize);
            return maList;
        }

        private decimal GetAmountRate()
        {
            if (this.MaxHold <= 0)
            {
                return 0;
            }

            decimal totalAmount = market.AvalibleInCtcMarket + market.HoldInCtcMarket;

 
            decimal priceCNY = this.GetBasePrice();

            if (priceCNY <= 0) return 0;

            decimal totalMoney = priceCNY * totalAmount;

            decimal amountRate = totalMoney / this.MaxHold;//

            return amountRate;
        }


        /// <summary>
        /// 币币市场换算人民币后的基准卖出价格
        /// </summary>
        /// <returns></returns>
        private decimal GetBasePrice()
        {
             return (market.CTCAsk + market.CTCBid) * 0.5m  * GetOTCUsdtAsk();
        }

        /// <summary>
        /// 币币市场换算人民币后的基准卖出价格
        /// </summary>
        /// <returns></returns>
        private decimal GetAskPrice()
        {
            return market.CTCAsk* GetOTCUsdtAsk();
        }

        /// <summary>
        /// 币币市场换算人民币后的基准卖出价格
        /// </summary>
        /// <returns></returns>
        private decimal GetBidPrice()
        {
            return market.CTCBid * GetOTCUsdtAsk();
        }


        /// <summary>
        /// 根据等值人民币从币币市场买入
        /// </summary>
        /// <param name="cny"></param>
        private void ReplenishByCNY(decimal cny)
        {
            decimal priceCNY = GetAskPrice();

            if (priceCNY <= 0)
            {
                return;
            }

            decimal amount = cny / priceCNY;

            if (amount >= instrument.MinSize)
            {
                this.BuyFromCTCMarket(amount);
            }
        }

        private bool ExistOrder(OrderOparete side)
        {
            return CTCOrderManager.Instance.ExistOrder(this.market.Currency, Config.Instance.Anchor, side);
        }

        private void CheckAndCancelOrder(OrderOparete side)
        {
            if (this.PriceLimit)
            {
                CTCOrderManager.Instance.CancelOrder(this.market.Currency, Config.Instance.Anchor, side);
            }
        }

        /// <summary>
        /// 根据等值人民币从币币市场卖出
        /// </summary>
        /// <param name="cny"></param>
        private void SellByCNY(decimal cny)
        {
            decimal priceCNY = GetBidPrice();

            if (priceCNY <= 0)
            {
                return;
            }

            decimal amount = cny / priceCNY;
            if (amount >= instrument.MinSize)
            {
                this.SellByAmount(amount);
            }
        }

        private void SellByAmount(decimal amount)
        {
            if (amount >= instrument.MinSize)
            {
                this.CheckAndCancelOrder(OrderOparete.Sell);
                this.market.SellToCTCMarketWithAmount(amount,!this.PriceLimit);
            }
        }

        private void OnAmountAllocation(object obj)
        {
            if (string.Compare(this.market.Currency, obj.ToString(),true) == 0)
            {
                lock (this.autoCheckAmountCD)
                {
                    this.autoCheckAmountCD.SkipOnce();
                }
            }
        }


        public override void Dispose()
        {
            market.OnMarketChanged -= this.MarketChange;
            this.market.UnloadCandle(this._candle);
            EventCenter.Instance.RemoveListener(EventNames.AmountAllocation, this.OnAmountAllocation);
            base.Dispose();
        }
    }
}
