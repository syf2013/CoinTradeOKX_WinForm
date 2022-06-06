using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class OrderBase
    {
        public int Index { get; set; }
        public string Currency { get; set; }
        public string InstrumentId { get; set;}
        public decimal Price { get; set; }
        public decimal AvailableAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public long PublicId { get; set; }
        //public bool IsSelf { get; set; }
        public OrderOparete Side { get; set; }
        public MarketTypeEnum MarketType { get; set; }

        public string State { get; set; }

        public OrderBase()
        {
            this.MarketType = MarketTypeEnum.CTCMarket;
        }

        public virtual void CopyFrom(OrderBase others)
        {
            this.PublicId = others.PublicId;
            this.Price = others.Price;
            this.AvailableAmount = others.AvailableAmount;
            this.Side = others.Side;
            this.CreatedDate = others.CreatedDate;
            this.Currency = others.Currency;
            this.InstrumentId = others.InstrumentId;
            this.State = others.State;
            this.MarketType = others.MarketType;
        }

        public virtual void ParseFromJson (JToken obj)
        {
            this.PublicId = obj.Value<long>("ordId");
            decimal px = 0;
            Decimal.TryParse(obj.Value<string>("px"), out px);
            this.Price = px;
         
            this.AvailableAmount = obj.Value<decimal>("sz") - obj.Value<decimal>("fillSz");
            this.Side = obj.Value<string>("side") == "sell" ? OrderOparete.Sell : OrderOparete.Buy;
            this.CreatedDate = DateUtil.TimestampMSToDateTime( obj.Value<long>("cTime"));
            this.Currency = obj.Value<string>("instId");
            this.InstrumentId = obj.Value<string>("instId");
            this.State = obj.Value<string>("state");/*
            订单状态
canceled：撤单成功
live：等待成交
partially_filled：部分成交
filled：完全成交*/
        }
    }
}
