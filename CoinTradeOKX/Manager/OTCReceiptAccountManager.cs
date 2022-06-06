using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTradeOKX.Okex.Entity;
using Newtonsoft.Json.Linq;
using Common.Classes;
using CoinTradeOKX.Event;
using Common;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Util;
using System.IO;
using System.Windows.Forms;
using CoinTradeOKX.Okex;

namespace CoinTradeOKX.Manager
{
    public class AmountTimes
    {
        public AmountTimes()
        {
            this.Times = 0;
            this.Amount = 0;
        }

        public decimal Amount { get; set; }
        public int Times { get; set; }
    }

    public class OTCReceiptAccountManager
    {

        ReceiptAccountMonitor monitor = null;
        List<ReceiptAccountItem> accounts = new List<ReceiptAccountItem>();
        Dictionary<long, AmountTimes> ReceiptCount = new Dictionary<long, AmountTimes>(); //收款统计
        Dictionary<long, ReceiptAccountSetting> Settings = new Dictionary<long, ReceiptAccountSetting>();

        /*
         accountName: "林锦燕"
        accountNo: "6216611700003219195"
        accountQrCodeUrl: ""
        applyType: 2
        bankBranchName: ""
        bankCode: ""
        bankName: "中国银行"
        currency: "CNY"
        disabled: true
        id: 909741
        type: "BANK" 
         */

        OTCReceiptAccountManager()
        {
            this.monitor = new ReceiptAccountMonitor();

            MonitorManager.Default.AddMonotor(monitor);


            this.LoadSettings();
            EventCenter.Instance.AddEventListener(EventNames.ContractPaid, OnContractPaid);
            //EventCenter.Instance.RemoveListener(EventNames.ContractPaid, OnContractPaid);
        }

        private object list_lock = new object();

        private string GetSettingPath()
        {
            return Path.Combine(Application.StartupPath, "ReceiptSettings.json");
        }

        private void LoadSettings()
        {
            lock(this.Settings)
            {
                string filePath = this.GetSettingPath();
                if (File.Exists(filePath))
                {
                    string str = File.ReadAllText(filePath);
                    List<ReceiptAccountSetting> list = null;
                    try
                    {
                         list = JsonUtil.JsonStringToObject<List<ReceiptAccountSetting>>(str);
                    }
                    catch
                    {
                        return;
                    }

                    this.Settings.Clear();

                    foreach (var s in list)
                    {
                        this.Settings.Add(s.Id, s);
                    }
                }
            }
        }

