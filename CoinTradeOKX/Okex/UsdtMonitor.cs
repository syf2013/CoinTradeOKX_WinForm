using CoinTradeGecko.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex
{
    [MonitorNameAttribute(Name = "USDX法币")]
    public class OTCUSDTMarketMonitor:OTCMarketMonitor
    {
        public decimal PriceAvg { get; private set; }
        public double TotleSellAmount { get;private set; }

        public decimal PriceMin { get; private set; }
        public double MinPriceAmount { get; private set; }

        public OTCUSDTMarketMonitor(uint interval)
            :base(Config.Instance.Anchor.ToUpper())
        {
            this.Interval = 5000;
        }

        protected override void OnDataUpdate(JToken marketData)
        {
            base.OnDataUpdate(marketData);
            JArray sellList = marketData["sell"] as JArray;

            if (sellList != null)
            {
                double totalAmount = 0;
                decimal totalMoney = 0;

                decimal priceMin = decimal.MaxValue;
                foreach (JToken o in sellList)
                {
                    decimal price = o["price"].Value<decimal>();

                    priceMin = Math.Min(price, priceMin);

                    double amount = o["availableAmount"].Value<double>();
                    totalAmount += amount;
                    totalMoney += price * (decimal)amount;
                }

                this.PriceMin = priceMin;
                this.MinPriceAmount = 0;
                foreach (JToken o in sellList)
                {
                    if (o["price"].Value<decimal>() == priceMin)
                    {
                        this.MinPriceAmount += o["availableAmount"].Value<double>();
                    }
                }
                this.PriceAvg = totalAmount <= 0 ? 0 : totalMoney / (decimal)totalAmount;
                this.TotleSellAmount = totalAmount;

                if(sellList.Count > 0)
                    this.Feed();
            }
        }
    }
}
