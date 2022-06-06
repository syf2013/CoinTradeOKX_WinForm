using CoinTradeGecko.Invoke;
using CoinTradeGecko.Okex.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex
{
    public static class OTCTrading
    {
        public static Order CreateOrderSync(string currency, decimal amount, decimal price, OrderOparete type,int kycLevel = 1)
        {
            okex_Order_Create invoke = new okex_Order_Create();

            invoke.baseCurrency = currency;
            invoke.baseAmount = amount;
            invoke.price = price.ToString();
            
            switch(type)
            {
                case OrderOparete.Buy:
                    invoke.side = "buy";
                    break;
                case OrderOparete.Sell:
                    invoke.side = "sell";
                    break;
            }

            JToken data =  invoke.execSync();
            if(data["code"].Value<int>() == 0)
            {
                Order o = Pool<Order>.GetPool().Get();
                o.ParseFromJson(data["data"]);

                return o;
            }
            else
            {
                //Logger.Instance.Log(LogType.Error, invoke.GetType().Name + " " + data["msg"].Value<string>());
            }

            return null;
            //{"code":0,"data":{"availableAmount":0.9997,"banned":false,"bannedReason":"","bannedReasonCode":0,"baseCurrency":"ltc","baseScale":0,"blackState":"","blocked":false,"brokerId":0,"completedAmount":0,"completedOrderTotal":0,"createdDate":1558656755000,"creator":{"acceptState":"start","agreedTos":false,"avgCompleteTime":82,"avgPaymentTime":137,"boundBankCard":false,"boundPhoneNumber":false,"cancelledOrderQuantity":1,"commonOrderTotal":"","completedOrderQuantity":87,"completionRate":0.99,"createdDate":1518826461000,"disabled":false,"kycLevel":3,"nickName":"温**","realName":"温**","strategyDisabled":false,"strategyDisabledReason":"","type":"common"},"exchangeRateDeviateTooFar":false,"floatRate":1,"hidden":false,"hiddenPrice":0,"holdAmount":0,"index":0,"isBlockTrade":false,"maxAvgCompleteTime":0,"maxAvgPaymentTime":0,"maxCompletedOrderQuantity":0,"maxUserCreatedDate":1555716645000,"minCompletedOrderQuantity":0,"minCompletionRate":0,"minKycLevel":1,"mine":false,"paymentMethods":["bank"],"platformCommissionRate":"0.0003","price":656,"publicId":"190524081235595","publicMerchantId":"","quoteCurrency":"cny","quoteMaxAmountPerOrder":2.0E7,"quoteMinAmountPerOrder":400,"quotePriceScale":0,"quoteScale":0,"remark":"","side":"sell","status":"new","type":"limit","unpaidOrderTimeoutMinutes":10,"userType":"all"},"detailMsg":"","msg":""}

        }

        //public static Task<JToken> CreateOrderAsync(BrowserProxy browser, string currency, decimal amount, decimal price, OrderOparete type, int kycLevel = 1)
    }
}
