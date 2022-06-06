using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Control
{
    public interface IMarketView
    {
        decimal TotalAmount { get;  set; }
    }
}
