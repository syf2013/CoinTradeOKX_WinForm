using CoinTradeOKX.Classes;
using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class Order: OrderBase,ICloneable
    {
        public double FloatRate { get; set; }
        public Range<decimal> AmountRange { get; set; }
        public Trader Owner { get; set; }
        public float CompletionRates { get; set; }
        public PayType PayType { get; set; }
        public uint TotalOrder { get; set; }
        public int MinKycLevel { get; set; }
        //public bool IsSelf { get; set; }
        public OrderOparete Oparete { get; set; }
        public string UserType { get; set; }
        public PriceTypeEnum PriceType { get; set; }
        public DateTime MaxUserCreatedDate { get; set; }
        public int MinCompletedOrderQuantity { get; set; }
        public int MaxCompletedOrderQuantity { get; set; }
        public bool Hidden { get; set; }
        public bool SafetyLimit { get; set; }
        public decimal HiddenPrice { get; set; }
        public int MinSellOrderQuantity { get; set; }

        private PriceDetail _priceDetail = new PriceDetail();

        public PriceDetail PriceDetail
        {
            get
            {
                this._priceDetail.Type = this.PriceType;
                this._priceDetail.Price = this.Price;
                this._priceDetail.FloatRate = this.FloatRate;
                return _priceDetail;
            }
        }

        public Order()
        {
            this.MarketType = MarketTypeEnum.OTCMarket;
        }

        private T GetField<T>(JToken o, string name)
        {
            if (o[name] != null)
                return o.Value<T>(name);

            return default(T);
        }

        public override void ParseFromJson(JToken o)
        {
            JToken id = o["publicId"];
            this.PublicId = id.Value<long>();// long.Parse(o["publicId"].ToString());//.Value<long>();

            this.Index = o["index"].Value<int>();
            this.AvailableAmount = o["availableAmount"].Value<decimal>();
            this.FloatRate = o["floatRate"].Value<double>();
            this.MinKycLevel = o["minKycLevel"].Value<int>();
            this.Oparete = this.Side = o["side"].Value<string>() == Const.Side.Sell ? OrderOparete.Sell : OrderOparete.Buy;
            this.Price = o["price"].Value<decimal>();
            //this.IsSelf = o["mine"].Value<bool>();
            this.Currency = o["baseCurrency"].Value<string>();
            this.CreatedDate = DateUtil.TimestampMSToDateTime(o["createdDate"].Value<long>());
            this.PriceType = o["type"].Value<string>() == Const.PriceType.Limit ? PriceTypeEnum.Fixed : PriceTypeEnum.Float;
            this.CompletionRates = o["minCompletionRate"].Value<float>();
            this.MinCompletedOrderQuantity = o["minCompletedOrderQuantity"].Value<int>();
            this.MaxCompletedOrderQuantity = o["maxCompletedOrderQuantity"].Value<int>();
            this.SafetyLimit = o["safetyLimit"] != null && o.Value<bool>("safetyLimit");
            
            this.Hidden = false;// o["hidden"].Value<bool>();
            this.HiddenPrice = o["hiddenPrice"].Value<decimal>();
            this.UserType = o["userType"].Value<string>();
            this.MinSellOrderQuantity = o["minSellOrderQuantity"].Value<int>();

            if (this.HiddenPrice > 0 )
            {
                if (Side == OrderOparete.Sell && this.Price < this.HiddenPrice)
                    this.Hidden = true;

                if (Side == OrderOparete.Buy && this.Price > this.HiddenPrice)
                    this.Hidden = true;
            }

            this.AmountRange = new Range<decimal>( o["quoteMinAmountPerOrder"].Value<decimal>()
                ,o["quoteMaxAmountPerOrder"].Value<decimal>()
                );

            JArray payTypes = o["paymentMethods"] as JArray;



            this.MaxUserCreatedDate = DateTime.Now;
            
            if(o["maxUserCreatedDate"] != null)
            {
               this.MaxUserCreatedDate = DateUtil.TimestampMSToDateTime(o["maxUserCreatedDate"].Value<long>());
            }

            this.PayType = PayType.None;

            foreach (var s in payTypes)
            {
                var v = s.Value<string>();

                if (v == "bank")
                    this.PayType = PayType.Bank | this.PayType;
                if (v == "aliPay")
                    this.PayType = PayType.Alipay | this.PayType;
                if (v == "wxPay")
                    this.PayType = PayType.WechatPay | this.PayType;
            }

            /*

      "banned": false,
      "bannedReason": "",
      "bannedReasonCode": 0,
      "baseCurrency": "ltc",
      "baseScale": 0,
      "blackState": "",
      "blocked": false,
      "brokerId": 0,
      "completedAmount": 0,
      "completedOrderTotal": 0,

      "exchangeRateDeviateTooFar": false,
      "floatRate": 1,
      "hidden": false,
      "hiddenPrice": 0,
      "holdAmount": 0,
      "index": 35,
      "isBlockTrade": false,
      "maxAvgCompleteTime": 0,
      "maxAvgPaymentTime": 0,
      "maxCompletedOrderQuantity": 0,
      "maxUserCreatedDate": 1546272000000,
      "minCompletedOrderQuantity": 0,
      "minCompletionRate": 0,
      "minKycLevel": 3,
      "mine": false,
      "": [
        "bank"
      ],
      "platformCommissionRate": "0.0003",
      "price": 760,
      "publicId": "190516142900154",
      "publicMerchantId": "",
      "quoteCurrency": "cny",
      "quoteMaxAmountPerOrder": 10670.9217628,
      "quoteMinAmountPerOrder": 400,
      "quotePriceScale": 0,
      "quoteScale": 0,
      "remark": "",
      "side": "sell",
      "status": "new",
      "type": "limit",
      "unpaidOrderTimeoutMinutes": 10,
      "userType": "all"
      */


            this.Owner = this.Owner == null?  new Trader() : this.Owner;
            this.Owner.ParseFromJson(o["creator"]);
        }

        public object Clone()
        {
            Order result = new Order
            {
                PublicId = this.PublicId,
                AmountRange = this.AmountRange,
                AvailableAmount = this.AvailableAmount,
                CompletionRates = this.CompletionRates,
                CreatedDate = this.CreatedDate,
                Currency = this.Currency,
                FloatRate = this.FloatRate,
                Hidden = this.Hidden,
                HiddenPrice = this.HiddenPrice,
                Index = this.Index,
                MaxUserCreatedDate = this.MaxUserCreatedDate,
                MinCompletedOrderQuantity = this.MinCompletedOrderQuantity,
                MinKycLevel = this.MinKycLevel,
                MinSellOrderQuantity = this.MinSellOrderQuantity,
                Oparete = this.Oparete,
                Owner = this.Owner != null ? this.Owner.Clone() as Trader : null,
                PayType = this.PayType,
                Price = this.Price,
                PriceType = this.PriceType,
                Side = this.Side,
                MarketType = this.MarketType,
                TotalOrder = this.TotalOrder,
                UserType = this.UserType
            };
            return result;
        }
    }
}
