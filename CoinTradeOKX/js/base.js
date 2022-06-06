
String._formater = [/\{0\}/ig, /\{1\}/ig, /\{2\}/ig, /\{3\}/ig, /\{4\}/ig, /\{5\}/ig, /\{6\}/ig];
String.format = function () {
    var str = arguments[0];
    var k = 0;
    for (var i = 1; i < arguments.length; i++) {
        k = i - 1;
        var reg = String._formater[k];
        if (reg === undefined) {
            reg = new RegExp("\\{" + k + "\\}", "ig");
            String._formater[k] = reg;
        }

        str = str.replace(reg, arguments[i].toString());
    }
    return str;
};

(function (exp) {

    //发送到C#
    function callCSharp(msg, data) {

        data = data || "";

        if (typeof (data) == "object") {
            data = JSON.stringify(data);
        }

        let event = new MessageEvent(msg.toString(),
            {
                'view': window,
                'bubbles': false,
                'cancelable': false,
                'data': data
            });

        document.dispatchEvent(event);
    };


    function callback_result( invoke,data,error)
    {
        if (invoke)
        {
            if (!error) {
                callCSharp(invoke, data);
            }
            else 
            {
                callCSharp(invoke, { code:12003, msg:"Extended Error " + error});
            } 
        }
    }

    let BrowserMessage = {
        Initializa      : 10000,
        LogInfo: 11001,
        LogError: 11002,
        StoreLoaded: 21000,
        OrderListCreate: 21001,
        OrderListChange: 21004,
        UpdateBalance: 21005,
        UpdateMyOrders: 21006,
        UpdateCurrency: 21007
    };


    exp.LogError = function (log) {
        callCSharp(BrowserMessage.LogError, log);
    }

    exp.LogInfo = function (log) {
        callCSharp(BrowserMessage.LogInfo, log);
    }

    function getChildsByTagName(parent, tagName) {
        let result = [];
        let childs = parent.childNodes;

        tagName = tagName.toUpperCase()

        for (let i in childs) {
            let n = childs[i];
            if (n.tagName == tagName)
                result.push(n);
        }

        return result;
    }

    function selectNodeWithIdClass(nodes, index, className) {
        if (index == -1 && !className)
            return [].concat(nodes);

        let result = [];
        if (index != -1) {
            let n = nodes[index];
            if (n) {
                result.push(n);
            }
        }
        else {
            result = result.concat(nodes);
        }

        if (className) {
            for (let i = result.length - 1; i >= 0; i--) {
                if (!classNameContainsClass(result[i].className, className)) {
                    result.splice(i, 1);
                }
            }
        }

        return result;
    }


    function classNameContainsClass(className, cssClass) {
        if (!className || !cssClass)
            return false;

        return className == cssClass || className.indexOf(cssClass + ' ') == 0 || className.indexOf(' ' + cssClass + ' ') > -1 || className.indexOf(' ' + cssClass) == className.length - (cssClass.length + 1);
    }

    //let node_path = "html/body/div@home-container/div/div@app-root/div@app-body/div@view-wrap/div@main/div@flex-1/div@trade-container/div@entrust-order-container/div@sub-tabs-and-release-btn-container/div@sub-tabs/span/em"

    function selectNodes(path, root) {
        root        = root || document;
        let paths   = path.split('/');
        let pi      = 0;
        let node_chain      = [[root]];
        let results = null;
        let reg_node_index  = /\[(\d+)\]/;
        let reg_node_class = /@([\w\-]+)/;
        let reg_node_id = /#(.+)/;

        let retry = false;

        for (let i = paths.length - 1; i >= 0; i--) //do remove empty path
        {
            if (!paths[i]) {
                paths.splice(i, 1);
            }
        }

        do {
            retry = false;
            for (let i = pi; i < paths.length; i++) {
                let n = paths[i];
                let index = -1;
                let className = "";
                let finded = null;
                let curnode = node_chain[i][0];

                if (!curnode) return [];

                let match = reg_node_index.exec(n);

                if (match && match[1]) {
                    index = parseInt(match[1]);
                    n = n.replace(match[0], "");
                }

                match = reg_node_class.exec(n);

                if (match && match[1]) {
                    className = match[1];
                    n = n.replace(match[0], "");
                }

                let nodes = node_chain[i + 1] || getChildsByTagName(curnode, n);

                nodes = selectNodeWithIdClass(nodes, index, className);

                node_chain[i + 1] = nodes;

                if (nodes.length > 0) {
                    curnode = nodes[0];
                    results = nodes;
                }
                else {
                    results = null;
                    node_chain.splice(i + 1);
                    let preNodes = node_chain[i];
                    if (preNodes && preNodes.length > 1) {
                        preNodes.shift();
                        curnode = preNodes[0];
                        pi = i - 1;
                        retry = true;
                    }
                    else {
                        return [];
                    }
                }
            }
        } while (retry);

        return results ? results : [];
    }

    exp.callCSharp = callCSharp;
    exp.selectElementsByXPath = selectNodes;
    exp.callback_result = callback_result;

    //hook ajax send, 拦截各种ajax请求
    (function () {

        let OnReadyStateChange = function ()
        {
            if (this.readyState == 4)
            {
                if (this.status == 200)
                {
                    let content = this.responseText;
                    console.log("ajax received " + this.url)
                }
            }
        }

        let customizeOpen = function (method, url, b, c, d) {
            
            this.url = url;
            this.nativeOpen(method,url,b,c,d);
        };

        let customizeSetRequestHeader = function (h, v) {
            this.nativeSetRequestHeader(h,v);
        };
        let customizeSend = function (data) {
            this.addEventListener("readystatechange", OnReadyStateChange, true);
            this.nativeSend(data);
        };

        XMLHttpRequest.prototype.nativeOpen = XMLHttpRequest.prototype.open;
        XMLHttpRequest.prototype.nativeSend = XMLHttpRequest.prototype.send;
        XMLHttpRequest.prototype.nativeSetRequestHeader = XMLHttpRequest.prototype.setRequestHeader;


        XMLHttpRequest.prototype.open = customizeOpen;
        XMLHttpRequest.prototype.send = customizeSend;
        XMLHttpRequest.prototype.setRequestHeader = customizeSetRequestHeader;
    })();


    let Ajax = {
        _send: function (url, method, datas, responseCallback) {
            let errMsg = "";
            let request = new XMLHttpRequest();

            request.timeout = 10000;
            request.onreadystatechange = function () {
                if (request.readyState == 4) {
                    let data = null;
                    let response = request.responseText;
                    if (request.status == 200) {
                        
                        try {
                            data = JSON.parse(response);
                        }
                        catch (e) {
                            errMsg = e.message;
                        }
                    }
                    else {
                        errMsg = String.format("request error :{0} {1}", request.status , response || "");
                    }

                    if (errMsg)
                        LogError(errMsg + " " + url);

                    if (responseCallback) {
                        responseCallback(data, errMsg);
                    }
                }
                else if (request.readyState === 0) {
                    LogError("request abort " + url);
                    if (responseCallback) {
                        responseCallback(null, "abort");//request abort
                    }
                }
            };

            request.onerror = function () {
                LogError("request closed " + url);
                if (responseCallback) responseCallback(null, "request closed");
            }

            request.open(method, url, true);
            if (method == "POST") {
                let dataForPost = {};

                if (datas) {
                    for (let k in datas) {
                        dataForPost[k] = datas[k];
                    }
                }
                request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
                let dataJson = JSON.stringify(dataForPost);
                request.send(dataJson);
            }
            else {
                request.send();
            }
        }
        , post: function (url, datas, responseCallback) {
            this._send(url, "POST", datas, responseCallback);
        }
        , get: function (url, responseCallback) {
            this._send(url, "GET", null, responseCallback);
        }
    };
})(window);
