using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Classes
{
    public struct Range <T>
    {
        public T Max;
        public T Min;

        public Range(T min,T max)
        {
            Max = max;
            Min = min;
        }
    }

}
