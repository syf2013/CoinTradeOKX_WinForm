using CoinTradeOKX.Event;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Manager
{
    public class OTCCashManager
    {

        private decimal m_totalCash = 0;
        public decimal TotalCash
        {
            get
            {
                lock (this)
                {
                    return this.m_totalCash;
                }
            }
        }


        public void SpendCash(decimal money)
        {
            bool changed = false;
            lock (this)
            {
                var old = this.m_totalCash;
                var m = this.m_totalCash - money;
                m = Math.Max(m, 0);
                this.m_totalCash = m;
                changed = old != m;
            }

            if (changed)
            {
                EventCenter.Instance.Emit(EventNames.CashChanged, this.TotalCash);
            }
        }

        public void ResetCash(decimal money)
        {
            bool changed = false;
            lock (this)
            {
                var old = this.m_totalCash;
                var m = Math.Max(money, 0);
                this.m_totalCash = m;
                changed = old != m;
            }

            if (changed)
            {
                EventCenter.Instance.Emit(EventNames.CashChanged, this.TotalCash);
            }
        }

 
        private OTCCashManager()
        {
            EventCenter.Instance.AddEventListener(EventNames.ContractPaid,this.OnContractPaid);
        }

        private void OnContractPaid(object contractObj)
        {
            if (Config.Instance.PlatformConfig.SellPaidAddToCash)
            {
                OTCContract contract = contractObj as OTCContract;

                if (contract != null && contract.Side == Side.Sell)
                {
                    var money = contract.QuoteAmount;
                    this.m_totalCash += money;
                    EventCenter.Instance.Emit(EventNames.CashChanged, this.TotalCash);
                }
            }
        }

        private static OTCCashManager _instance = null;
        private static object locker = new object();

        public static OTCCashManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null)
                        {
                            var cm = new OTCCashManager();

                            _instance = cm;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
