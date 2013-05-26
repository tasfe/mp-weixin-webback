<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="WeChat.Logout" %>
<html>
    <head>
    <title>退出登录|微信公众帐号管理系统 - Weback</title>
    <script src="res/jquery.min.js"></script>
    <script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
    <script src="res/wln/wln.js" id="resTag" type="text/javascript"></script>
    <script type="text/javascript">
        wln.publicPage();
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
                self.location.href = 'default.aspx';
            }
        }
    </script>
    </head>
    <body>
	<div class="container">
		<div class="logo"><img id="imgLogo" src="res/wln/publicPage/logo.png" alt="Wlniao未来鸟软件" /></div>
		<div class="box">
            <p>恭喜你，您已退出成功，系统将在 <font id="timeM" color="red">5</font> 秒钟后跳转至登录页面...</p>			
		</div>
	</div>
    </body>
</html>