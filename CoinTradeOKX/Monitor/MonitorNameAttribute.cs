using CoinTradeOKX.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Monitor
{
    public class MonitorNameAttribute:Attribute
    {
        public string Name { get; set; }
    }

    public class BehaviorNameAttribute : Attribute
    {
        public string Name { get; set; }
    }


    public class BehaviorParameter : Attribute
    {
        public string Name { get; set; }

        /**
         * 依赖另外一个值，才有效
         */
        public string Dependent { get; set; }
        public object DependentValue { get; set; }

        public double Min = double.MinValue;
        public double Max = Double.MaxValue;

        /// <summary>
        /// 参数说明
        /// </summary>
        public string Intro { get; set; }
    }
}
