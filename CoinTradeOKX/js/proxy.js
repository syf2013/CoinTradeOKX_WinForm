

(function (exp) {
    let BrowserMessage = {

        Initializa: 10000,
        LogInfo : 11001,
        LogError : 11002,
        StoreLoaded : 21000,
        OrderListCreate : 21001,
        OrderListChange : 21004,
        UpdateBalance : 21005,
        UpdateMyOrders : 21006,
        UpdateCurrency: 21007,
        UpdateUSDT: 21008
    };


    let sendOrderList = function(order_list) {
        callCSharp(BrowserMessage.OrderListCreate, order_list);
    }


    let sendInitData = function (data)
    {
        callCSharp(BrowserMessage.Initializa, data);
    }

    let updateMineOrderList = function (list)
    {
        callCSharp(BrowserMessage.UpdateMyOrders, list);
    }

    let updateCoinAmount = function(data)
    {
        var balance = { available: data.available };

        if (data.frozen)
        {
            balance.frozen = data.frozen || 0
        }

        if (data.code) {
            balance.code = data.code;
        }

        callCSharp(BrowserMessage.UpdateBalance, balance);
    }


    let updateCurrentCurrency = function(data)
    {
        callCSharp(BrowserMessage.UpdateCurrency, data);
    }

    let UpdateUsdt =function(price,amount)
    {
        callCSharp(BrowserMessage.UpdateUSDT, {price:price, amount:amount});
    }

    exp.sendOrderList       = sendOrderList;
    exp.updateMineOrderList = updateMineOrderList;
    exp.updateCoinAmount    = updateCoinAmount;
    exp.updateCurrentCurrency = updateCurrentCurrency;
    exp.UpdateUsdt = UpdateUsdt;
    exp.sendInitData = sendInitData;

    LogInfo("running proxy script");
})(window);

