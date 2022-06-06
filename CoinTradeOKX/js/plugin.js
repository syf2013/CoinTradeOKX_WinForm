(function () {


    window.GetToken = function () {
        return {
            ftID: localStorage.getItem("ftID") || "",
            devid: localStorage.getItem("devid") || "",
            authorization: localStorage.getItem("token") || "",
            loginName : localStorage.getItem("loginName") || ""
        }
    }
    //alert("plugin started");
    let href = window.location.href;
    if (href != "https://okx.com/otc" && href != "https://www.okx.com/otc")
        return;

    let WssCommand =
        {
            login: "login",
            web: "web",
            get: "get",
            ajax: "ajax",
            post: "post",
            rpc: "rpc",
            reject: "reject"
        }

    let CommonHeader = null;

    //console.log("plugin starting.......");
    // 给本地服务器发送中转数据
    let WssConnection = class {
        wss = null;
        url = null;
        timer = -1;
        loginName = "";
        connected = false;
        package = {};
        CommandProcessor = {};
        constructor(url) {
            this.url = url;
            this.connect();
            this.timer = setInterval(() => {
                if (!this.wss) {
                    this.connect();
                }

                if (this.wss && this.connected) {
                    this.sendPing();
                }
            }, 10000);

            this.CommandProcessor[WssCommand.get] = this.processAjaxGet;
            this.CommandProcessor[WssCommand.post] = this.processAjaxPost;
            this.CommandProcessor[WssCommand.reject] = this.processReject;
        }

        sendLoginInfo(name) {

        }

        sendPing() {
            this.wss.send("ping");
        }

        connect() {
            let self = this;

            let ws = new WebSocket(this.url);
            this.wss = ws;
            ws.onopen = function (evt) {
                self.connected = true;
                //console.log("websocket connect success " + this.url);
                let loginData = { loginName: self.loginName };

                loginData.ftID = localStorage.getItem("ftID"),
                loginData.devId = localStorage.getItem("devid"),
                loginData.authorization = localStorage.getItem("token"),
                self.send(WssCommand.login, loginData);
            };

            ws.onclose = function () {
                self.wss = null;
                self.connected = false;
            }

            ws.onerror = function () {
                self.wss = null;
                self.connected = false;
            }

            ws.onmessage = this.onReceive.bind(this);
        }

        processMessage(msg) {
            if (msg == "pong") {
                return;
            }

            let json = JSON.parse(msg);
            let cmd = json.cmd;

            let processor = this.CommandProcessor[cmd];

            if (processor) {
                processor.call(this, json);
            }

        }

        onReceive(evt) {
            let data = evt.data;

            if (evt.data instanceof Blob) {
                let reader = new FileReader();
                reader.readAsArrayBuffer(evt.data);
                reader.onload = function (e) {
                    let arrayBuffer = reader.result;

                    this.processMessage(_arrayBufferToBase64(arrayBuffer));
                }
            }
            else {
                this.processMessage(data);
            }
        }

        sendAjaxResult(id, status, response) {
            let r = { id: id, status: status, response: response };
            this.send(WssCommand.ajax, r);
        }

        send(cmd, data) {
            if (this.wss && this.connected) {
                this.package.cmd = cmd;
                this.package.data = data || {};
                let str = JSON.stringify(this.package);
                this.wss.send(str);
                return true;
            }

            return false;
        }

        processAjaxPost(obj) {
            this.processAjaxRequest(obj.id, "POST", obj.url, obj.data);
        }

        processAjaxGet(obj) {
            this.processAjaxRequest(obj.id, "GET", obj.url);
        }

        processAjaxRequest(id, method, url, data) {
            let headers = CommonHeader;

            if (!headers)
                return;

            /*{
                Accept: "application/json",
                "App-Type": "web",
                ftID : localStorage.getItem("ftID"),
                devId : localStorage.getItem("devid"),
                Authorization : localStorage.getItem("token"),
                timeout: "10000"
            }*/

            let self = this;
            let t = (new Date()).getTime()

            if (url.indexOf("?") > -1) {
                url = url.replace("?", "?t=" + t + "&");
            }
            else {
                url = url + "?t=" + t;
            }

            let xhr = new XMLHttpRequest();
            xhr._is_inner = true;
            xhr.open(method.toUpperCase(), url, true);

            for (var k in headers) {
                xhr.setRequestHeader(k, headers[k]);
            }

            if (method == "POST") {
                xhr.setRequestHeader("Origin", "https://www.okx.com");
            }

            xhr.onreadystatechange = function () {
                if (this.readyState == 4) {
                    let status = this.status;
                    let response = this.responseText;
                    self.sendAjaxResult(id, status, response);
                }
            }
            if (method == "GET")
                xhr.send();
            else {
                xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
                xhr.send(data);
            }
        }

        processReject() {
            clearInterval(this.timer);
            this.wss.close();
            this.wss = null;

            //console.log("local websocket server connect rejected");
        }
    }


    let okex_api_cancel_order = "/v3/c2c/tradingOrders/{0}/cancel"; //取消挂单  （重要）
    let okex_api_order_paid = "/v3/c2c/orders/{0}/payment/paid";  //订单已支付
    let okex_api_order_place = "/v3/c2c/tradingOrders/";           //挂单（重要）
    let okex_api_order_info = "/v3/c2c/tradingOrders/{0}";         //获取挂单的详细信息
    let okex_api_coin_amount = "/v3/c2c/accounts/balance/{0}";     //查询余量 {0}为币种类型{重要}
    let okex_api_mine_orders = "/v3/c2c/tradingOrders/my";         //获取当前我的挂单 （重要）
    let okex_api_contract = "/v3/c2c/orders/?status=new";        //获取未完成订单
    let okex_api_order_confrimed = "/v3/c2c/orders/{0}/payment/confirmed"; //放币接口

    let nodes = document.getElementsByClassName("order-list-container");
    let entryNode = nodes.length > 0 ? nodes[0] : null;

    if (entryNode) {
        for (k in entryNode) {
            if (k.indexOf("__reactInternalInstance") > -1) {
                let mobxStores = entryNode[k].child.stateNode.__reactInternalMemoizedMaskedChildContext.mobxStores;//<<--------
            }
        }
    }

    let GetCookie = function (sKey) {
        return decodeURIComponent(document.cookie.replace(new RegExp("(?:(?:^|.*;)\\s*" + encodeURIComponent(sKey).replace(/[-.+*]/g, "\\$&") + "\\s*\\=\\s*([^;]*).*$)|^.*$"), "$1")) || null;
    };

    let isLogin = GetCookie("isLogin");

    isLogin = isLogin && parseInt(isLogin) == 1;

    //if(window.location.href.indexOf("c2c.huobi.br.com") != -1)
    if (isLogin) {
        //alert("plugin running");
        let loginName = localStorage.getItem("loginName");
        let remoteWss = "ws://localhost:8808";
        let socketConenction = new WssConnection(remoteWss);
        socketConenction.loginName = loginName;

        //console.log("the plugin is running");
        //ajax hook

        (function () {

            let OnReadyStateChange = function () {
                if (this.readyState == 4) {
                    if (this.status == 200) {
                        let content = this.responseText;
                        //console.log("ajax received size = " + content.length + " url = " + this.url)
                    }
                }
            }

            let nativeOpen = XMLHttpRequest.prototype.open;
            let nativeSend = XMLHttpRequest.prototype.send;
            let nativeSetRequestHeader = XMLHttpRequest.prototype.setRequestHeader;

            let customizeOpen = function (method, url, b, c, d) {

                this.url = url;

                nativeOpen.call(this, method, url, b, c, d);
            }

            let customizeSetRequestHeader = function (h, v) {
                if (!this._is_inner) {
                    if (h != "Content-Type") {
                        CommonHeader = CommonHeader || {};
                        CommonHeader[h] = v;
                    }
                }

                nativeSetRequestHeader.call(this, h, v);
            };
            let customizeSend = function (data) {
                //this.addEventListener("readystatechange", OnReadyStateChange, true);
                nativeSend.call(this, data);
            };
            ;
            XMLHttpRequest.prototype.open = customizeOpen;
            XMLHttpRequest.prototype.send = customizeSend;
            XMLHttpRequest.prototype.setRequestHeader = customizeSetRequestHeader;
        })();
    }
})();