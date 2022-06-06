
(function(namespace) {
    //获取token 和deveiceid 都在这个文件里
    //https://img.bafang.com/cdn/assets/okfe/libs/tool/okAjax-1.0.0.min.js

    /**
     * 以下是已知的法币接口
     */
    let okex_api_cancel_order = "/v3/c2c/tradingOrders/{0}/cancel"; //取消订单  （重要）
    let okex_api_order_paid = "/v3/c2c/orders/{0}/payment/paid";    //订单已支付
    let okex_api_order_place = "/v3/c2c/tradingOrders/";            //挂单        （重要）
    let okex_api_order_query = "/v3/c2c/tradingOrders/{0}";         //获取挂单的详细信息
    let okex_api_coin_amount = "/v3/c2c/accounts/balance/{0}";      //查询余量 {0}为币种类型{重要}
    let okex_api_mine_orders = "/v3/c2c/tradingOrders/my";         //获取当前我的挂单 （重要）
    let okex_api_get_order_list = "/v3/c2c/tradingOrders/book?side=all&baseCurrency={0}&quoteCurrency=cny&userType=all&paymentMethod=all"
    let okex_api_coin_transfer = "/v2/asset/accounts/transfer";
    let okex_api_contract = "/v3/c2c/orders/?status=new";



    let ns = {};

    window[namespace] = ns;

    let header = {
        "loginname": "13365904455"
    }

    /**
     * 更新我的挂单
     */
    ns.okex_My_Order_List = function(param)
    {
        let url = okex_api_mine_orders;

        Ajax.getH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }

    /**
     * 取消订单
     * @param {any} id 订单id这个参数最好用字符串传递，避免出现js数值溢出问题
     */
    ns.okex_Order_Cancel = function(param)
    {
        let id = param.id;

        if (!id) {

            callbacl_result(param._Invoke_, null, "缺少参数id" );
            return;
        }

        let url = String.format(okex_api_cancel_order, id); 

        Ajax.postH(url, {}, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }

    /**
     * 挂单
     * @param {any} currency 数字货币类型
     * @param {any} type     买卖类型（sell/buy） 
     * @param {any} price    单价
     * @param {any} amount   数量
     * @param {any} kycLimit 验证等级限制（默认1）
     */
    /*{"side":"sell","baseCurrency":"ltc","quoteCurrency":"cny","type":"limit","baseAmount":1,"quoteMinAmountPerOrder":400,"quoteMaxAmountPerOrder":20000000,"remark":"","hiddenPrice":null,"userType":"all","minKycLevel":1,"price":"860","unpaidOrderTimeoutMinutes":"10","maxUserCreatedDate":1555716645000}*/
    ns.okex_Order_Create = function(param) {
        if (!param.currency) {
            callCSharp(param._Invoke_, { code: 12022, msg: "缺少参数currency" });
            LogError("创建OTC挂单错误，缺少参数 currency")
            return;
        }

        if (!param.side || !(param.side == "sell" || param.side == "buy")) {
            callCSharp(param._Invoke_, { code: 12022, msg: "缺少参数side" });
            LogError("创建OTC挂单错误 side 错误" + param.type);
            return;
        }

        if (!param.price) {
            callCSharp(param._Invoke_, { code: 12022, msg: "缺少参数price" });
            LogError("创建OTC挂单错误 缺少参数 price");
            return;
        }
        else if (typeof (param.price) != "number" || param.price <= 0) {
            callCSharp(param._Invoke_, { code: 12022, msg: "错误 参数 price" });
            LogError("创建OTC挂单错误 参数 price" + param.price);
            return;
        }

        if (!param.amount) {
            callCSharp(param._Invoke_, { code: 12022, msg: "缺少参数 amount" });
            LogError("创建OTC挂单错误 缺少参数 amount");
            return;
        }
        else if (typeof (param.amount) != "number" || param.amount <= 0) {
            callCSharp(param._Invoke_, { code: 12022, msg: "错误 参数 amount" });
            LogError("创建OTC挂单错误 参数 amount" + param.amount);
            return;
        }

        kycLimit = param.kycLimit || 1;
        let url = okex_api_order_place;
        let time = (new Date()).getTime();

        time -= 10 * 86400000; //对手注册时间往前推10天

        let orderinfo =
            {
                "side": param.side,
                "baseCurrency": param.currency,
                "quoteCurrency": "cny",
                "type": param.type || "limit",
                "baseAmount": param.amount,
                "quoteMinAmountPerOrder": param.quoteMinAmountPerOrder || 400,
                "quoteMaxAmountPerOrder": param.quoteMaxAmountPerOrder || 20000000,
                "remark": param.remark || "",
                "hiddenPrice": null,
                "userType": param.userType || "all",
                "minKycLevel": kycLimit, //对手等级限制
                "price": "" + param.price, //价格    
                "unpaidOrderTimeoutMinutes": param.unpaidOrderTimeoutMinutes || "15", //付款时间限制
                "maxUserCreatedDate": time,
                "minCompletedOrderQuantity": param.minCompletedOrderQuantity || 1 //最低完成订单数
            }

        Ajax.postH(url, orderinfo, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }

    /**
     *更新OTC市场挂单列表
     * @param {any} coin  币种
     */
    ns.okex_Order_List_Update = function(param) {
        let currency = param.currency;

        let url = String.format(okex_api_get_order_list, currency);
        Ajax.getH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }

    ns.okex_query_usdt = function(param)
    {
        let anchor = param.anchor;

        let url = String.format(okex_api_get_order_list, anchor);
        Ajax.getH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }

    ns.okex_contract_list = function (param)
    {
        let url = okex_api_contract;
       
        Ajax.getH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }


    ns.okex_order_paid = function (param)
    {
        let id = param.id;
        let url = string.format(okex_api_order_paid, id);// "/v3/c2c/orders/{0}/payment/paid"

        let data =
        {
            receiptAccountId : param.receiptAccountId
        };

        Ajax.postH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }


    /**
     * 查询otc账户 剩余数量
     * {any} currency 货币类型
     */
    ns.okex_Coin_Amount = function(param)
    {

        let currency = param.currency;


        let url = String.format(okex_api_coin_amount, currency);

        Ajax.getH(url, function (data, error) {
            callback_result(param._Invoke_, data, error);
        });
    }


    function getCurrency()
    {
        let c = localStorage["current-digital-symbol"];
        while (c.indexOf("\"") >= 0)
            c = c.replace("\"", "");

        return c;
    }

    let lastCurrnecy = getCurrency();

    function sendCurrency() {
        let c = getCurrency();
       
        let currencyData ={baseName: c};
        updateCurrentCurrency(currencyData);
    }

    function checkCurrencyChange() {
        let c = getCurrency();

        if (lastCurrnecy != c) {
            sendCurrency();
            lastCurrnecy = c;
        }
    }

    setInterval(checkCurrencyChange,200);

    let initData = {};
    initData.devid = localStorage.devid;
    initData.ftID = localStorage.ftID||"";
    initData.authorization = localStorage.token;
    initData.loginName = localStorage.loginName;
    initData.currency = getCurrency();
    initData.userAgent = navigator.userAgent;
    initData.cookie = document.cookie;
    

    sendInitData(initData);
    sendCurrency();

    LogInfo("running okex script")
})("otc_proxy")