using CoinTradeOKX.Event;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX.Okex.Behavior
{
    [BehaviorName(Name ="仓位控制")]
    public class StoreBehavior : BehaviorBase
    {
        [BehaviorParameter(Name = "资金持仓金额(RMB)", Min = 0, Max = 1500000)]
        public decimal OTCHold
        {
            get;set;
        }

        [BehaviorParameter(Name = "币币持仓金额(RMB)", Min = 0, Max = 1500000)]
        public decimal CTCHold
        {
            get;set;
        }

        private bool _autoBuy = true;
        /**
         * 卖出一笔自动补仓一笔
      
        [BehaviorParameter(Name = "自动补仓")]
        public bool AutoBuy
        {
            get
            {
                return _autoBuy;
            }
            set
            {

                _autoBuy = value;
            }
        }   */

        private CurrencyMarket market = null;
        private bool checking = false;
        private bool isAnchorCurrency = false;

        private CountDown cd = new CountDown(2100, true);
        public StoreBehavior(CurrencyMarket market)
        {
            this.Enable = true;
            this.market = market;
            market.OnMarketChanged += CheckAmount;
            market.OnAmountChanged += CheckAmount;
            isAnchorCurrency = string.Compare(market.Currency, Config.Instance.Anchor) == 0;
        }


 
        public override void Dispose()
        {
            market.OnMarketChanged -= this.CheckAmount;
            market.OnAmountChanged -= this.CheckAmount;
        }

        //public override void OnChanged()
        //{
        //    base.OnChanged();

        //    this.CheckAmount();
        //}

        private void CheckAmount()
        {
            if (!this.Enable)
                return;

            if (!market.Effective)
                return;

            if (!USDXMarket.Instance.Effective)
                return;

            if (this.checking)
                return;

            this.checking = true;

            decimal basePrice = isAnchorCurrency ? market.OTCAsk : GetBasePriceCNY();

            decimal otcAvalibleAmount = basePrice * (market.AvalibleInAccount);// + market.HoldInAccount);
            decimal ctcAvalibleAmount = basePrice * (market.AvalibleInCtcMarket);// + market.HoldInCtcMarket);

            decimal trans = 0;
            WalletType from = WalletType.Account;
            WalletType to = WalletType.Account;
            decimal minSize = isAnchorCurrency ? 1 : market.Instrument.MinSize;

            const decimal error = (decimal)0.02;

            if (this.OTCHold > 0 && otcAvalibleAmount < this.OTCHold && ctcAvalibleAmount > this.CTCHold)
            {
                decimal p = (this.OTCHold - otcAvalibleAmount) / this.OTCHold;

                if (p >= error)
                {
                    decimal diff = (this.OTCHold - otcAvalibleAmount) / basePrice;

                    if (diff > minSize)
                    {
                        trans = Math.Min(diff, market.AvalibleInCtcMarket);
#if OKEX_API_V5
                        from = WalletType.Unified;
#else
                        from = WalletType.CTC;
#endif
                        to = WalletType.Account;
                    }
                }
            }
            else if (this.CTCHold > 0 && ctcAvalibleAmount < this.CTCHold && otcAvalibleAmount > this.OTCHold)
            {
                decimal p = (this.CTCHold - ctcAvalibleAmount) / this.CTCHold;

                if (p >= error)
                {
                    decimal diff = (this.CTCHold - ctcAvalibleAmount) / basePrice;

                    if (diff > minSize)
                    {
                        trans = Math.Min(diff, market.AvalibleInAccount);

                        from = WalletType.Account;

#if OKEX_API_V5
                        to = WalletType.Unified;
#else
                        to = WalletType.CTC;
#endif

                    }
                }
            }

            if (trans > 0)
            {
    
                this.Executing = true;
                this.market.CurrencyTrensfer(from, to, trans);
                this.Executing = false; 
            }

            this.checking = false;
        }


        
        /// <summary>
        /// 仓位控制基准价格
        /// </summary>
        /// <returns></returns>
        private decimal GetBasePriceCNY()
        {
            const decimal half = (decimal)0.5;

            decimal anchorAsk = USDXMarket.Instance.OTCAsk;
            decimal anchorBid = USDXMarket.Instance.OTCBid;

            anchorAsk = Math.Max(anchorAsk, anchorBid); //防止价格倒挂出现的价格错误
            anchorBid = Math.Min(anchorBid, anchorAsk);

            decimal anchorPrice = (anchorAsk + anchorBid) * half;

            return   (market.CTCBid + market.CTCAsk) * half * anchorPrice;
        }

#region 交易单处理
        HashSet<long> ProcessedContracts = new HashSet<long>();
        private string ProcessedContractFile = "Processed_Contract.txt";
        private string GetProcessedContractFilePath()
        {
            return Path.Combine(Application.StartupPath, ProcessedContractFile);
        }
        private void LoadProcessedContract()
        {
            string content = "";
            string path = GetProcessedContractFilePath();
            try
            {
                if (File.Exists(path))
                    content = File.ReadAllText(GetProcessedContractFilePath());
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                return;
            }

            var ids = content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var id in ids)
            {
                this.ProcessedContracts.Add(long.Parse(id));
            }
        }
        private void AddProcessedContract(long orderId)
        {
            ProcessedContracts.Add(orderId);

            try
            {
                File.AppendAllText(GetProcessedContractFilePath(), orderId + ",");
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }
#endregion
    }
}
