using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.API
{
    public static class Okex_otc_api_url
    {
        //public static readonly string okex_api_order_info = "/v3/c2c/tradingOrders/{0}";
        public static readonly string okex_api_cancel_order = "/v3/c2c/tradingOrders/{0}/cancel"; //取消订单  （重要）
        public static readonly string okex_api_order_paid   = "/v3/c2c/orders/{0}/payment/paid";    //订单已支付
        public static readonly string okex_api_order_place  = "/v3/c2c/tradingOrders/";            //挂单        （重要）
        public static readonly string okex_api_order_info  = "/v3/c2c/tradingOrders/{0}";         //获取挂单的详细信息
        public static readonly string okex_api_coin_amount  = "/v3/c2c/accounts/balance/{0}";      //查询余量 {0}为币种类型{重要}
        public static readonly string okex_api_mine_orders  = "/v3/c2c/tradingOrders/my";         //获取当前我的挂单 （重要）
        public static readonly string okex_api_contract     = "/v3/c2c/orders/?status=new";       // 新订单
        //https://www.okx.com/v3/c2c/orders/?t=1589203495689&status=appealed //申诉中订单
        public static readonly string okex_api_order_confrimed = "/v3/c2c/orders/{0}/payment/confirmed";
        public static readonly string okex_api_history = "/v3/c2c/accounts/orders?symbol={0}&type={1}&orderState={2}&currentPage={3}&pageSize={4}";//获取历史数据
        public static readonly string okex_api_receiptAccounts = "/v3/c2c/receiptAccounts"; //获取首付款的账号列表

        public static readonly string okex_api_receiptAccount_apply = "/v3/c2c/receipt/display/{0}";  //"/v3/c2c/receipt/display/1465735";
        public static readonly string okex_api_account_status = "/v3/c2c/receiptAccounts/{0}/status";//修改账号开关，账号id，post
    }
}
