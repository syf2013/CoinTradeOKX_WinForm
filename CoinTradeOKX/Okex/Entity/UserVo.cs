using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class UserVo:ICloneable
    {
        public int AvgCompleteTime { get; set; }
        public int AvgPaymentTime { get; set; }
        public bool Blacker { get; set; }
        public int CancelledOrderQuantity { get; set; }
        public int CompletedOrderQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string HxUserName { get; set; }
        public int KycLevel { get; set; }
        public string NickName { get; set; }
        public string PublicUserId { get; set; }
        public string RealName { get; set; }

        public bool ShowContractInfo { get; set; }
        public string Type { get; set; }

        public void ParseFromJson(JToken json)
        {
            this.AvgCompleteTime = json["avgCompleteTime"].Value<int>();
            this.AvgPaymentTime = json["avgPaymentTime"].Value<int>();
            this.Blacker = json["blacker"].Value<bool>();
            this.CancelledOrderQuantity = json["cancelledOrderQuantity"].Value<int>();
            this.CompletedOrderQuantity = json["completedOrderQuantity"].Value<int>();
            this.CreatedDate = DateUtil.TimestampMSToDateTime(json["createdDate"].Value<long>());
            this.HxUserName = json["hxUserName"].Value<string>();
            this.KycLevel = json["kycLevel"].Value<int>();
            this.NickName = json["nickName"].Value<string>();
            this.PublicUserId = json["publicUserId"].Value<string>();
            this.RealName = json["realName"].Value<string>();
            

            this.ShowContractInfo = json["showContractInfo"].Value<bool>();
            this.Type = json["type"].Value<string>();
        }

        public object Clone()
        {
            var n = new UserVo();
            n.AvgCompleteTime = this.AvgCompleteTime;
            n.AvgPaymentTime = this.AvgPaymentTime;
            n.Blacker = this.Blacker;
            n.CancelledOrderQuantity = this.CancelledOrderQuantity;
            n.CompletedOrderQuantity = this.CompletedOrderQuantity;
            n.CreatedDate = this.CreatedDate;
            n.HxUserName = this.HxUserName;
            n.KycLevel = this.KycLevel;
            n.NickName = this.NickName;
            n.PublicUserId = this.PublicUserId;
            n.RealName = this.RealName;
            n.ShowContractInfo = this.ShowContractInfo;
            n.Type = this.Type;

            return n;
        }
    }
}

/*
 * {
                    "avgCompleteTime":0
                    ,"avgPaymentTime":413
                    ,"blacker":false
                    ,"cancelledOrderQuantity":0
                    ,"completedOrderQuantity":1
                    ,"createdDate":1539671131000
                    ,"hxUserName":"35e11bca70"
                    ,"kycLevel":2
                    ,"nickName":""
                    ,"publicUserId":"35e11bca70"
                    ,"realName":"吕苏"
                    ,"sellerAllReceiptAccountList":
                    [
                        {"accountName":"吕苏"
                            ,"accountNo":"6222801252412529184"
                            ,"accountQrCodeUrl":""
                            ,"bankBranchName":"建行牌楼支行"
                            ,"bankCode":"BANK"
                            ,"bankName":"建设银行"
                            ,"currency":"CNY"
                            ,"disabled":false
                            ,"id":547463
                            ,"type":"bank"
                        }
                    ]
                    ,"sellerReceiptAccount":
                    {
                        "accountName":"吕苏"
                        ,"accountNo":"6222801252412529184"
                        ,"accountQrCodeUrl":""
                        ,"bankBranchName":"建行牌楼支行"
                        ,"bankCode":"BANK"
                        ,"bankName":"建设银行"
                        ,"currency":"CNY"
                        ,"disabled":false
                        ,"id":547463
                        ,"type":"bank"
                    }
                    ,"showContractInfo":false
                    ,"type":"common"
                }

    */