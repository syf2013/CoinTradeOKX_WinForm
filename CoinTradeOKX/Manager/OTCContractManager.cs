using CoinTradeOKX.Event;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX
{
    public delegate void ContractDelegate(OTCContract contract);
    public class OTCContractManager:IDisposable
    {

        public bool Effective
        {
            get
            {
                return this.monitor.Effective;
            }
        }

        /// <summary>
        /// 当前未完成的成交单数量
        /// </summary>
        public uint ContractCount
        {
            get
            {
                lock(locker_contract)
                {
                    return (uint) this.contracts.Count;
                }
            }
        }

        private object locker_new_contract_listener = new object();
        private object locker_remove_contract_listener = new object();
        private object locker_contract = new object();

        private OTCContractMonitor monitor = null;

        private List<OTCContract> contracts = new List<OTCContract>();

        HashSet<long> ContractIds = new HashSet<long>();
        HashSet<long> PaidContracts = new HashSet<long>();

        public List<Action<OTCContract>> NewContractListener = new List<Action<OTCContract>>();
        public List<Action<OTCContract>> RemoveContractListener = new List<Action<OTCContract>>();

        private OTCContractManager()
        {
            monitor = new OTCContractMonitor(string.Empty);
            monitor.OnData += Monitor_OnData;

            MonitorManager.Default.AddMonotor(monitor);

            this.LastRemoveContractTime = DateTime.MinValue;
        }

        public Task<bool> ReleaseContract(long id)
        {
            return Task.Run<bool>(() =>
            {
                //if (contract.PaymentStatus != PaymentStatus.Paid)
                //    return false;

                okex_api_order_confrimed api = new okex_api_order_confrimed(id.ToString(), Config.Instance.Account.GetReleasePassword());
                JToken result = api.execSync();


                if (result["code"].Value<int>() == 0)
                {
                    this.monitor.RemoveContract(id);

                    lock(locker_contract)
                    {
                        foreach(OTCContract c in contracts)
                        {
                            if(c.PublicOrderId == id)
                            {
                                contracts.Remove(c);
                                CallRemoveListner(c);
                                break;
                            }
                        }
                    }

                    return true;
                }

                return false;
            });
        }


        public DateTime LastRemoveContractTime
        {
            get;
            private set;
        }
        private void CallRemoveListner(OTCContract contract)
        {
            lock (locker_remove_contract_listener)
            {
                foreach (var l in RemoveContractListener)
                {
                    l.Invoke(contract);
                }
            }

            this.LastRemoveContractTime = DateTime.Now;
        }

        public void AddNewContractListener(Action<OTCContract> listener)
        {
            lock (locker_new_contract_listener)
            {
                this.NewContractListener.Add(listener); //TODO
            }
        }

        public void RemoveNewContractListner(Action<OTCContract> listener)
        {
            lock (locker_new_contract_listener)
            {
                this.NewContractListener.Remove(listener);//TODO
            }
        }

        public void AddRemoveContractListener(Action<OTCContract> listener)
        {
            lock (locker_remove_contract_listener)
            {
                this.RemoveContractListener.Add(listener); //TODO
            }
        }

        public void AddOnContractPaidListener(Action<OTCContract> listener)
        {
           //TODO this.ContractPaid += listener;
        }

        public void RemoveRemoveContractListner(Action<OTCContract> listener)
        {
            lock (locker_remove_contract_listener)
            {
                this.RemoveContractListener.Remove(listener);//TODO
            }
        }

        private void Monitor_OnData(MonitorBase obj)
        {
            List<OTCContract> newContracts = new List<OTCContract>();
            List<OTCContract> oldContects = new List<OTCContract>(this.contracts);
            List<OTCContract> paidContract = new List<OTCContract>();

             lock (locker_contract)
            {
                this.monitor.EachContract((contract) =>
                {
                    bool find = false;

                    if(contract.PaymentStatus == PaymentStatus.Paid && !this.PaidContracts.Contains(contract.PublicOrderId))
                    {
                        this.PaidContracts.Add(contract.PublicOrderId);
                        paidContract.Add(contract);
                    }

                    for (int i = 0; i < oldContects.Count; i++)
                    {
                        var c = oldContects[i];
                        if (c.PublicOrderId == contract.PublicOrderId)
                        {
                            oldContects.RemoveAt(i);
                            break;
                        }
                    }

                    foreach (var c in this.contracts)
                    {
                        if (c.PublicOrderId == contract.PublicOrderId)
                        {
                            contract.CopyTo(c);
                            find = true;
                            break;
                        }
                    }

                    if (!find)
                    {
                        OTCContract nc = contract.Clone() as OTCContract;
                        contracts.Add(nc);
                        newContracts.Add(nc);
                    }

                    if (!this.ContractIds.Contains(contract.PublicOrderId))
                    {
                        this.ContractIds.Add(contract.PublicOrderId);
                    }
                });


                foreach (var c in oldContects)
                {
                    this.contracts.Remove(c);
                    CallRemoveListner(c);
                }
            }

            if (newContracts.Count > 0)
            {
                lock (locker_new_contract_listener)
                {
                    foreach (var c in newContracts)
                    {
                        EventCenter.Instance.Emit(EventNames.NewContract, c);

                        foreach (var a in this.NewContractListener)
                        {
                            a(c);
                        }
                    }
                }
            }

            foreach (var c in paidContract)
            {
                EventCenter.Instance.Emit(EventNames.ContractPaid, c);
            }
        }

        public void EachContract(string currency, Action<OTCContract> callback)
        {
            lock (locker_contract)
            {
                foreach (var contract in this.contracts)
                {
                    if (string.IsNullOrEmpty(currency) || string.Compare(contract.BaseCurrency, currency, true) == 0)
                    {
                        callback(contract);
                    }
                }
            }
        }

        public void Dispose()
        {
            this.monitor.Destory();
        }

        private static OTCContractManager _instance = null;
        public static OTCContractManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OTCContractManager();

                return _instance;
            }
        }
    }
}
