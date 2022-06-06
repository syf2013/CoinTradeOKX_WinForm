using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CoinTradeOKX.Okex.Behavior
{
    [BehaviorName(Name = "自动仓位调节")]
    public class StoreControlBehavior:BehaviorBase
    {
        private CurrencyMarket market = null;

        private CountDown cd = new CountDown(2500, false);
        public StoreControlBehavior(CurrencyMarket market)
        {
            this.Enable = true;
            this.market = market;
            market.OnMarketChanged += CheckAmount;
        }

        public override void Dispose()
        {
            market.OnMarketChanged -= this.CheckAmount;
        }

        //public override void OnChanged()
        //{
        //    base.OnChanged();

        //    this.CheckAmount();
        //}

        private void CheckAmount()
        {
            if (!this.Enable)
                return;

            if (!market.Effective)
                return;

            this.Executing = false;

            if (!this.cd.Check())
                return;

            lock (this.cd)
            {
                decimal trans = 0;
                WalletType from = WalletType.Account;
                WalletType to = WalletType.Account;

                decimal minSize = market.Instrument.MinSize;
                decimal forSellAmount = market.AmountForCTCSell;

                if (forSellAmount >= minSize)
                {
                    if (market.AvalibleInCtcMarket < forSellAmount)
                    {
                        decimal diff = forSellAmount - market.AvalibleInCtcMarket;

                        if (diff >= minSize && market.AvalibleInAccount >= minSize)
                        {
                            trans = Math.Min(diff, market.AvalibleInAccount);
                            from = WalletType.Account;
#if OKEX_API_V5
                            to = WalletType.Unified;
#else
                        to = WalletType.CTC;
#endif
                        }
                    }
                }
                else if(market.AvalibleInCtcMarket >= minSize)
                {
                    trans = market.AvalibleInCtcMarket;
#if OKEX_API_V5
                    from = WalletType.Unified;
#else
                    from = WalletType.CTC;
#endif
                    to = WalletType.Account;
                }

                if (trans > 0)
                {
                    this.Executing = true;
                    this.market.CurrencyTrensfer(from, to, trans);
                }
            }
        }
    }
}
