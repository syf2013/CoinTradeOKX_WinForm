using CoinTradeOKX.Event;
using Common;
using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class OTCAccount
    {
 
        public string RealName { get;  set; }    //实名
        public string LoginName { get; set; }
        //public string BankAccount { get;  set; } //银行户名
        //public string BankNo { get;  set; }      //银行卡
        public string ReleasePassword { get; set; }
        public bool IsBusiness { get; set; }
        public decimal Deposit { get; set; } //平台USDT押金

        public MarketTypeEnum MarketType
        {
            get;set;
        }

        public string GetLoginName()
        {
            return this.LoginName;
        }

        public string GetReleasePassword()
        {
            return this.ReleasePassword;
        }

        public void SetLoginName(string val)
        {
            this.LoginName = val;
        }

        public void SetReleasePassword(string val)
        {
            this.ReleasePassword = val;
        }

        public bool ParseFromJson(JToken json)
        {
            try
            {
                 
                this.RealName = json["RealName"].Value<string>();
                this.LoginName = json["LoginName"].Value<string>();
                this.ReleasePassword = json["ReleasePassword"].Value<string>();
                this.IsBusiness = json["IsBusiness"] != null ? json["IsBusiness"].Value<bool>() : false;
                this.Deposit = json["Deposit"] != null ? json["Deposit"].Value<decimal>() : 0;

                MarketTypeEnum marketType;

                if (json["MarketType"] != null && Enum.TryParse<MarketTypeEnum>(json["MarketType"].Value<string>(), out marketType))
                {
                    this.MarketType = marketType;
                }
                else
                {
                    this.MarketType = MarketTypeEnum.OTCMarket;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
