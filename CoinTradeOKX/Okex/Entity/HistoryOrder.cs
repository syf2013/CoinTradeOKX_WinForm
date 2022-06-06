using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{

    /*
     amount: "0.039046"
counterPartyName: "孙友"
createdDate: "2019-11-23 22:23:00"
exchangeRate: "51195.36CNY"
orderStatus: "已取消"
orderTotal: "1999.00CNY"
publicOrderId: 191123222300860
receiptAccountType: "--"
symbol: "BTC"
type: "sell"
         */
    public class HistoryOrder
    {
        public decimal Amount { get; set; }
        public string CounterPartyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ExchangeRate { get; set; }
        public string OrderState { get; set; }
        public decimal OrderTotal { get; set; }
        public long PublicOrderId { get; set; }
        public string ReceiptAccountType { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }


        public void ParseFromJson(JToken json)
        {
            this.Amount = json.Value<decimal>("amount");
            this.CounterPartyName = json.Value<string>("counterPartyName");
            this.CreatedDate = json.Value<DateTime>("createdDate");
            this.ExchangeRate = json.Value<decimal>("exchangeRate");
            this.OrderState = json.Value<string>("orderStatus");
            this.OrderTotal = json.Value<decimal>("orderTotal");
            this.PublicOrderId = json.Value<long>("publicOrderId");
            this.ReceiptAccountType = json.Value<string>("ReceiptAccountType");
            this.Symbol = json.Value<string>("symbol");
            this.Type = json.Value<string>("type");
        }
    }
}
