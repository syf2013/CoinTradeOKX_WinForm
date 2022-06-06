using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{

    /**
     * 还未交割订单
     */
    public class OTCContract:ICloneable
    {
        public bool Arbitrating { get; set; }
        public decimal BaseAmount { get; set; }
        public string BaseCurrency { get; set; }
        public bool Blacker { get; set; }
        public string CompleteType { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExpiredAfterSeconds { get; set; }
        public bool Frozen { get; set; }
        public string FrozenType { get; set; }
        public bool IsBlockTrade { get; set; }
        public UserVo OrderDetailUserVo { get; set; }
        public List<ReceiptAccount> SellerAllReceiptAccountList { get; set; }
        public ReceiptAccount SellerReceiptAccount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime PaidDate { get; set; }
        public string[] PaymentProofFileUrls { get; set; }
        public string PaymentStatus { get; set; }
        public decimal PlatformInAmount { get; set; }
        public decimal Price { get; set; }
        public long PublicOrderId { get; set; }
        public decimal QuoteAmount { get; set; }
        public string QuoteCurrency { get; set; }
        public string RealName { get; set; }
        public bool Reward { get; set; }
        public string Side { get; set; }
        public string TradingOrderRemark { get; set; }

        public void ParseFromJson(JToken json)
        {
            Arbitrating = json["arbitrating"].Value<bool>();
            BaseAmount = json["baseAmount"].Value<decimal>();
            BaseCurrency = json["baseCurrency"].Value<string>();
            Blacker = json["blacker"].Value<bool>();
            CompleteType = json["completeType"].Value<string>();
            CompletedDate = DateUtil.TimestampMSToDateTime(json["completedDate"].Value<long>());
            CreatedDate = DateUtil.TimestampMSToDateTime(json["createdDate"].Value<long>());
            ExpiredAfterSeconds = json["expiredAfterSeconds"].Value<int>();
            Frozen = json["frozen"].Value<bool>();
            FrozenType = json["frozenType"].Value<string>();
            IsBlockTrade = json["isBlockTrade"].Value<bool>();

            UserVo u = new UserVo();
            u.ParseFromJson(json["orderDetailUserVo"]);

            OrderDetailUserVo = u;


            OrderStatus = json["orderStatus"].Value<string>();
            PaidDate = DateUtil.TimestampMSToDateTime(json["paidDate"].Value<long>());

            PaymentStatus = json["paymentStatus"].Value<string>();
            PlatformInAmount = json["platformInAmount"].Value<decimal>();
            Price = json["price"].Value<decimal>();
            PublicOrderId = json["publicOrderId"].Value<long>();
            QuoteAmount = json["quoteAmount"].Value<decimal>();
            QuoteCurrency = json["quoteCurrency"].Value<string>();
            RealName = json["realName"].Value<string>();
            Reward = json["reward"].Value<bool>();
            Side = json["side"].Value<string>();
            TradingOrderRemark = json["tradingOrderRemark"].Value<string>();


            var jarr = json["paymentProofFileUrls"] as JArray;

            if (jarr != null)
            {
                var s = new string[jarr.Count];
                for (var i = 0; i < jarr.Count; i++)
                {
                    s[i] = jarr[i].Value<string>();
                }
            }
            else
            {
                PaymentProofFileUrls = new string[0];
            }

            this.SellerAllReceiptAccountList = new List<ReceiptAccount>();

            var list = json["sellerAllReceiptAccountList"] as JArray;
            if (list != null)
            {
                foreach (var acc in list)
                {
                    var account = new ReceiptAccount();
                    account.ParseFromJson(acc);
                    this.SellerAllReceiptAccountList.Add(account);
                }
            }

            var receiptAccount = json["sellerReceiptAccount"];
            if (receiptAccount != null)
            {
                var account = this.SellerReceiptAccount == null ?  new ReceiptAccount(): this.SellerReceiptAccount;
                account.ParseFromJson(receiptAccount);
                this.SellerReceiptAccount = account;
            }
        }

        public object Clone()
        {
            var n = new OTCContract();
            this.CopyTo(n);
            return n;
        }

        public void CopyTo(OTCContract contract)
        {
            var n = contract;
            n.Arbitrating = this.Arbitrating;
            n.BaseAmount = this.BaseAmount;
            n.BaseCurrency = this.BaseCurrency;
            n.Blacker = this.Blacker;
            n.CompletedDate = this.CompletedDate;
            n.CompleteType = this.CompleteType;
            n.CreatedDate = this.CreatedDate;
            n.ExpiredAfterSeconds = this.ExpiredAfterSeconds;
            n.Frozen = this.Frozen;
            n.FrozenType = this.FrozenType;
            n.IsBlockTrade = this.IsBlockTrade;
            n.OrderDetailUserVo = this.OrderDetailUserVo.Clone() as UserVo;
            n.OrderStatus = this.OrderStatus;
            n.PaidDate = this.PaidDate;

            if (this.PaymentProofFileUrls != null)
            {
                n.PaymentProofFileUrls = new string[this.PaymentProofFileUrls.Length];
                Array.Copy(this.PaymentProofFileUrls, n.PaymentProofFileUrls, n.PaymentProofFileUrls.Length);
            }

            n.PaymentStatus = this.PaymentStatus;
            n.PlatformInAmount = this.PlatformInAmount;
            n.Price = this.Price;
            n.PublicOrderId = this.PublicOrderId;
            n.QuoteAmount = this.QuoteAmount;
            n.QuoteCurrency = this.QuoteCurrency;
            n.RealName = this.RealName;
            n.Reward = this.Reward;
            n.SellerAllReceiptAccountList = new List<ReceiptAccount>();

            foreach (var s in this.SellerAllReceiptAccountList)
            {
                n.SellerAllReceiptAccountList.Add(s.Clone() as ReceiptAccount);
            }

            n.SellerReceiptAccount = this.SellerReceiptAccount.Clone() as ReceiptAccount;
            n.Side = this.Side;
            n.TradingOrderRemark = this.TradingOrderRemark;
        }
    }
}


/** 买入单
                  "arbitrating":false
                ,"baseAmount":"95.51"
                ,"baseCurrency":"usdt"
                ,"blacker":false
                ,"completeType":""
                ,"completedDate":0
                ,"createdDate":1559088832000
                ,"expiredAfterSeconds":574
                ,"frozen":false
                ,"frozenType":""
                ,"isBlockTrade":false
                ,"orderDetailUserVo":
                {
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
                ,"orderStatus":"new"
                ,"paidDate":0
                ,"paymentProofFileUrls":[]
                ,"paymentStatus":"unpaid"
                ,"platformInAmount":"0"
                ,"price":"6.97"
                ,"publicOrderId":190529081351714
                ,"quoteAmount":"665.70"
                ,"quoteCurrency":"cny"
                ,"realName":"吕苏"
                ,"reward":false
                ,"side":"buy"
                ,"tradingOrderRemark":""
 * 
 */

/*
 * 卖出成交
 * 未付款
 * {"code":0,"data":{"items":[{"arbitrating":false,"baseAmount":"0.7484","baseCurrency":"ltc","blacker":false,"completeType":"","completedDate":0,"createdDate":1559175092000,"expiredAfterSeconds":1192,"frozen":false,"frozenType":"","isBlockTrade":false,"orderDetailUserVo":{"avgCompleteTime":0,"avgPaymentTime":0,"blacker":false,"cancelledOrderQuantity":0,"completedOrderQuantity":0,"createdDate":1557537279000,"hxUserName":"198d4ef354","kycLevel":1,"nickName":"","publicUserId":"198d4ef354","realName":"林锦燕","sellerAllReceiptAccountList":[{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"}],"sellerReceiptAccount":{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"},"showContractInfo":false,"type":"common"},"orderStatus":"new","paidDate":0,"paymentProofFileUrls":[],"paymentStatus":"unpaid","platformInAmount":"0.0015","price":"820","publicOrderId":190530081132976,"quoteAmount":"613.69","quoteCurrency":"cny","realName":"林锦燕","reward":false,"sellerAllReceiptAccountList":[{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"}],"sellerReceiptAccount":{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"},"side":"sell","tradingOrderRemark":""}],"orderNotice":{"new":0,"cancelled":0,"completed":0},"pageCount":1,"pageIndex":1,"pageInfo":{"pageCount":1,"pageIndex":1,"pageSize":50,"totalItemCount":1},"pageSize":50,"totalItemCount":1},"detailMsg":"","msg":""}
 * 已付款
 * {"code":0,"data":{"items":[{"arbitrating":false,"baseAmount":"0.7484","baseCurrency":"ltc","blacker":false,"completeType":"","completedDate":0,"createdDate":1559175092000,"expiredAfterSeconds":43184,"frozen":false,"frozenType":"","isBlockTrade":false,"orderDetailUserVo":{"avgCompleteTime":0,"avgPaymentTime":0,"blacker":false,"cancelledOrderQuantity":0,"completedOrderQuantity":0,"createdDate":1557537279000,"hxUserName":"198d4ef354","kycLevel":1,"nickName":"","publicUserId":"198d4ef354","realName":"林锦燕","sellerAllReceiptAccountList":[{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"}],"sellerReceiptAccount":{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"},"showContractInfo":true,"type":"common"},"orderStatus":"new","paidDate":1559175215000,"paymentProofFileUrls":[],"paymentStatus":"paid","platformInAmount":"0.0015","price":"820","publicOrderId":190530081132976,"quoteAmount":"613.69","quoteCurrency":"cny","realName":"林锦燕","reward":false,"sellerAllReceiptAccountList":[{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"}],"sellerReceiptAccount":{"accountName":"温泉","accountNo":"6214855921076077","accountQrCodeUrl":"","bankBranchName":"招商银行厦门分行金榜支行","bankCode":"CMB","bankName":"招商银行","currency":"CNY","disabled":false,"id":259634,"type":"bank"},"side":"sell","tradingOrderRemark":""}],"orderNotice":{"new":0,"cancelled":0,"completed":0},"pageCount":1,"pageIndex":1,"pageInfo":{"pageCount":1,"pageIndex":1,"pageSize":50,"totalItemCount":1},"pageSize":50,"totalItemCount":1},"detailMsg":"","msg":""}
 * */
