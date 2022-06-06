using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public static class RandomUtil
    {
        static  Random rnd = new Random();
        public static int GetRandom(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static double GetRandom(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }
    }
}
