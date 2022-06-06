using CoinTradeOKX.Event;
using CoinTradeOKX.Okex;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Manager
{

    public enum BalanceType
    {
        Account,
        Spot,
    }

    public class Balance:ICloneable
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 冻结
        /// </summary>
        public decimal Frozen
        {
            get;set;
        }

        /// <summary>
        /// 有效
        /// </summary>
        public decimal Avalible { get; set; }

        /// <summary>
        /// 总余额
        /// </summary>
        public decimal Total
        {
            get
            {
                return Avalible + Frozen;
            }
        }

        public object Clone()
        {
            var clone = new Balance()
            {
                Currency = this.Currency,
                Avalible = this.Avalible,
                Frozen = this.Frozen
            };

            return clone;
        }
    }

    public class AssetsManager
    {
        AccountRESTMonitor AccountMonitor = new AccountRESTMonitor();
        SpotMarketRESTMonitor SpotMonitor = new SpotMarketRESTMonitor();

        Dictionary<string, Balance> AccountBlance = new Dictionary<string, Balance>();
        Dictionary<string, Balance> SpotMarketBalance = new Dictionary<string, Balance>();

        HashSet<string> LastUpdateAccountBalance = new HashSet<string>();
        HashSet<string> LastUpdateSpotMarketBalance = new HashSet<string>();

        private AssetsManager()
        {
            MonitorManager mgr = MonitorManager.Default;
   

            AccountMonitor.OnBeforeUpdateBalance = this.OnBeforeUpdateAccountBalance;
            AccountMonitor.OnAfterUpdateBalance = this.OnAfterUpdateAccountBalance;
            AccountMonitor.SetBalanceCallback(Account_OnData);

            SpotMonitor.OnBeforeUpdateBalance = this.OnBeforeUpdateSpotMarketBalance;
            SpotMonitor.OnAfterUpdateBalance = this.OnAfterUpdateSpotMarketBalance;
            SpotMonitor.SetBalanceCallback(PlotMarket_OnData);

            mgr.AddMonotor(AccountMonitor);
            mgr.AddMonotor(SpotMonitor);

            //this.pool = Pool<Balance>.GetPool();
        }

        //Pool<Balance> pool = null;


        private void OnAfterUpdateSpotMarketBalance()
        {
            foreach (string ccy in LastUpdateSpotMarketBalance)
            {
                this.SpotMarketBalance.Remove(ccy);
            }
        }

        private void OnBeforeUpdateSpotMarketBalance()
        {
            this.LastUpdateSpotMarketBalance.Clear();
            foreach (string s in this.AccountBlance.Keys)
            {
                this.LastUpdateSpotMarketBalance.Add(s);
            }
        }

        private void OnAfterUpdateAccountBalance()
        {
            foreach(string ccy in LastUpdateAccountBalance)
            {
                this.AccountBlance.Remove(ccy);   
            }
        }

        private void OnBeforeUpdateAccountBalance()
        {
            this.LastUpdateAccountBalance.Clear();
            foreach(string s in this.AccountBlance.Keys)
            {
                this.LastUpdateAccountBalance.Add(s);
            }
        }

        public Balance GetBalance(BalanceType type, string ccy)
        {
            ccy = ccy.ToUpper();
            Dictionary<string, Balance> dict = null;
            switch(type)
            {
                case BalanceType.Account:
                    dict = this.AccountBlance;
                    break;
                case BalanceType.Spot:
                    dict = this.SpotMarketBalance;
                    break;
            }

            return dict.ContainsKey(ccy) ? dict[ccy].Clone() as Balance : new Balance() { Currency = ccy };
        }

        public IList<Balance> GetBalanceList(BalanceType type)
        {
            List<Balance> list = new List<Balance>();
            Dictionary<string, Balance> dict = null;
            switch (type)
            {
                case BalanceType.Account:
                    dict = this.AccountBlance;
                    break;
                case BalanceType.Spot:
                    dict = this.SpotMarketBalance;
                    break;
            }

            foreach (var c in dict.Values)
            {
                list.Add(c.Clone() as Balance);
            }

            return list;
        }

        private void PlotMarket_OnData(string ccy, decimal avalible,decimal frozen)
        {
            this.UpdateBalance(SpotMarketBalance, LastUpdateSpotMarketBalance, ccy, avalible, frozen);
        }

        void UpdateBalance(Dictionary<string, Balance> dict,HashSet<string> lastUpdate, string ccy, decimal avalible, decimal frozen)
        {
            Balance balance = dict.ContainsKey(ccy) ? dict[ccy] : null;

            if (balance == null)
            {
                balance = new Balance();
                dict[ccy] = balance;
                balance.Currency = ccy;
            }

            bool isChanged = false;

            isChanged = balance.Avalible != avalible || balance.Frozen != frozen;

            balance.Avalible = avalible;
            balance.Frozen = frozen;

            if (isChanged)
            {
                EventCenter.Instance.Emit(EventNames.BalanceChanged, balance);
            }

            lastUpdate.Remove(ccy);
        }

        private void Account_OnData(string ccy, decimal avalible, decimal frozen)
        {
            this.UpdateBalance(AccountBlance, LastUpdateAccountBalance, ccy, avalible, frozen);
        }

        public void Resync()
        {

        }

        private static AssetsManager _instance = null;
        public static AssetsManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AssetsManager();
                }

                return _instance;
            }
        }
    }
}
