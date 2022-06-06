using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class Trader:ICloneable
    {
        public string RealName { get; set; }
        public string Nickname { get; set; }
        public int AvgCompleteTime { get; set; }
        public int AvgPaymentTime { get; set; }
        public PayType PayType { get; set; }
        public uint TotalOrder { get; set; }
        public float CompletionRates { get; set; }

        public bool BoundBankCard { get; set; }
        public bool BoundPhoneNumber { get; set; }

        public int CancelledOrderQuantity { get; set; }
        public string CommonOrderTotal { get; set; }
        public int CompletedOrderQuantity { get; set; }
        public float CompletionRate { get; set; }
        public long CreatedDate { get; set; }
        public int KycLevel { get; set; }
        public bool Disabled { get; set; }
        public string AcceptState { get; set; }
        public bool AgreedTos { get; set; }
        public string Type { get; set; }
        public bool StrategyDisabled { get; set; }
        public string StrategyDisabledReason { get; set; }

        public object Clone()
        {
            var ret = new Trader();

            ret.RealName = this.RealName;
            ret.Nickname = this.Nickname;
            ret.AvgCompleteTime = this.AvgCompleteTime;
            ret.AvgPaymentTime = this.AvgPaymentTime;
            ret.PayType = this.PayType;
            ret.TotalOrder = this.TotalOrder;
            ret.CompletionRates = this.CompletionRates;

            ret.BoundBankCard = this.BoundBankCard;
            ret.BoundPhoneNumber = this.BoundPhoneNumber;

            ret.CancelledOrderQuantity = this.CancelledOrderQuantity;
            ret.CommonOrderTotal = this.CommonOrderTotal;
            ret.CompletedOrderQuantity = this.CompletedOrderQuantity;
            ret.CompletionRate = this.CompletionRate;
            ret.CreatedDate = CreatedDate;
            ret.KycLevel = KycLevel;
            ret.Disabled = Disabled;
            ret.AcceptState = AcceptState;
            ret.AgreedTos = AgreedTos;
            ret.Type = Type;
            ret.StrategyDisabled = StrategyDisabled;
            ret.StrategyDisabledReason = StrategyDisabledReason;
            return ret;
        }

        public bool ParseFromJson(JToken o)
        {
            RealName = o["realName"].Value<string>();
            Nickname = o["nickName"].Value<string>();
            AvgCompleteTime = o["avgCompleteTime"].Value<int>();
            AvgPaymentTime = o["avgPaymentTime"].Value<int>();
            BoundBankCard = o["boundBankCard"].Value<bool>();
            BoundPhoneNumber = o["boundPhoneNumber"].Value<bool>();
            CancelledOrderQuantity = o["cancelledOrderQuantity"].Value<int>();


            CompletedOrderQuantity = o["completedOrderQuantity"].Value<int>();
            CommonOrderTotal = o["commonOrderTotal"].Value<string>();

            CompletionRate = o["completionRate"].Value<float>();

            CreatedDate = o["createdDate"].Value<long>();
            KycLevel = o["kycLevel"].Value<int>();
            Disabled = o["disabled"].Value<bool>();
            AgreedTos = o["agreedTos"].Value<bool>();
            StrategyDisabled = o["strategyDisabled"].Value<bool>();
            StrategyDisabledReason = o["strategyDisabledReason"].Value<string>();

            AcceptState = o["acceptState"].Value<string>();
            Type = o["type"].Value<string>();

            return true;
        }
    }
}
