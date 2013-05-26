<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelUp.aspx.cs" Inherits="WeChat.LevelUp" %>
<html>
    <head>
    <title>版本升级|微信公众帐号管理系统 - Weback</title>
    <script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
    <script type="text/javascript" src="res/wln/wln.js"></script>
    <script type="text/javascript">            $(function () { _publicPage() })</script>
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
                setCookie('RememberAccount', '');
                setCookie('RememberPassword', '');
                self.location.href = 'default.aspx';
            }
        }
    </script>
    </head>
    <body>
	<div class="container">
		<div class="logo"><img id="imgLogo" src="res/wln/publicPage/logo.png" alt="Wlniao未来鸟软件" /></div>
		<div class="box">
            <p>恭喜你，您的程序已更新成功，<font id="Font1" color="red">请删除LevelUp.aspx文件</font>，系统将在 <font id="timeM" color="red">5</font> 秒钟后跳转至登录页面...</p>			
		</div>
	</div>
    </body>
</html>