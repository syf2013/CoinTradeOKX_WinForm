
//以一个对象的x和y属性放回滚动条的位置
function getScrollOffsets(w) {
    w = w || window;
    //除了IE 8以及更早的版本以外，其他浏览器都支持
    if (w.pageXOffset != null) return { x: w.pageXOffset, y: w.pageYOffset };
    //对标准模式下的IE
    var d = w.document;
    if (document.compatMode == "CSS1Compat")
        return { x: d.documentElement.scrollLeft, y: d.documentElement.scrollTop };
    //对怪异模式下的浏览器
    return { x: d.body.scrollLeft, y: d.body.scrollTop };
}

function getRect(element)
{
// For scrollX

    var offsets = getScrollOffsets();

    let t;

    let sl = (((t = document.documentElement) || (t = document.body.parentNode)) && typeof t.scrollLeft == 'number' ? t : document.body).scrollLeft
        // For scrollY
    let st =  (((t = document.documentElement) || (t = document.body.parentNode))&& typeof t.scrollTop == 'number' ? t : document.body).scrollTop

    let rect = element ? element.getBoundingClientRect() : null;

    if (rect)
    {
        rect.x = rect.left = rect.x + sl;
        rect.y = rect.top = rect.y + st;
    }
    return rect;
}


function select(parent, identify)
{
    let regIndex = /\[(\d+)\]/ig;

    parent = parent || document;
    if (identify.indexOf("#") === 0) { //id selector
        let id = identify.slice(1);
        return document.getElementById(id);
    }
    else if (identify.indexOf(".") === 0) { //class selector
        let className = identify.slice(1);
        let elements = parent.getElementsByClassName(className);
        
        if (elements.length > 0) {
            let match = regIndex.exec(identify);
            let index = match ? parseInt(match[1]) : 0;
            return elements[index];
        }
    }
    else if (identify.indexOf("@") === 0) { // name selector
        let name = identify.slice(1);
        let elements = parent.getElementsByName(name);

        if (elements.length > 0) {
           
            let match = regIndex.exec(identify);
            let index = match ? parseInt(match[1]) : 0;


            return elements[index];
        }
    }

    return null;
}

function getRectBySelector(selector)
{
    let element = null;
    try
    {
        element = document.querySelector(selector);
    }
    catch (e)
    {
        return null;
    }

    if (element)
    {
        return getRect(element);
    }

    return null;
}

let ids = [".text"];


function getRectList() {

    let rectList = {};

    for (let i in ids)
    {
        let id = ids[i];
        let element = select(id);
        let rect = getRect(element);
        rectList[id] = rect;

        if (rect)
        {
            alert("x = " + rect.x + " y = " + rect.y + " right = " + rect.right + " bottom = " + rect.bottom);
        }
    }

    return rectList;
}


//document.onclick = getRectList;

document.createEvent = function (evt) {
    console.log("create event" + evt);
}

document.onclick = function (evt)
{
   // console.log("x = " + evt.x + " y = " + evt.y);
}
//let orderList = document.querySelector("//div.order-list-container > div > div.ok-table-container > table > tbody");