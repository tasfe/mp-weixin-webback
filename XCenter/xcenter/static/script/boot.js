__CreateJSPath = function (js) {
    var scripts = document.getElementsByTagName("script");
    var path = "";
    for (var i = 0, l = scripts.length; i < l; i++) {
        var src = scripts[i].src;
        if (src.indexOf(js) != -1) { var ss = src.split(js); path = ss[0]; break; }
    }
    var href = location.href;
    href = href.split("#")[0];
    href = href.split("?")[0];
    var ss = href.split("/");
    ss.length = ss.length - 1;
    href = ss.join("/");
    if (path.indexOf("https:") == -1 && path.indexOf("http:") == -1 && path.indexOf("file:") == -1 && path.indexOf("\/") != 0) {
        path = href + "/" + path;
    }
    return path;
}
var bootPATH = __CreateJSPath("boot.js");
document.write('<script src="' + bootPATH + 'jquery-1.6.2.min.js" type="text/javascript"></sc' + 'ript>');
document.write('<script src="' + bootPATH + 'miniui.js" type="text/javascript" ></sc' + 'ript>');
document.write('<link href="' + bootPATH + 'themes/default/miniui.css" rel="stylesheet" type="text/css" />');
document.write('<link href="' + bootPATH + 'themes/icons.css" rel="stylesheet" type="text/css" />');
function getCookie(sName) {
    var aCookie = document.cookie.split("; ");
    var lastMatch = null;
    for (var i = 0; i < aCookie.length; i++) {
        var aCrumb = aCookie[i].split("=");
        if (sName == aCrumb[0]) {
            lastMatch = aCrumb;
        }
    }
    if (lastMatch) {
        var v = lastMatch[1];
        if (v === undefined) return v;
        return unescape(v);
    }
    return null;
}
function tipMsg(content, time) {
    if (!time) { time = 1500; }
    var messageid = mini.loading(content, "提示");
    setTimeout(function () { mini.hideMessageBox(messageid); }, time);
}
function alertMsg(content, btn, type, fn) {
    if (btn == 1) {
        mini.showMessageBox({
            width: 250,
            title: "提示",
            buttons: ["ok"],
            message: content,
            iconCls: type,
            callback: fn
        })
    } else if (btn == 2) {
        mini.showMessageBox({
            width: 250,
            title: "提示",
            buttons: ["ok", "cancel"],
            message: content,
            iconCls: type,
            callback: fn
        })
    } else {
        mini.showMessageBox({
            width: 250,
            title: "提示",
            buttons: ["ok", "no", "cancel"],
            message: content,
            iconCls: type,
            callback: fn
        })
    }
}
function MyalertMsg(content, type, fn) {
    var icon = 'mini-messagebox-info';
    switch (type) {
        case 'error': icon = 'mini-messagebox-error'; break;        //错误
        case 'question': icon = 'mini-messagebox-question'; break;    //Question
        case 'warning': icon = 'mini-messagebox-warning'; break;       //Warning
        case 'download': icon = 'download'; break;   //download
    }
    try {
        try {
            try { parent.parent.alertMsg(content, 1, icon, fn); } catch (e) { parent.alertMsg(content, 1, icon, fn); }
        } catch (ee) {alertMsg(content, 1, icon, fn); }
    } catch (eee) { alert('error:' + eee.message);}
}
function reload(){window.location.reload();}
function Goto(url){location.href = url;}
function TopGoto(url){top.location.href = url;}
function NewGoto(url){window.open(url);}
function GotoCode(url, code) { if (checkRight(code)) { top.location.href = url; } }
window.onload = function () {
    try {
        var obj = document.getElementById('nav').getElementsByTagName('li');
        for (var i = 0; i < obj.length; i++) {
            obj[i].onmouseover = function () {
                try {
                    var el; el = this.lastElementChild;
                    if (!el) { try { el = this.children[1]; } catch (e) { } }
                    el.style.left = 0;
                } catch (ee) { }
            }
            obj[i].onmouseout = function () {
                try {
                    var el;
                    try { el = this.children[1]; } catch (e) { }
                    if (!el) { el = this.lastElementChild; }
                    el.style.left = -1000;
                } catch (ee) { }
            }
        }
    } catch (er) { }
}
function HTMLEnCode(str) {
    var converter = document.createElement("DIV");
    converter.innerText = str;
    var output = converter.innerHTML;
    converter = null;
    return output;
}
function checkRight(rightcode) {
    var result = false;
    $.ajax({
        type: "POST",
        async: false,
        dataType: 'json',
        url: bootPATH + "../../../Admin/sys/account.aspx?handle=CheckRight&code=" + rightcode,
        success: function (data) {
            if (data.success) { result = true; }
            else {
                try {
                    if (data.code == 'logout') {
                        MyalertMsg(data.msg, 'info', function () { top.location.href = bootPATH + "../../../Admin/login.aspx"; });
                    }
                    else if (rightcode != 'Administrator') { tipMsg(data.msg); }
                } catch (e) { tipMsg(data.msg); }
            }
        }
    });
    return result;
}
function LoginShow() {
    $.ajax({
        type: "POST",
        async: false,
        dataType: 'json',
        url: bootPATH + "../../../Admin/sys/account.aspx?handle=loginshow",
        success: function (data) {
            if (data.success) {
                try {
                    document.getElementById('loginUser').innerHTML = data.username;
                } catch (e) { }
                try {
                    document.getElementById('loginTime').innerHTML = data.logincount;
                } catch (e) { }
            }
        }
    });
}
function LoginOut() { mini.confirm("您确定要退出登录吗？", "确定？", function (action) { if (action == "ok") { top.location.href = bootPATH + "../../../Admin/logout.aspx"; } });}