using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class CoinInfo
    {
        public int BaseCurrencyId { get; set; }
        public string BaseCurrency { get; set; }
        public string BaseName { get; set; }
        public double BaseDeposit { get; set; }
        public string BaseSymbol { get; set; }
        public double BaseScale { get; set; }

        public decimal Avalible { get; set; } //可用
        public decimal Frozen { get; set; } //冻结
        public void ParseFromJson(JToken o)
        {
            BaseCurrencyId = o["baseCurrencyId"].Value<int>();
            BaseCurrency = o["baseCurrency"].Value<string>();
            BaseDeposit = o["baseDeposit"].Value<double>();
            BaseName = o["baseName"].Value<string>();
            BaseScale = o["baseScale"].Value<double>();
            BaseSymbol = o["baseSymbol"].Value<string>();

            this.Avalible = 0;
            this.Frozen = 0;
        }
    }
}
