using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Classes
{

    public struct PriceDetail
    {
        public PriceTypeEnum Type; //
        public double FloatRate;
        public decimal Price;
        public int Index;

        public static bool operator >(PriceDetail detail, decimal price)
        {
            return detail.Price > price;
        }

        public static bool operator <(PriceDetail detail, decimal price)
        {
            return detail.Price < price;
        }

        public static bool operator ==(PriceDetail detail, decimal price)
        {
            return detail.Price == price;
        }

        public static bool operator !=(PriceDetail detail, decimal price)
        {
            return detail.Price != price;
        }
    }

}
