
$(wln.ele).before('<link rel="stylesheet" href="' + wln.path + 'publicPage/base.css">');
$('#imgLogo').attr('src', wln.path + 'publicPage/logo.png');

$(wln.ele).before('<script src="' + wln.path + '../artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></sc' + 'ript>');
$(wln.ele).before('<link rel="stylesheet" href="' + wln.path + '../artDialog/skins/twitter.css">');
function Install(str) {
    var dialog = art.dialog({
        title: '请填写数据库信息',
        content: '<div id="dbInfo" style="height:60px; width:300px;">'
    	+ '<p><span style="display:inline-block;width:80px;text-align:right;">数据库类型</span>:&nbsp;<input type="radio" name="dbtype" value="access" checked="checked" onclick="radioDbTypeChange()" />Access&nbsp;<input type="radio" name="dbtype" value="mysql" onclick="radioDbTypeChange()" />MySql&nbsp;<input type="radio" name="dbtype" value="mssql" onclick="radioDbTypeChange()" />MsSql<br/>&nbsp;</p>'
    	+ '<p class="toHide"><span style="display:inline-block;width:80px;text-align:right;">服务器地址</span>:<input id="server" style="width:15em; padding:4px" /><br/>&nbsp;</p>'
        + '<p class="toHide"><span style="display:inline-block;width:80px;text-align:right;">数据库名称</span>:<input id="dbname" style="width:15em; padding:4px" /><br/>&nbsp;</p>'
        + '<p class="toHide"><span style="display:inline-block;width:80px;text-align:right;">数据库帐号</span>:<input id="username" style="width:15em; padding:4px" /><br/>&nbsp;</p>'
        + '<p class="toHide"><span style="display:inline-block;width:80px;text-align:right;">密&nbsp;&nbsp;码</span>:<input id="password" style="width:15em; padding:4px" /><br/></p>'
        + '</div>',
        fixed: true,
        lock: true,
        id: 'Fm7',
        okVal: '继续',
        ok: function () {
            if ($(":radio[checked=checked]").val() != 'access') {
                if (!$('#server').val()) {
                    alert('Sorry,服务器地址未填写！');
                    $('#server').focus();
                    return false;
                }
                if (!$('#dbname').val()) {
                    alert('Sorry,数据库名称未填写！');
                    $('#dbname').focus();
                    return false;
                }
                if (!$('#username').val()) {
                    alert('Sorry,数据库帐号未填写！');
                    $('#username').focus();
                    return false;
                }
                if (!$('#password').val()) {
                    alert('Sorry,数据库密码未填写！');
                    $('#password').focus();
                    return false;
                }
            }

            var dialog = art.dialog({
                id: 'install',
                lock: true,
                drag: false,
                resize: false,
                title: '系统初始化',
                content: '正在为您初始化系统,请稍等！',
                ok: function () {
                    return false;
                },
                close: function () {
                },
                button: [{
                    name: '确定',
                    disabled: true,
                    focus: true
                }]
            });
            dialog.button({
                name: '确定',
                callback: function () {
                },
                disabled: true
            });
            $.getJSON("install.aspx", {
                "do": "init",
                "dbtype": $(":radio[checked=checked]").val(),
                "server": $("#server").val(),
                "dbname": $("#dbname").val(),
                "username": $("#username").val(),
                "password": $('#password').val()
            }, function (json) {
                if (json.success) {
                    setTimeout(function () {
                        dialog.content('Success,系统初始化成功，赶紧登录您的系统吧!<br/>默认帐号：admin&nbsp;默认密码：admin<br/><font color="red">注意：安装成功后请删除install.aspx以免留下安全隐患!!!</font>');
                        dialog.button({
                            name: '确定',
                            callback: function () {
                                window.location.href = 'login.aspx';
                            },
                            disabled: false
                        });
                    }, 5000);
                } else {
                    dialog.close();
                    art.dialog({
                        lock: true,
                        icon: 'error',
                        title: '错误提示',
                        content: json.msg
                    });
                }
            });

        },
        close: function () {
            return false;
        },
        cancel: true
    });
    $('.toHide').hide();
}
function InstallByAccess() {
    var dialog = art.dialog({
        id: 'install',
        lock: true,
        drag: false,
        resize: false,
        title: '系统初始化',
        content: '正在为您初始化系统,请稍等！',
        ok: function () {
            return false;
        },
        close: function () {
        },
        button: [{
            name: '确定',
            disabled: true,
            focus: true
        }]
    });
    dialog.button({
        name: '确定',
        callback: function () {
        },
        disabled: true
    });
    $.getJSON("install.aspx", {
        "do": "init",
        "dbtype": 'accessE'
    }, function (json) {
        if (json.success) {
            setTimeout(function () {
                dialog.content('Success,系统初始化成功，赶紧登录您的系统吧!<br/>默认帐号：admin&nbsp;默认密码：admin<br/><font color="red">注意：安装成功后请删除install.aspx以免留下安全隐患!!!</font>');
                dialog.button({
                    name: '确定',
                    callback: function () {
                        window.location.href = 'login.aspx';
                    },
                    disabled: false
                });
            }, 5000);
        } else {
            dialog.close();
            art.dialog({
                lock: true,
                icon: 'error',
                title: '错误提示',
                content: json.msg
            });
        }
    });
}
function radioDbTypeChange() {
    if ($(":radio[checked=checked]").val() == 'access') {
        $('.toHide').hide();
        $('#dbInfo').height(60);
    } else {
        $('.toHide').show();
        $('#dbInfo').height(210);
    }
}
function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}
$(wln.ele).before('<script id="resTag" src="http://tajs.qq.com/stats?sId=23924686" type="text/javascript"></sc' + 'ript>');
function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}