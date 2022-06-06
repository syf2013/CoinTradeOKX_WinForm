
(function (exp) {
    function findData() {

        //let root_div = document.getElementById("root"); //查找根节点的那个div减少搜索范围

        //if (!root_div) {
        //    return false;
        //}

        let node_path = "html/body/div@home-container/div/div@app-root/div@app-body/div@view-wrap/div@main/div@flex-1/div@trade-container/div@entrust-order-container/div@sub-tabs-and-release-btn-container/div@sub-tabs/span/em"

        //let node_path = "div@app-root/div@app-body/div@view-wrap/div@main/div@flex-1/div@trade-container/div@entrust-order-container/div@sub-tabs-and-release-btn-container/div@sub-tabs/span/em"

        let findNodes = selectElementsByXPath(node_path);

        if (findNodes.length == 0) {
            LogError("can not find " + node_path);
            return null;
        }


        let node = findNodes[0];

        let __reactEventHandlers = null;
        for (let k in node) {
            if (k.indexOf("__reactEventHandlers") == 0) {
                __reactEventHandlers = node[k];
                break;
            }
        }

        if (!__reactEventHandlers) {
            LogError("can not find memoizedProps");
            return null;
        }

        let memoizedProps = __reactEventHandlers.children._owner.memoizedProps;

        if (!memoizedProps) {
            LogError("can not find memoizedProps"  );
            return null;
        }

        /**
         * activeEntrustOrderId: 0
bindPhoneDialogShow: false
blackListDialogShow: false
blockGuideDialog: Object
currentTargetType: "all"
currentTradeMode: "all"
entrustDetail: Object
entrustErrorInfo: Object
entrustFormInfo: Object
entrustOrderList: t
entrustPreDialog: false
entrustPreDrawerShow: (...)
entrustPreText: ""
entrustScrollTop: 1044
entrustSellOrderListSize: 35
entrustTimer: 8699
filter: Object
hasOpenedEntrustDrawer: false
isScrolled: false
kycDialogShow: false
loading: false
loadingShow: false
rootStore: e {uiStore: e, fiatStore: e, userStore: e, assetStore: e, tickerStore: e, …}
showEntrustDetailDrawer: false
         */
        let entrustStore = memoizedProps.entrustStore; //订单列表等

        if (!entrustStore) {
            LogError("can not find entrustStore");
            return null;
        }

        let root = entrustStore.rootStore; //根存储
        if (!root) {
            LogError("can not find rootStore");
            return null;
        }

        

        /**
         activeId: "ltc"
baseCurrency: "ltc"
baseCurrencyId: 1
baseDeposit: 2
baseScale: 4
broker: Object
btnStatus: Object
config: Object
currencyObj: Object
digitalCurrencyList: t
digitalCurrencyObj: Object
isHidden: false
manageMenuConf: t
overseaCurrencyList: t
platformCommissionRate: 0.0003
priceDeviationRateLimit: 0.3
quoteCurrency: "cny"
quoteCurrencyList: t
quotePriceScale: 2
quoteScale: 2
         */
        let fiatStore = root.fiatStore; //当前币种信息


        /**
         *
        userStore: e
clauseDialog: (...)
kycLimit: Object
kycLimitInfo: t
newUser: Object
rootStore: e {uiStore: e, fiatStore: e, userStore: e, assetStore: e, tickerStore: e, …}
userInfo: Object
acceptOrder: true
agreedTos: true
cancelIn30Days: 0
commonOrderTotal: "4968.78"
completeCount: 84
completeRate: "98.82"
completeRatePure: "0.9882"
createdDate: 1518826461000
isBindPhone: true
isBoundBankCard: true
isCertifiedUser: false
isCommon: true
isDiamondUser: false
isFrozen: false
isFrozenPerm: false
isFrozenTemp: false
isLoaded: true
kycLevel: 3
maliciousCancel: 0
myOrderCount: 1
nickName: "温泉"
orderCount: 85
payEfficiency: "02′17″"
proactiveCancel: 0
realName: "温泉"
receiptAccount: Object
registerDate: "2018-02-17"
releaseEfficiency: "01′22″"
strategyDisabled: false
strategyDisabledReason: ""
timeoutCancel: 0
totalTrade: 52448.95
unDealOrderCount: 0
         */
        let useStore = root.userStore;
        let assetStore = root.assetStore;
        let releaseEntrustStore = root.releaseEntrustStore; //存储挂单条件等

        let entrustOrderList = entrustStore.entrustOrderList;

        if (!entrustOrderList) {
            LogError("can not find entrustOrderList");
            return null;
        }

        return root;
    }


    let rootStore = null;

    /*
    let htimer = setInterval(function () {
        if (!rootStore)
            rootStore = findData();

        if (rootStore) {

            clearInterval(htimer);
            let entrustStore = rootStore.entrustStore;
            let fiatStore    = rootStore.fiatStore; //币种信息
            let assetStore   = rootStore.assetStore;

            function sendCurrency() {
                let currency = fiatStore.currencyObj[fiatStore.activeId];

                let currencyData =
                    {
                        baseCurrency: currency.baseCurrency,
                        baseCurrencyId: currency.baseCurrencyId,
                        baseDeposit: currency.baseDeposit,
                        baseName: currency.baseName,
                        baseScale: currency.baseScale,
                        baseSymbol: currency.baseSymbol
                    }

                exp.updateCurrentCurrency(currencyData);
            }

            
            let activeId = fiatStore.activeId;
            Object.defineProperty(fiatStore, "activeId", {
                get: function () { return activeId; }
                , set: function (val) {
                    activeId = val;
                    sendCurrency();
                }
            });
            
            sendCurrency();
            LogInfo("order list finded ^_^");
        }
    }, 1000);
    */
    LogInfo("running custom script");
})(window);
