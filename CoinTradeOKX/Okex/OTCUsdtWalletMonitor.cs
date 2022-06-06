using CoinTradeGecko.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex
{
    [MonitorName(Name = "法币USDX")]
    public class OTCUsdtWalletMonitor: OTCWalletMonitor
    {
        public OTCUsdtWalletMonitor():base(Config.Instance.Anchor.ToLower())
        {
            this.Interval = 3000;
        }
    }
}
