﻿using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public static class IndexUtil
    {
        public static void RSI(ICandleProvider icp, uint count, uint size)
        {

        }

        public static IList<decimal> MA(ICandleProvider icp, uint count,uint size)
        {
           
            uint barCount = count + size;
            IList<decimal> result = new List<decimal>((int)size);
            int barIndex = 0;

            for (int i = 0; i < size; i++)
                result.Add(0);

            icp.EachCandle((candle) => {
                if(barCount > 0)
                {
                 
                    for(int i = 0; i < size; i ++)
                    {
                        if(barIndex >= i && barIndex<count + i)
                        result[i] += candle.Close;
                    }
                    barCount--;
                }
                barIndex++;
            });

            for (int i = 0; i < size; i++)
            {
                result[i] /= count;
                if(result[i] == 0)
                {
                    return null;
                }
            }

            return result;
        }
    }
}
