using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class ReceiptAccountSetting
    {
        public ReceiptAccountSetting()
        {
            this.AvalibleBeginHour = 0;
            this.AvalibleEndHour = 24;
            this.TotalDayReceiveAmount = 500000;
            this.TotalDayReceiveTimes = 20;
        }

        public long Id
        {
            get; set;
        }

        //每日最大收款额度
        public decimal TotalDayReceiveAmount
        {
            get; set;
        }

        //每日可接受笔数
        public int TotalDayReceiveTimes
        {
            get; set;
        }

        //有效时段开始小时数
        public int AvalibleBeginHour
        {
            get; set;
        }

        //有效时段结束小时
        public int AvalibleEndHour
        {
            get; set;
        }

        //用于循环收款
        public bool ForRecycle
        {
            get; set;
        }
    }
}
