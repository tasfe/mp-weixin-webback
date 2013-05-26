<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="WeChat.Install" %>
<html>
    <head>
        <title>系统初始化|微信公众帐号管理系统 - Weback</title>
        <script src="res/jquery.min.js"></script>
        <script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
        <script src="res/wln/wln.js" id="resTag" type="text/javascript"></script>
        <script type="text/javascript">            $(function () { wln.publicPage(); })</script>
    </head>
    <body>
	    <div class="container">
		    <div class="logo"><img id="imgLogo" src="res/wln/publicPage/logo.png" alt="Wlniao未来鸟软件" /></div>
		    <div class="box">
                <p><input class="button" type="button" value="立即安装" onclick="InstallByAccess();"/></p>			
		    </div>
	    </div>
    </body>
</html>