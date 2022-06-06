using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Behavior
{
    [BehaviorName(Name = "收款账号轮换/统计")]
    public class OTCAccountBehavior: BehaviorBase
    {
        private int lastDay = 0;
        private bool disposed = false;

        [BehaviorParameter(Name = "开启账号轮换")]
        public bool OpenSwap
        {
            get;set;
        }

        [BehaviorParameter(Name = "隔日强制轮换" )]
        public bool CloseRecycelWhenNextDay
        {
            get;set;
        }

        [BehaviorParameter(Name = "逐笔轮换")]
        public bool EveryOneOrder
        {
            get; set;
        }

        public override void Dispose()
        {
            EventCenter.Instance.RemoveListener(EventNames.ContractPaid, this.OnContractPaid);
            this.disposed = true;
            base.Dispose();
        }

        private int Interval = 2000;

        public OTCAccountBehavior()
        {
            this.Enable = true;
            lastDay = DateTime.Now.Day;
            EventCenter.Instance.AddEventListener(EventNames.ContractPaid, this.OnContractPaid);
            this.Run();
        }

        private void OnContractPaid(Object obj)
        {
            OTCContract contract = obj as OTCContract;

            if(contract == null)
            {
                return;
            }

            if (this.Enable && this.EveryOneOrder)
            {
                bool hasUnpaied = false;

                OTCContractManager.Instance.EachContract(null, (otcContract) => {
                    if ( otcContract.PublicOrderId != contract.PublicOrderId && otcContract.Side == Side.Sell && otcContract.PaymentStatus == PaymentStatus.Unpaid)
                    {
                        hasUnpaied = true;
                    }
                });

                if (hasUnpaied)
                {
                    return;
                }


                if(contract.Side == Side.Sell)
                {
                    long id = contract.SellerReceiptAccount.Id;
                    this.Executing = true;
                    this.ChangeAccount(id);
                }
            }
        }

        private async void ChangeAccount(long id)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            long openId = this.GetAvalibeAccount(new long[] { id });

            if (openId > 0)
            {
                this.Executing = true;
                var ret = await mgr.OpenAccount(openId);

                if (ret.Code == 0)
                {
                    await mgr.CloseAccount(id);
                }
            }
            else
            {
                Logger.Instance.LogDebug("no have avalible account");
            }
        }

        private async void SwapAccount(long openAccount, IEnumerable<long> closeAccounts)
        {
            Logger.Instance.LogDebug("Swap Account " + string.Join(",",closeAccounts) + " TO " + openAccount );
            var mgr = OTCReceiptAccountManager.Instance;
            var ret = await mgr.OpenAccount(openAccount);

            if (ret.Code == 0)
            {
                foreach (var id in closeAccounts)
                {
                    await mgr.CloseAccount(id);
                }
            }
        }

        private long GetAvalibeAccount(IEnumerable<long> exludes)
        {
            var mgr = OTCReceiptAccountManager.Instance;
            int times = int.MaxValue;
            long id = 0;

            mgr.EachAccount((account) =>
            {
                if (!exludes.Contains(account.Id))
                {
                    if (mgr.AccountAvalibleForReceipt(account.Id))
                    {
                        var counter = mgr.GetReceiptCount(account.Id);
                        if (counter.Times < times)
                        {
                            times = counter.Times;
                            id = account.Id;
                        }
                    }
                }
            });

            return id;
        }

        private async void  Run()
        {
            await Task.Run(() =>
            {
                while (!this.disposed)
                {
                    Thread.Sleep(Interval);

                    this.Executing = false;
                    if (!this.Enable)
                        continue;

                    if (OTCContractManager.Instance.ContractCount > 0)//有单子不操作
                        continue;

                    var now = DateTime.Now;

                    var mgr = OTCReceiptAccountManager.Instance;

                    bool isNewDay = lastDay != now.Day; //false;

                    if (OpenSwap)
                    {
                        long forOpenAccount = 0;
                        List<long> forCloseAccounts = new List<long>();

                        if (isNewDay)
                        {
                            if (this.CloseRecycelWhenNextDay)
                            {
                                Logger.Instance.LogDebug("New Day Ready Swap Account");
                                mgr.EachAccount((account) =>
                                {
                                    if (!account.Disabled)
                                    {
                                        var settings = mgr.GetReceiptAccountSetting(account.Id);

                                        if (settings.ForRecycle)
                                        {
                                            forCloseAccounts.Add(account.Id);//关闭账号列表
                                        }
                                    }

                                }, AccountApplyType.Receipt);
                            }
                        }
                        else
                        {
                            mgr.EachAccount((account) =>
                            {
                                if (!account.Disabled)
                                {
                                    if (mgr.GetReceiptAccountSetting(account.Id).ForRecycle && !mgr.AccountAvalibleForReceipt(account.Id))
                                    {
                                        forCloseAccounts.Add(account.Id);//关闭账号列表
                                    }
                                }
                            }, AccountApplyType.Receipt);
                        }

                        if (forCloseAccounts.Count > 0)//有需要关闭的账号
                        {
                            forOpenAccount = GetAvalibeAccount(forCloseAccounts);
                        }

                        if (forOpenAccount > 0)
                        {
                            this.Executing = true;
                            this.SwapAccount(forOpenAccount, forCloseAccounts);
                        }
                    }

                    if (isNewDay)//隔天清零
                    {
                        lastDay = now.Day;
                        this.Executing = true;
                        mgr.ClearCount();
                    }
                }
            });
        }
    }
}
