using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Monitor
{
    public interface IMonitor
    {
        bool Effective { get;}
        void Update(int deltaTime);
    }
}
