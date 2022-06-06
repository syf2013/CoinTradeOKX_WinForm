
(function (namespace) {

    let ns = {};


    let connects = {};


    window[namespace] = ns;


    let msg_onOpen = "wss_onOpen";
    let msg_onError = "wss_onError";
    let msg_onMessage = "wss_onMessage";
    let msg_onClose = "wss_onClose";

    
    ns.send = function(param)
    {
        let data = param.data;
        let hconnect = param.hconnect;

        let websocket = connects[hconnect];

        if (websocket) {
            websocket.send(data);
        }
        else
        {
            //todo
        }

        if (param._Invoke_) {
            callCSharp(param._Invoke_, { code: 0 });
        }
    }

    ns.close = function (param)
    {
        let hconnect = param.hconnect;

        let websocket = connects[hconnect];

        if (websocket)
        {
            websocket.close();
        }


        if (param._Invoke_) {
            callCSharp(param._Invoke_, { code: 0 });
        }
    }

    function _arrayBufferToBase64(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }

    ns.connect =function (param)
    {
        let addr     = param.address;
        let hconnect = param.hconnect;


        let websocket = new WebSocket(addr);
        connects[hconnect] = websocket;

        if (param._Invoke_)
        {
            callCSharp(param._Invoke_, { code: 0 });
        }

        LogInfo("websocket connecting " + addr);

        websocket.onopen = function (evt) {
            callCSharp(msg_onOpen, hconnect);
        };

        websocket.onclose = function (evt) {
            callCSharp(msg_onClose, hconnect);
            connects[hconnect] = null;

            LogInfo("websocket close " + addr);
        };

        websocket.onmessage = function (evt) {
            let data = evt.data;

            if (evt.data instanceof Blob) {
                let reader = new FileReader();
                reader.readAsArrayBuffer(evt.data);
                reader.onload = function (e) {
                    var arrayBuffer = reader.result;
                    var str = _arrayBufferToBase64(arrayBuffer);

                    let msg = [hconnect, "|", str].join("");
                    callCSharp(msg_onMessage, msg);
                }
            }
            else 
            {
                let msg = [hconnect, "|", data].join("");

                callCSharp(msg_onMessage, msg);
            }
        };

        websocket.onerror = function (evt) {
            callCSharp(msg_onError, hconnect);
            connects[hconnect] = null;

            LogError("websocket error " + addr);
        };
    }

})("wss_proxy");