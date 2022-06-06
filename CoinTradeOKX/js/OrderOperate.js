

function WatchResponse(type, params)
{

}

//获取指定取消订单的链接矩形
function GetCancelOrderRect(orderId)
{
    let buttonCancel = null;
    let tab = null;

    try {
        tab = document.querySelector(".order-tab-box>ul"); //确保切换到了订单列表的tab上

        if (tab) {
            let list = tab.getElementsByTagName("li");

            if (list.length > 0) {
                let liOrder = list[list.length - 1];
                if (liOrder.className.indexOf("active") < 0) { //未选中
                    liOrder.click();
                }
            }
            else {
                return null;
            }
        }
    }
    catch (ex) {
        return null;
    }

    try {
        let tbody = document.querySelector(".my-order-list>.ok-table-container>table>tbody");
        let rows = tbody.getElementsByTagName("tr");

        orderId = "" + orderId;

        for (let r in rows) {
            let row = rows[r];
            let idCell = row.getElementsByTagName("td")[0];
            if (idCell.innerText == orderId) {
                buttonCancel = row.getElementsByTagName("button")[0];
                break;
            }
        }
    }
    catch (ex)
    {
        return null;
    }

    if (buttonCancel)
    {
        let rect = getRect(buttonCancel);

        return rect;
    }
    return null;
    //GetCancelOrderRect(190913011309195)
    //#root > div > div > div.view-wrap > div.main > div.flex-1 > div > div.order-list-container > div > div.order-list-container > div > div.ok-table-container > table > tbody
    //.order-list-container>.my-order-list>.ok-table-container>table>tbody
    //#root > div > div > div > div.main > div.flex-1 > div > div.order-tab-box > ul > li.active
    //#root > div > div > div > div.main > div.flex-1 > div > div.order-list-container > div > div.ok-table-container > table > tbody
}

//获取确认对话框按钮的矩形
function GetCancelOrderConfirmRect()
{
    let btn = null;
    try
    {
        btn = document.querySelector('.dialog-confirm-btn');
    }
    catch (ex)
    {
        return null;
    }

    if (btn) {
        btn.disable = "";

        return getRect(btn);
    }

    return null;
}

let Side =
    {
        Buy: "buy",
        Sell: "sell"
    }


//获取买卖的矩形
function GetSideTabRect(side)
{
    side = ("" + side).toLowerCase();
    
    let tabParent = null;

    try
    {
        tabParent = document.querySelector(".release-entrust-form>div.type-box.flex");
    }
    catch (ex)
    {
        return null;
    }

    if (tabParent)
    {
        let items = tabParent.getElementsByTagName("span");
       
        if (items.length > 1)
        {
            return side == Side.Buy ? getRect(items[0]) : getRect(items[1]);     
        }
    }

    return null;
    //".release-entrust-form>div.type-box.flex" span[0] == buy span[1] = sell
}

let PriceType = 
    {
        Fixed : 1,
        Float : 2
    }



//获取价格输入框的坐标
function GetFloatRect() {
    let input= GetInput("float");
    return inputs.length > 0 ? getRect(inputs[0]) : null;
}

//获取价格输入框的坐标
function GetPriceRect()
{
    let inputs = document.getElementsByName("price");
    return inputs.length > 0 ? getRect(inputs[0]) : null;
}


function GetReleaseForm()
{
    return document.querySelector(divReleaseFormSelector);
}

function GetInput(name) {
    let form = GetReleaseForm();

    if (form)
    {
        let inputs = form.getElementsByTagName("input");

        for (var i = 0; i < inputs.length; i++)
        {
            let input = inputs[i];
            if (input.name == name)
            {
                return input;
            }
        }
    }

    return null;
}

//获取数量输入框的坐标
function GetAmountRect()
{
    let n = GetInput("amount");

    return n ? getRect(n) : null;
}

//设置输入框的值
function SetTextValue(name, value)
{
    let n = GetInput(name);

    if (n)
    {
        n.value = value;
        return true;
    }

    return false;
}

let btnReleaseSelector = ".release-entrust-box > div.fixed-bottom-button > div.button-group> button:nth-child(2)";
let divReleaseFormSelector = ".release-entrust-form";
let divPriceTypeSelector = ".release-entrust-form>div.input-box>div.select-box.flex>div> div>span.Select-multi-value-wrapper>div.Select-value>.Select-value-label";
let divPriceTypeItems = ".release-entrust-form > div.input-box > div.select-box.flex > div > .Select-menu-outer >.Select-menu"
//价格类型的矩形
function GetPriceTypeRect()
{
    let node = document.querySelector(divPriceTypeSelector);
    return node ? getRect(node) : null;
}

//获取那个发布按钮的矩形
function GetReleaseButtonRect()
{
    let node = document.querySelector(btnReleaseSelector);
    return node ? getRect(node) : null;
}

//价格选项的矩形
function GetPriceTypeItemRect(type)
{
    let node = document.querySelector(divPriceTypeItems);

    if (node)
    {
        let childs = node.getElementsByClassName("Select-option");// node.getElementsByTagName("div");    

        if (childs.length > 1)
        {
            let item = null;
            if (type == PriceType.Fixed) {
                item = childs[0];
            }
            else if (type == PriceType.Float) {
                item = childs[1];
            }
            return item ? getRect(item) : null;
        }
    }

    return null;
}

//#root > div > div > div.view-wrap > div.main > div.flex-1 > div > div.after-login-container > div.release-entrust > div > div.react-drawer-drawer.css-sourceMap-modules-importLoaders-1-localIdentName-ReactDrawer__drawer___2r5VH-sass.css-sourceMap-modules-importLoaders-1-localIdentName-ReactDrawer__drawer-right___h_uSC-sass.css-sourceMap-modules-importLoaders-1-localIdentName-animate__animated___2O131.css-sourceMap-modules-importLoaders-1-localIdentName-animate__fadeInRight___uwTeO > div > div.release-entrust-form > div.input-box > div:nth-child(2) > input

//蓝色按钮矩形
function GetEntrustButtonRect()
{
    let btn = null;

    try
    {
       btn =  document.querySelector(".ok-btn.entrust")
    }
    catch (e)
    {
        return null;
    }

    return btn ? getRect(btn) : null;
}

//#root > div > div > div.view-wrap > div.main > div.flex-1 > div > div.entrust-order-container > div.sub-tabs-and-release-btn-container.align-right > div > div