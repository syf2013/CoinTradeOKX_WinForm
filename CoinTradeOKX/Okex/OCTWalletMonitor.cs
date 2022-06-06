using CoinTradeOKX.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name ="法币账户")]
    public class OTCWalletMonitor: JSMonitorBase, IWalletMonitor
    {
        public long Id { get; private set; }
        public string Currency { get; private set; }

        public decimal Availible { get; private set; }
        public decimal Hold { get; private set; }

        public OTCWalletMonitor(string currency)
            :base(new okex_Coin_Amount(currency), 1500)
        {
            this.Currency = currency.ToUpper();
        }

        protected override void OnDataUpdate(JToken data)
        {
            try
            {
                DateTime serverTime = DateUtil.GetServerDateTime();
                var hour = serverTime.Hour;
                var avalible = data["available"].Value<decimal>();
                var hold = data["hold"].Value<decimal>();

                if (hour == 0 || hour == 8 || hour == 16)
                {
                    if (serverTime.Minute < 2 && hold == 0 && avalible == 0)
                    {
                        this.Feed();
                        return;
                    }
                }

                this.Availible = avalible;
                this.Hold = hold;
                this.Feed();
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }
    }
}
