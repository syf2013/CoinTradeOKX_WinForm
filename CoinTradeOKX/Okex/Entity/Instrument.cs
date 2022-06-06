using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class Instrument
    {
        private int GetDigit(decimal num){return num == 0 ? 0 : (int)Math.Log10((double)(1 / num));}

        /**
         * "BTC",交易货币币种
         */
        public string BaseCurrency { get; set; }

        public string QuoteCcy { get; set; }

        /**
         * "BTC-USDT"	币对名称
         */
        public string InstrumentId { get; set; }

        /**
         * "0.001"最小可交易数量
         */
        public decimal MinSize { get; set; }
        public int MinSizeDigit { get { return Math.Max(0, GetDigit(MinSize)); } }

        public string QuoteCurrency { get; set; } // ""USDT",	计价货币币种


        /**
         * 价格精度"0.1":	
         */
        public decimal TickSize{get;set; }

        /**
         * 价格精度小数位数:	
         */
        public int TickSizeDigit{get{return GetDigit(TickSize);}}

        private string _priceFormat = "";
        public string PriceFormat
        {
            get
            {
                if (string.IsNullOrEmpty(_priceFormat))
                {
                    _priceFormat  = this.TickSize.ToString().Replace("1", "0");
                }

                return _priceFormat;
            }
        }

        /**
         * 格式化时候显示的数字
         */
        private string _amountFormat = null;
        public string AmountFormat
        {
            get
            {
                if (_amountFormat == null)
                    _amountFormat = (new Regex("[^0\\\\.]")).Replace(this.MinSize.ToString(), "0");// .Replace("1", "0");

                return _amountFormat;
            }
        }

        public void ParseFromJsonV5(JToken data)
        {
            this.BaseCurrency = data["baseCcy"].Value<string>();
            this.InstrumentId = data["instId"].Value<string>();
            this.QuoteCcy = data ["quoteCcy"].Value<string>(); 
            this.MinSize = data["minSz"].Value<decimal>();
            this.QuoteCurrency = data["quoteCcy"].Value<string>();
            this.TickSize = data["tickSz"].Value<decimal>();
        }
    }
}
