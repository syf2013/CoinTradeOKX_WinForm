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

    [BehaviorName(Name = "自动盯盘")]
    public class CTCTradingBehavior : BehaviorBase
    {
        public override string Message
        {
            get
            {
                return string.Format("买入{0}  清仓{1} 数量 {2}", BuyPrice,SellPrice, MaxBuy);
            }
        }


        decimal sellPrice;

        [BehaviorParameter(Name = "清仓价格",Intro = "当价格高于这个价格时触发清仓操作")]
        public decimal SellPrice
        {
            get { return sellPrice; }
            set 
            {
                sellPrice = value;
                sellTriggered = false;
            }
        }

        private decimal _maxHold = 0;

        [BehaviorParameter(Name = "买入数量", Min = 0, Max = double.MaxValue)]
        public decimal MaxBuy
        {
            get
            {
                return _maxHold;
            }
            set
            {
                decimal minSize = instrument.MinSize;
                if (value < minSize)
                    throw new Exception("最少买入数量不能少于" + minSize);

                buyTriggered = false;
                _maxHold = value;
            }
        }

        decimal buyPrice = 0;
        [BehaviorParameter(Name = "买入价格", Intro = "当价格低于这个价格时触发买入操作")]
        public decimal BuyPrice
        {
            get 
            { 
                return buyPrice; 
            }
            set
            {
                buyPrice = value;
                buyTriggered = false;
            }
        }

        Instrument instrument = null;


        CurrencyMarket market = null;
        USDXMarket anchorMarket = null;

        public CTCTradingBehavior(CurrencyMarket market, USDXMarket anchorMarket)
            : base()
        {
            this.market = market;
            this.anchorMarket = anchorMarket;
 
            market.OnMarketChanged += this.MarketChange;

            instrument = market.Instrument;
            this.MaxBuy =   instrument.MinSize * 10;

            if (instrument == null)
            {
                Logger.Instance.LogError(market.Currency + "instrument 获取失败");
            }
         }
 

     
        private void MarketChange()
        {
            if (instrument == null)
                return;

            this.Executing = false;

            try
            {
                if (!(this.market.Effective) || !this.Enable)
                {
                    return;
                }

                this.CheckPrices();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
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

        bool buyTriggered = false;
        bool sellTriggered = false;



        /// <summary>
        /// 检查持仓情况
        /// </summary>
        private  void CheckPrices()
        {
            if (!this.Enable)
                return;

           
            decimal askPrice = market.CTCAsk;
            decimal bidPrice = market.CTCBid;
         
             decimal avalibleAmount = this.market.AvalibleInCtcMarket;//没有挂单的金额
            
             //decimal holdAmount = this.market.HoldInCtcMarket; //已挂单的额度
            //decimal amountCount = avalibleAmount + holdAmount; //总持仓
            //decimal amountDiff = MaxBuy - amountCount;


            if (!buyTriggered)
            {
                if (askPrice <= BuyPrice)
                {
                    this.Executing = true;
                    market.BuyFromCTCMarketWithAmountV5(MaxBuy);
                    buyTriggered = true;
                }
            }

            if (!sellTriggered)
            {
                if (bidPrice >= SellPrice)
                {
                    this.Executing = true;
                    market.SellToCTCMarketWithAmount(avalibleAmount, true);
                    sellTriggered = true;
                }
            }
        }

        public override void Dispose()
        {
            market.OnMarketChanged -= this.MarketChange;

            //this.market.UnloadCandle(this._candle);

             base.Dispose();
        }
    }
}