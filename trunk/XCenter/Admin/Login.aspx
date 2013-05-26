<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XCenterCMS.Web.Admin.Login" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("Login")%></title>
        <script src="../xcenter/static/script/boot.js" type="text/javascript"></script>
        <script type="text/javascript">
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
                    tipMsg('登录帐号未填写，请填写!!');
                    document.getElementById('txtInputstr').focus();
                    result = false;
                    return;
                }
                if (password == "") {
                    tipMsg('登录密码未填写，请填写!!');
                    document.getElementById('txtPassword').focus();
                    result = false;
                    return;
                }
                if (result) {
                    if (document.getElementById('cbkRememberAccount').checked) {
                        mini.Cookie.set('RememberAccount', inputstr, 100);
                        mini.Cookie.set('RememberPassword', password, 100);
                    }
                    $.getJSON("Login.aspx", {
                        "handle": "loginin", "inputstr": inputstr, "password": password
                    }, function (data) {
                        if (data.success) {
                            tipMsg(data.msg);
                            location.href = "Default.aspx";
                        }
                        else {
                            tipMsg(data.msg);
                        }
                    });
                }
            }
            window.onload = function () {
                try {
                    var inputstr = mini.Cookie.get('RememberAccount');
                    var password = mini.Cookie.get('RememberPassword');
                    if (inputstr && password && inputstr.length > 0 && password.length > 0) {
                        $.getJSON("Login.aspx", {
                            "handle": "loginin", "inputstr": inputstr, "password": password
                        }, function (data) {
                            if (data.success) {
                                tipMsg(data.msg);
                                location.href = "Default.aspx";
                            }
                            else {
                                tipMsg(data.msg);
                            }
                        });
                    }
                } catch (e) { }
            }
        </script>
        <style type="text/css">
            * {margin:0; padding:0;}
            body {position:relative; height:100%;background:#fff url('../xcenter/static/assets/loginbg.png') left top repeat-x;font-family:Tahoma, Arial, sans-serif;}
            img {border:0px;}
            #container {width:960px;margin:0px auto;}
            #container .logo {width:230px;margin:240px auto 0px;}
            #container #box {clear:both;float:none;width:70%;margin:50px auto 0px;}
            p.main label {float:left; padding:5px;display:inline;margin-left:40px;font-size:13px;color:#000;margin-right:10px;}
            #box p {clear:both;float:none;width:100%;}
            p.main input {background:url('../xcenter/static/assets/input.png') 0 0 repeat-x; height:31px;border:1px solid #d3d3d3;color:#555;padding:5px;float:left;width:200px;}
            input.login {float:right;padding:3px 10px 3px 10px;color:#fff;font-size:12px;text-decoration:none;border:1px solid #555;background:url('../xcenter/static/assets/rep1.png') 50% 50% repeat-x;display:inline;cursor:pointer;margin-right:80px;}
            span { font-size:13px;color:#666;}
            .space {padding-top:15px; text-align:left;}
            span input {margin-left:125px;margin-right:5px;}
        </style>
    </head>
    <body style=" height:100%; width:100%; overflow:hidden; text-align:center;">
	<div id="container">
		<div class="logo"><a href="#"><img src="../xcenter/static/assets/logo.png" alt="" /></a></div>
		<div id="box">
			<form action="#">
			    <p class="main">
				    <label><%=UIConfig("Username")%>: </label><input id="txtInputstr" name="username" /> 
				    <label><%=UIConfig("Password")%>: </label><input id="txtPassword" type="password" name="password" onkeypress="KeySubmit()" />	
			    </p>
			    <p class="space">
				    <span><input id="cbkRememberAccount" type="checkbox" style=" border:none; padding:0px;" /><%=UIConfig("RememberAccount")%></span>
				    <input type="button" value="<%=UIConfig("Login")%>" onclick="DoSubmit();" class="login" />
			    </p>
			</form>
		</div>
	</div>
    </body>
</html>