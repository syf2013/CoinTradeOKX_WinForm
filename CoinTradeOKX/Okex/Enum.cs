using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    /*
     1：币币账户 3：交割合约 5：币币杠杆账户 6：资金账户 9：永续合约账户 12：期权合约 18：统一账户
    */

    public enum WalletType
    {
        /*币币钱包id*/
        CTC = 1,
        /*法币钱包id*/
        OTC = 6, //4,已取消法币账户4
                 /**
                  * 资金账户
                  */
        Account = 6,

        Unified = 18 //统一账户
    }


    public enum PayType : int
    {
        None = 0,
        Bank = 1,
        Alipay = 2,
        WechatPay = 4
    }

    public enum CancelOrderRason
    {
        None,
        NoProfit,
        Compet
    }

    public enum OrderOparete
    {
        Buy,
        Sell,
    }
}
