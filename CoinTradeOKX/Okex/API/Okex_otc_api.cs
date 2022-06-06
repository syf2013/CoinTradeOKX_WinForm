using CoinTradeOKX.Invoke;
using CoinTradeOKX.Okex.API;
using CoinTradeOKX.Util;
using Common.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    public abstract class OTCInvokeBase : OTCInvoke
    {
        public OTCInvokeBase(string url)
        {
            // this.Namespance = "otc_proxy";
            this.Url = url.StartsWith(Okex_REST_API.UrlRoot)? url: Okex_REST_API.UrlRoot + url;
            this.Method = Http.Method_Post;
        }


        protected virtual string Url
        {
            get;set;
        }

        protected virtual string Method
        {
            get;set;
        }


        public override Task<JToken> execAsync()
        {
            return Task<JToken>.Run(() => { return this.execSync(); });
        }

        public override JToken execSync()
        {
            if (isExecuting)//---
            {
                var err = "Request Pending";
                Logger.Instance.Log(LogType.Error, this.GetType().Name + " " + err);

                return
                 new JObject()
                 {
                     ["code"] = 12026,
                     ["msg"] = err
                 };
            }

            return null;
        }
    }

    public class okex_My_Order_List : OTCInvokeBase
    {
        public okex_My_Order_List()
            :base(Okex_otc_api_url.okex_api_mine_orders)
        {
            this.Method = Http.Method_Get;
        }
    }

    /**
     * 查询法币账户余额
     */
    public class okex_Coin_Amount: OTCInvokeBase
    {
        public okex_Coin_Amount( string currency )
            :base(string.Format( Okex_otc_api_url.okex_api_coin_amount, currency.ToLower()))
        {
            this.currency = currency;
            this.Method = Http.Method_Get;
        }

        [JsonProperty]
        public string currency { get; set; }//数字货币类型
    }

    /**
    * 收付款账户列表
    */
    public class okex_receipetAccount_list : OTCInvokeBase
    {
        public okex_receipetAccount_list()
            : base(Okex_otc_api_url.okex_api_receiptAccounts)
        {
            this.Method = Http.Method_Get;
        }
    }

    /**
    * 收付款删除账号
    */
    public class okex_receipetAccount_Delete : OTCInvokeBase
    {
        public okex_receipetAccount_Delete(long id)
            : base( string.Format("{0}/{1}", Okex_otc_api_url.okex_api_receiptAccounts,id))
        {
            this.Method = Http.Method_Delete;
        }
    }

    /// <summary>
    /// 开启或关闭收款账号
    /// </summary>
    public class okex_receipetAccount_Disable : OTCInvokeBase
    {
        public okex_receipetAccount_Disable(long id,bool disable)
            : base( string.Format(Okex_otc_api_url.okex_api_account_status, id))
        {
            this.Method = Http.Method_Post;

            this.disabled = disable;
        }

        public bool disabled
        {
            get;set;
        }
    }

    /// <summary>
    /// 设置账号的首付款
    /// </summary>
    public class okex_receipetAccount_ApplyType : OTCInvokeBase
    {
        public okex_receipetAccount_ApplyType(long id, AccountApplyType type)
            : base(string.Format( Okex_otc_api_url.okex_api_receiptAccount_apply, id))
        {
            this.Method = Http.Method_Post;
            this.option = Convert.ToInt32(type);
        }

        public int option
        {
            get;set;
        }
    }


    /**
   * 法币成交
   */
    public class okex_contract_list : OTCInvokeBase
    {
        public okex_contract_list()
            :base(Okex_otc_api_url.okex_api_contract)
        {
            this.Method = Http.Method_Get;
        }

        public string status
        {
            get;set;
        }
    }

    public class okex_api_order_confrimed:OTCInvokeBase
    {
        public okex_api_order_confrimed(string orderId, string tradePassword )
            :base(string.Format(Okex_otc_api_url.okex_api_order_confrimed, orderId))
        {
            this.Method = Http.Method_Post;
            this.tradePassword = tradePassword;
        }

        [JsonProperty]
        public string tradePassword { get; set; }//交易密码
    }

    public class okex_api_history :OTCInvokeBase
    {
        public okex_api_history(string currency, int pageSize, int pageIndex, HistoryOrderStateEnum type,HistoryOrderStateEnum state)
            :base(string.Format(Okex_otc_api_url.okex_api_history, currency, ((int)type), ((int)state), pageIndex, pageSize))
        {
            this.Method = Http.Method_Get;
        }
    }

    /**
     * 挂单取消
     */
    public class okex_Order_Cancel : OTCInvokeBase
    {

        public string Id { get; private set; }
        public okex_Order_Cancel(string id)
            :base(string.Format(Okex_otc_api_url.okex_api_cancel_order, id))
        {
            this.Id = id;
        }
    }

    /**
     * 
     */ 
    public class okex_Order_Info : OTCInvokeBase
    {
        public okex_Order_Info(long orderId)
            : base(string.Format(Okex_otc_api_url.okex_api_order_info, orderId))
        {

        }
    }

    public class okex_Order_Create : OTCInvokeBase
    {

        public okex_Order_Create()
            :base(Okex_otc_api_url.okex_api_order_place)
        {

            this.price = "";
            this.hiddenPrice = "";
            this.quoteCurrency = "cny";
            this.type = Const.PriceType.Limit;
            this.quoteMinAmountPerOrder = 400;
            this.quoteMaxAmountPerOrder = 20000000;
            this.userType = "all";
            this.unpaidOrderTimeoutMinutes = 10;
 

            this.remark = "";



            /*
             * let orderinfo =
            {
                "side": param.side,
                "baseCurrency": param.currency,
                "quoteCurrency": "cny",
                "type": param.type || "limit",
                "baseAmount": param.amount,
                "quoteMinAmountPerOrder": param.quoteMinAmountPerOrder || 400,
                "quoteMaxAmountPerOrder": param.quoteMaxAmountPerOrder || 20000000,
                "remark": param.remark || "",
                "hiddenPrice": null,
                "userType": param.userType || "all",
                "minKycLevel": kycLimit, //对手等级限制
                "price": "" + param.price, //价格    
                "unpaidOrderTimeoutMinutes": param.unpaidOrderTimeoutMinutes || "15", //付款时间限制
                "maxUserCreatedDate": time,
                "minCompletedOrderQuantity": param.minCompletedOrderQuantity || 1 //最低完成订单数
            }
             */
        }

        /// <summary>
        /// 数字货币类型
        /// </summary>
        [JsonProperty]
        public string baseCurrency { get; set; }//

  

        /// <summary>
        /// 完成率限制
        /// </summary>
        [JsonProperty]
        public float minCompletionRate { get; set; }

        /// <summary>
        /// 最小卖单数，仅在挂买单时有效
        /// </summary>
        [JsonProperty]
        public int minSellOrderQuantity { get; set; }

        /// <summary>
        /// 对手类型，所有、商户、非商户
        /// </summary>
        [JsonProperty]
        public string userType { get; set; }

        /// <summary>
        /// 隐藏价格
        /// </summary>
        [JsonProperty]
        public string hiddenPrice { get; set; }

        /// <summary>
        /// 法币类型(cny)
        /// </summary>
        [JsonProperty]
        public string quoteCurrency { get; set; } 

        /// <summary>
        /// 付款时间， 仅在挂卖单时候有效
        /// </summary>
        [JsonProperty]
        public int unpaidOrderTimeoutMinutes { get; set; }

        /// <summary>
        /// 买入buy或者卖出sell
        /// </summary>
        [JsonProperty]
        public string side { get; set; }//买卖类型（sell/buy）
        

        [JsonProperty]
        public decimal quoteMinAmountPerOrder { get; set; }



        [JsonProperty]
        public decimal quoteMaxAmountPerOrder { get; set; }

        [JsonProperty]
        public string price { get; set; } // 单价

        [JsonProperty]
        public decimal baseAmount { get; set; } //数量

        [JsonProperty]
        public string type { get; set; } //价格类型，浮动、固定 limit

        [JsonProperty]
        public double floatRate { get; set; } //价格浮动范围



        [JsonProperty]
        public string tradePassword { get; set; } //资金密码

        [JsonProperty]
        public string remark { get; set; }//挂单备注

        /*废弃参数
        /// <summary>
        /// 用户注册日期限制
        /// </summary>
        [JsonProperty]
        public long maxUserCreatedDate { get; set; }

        /// <summary>
        /// 最少完成订单数限制
        /// </summary>
        [JsonProperty]
        public int minCompletedOrderQuantity { get; set; }

        [JsonProperty]
        public int minKycLevel { get; set; } //验证等级限制（默认1）
        */
    }

    public class okex_order_paid:OTCInvokeBase
    {
        public okex_order_paid(string id, long receiptAccountId)
            :base(string.Format( Okex_otc_api_url.okex_api_order_paid,id))
        {
            this.receiptAccountId = receiptAccountId;
        }
        [JsonProperty]
        public long receiptAccountId { get; set; }
    }
}
