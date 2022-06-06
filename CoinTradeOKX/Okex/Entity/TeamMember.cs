using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    /**
     * 
     * 团队成员账号，由于无法识别用户ID只能用姓氏和注册时间组合识别
     */ 
    public class TeamMember
    {
        /**
         * 注册时间
         */ 
        public string PublicId { get; set; }
        /**
         * 姓氏
         */ 
        public string Name { get; set; }
    }
}
