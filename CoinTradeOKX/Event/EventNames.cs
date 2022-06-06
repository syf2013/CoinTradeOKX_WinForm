using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Event
{
    public static class EventNames
    {
        public static readonly string NewContract = "event_new_contract";           //新订单
        public static readonly string ContractPaid = "event_contract_paid";         //已付款
        public static readonly string AmountAllocation = "event_amount_allocation";//仓位重新划转
        public static readonly string CashChanged = "event_cash_changed";//现金金额发生改变
        public static readonly string ConfigChanged = "event_config_changed";//修改配置文件
        public static readonly string PricesDown = "event_as_prices_down";//价格下挫
        public static readonly string DisableBuyBehavior = "event_disable_buy"; //停止收单
        public static readonly string DisableSellBehavior = "event_disable_sell"; //停止卖单
        public static readonly string Set_Sell_Profit = "event_Set_Sell_Profit";
        public static readonly string Set_Buy_Profit = "event_Set_Buy_Profit";
        public static readonly string ReceiptAccountStateChanged = "event_receipt_account_state";
        public static readonly string MonitorChanged = "event_monitor_changed";
        public static readonly string BalanceChanged = "event_balance_changed";
    }
}
