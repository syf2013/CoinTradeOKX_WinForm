using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    /**
     * 收款账号类
     */ 
    public class ReceiptAccount:ICloneable
    {
        /**
         * 户名
         */
        public string AccountName { get; set; }
        /**
         * 账号
         */ 
        public string AccountNo { get; set; }
        /**
         * 二维码地址
         */ 
        public string AccountQrCodeUrl { get; set; }
        /**
         * 开户行
         */ 
        public string BankBranchName { get; set; }
        /**
         * BANK
         */ 
        public string BankCode { get; set; }
        /**
         * 银行名称
         */ 
        public string BankName { get; set; }
        /**
         * 货币类型
         */ 
        public string Currency { get; set; }
        /**
         *禁用 
         */
        public bool  Disabled { get; set; }
        /**
         * id
         */ 
        public long Id{ get; set; }
        /**
         * bank
         */
        public string  Type { get; set; }

        public virtual void ParseFromJson(JToken json)
        {
            this.AccountName = json["accountName"].Value<string>();
            this.AccountNo = json["accountNo"].Value<string>();
            this.AccountQrCodeUrl = json["accountQrCodeUrl"].Value<string>();
            this.BankBranchName = json["bankBranchName"].Value<string>();
            this.BankCode = json["bankCode"].Value<string>();
            this.BankName = json["bankName"].Value<string>();
            this.Currency = json["currency"].Value<string>();
            this.Disabled = json["disabled"].Value<bool>();
            this.Id = json["id"].Value<long>();
            this.Type = json["type"].Value<string>();
        }

        public object Clone(ReceiptAccount target)
        {
            var n = target != null ? target : new ReceiptAccount();
            n.AccountName = this.AccountName;
            n.AccountNo = this.AccountNo;
            n.AccountQrCodeUrl = this.AccountQrCodeUrl;
            n.BankBranchName = this.BankBranchName;
            n.BankCode = this.BankCode;
            n.BankName = this.BankName;
            n.Currency = this.Currency;
            n.Disabled = this.Disabled;
            n.Id = this.Id;
            n.Type = this.Type;

            return n;
        }

        public virtual object Clone()
        {
            return this.Clone(null);
        }

        /*
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
                    */
    }

    public class ReceiptAccountItem:ReceiptAccount,ICloneable
    {
        public AccountApplyType ApplyType
        {
            get;set;
        }

        public override void ParseFromJson(JToken json)
        {
            base.ParseFromJson(json);
            this.ApplyType = (AccountApplyType)json.Value<int>("applyType");

        }

        public override object Clone()
        {
            var obj =  new ReceiptAccountItem();
             base.Clone(obj);
            obj.ApplyType = this.ApplyType;

            return obj;
        }
    }
}
