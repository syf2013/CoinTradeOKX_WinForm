using CoinTradeOKX.Okex.Const;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public enum OrderInterface:int
    {
        InnerHttp = 1, //内部http直通
        Browser = 2,   //浏览器代理
        Simulate = 3,  //通过浏览器模拟操作
    }

    public class Platform
    {
        public Platform()
        {
            this.ReorderCountDownMS = 5000;
            this.OrderCountDownMS = 25000;
            this.ApiTimeout = 10;
            this.OTCInterface = OrderInterface.InnerHttp;
            this.SellOrderUserType = UserType.All;
            this.BuyOrderUserType = UserType.All;
            this.MaxUnpaidBuyOrder = 10;
            this.DaySellAmountLimit = 500;

            this.Currencies = new List<string>(new string[] { "BTC","ETH","LTC"});
        }
       /// <summary>
       /// 取消订单，再挂单冷却
       /// </summary>
       public uint ReorderCountDownMS
       {
            get;set;
       }

        /// <summary>
        /// 日最大销售额限制
        /// </summary>
        public uint DaySellAmountLimit
        {
            get;set;
        }


        public string SellOrderUserType
        {
            get;set;
        }

        public bool SellPaidAddToCash
        {
            get;set;
        }

        public string BuyOrderUserType
        {
            get;set;
        }

        public decimal OTCFeee
        {
            get;set;
        }

        public decimal CTCFee
        {
            get;set;
        }

        public List<string> Currencies
        {
            get;set;
        }

        public uint ApiTimeout
        {
            get;set;
        }


        /// <summary>
        /// 最大未处理买单数， 避免买单积压，无法即时付款， 如果超过这个数量
        /// 将强制停止挂买单，并把挂掉撤掉
        /// </summary>
        public int MaxUnpaidBuyOrder
        {
            get;set;
        }

        private uint _orderCountDownMs = 25000;
        /// <summary>
        /// 挂单冷却
        /// </summary>
        public uint OrderCountDownMS
        {
            get
            {
                return _orderCountDownMs;
            }

            set
            {
                _orderCountDownMs = value;
            }
        }

        public OrderInterface OTCInterface
        {
            get;set;
        }

        public bool ParseFromJson(JToken json)
        {
            try
            {
                if(json["OrderCountDownMS"] != null)
                this.OrderCountDownMS = json["OrderCountDownMS"].Value<uint>();

                if (json["ReorderCountDownMS"] != null)
                    this.ReorderCountDownMS = json["ReorderCountDownMS"].Value<uint>();

                if (json["ApiTimeout"] != null)
                    this.ApiTimeout = json["ApiTimeout"].Value<uint>();
                if (json["SellOrderUserType"] != null)
                    this.SellOrderUserType = json["SellOrderUserType"].Value<string>();
                if (json["BuyOrderUserType"] != null)
                    this.BuyOrderUserType = json["BuyOrderUserType"].Value<string>();
                if (json["SellPaidAddToCash"] != null)
                    this.SellPaidAddToCash = json["SellPaidAddToCash"].Value<bool>();
                if (json["MaxUnpaidBuyOrder"] != null)
                    this.MaxUnpaidBuyOrder = json["MaxUnpaidBuyOrder"].Value<int>();

                if (json["DaySellAmountLimit"] != null)
                    this.DaySellAmountLimit = json["DaySellAmountLimit"].Value<uint>();

                if (json["Currencies"] != null)
                {
                    JArray arr = json["Currencies"] as JArray;

                    List<string> currencies = new List<string>();
                    foreach(var r in arr)
                    {
                        currencies.Add(r.ToString());
                    }

                    if(currencies.Count>0)
                    {
                        this.Currencies = currencies;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