        public bool SaveSettings()
        {
            lock(this.Settings)
            {
                List<ReceiptAccountSetting> list = new List<ReceiptAccountSetting>(this.Settings.Values);
                string str = JsonUtil.ObjectToJsonString(list);
                try
                {
                    File.WriteAllText(GetSettingPath(), str);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void UpdateAccountList()
        {
            this.monitor.Update(Int32.MaxValue);
        }


        /// <summary>
        /// 判断是否可以启用来轮换
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AccountAvalibleForReceipt(long id)
        {
            ReceiptAccountItem account = GetReceiptAccount(id);

            if (account == null)// || account.Disabled)
            {
                Logger.Instance.LogDebug(" account is null " + id);
                return false;
            }
            if (account.ApplyType == AccountApplyType.Payment)
            {
                Logger.Instance.LogDebug(" account type is Payment " + id);
                return false;
            }

            ReceiptAccountSetting setting = GetReceiptAccountSetting(id);

            DateTime now = DateTime.Now;

            if (setting.ForRecycle) //开启轮换
            {
                AmountTimes count = GetReceiptCount(id);

                bool needSwitch = false;

                if (count.Amount >= setting.TotalDayReceiveAmount || count.Times >= setting.TotalDayReceiveTimes)//已经满额度需要替换了
                {
                    needSwitch = true;
                }

                if (now.Hour < setting.AvalibleBeginHour || now.Hour > setting.AvalibleEndHour)
                {
                    needSwitch = true;
                }

                if (needSwitch)
                {
                    Logger.Instance.LogDebug(" account need switch " + id);
                    return false;
                }
            }
            else

            {
                Logger.Instance.LogDebug(" account not for recycle " + id);
            }

            return true;
        }


        /// <summary>
        /// 遍历账户
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="type"></param>
        public void EachAccount(Action<ReceiptAccountItem> callback,AccountApplyType type = AccountApplyType.All)
        {
            this.monitor.EachAccount(callback, type);
        }

        public ReceiptAccountSetting GetReceiptAccountSetting(long id)
        {
            lock (this.Settings)
            {
                if (this.Settings.ContainsKey(id))
                {
                    return this.Settings[id];
                }
                else
                {
                    ReceiptAccount account = this.GetReceiptAccount(id);
                    ReceiptAccountSetting s = new ReceiptAccountSetting();
                    s.Id = id;
                    this.Settings[id] = s;
                    return s;
                }
            }
        }

        public ReceiptAccountItem GetReceiptAccount(long id)
        {
            ReceiptAccountItem result = null;
            this.EachAccount((account) => {
                if(account.Id == id)
                {
                    result = account;
                }
            });

            return result;
        }


        public void ClearCount()
        {
            lock(this.ReceiptCount)
            {
                this.ReceiptCount.Clear();
            }
        }

        private AmountTimes EmptyStat = new AmountTimes();

        /// <summary>
        /// 获取账号收款总计
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public AmountTimes GetReceiptCount(long accountId)
        {
            lock (this.ReceiptCount)
            {
                return ReceiptCount.ContainsKey(accountId) ? ReceiptCount[accountId] : EmptyStat;
            }
        }

        /// <summary>
        /// 有订单付款时
        /// </summary>
        /// <param name="contractObj"></param>
        private void OnContractPaid(object contractObj)
        {
            OTCContract contract = contractObj as OTCContract;

            bool isSell = false;

            if(contract != null && contract.Side == Side.Sell)
            {
                isSell = true;
                long id = contract.SellerReceiptAccount.Id;

                lock(this.ReceiptCount)
                {
                    AmountTimes total = ReceiptCount.ContainsKey(id) ? ReceiptCount[id] : new AmountTimes();
                    total.Amount += contract.QuoteAmount;
                    total.Times++;
                    ReceiptCount[id] = total;
                }
            }

            if(isSell && this.DaySellAmountIsFull())//检查销售额已满
            {
                List<long> orderIDs = new List<long>();
                OTCOrderManager.Instance.EachSellOrder((order) => {
                    orderIDs.Add(order.PublicId);
                });

                foreach(var id in orderIDs)
                {
                    OTCOrderManager.Instance.CancelOrder(id, false);
                }
            }
        }

        /// <summary>
        /// 关闭账户
        /// </summary>
        /// <param name="id"></param>
        public Task< ApiResult> CloseAccount(long id)
        {
            return this.SetAccountState(id, true);
        }

        /// <summary>
        /// 打开账户
        /// </summary>
        /// <param name="id"></param>
        public  Task< ApiResult> OpenAccount(long id)
        {
            return  this.SetAccountState(id, false);
        }

        /// <summary>
        /// 当日销售额是否超过限制
        /// </summary>
        /// <returns></returns>
        public bool DaySellAmountIsFull()
        {
            decimal m = Config.Instance.PlatformConfig.DaySellAmountLimit * 10000;

            lock (this.ReceiptCount)
            {
                foreach (var kv in this.ReceiptCount)
                {
                    m -= kv.Value.Amount;
                }
            }

            return m <= 0;
        }

        /// <summary>
        /// 设置账号的开/关状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="disabled">是否关闭</param>
        /// <returns></returns>
        public  Task<ApiResult> SetAccountState (long id, bool disabled)
        {
            return Task.Run(() =>
            {
                var api = new okex_receipetAccount_Disable(id, disabled);
                JToken result = api.execSync();

                ApiResult ar = new ApiResult(result);

                if (ar.Code != 0)
                {
                    Logger.Instance.LogError(ar.Message);
                }
                else
                {
                    ReceiptAccountItem account = this.GetReceiptAccount(id);
                    if (account != null)
                    {
                        account.Disabled = disabled;
                        EventCenter.Instance.Emit(EventNames.ReceiptAccountStateChanged, account);
                    }
                }

                return ar;
            });
        }

        public ApiResult SetAccountApplyType(long id, AccountApplyType type)
        {
            var api = new okex_receipetAccount_ApplyType(id, type);
            JToken result = api.execSync();

            ApiResult ar = new ApiResult(result);
            return ar;
        }
       
        private static OTCReceiptAccountManager _instance = null;
        private static object locker = new object();

        public static OTCReceiptAccountManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null)
                        {
                            var obj = new OTCReceiptAccountManager();

                            _instance = obj;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
