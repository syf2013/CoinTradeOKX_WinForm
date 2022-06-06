using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    public interface IWalletMonitor
    {
         string Currency { get;   }

         decimal Availible { get;   }
         decimal Hold { get; }

        //void AddDataCallback(Action callback);
    }
}
