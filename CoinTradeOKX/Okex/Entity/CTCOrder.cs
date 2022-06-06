using Common.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class CTCOrder : OrderBase
    {

        public string ClientOid { get; set; }

        public decimal Size { get; set; }
        public decimal Notional { get; set; }//  买入金额，市价买入时返回

        public string Type { get; set; } //  limit或market（默认是limit）

        public decimal FilledSize { get; set; } //已成交数量
        public decimal FilledNotional { get; set; } // 已成交金额
                                            /*
                                            String	0：普通委托
                                            1：只做Maker（Post only）
                                            2：全部成交或立即取消（FOK）
                                            3：立即成交并取消剩余（IOC）
                                            */
        public string OrderType { get; set; }

        /*
        state String  订单状态
        -2：失败
        -1：撤单成功
        0：等待成交
        1：部分成交
        2：完全成交
        3：下单中
        4：撤单中*/
        public CTCOrderState State { get; set; }
        public decimal PriceAvg { get; set; }//   String 成交均价
        public string FeeCurrency { get; set; } // String  交易手续费币种，如果是买的话，就是收取的BTC；如果是卖的话，收取的就是USDT
        public decimal Fee { get; set; }// String 订单交易手续费。平台向用户收取的交易手续费，为负数，例如：-0.01
        public string RebateCurrency { get; set; } // String  返佣金币种 USDT
        public decimal Rebate { get; set; }// String  返佣金额，平台向达到指定lv交易等级的用户支付的挂单奖励（返佣），如果没有返佣金，该字段为“”，为正数，例如：0.5
        public CTCOrder()
        {

        }

        public void ParseFromDataRow(DataRow row)
        {
            this.PublicId = Convert.ToInt64( row["order_id"]);
            this.ClientOid = row["client_oid"].ToString();
            this.Price = Convert.ToDecimal( row["price"]);
            this.AvailableAmount = Convert.ToDecimal( row["size"]);
            this.Size = this.AvailableAmount;
            this.Notional = Convert.ToDecimal(row["notional"]);
            this.Currency = row["instrument_id"].ToString();
            this.Type = row["type"].ToString();
            this.Side = row["side"].ToString() == "sell" ? OrderOparete.Sell : OrderOparete.Buy;
            this.CreatedDate = Convert.ToDateTime(row["timestamp"]);
            this.FilledSize = Convert.ToDecimal(row["filled_size"]);
            this.FilledNotional = Convert.ToDecimal( row["filled_notional"]);
            this.OrderType = row["order_type"].ToString();
            this.State = (CTCOrderState)row["state"];
            this.PriceAvg = Convert.ToDecimal( row["price_avg"]);

            this.FeeCurrency = row["fee_currency"].ToString();
            this.Fee = Convert.ToDecimal( row["fee"]);
        }

        public override void ParseFromJson(JToken obj)
        {

            this.PublicId = obj.Value<long>("order_id");
            this.ClientOid = obj.Value<string>("client_oid");
            this.Price = string.IsNullOrEmpty(obj.Value<string>("price")) ? 0 : obj.Value<decimal>("price");
            this.AvailableAmount = obj.Value<decimal>("size");
            this.Size = this.AvailableAmount;
            this.Notional = string.IsNullOrEmpty(obj.Value<string>("notional")) ? 0 : obj.Value<decimal>("notional");// obj.Value<decimal>("notional");
            this.Currency = obj.Value<string>("instrument_id");
            this.Type = obj.Value<string>("type");
            this.Side = obj.Value<string>("side") == "sell" ? OrderOparete.Sell: OrderOparete.Buy;
            this.CreatedDate = DateTime.Parse(obj.Value<string>("created_at")); // DateTime.Parse( obj.Value<string>("timestamp"));
            this.FilledSize = obj.Value<decimal>("filled_size");
            this.FilledNotional = obj.Value<decimal>("filled_notional");
            this.OrderType = obj.Value<string>("order_type");
            this.State = ( CTCOrderState) obj.Value<int>("state");
            this.PriceAvg = obj.Value<decimal>("price_avg");

            this.FeeCurrency = obj.Value<string>("fee_currency");
            this.Fee = string.IsNullOrEmpty(obj.Value<string>("fee")) ? 0 : obj.Value<decimal>("fee");


            /*
             order_id	String	订单ID
client_oid	String	用户设置的订单ID
price	String	委托价格
size	String	委托数量（交易货币数量）
notional	String	买入金额，市价买入时返回
instrument_id	String	币对名称
type	String	limit或market（默认是limit）
side	String	buy 或 sell
timestamp	String	订单创建的时间
filled_size	String	已成交数量
filled_notional	String	已成交金额
order_type	String	0：普通委托
1：只做Maker（Post only）
2：全部成交或立即取消（FOK）
3：立即成交并取消剩余（IOC）
state	String	订单状态
-2：失败
-1：撤单成功
0：等待成交
1：部分成交
2：完全成交
3：下单中
4：撤单中
price_avg	String	成交均价
fee_currency	String	交易手续费币种，如果是买的话，就是收取的BTC；如果是卖的话，收取的就是USDT
fee	String	订单交易手续费。平台向用户收取的交易手续费，为负数，例如：-0.01
rebate_currency	String	返佣金币种 USDT
rebate	String	返佣金额，平台向达到指定lv交易等级的用户支付的挂单奖励（返佣），如果没有返佣金，该字段为“”，为正数，例如：0.5
             */
        }
    }
}
