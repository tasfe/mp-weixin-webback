<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WeChat.Login" %>
<html>
    <head>
        <title>管理登录|微信公众帐号管理系统 - Weback</title>
        <script src="res/jquery.min.js"></script>
        <script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
        <script src="res/wln/wln.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () { wln.publicPage(); })
            
            function KeySubmit() {
                if (event.keyCode == 13) {
                    DoSubmit();
                }
            }
            function DoSubmit() {
                var result = true;
                var inputstr = document.getElementById('txtInputstr').value;
                var password = document.getElementById('txtPassword').value;
                if (inputstr == "") {
                    alert('登录帐号未填写，请填写!!');
                    document.getElementById('txtInputstr').focus();
                    result = false;
                    return;
                }
                if (password == "") {
                    alert('登录密码未填写，请填写!!');
                    document.getElementById('txtPassword').focus();
                    result = false;
                    return;
                }
                if (result) {
                    if (document.getElementById('cbkRememberAccount').checked) {
                        setCookie('RememberAccount', inputstr, 100);
                        setCookie('RememberPassword', password, 100);
                    }
                    var dialog = art.dialog({
                        id: 'login',
                        lock: true,
                        drag: false,
                        resize: false,
                        title: '管理平台登录',
                        content: '正在校验您的帐号密码,请稍等！',
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
                    $.getJSON("login.aspx", {
                        "do": "login",
                        "username": inputstr,
                        "password": password
                    }, function (json) {
                        if (json.success) {
                            dialog.content('Success,登录成功,点击确定转向系统首页!<br/><span color="gray">3秒后系统也会自动为您跳转</span>');
                            dialog.button({
                                name: '确定',
                                callback: function () {
                                    top.location.href = 'default.aspx';
                                },
                                disabled: false
                            });
                            setTimeout(function () {
                                top.location.href = 'default.aspx';
                            }, 3000);
                        } else {
                            dialog.close();
                            $.dialog({
                                lock: true,
                                icon: 'error',
                                title: '错误提示',
                                content: json.msg
                            });
                        }
                    });
                }
            }
            window.onload = function () {
                try {
                    var inputstr = getCookie('RememberAccount');
                    var password = getCookie('RememberPassword');
                    if (inputstr && password && inputstr.length > 0 && password.length > 0) {

                        var dialog = art.dialog({
                            id: 'login',
                            lock: true,
                            drag: false,
                            resize: false,
                            title: '管理平台登录',
                            content: '正在校验您的帐号密码,请稍等！',
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
                        $.getJSON("login.aspx", {
                            "do": "login",
                            "username": inputstr,
                            "password": password
                        }, function (json) {
                            if (json.success) {
                                dialog.content('Success,登录成功,点击确定转向系统首页!<br/><span color="gray">3秒后系统也会自动为您跳转</span>');
                                dialog.button({
                                    name: '确定',
                                    callback: function () {
                                        top.location.href = 'default.aspx';
                                    },
                                    disabled: false
                                });
                                setTimeout(function () {
                                    top.location.href = 'default.aspx';
                                }, 3000);
                            } else {
                                dialog.close();
                                $.dialog({
                                    lock: true,
                                    icon: 'error',
                                    title: '错误提示',
                                    content: json.msg
                                });
                            }
                        });
                    }
                } catch (e) { }
            }
        </script>
    </head>
    <body>
	<div class="container">
		<div class="logo"><img id="imgLogo" src="res/wln/publicPage/logo.png" alt="Wlniao未来鸟软件" /></div>
		<div class="box">
			<form action="#">
			    <p class="main">
				    <label>管理员帐号: </label><input id="txtInputstr" name="username" /> 
				    <label>登录密码: </label><input id="txtPassword" type="password" name="password" onkeypress="KeySubmit()" />	
			    </p>
			    <p class="space">
				    <span style=" display:none;"><input id="cbkRememberAccount" type="checkbox" style=" border:none; padding:0px;" />记住帐号</span>
				    <input type="button" value="登录" onclick="DoSubmit();" class="login" />
			    </p>
			</form>
		</div>
	</div>
    </body>
</html>