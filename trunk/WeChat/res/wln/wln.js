window.wln = window.wln || {};
var __path = getWlnPath();
var __ele;
window.wln = {
    anthor: "wlniao",
    init: "wln.js",
    ele: __ele,
    path: __path,
    info: function () {
        alert("init from " + this.init);
    },
    loadTable: function (tid, url, data) {
        $.getJSON(url, data, function (json) {
            $(tid + " tr:not(:first)").remove();
            var headrow = $(tid + " tr:first");
            $.each(json, function (i, item) {
                var newrow = headrow.clone();
                try {
                    $.each(newrow.children("td"), function () {
                        try {
                            var fnName = $(this).attr('function');
                            if (fnName) {
                                $(this).addClass('cell').html(wln[fnName](item));
                            } else {
                                $(this).addClass('cell').html(item[$(this).attr('filed')]);
                            }
                        } catch (e) { $(this).html(''); }
                    });
                } catch (e) { }
                newrow.insertAfter(headrow);

                //for (var filed in item) { //获取全部字段并依次绑定
                //newrow.children("[filed='" + filed + "']").html(item[filed]).attr('wlntablecell', 'filed');
                //}

            });
        });
    },
    wlnUpload: function (eleid, postfile, progressFn, successFn) {
        try {
            if (!postfile) {
                postfile = wln.path + 'upload/upload.aspx';
            }
            new SWFUpload({
                upload_url: postfile,
                upload_progress_handler: progressFn,
                upload_success_handler: successFn,
                button_placeholder_id: eleid,
                flash_url: wln.path + "../SWFUpload/swfupload.swf"
            });
        } catch (e) { alert(e); }
    }

}
$(__ele).after('<link href="' + wln.path + 'wln.css" rel="stylesheet" type="text/css" />');
function getWlnPath(js) {
    var scripts = document.getElementsByTagName("script");
    var path = "";
    for (var i = 0, l = scripts.length; i < l; i++) {
        var src = scripts[i].src;
        if (src.indexOf("wln.js") != -1) {
            __ele = scripts[i];
            var ss = src.split("wln.js"); path = ss[0]; break;
        }
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

wln.publicPage = function () {
    $(wln.ele).after('<script src="' + wln.path + 'publicPage/other.js"></script>');
}