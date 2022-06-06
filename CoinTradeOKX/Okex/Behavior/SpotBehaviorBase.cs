using CoinTradeOKX.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Behavior
{
    public abstract class SpotBehaviorBase: BehaviorBase
    {
        public SpotBehaviorBase(CurrencyMarket market, USDXMarket anchorMarket)
        {

        }


        protected void Assert(bool condition)
        {
            if (!condition)
            {
                throw new Exception(" condition is false ");
            }
        }
    }
}
