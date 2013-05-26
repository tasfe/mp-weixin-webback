
$.extend(
{
    PageSize: function () {
        var width = 0;
        var height = 0;
        width = window.innerWidth != null ? window.innerWidth : document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body != null ? document.body.clientWidth : null;
        height = window.innerHeight != null ? window.innerHeight : document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body != null ? document.body.clientHeight : null;
        return { Width: width, Height: height };
    }
, ScrollPosition: function () {
    var top = 0, left = 0;
    if ($.browser.mozilla) {
        top = window.pageYOffset;
        left = window.pageXOffset;
    }
    else if ($.browser.msie) {
        top = document.documentElement.scrollTop;
        left = document.documentElement.scrollLeft;
    }
    else if (document.body) {
        top = document.body.scrollTop;
        left = document.body.scrollLeft;
    }
    return { Top: top, Left: left };
}
});
jQuery.fn.extend({
   CloseDiv: function () {
    if ($.browser.mozilla) {
        this.hide();
    } else {
        this.fadeOut("fast");
    } $("html").css("overflow", $("#BigDiv").attr("htmloverflow"));
    window.scrollTo($("#BigDiv").attr("divbox_scrollleft"), $("#BigDiv").attr("divbox_scrolltop"));
    $("#BigDiv").data("divbox_selectlist").show();
    $("#BigDiv").remove();
}
});

window.wln = window.wln || {};
if (!window.wln.anthor) {
    window.wln = {
        anthor: "wlniao",
        init: "wlniao.Dialog.js",
        info: function () {
            alert("init from " + this.init);
        }
    }
}
var _WlniaoScript = $('#resTag');
_WlniaoScript.before('<link rel="stylesheet" href="' + wlniaoPATH + 'Dialog/Dialog.min.css">');
wln.dialog = function (options) {
    if (!options.id) {
        options.id = "wln-dialog-" + new Date().getTime();
    }
    dialog = "<div class='wln-dialog' id='" + options.id + "'><div class='wln-dialog-hd'>X</div><div class='wln-dialog-bd' style='width:" + options.width + "px;height:" + options.height + "px;'>dddddd</div></div>";
    var win = $(document.body).append(dialog).find("#" + options.id);
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    if (window.screen.availHeight > document.body.scrollHeight) {
        sHeight = window.screen.availHeight;
    } else {
        sHeight = document.body.scrollHeight + 20;
    }
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'BigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#111";
    maskObj.style.filter = "Alpha(opacity=80);";
    maskObj.style.opacity = "0.8";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "10000";
    $("body").attr("scroll", "no");
    document.body.appendChild(maskObj);
    $("#" + options.id).data("divbox_selectlist", $("select:visible"));
    $("select:visible").hide();
    $("#" + options.id).attr("divbox_scrolltop", $.ScrollPosition().Top);
    $("#" + options.id).attr("divbox_scrollleft", $.ScrollPosition().Left);
    $("#" + options.id).attr("htmloverflow", $("html").css("overflow"));
    $("html").css("overflow", "hidden");
    window.scrollTo($("#" + options.id).attr("divbox_scrollleft"), $("#" + options.id).attr("divbox_scrolltop"));
    var MyDiv_w = win.width();
    var MyDiv_h = win.height();
    MyDiv_w = parseInt(MyDiv_w);
    MyDiv_h = parseInt(MyDiv_h);
    var width = $.PageSize().Width;
    var height = $.PageSize().Height;
    var left = $.ScrollPosition().Left;
    var top = $.ScrollPosition().Top;
    var Div_topposition = top + (height / 2) - (MyDiv_h / 2);
    var Div_leftposition = left + (width / 2) - (MyDiv_w / 2);
    win.css("position", "absolute");
    win.css("z-index", "10001");
    win.css("background", "#fff");
    win.css("left", Div_leftposition + "px");
    win.css("top", Div_topposition + "px");
    if ($.browser.mozilla) {
        win.show();
        return;
    }
    win.fadeIn("fast");
}