using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Const
{   public static class Side
    {
        public static readonly string Sell = "sell";
        public static readonly string Buy = "buy";
    }

    public static class UserType
    {
        public static readonly string All = "all";
        public static readonly string Common = "common";
        public static readonly string Certified = "certified";
    }

    public static class ContractStatus
    {
        public static readonly string New = "new";
        public static readonly string Appealed = "appealed";
        public static readonly string Completed = "completed";
        public static readonly string Cancelled = "cancelled";
    }

    public static class OrderStatus
    {
        public static readonly string Completed = "已完成";
        public static readonly string Canceled = "已取消";
    }

    public static class PriceType
    {
        public static readonly string Limit = "limit";
        public static readonly string FloatMarket = "floating_market";
    }

    public static class PayType
    {
        public static readonly string Bank = "bank";
        public static readonly string Aliypay = "aliypay";
        public static readonly string WeChatPay = "wechatpay";
    }

    public static class PaymentStatus
    {
        public static readonly string Paid = "paid";
        public static readonly string Unpaid = "unpaid";
    }
}
