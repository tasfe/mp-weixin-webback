<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="XCenterCMS.Web.Admin.Install" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("Install")%></title>
        <script src="../xcenter/static/script/boot.js" type="text/javascript"></script>
        <script type="text/javascript">
            var time = 4500;
            var tick = 492;
            window.onload = function () {
                try {
                    setTimeout("GotoLogin()", tick)
                } catch (e) { }
            }
            function GotoLogin() {
                if (time > 0) {
                    time = time - tick;
                    setTimeout("GotoLogin()", tick)
                    document.getElementById('timeM').innerHTML = parseInt(time / 1000).toString();
                } else {
                    mini.Cookie.set('RememberAccount', '');
                    mini.Cookie.set('RememberPassword', '');
                    self.location.href = 'login.aspx';
                }
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
            <p>恭喜你，初始化执行成功，系统将在 <font id="timeM" color="red">5</font> 秒钟后跳转至登录页面...</p>			
		</div>
	</div>
    </body>
</html>