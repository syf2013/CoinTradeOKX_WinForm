using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Classes
{
    public class DepthInfo
    {
        public decimal Total { get; set; }
        public uint Orders { get; set; }
        public decimal Price { get; set; }

        public DepthInfo()
        {
            this.Total = 0;
            this.Orders = 0;
            this.Price = 0;
        }
    }
}
