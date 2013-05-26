<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeChat.Default" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title><%=_titleName%> 微信公众帐号管理系统 - Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="res/bootstrap.min.css" />
    <link rel="stylesheet" href="res/wlniao-style.css" />
    <link rel="stylesheet" href="res/wlniao-iframe.css" />
    <link rel="stylesheet" href="res/wln/font/font-wlniao.css" />
    <script src="res/wln/wlniao.com.js" type="text/javascript"></script>
</head>
<body style=" overflow:hidden;">
<div id="header"><h1><a href="#" class="tip-bottom" title="Weback">Weback</a></h1></div>
<div id="user-nav" class="navbar navbar-inverse">
  <ul class="nav">
    <li class=""><a title="" href="javascript:GotoPage('base/weixin.aspx');"><i class="icon icon-cog"></i> <span class="text">微信设置</span></a></li>
    <li class=""><a title="" href="javascript:GotoPage('base/fans.aspx');"><i class="icon icon-group"></i> <span class="text">订阅者</span><script src="base/fans.aspx?action=getnewcount"></script> </a></li>
    <li class=""><a title="" href="javascript:GotoPage('base/account.aspx');"><i class="icon icon-user"></i> <span class="text">管理员</span></a></li>
    <li class=""><a title="" href="logout.aspx"><i class="icon-key"></i> <span class="text">注销登录</span></a></li>
  </ul>
  <div style="clear:both;"></div>
</div>
<div id="sidebar">
  <ul>
    <li class="menuli"><a href="javascript:GotoPage('base/weixin.aspx');"><i class="icon icon-wlniao"></i> <span>帐号管理</span></a> </li>
    <li class="menuli"><a href="javascript:GotoPage('base/rulesreact.aspx');"><i class="icon icon-th"></i> <span>自动应答</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('base/mybutton.aspx');"><i class="icon icon-pushpin"></i> <span>自定义菜单</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('base/rulesapi.aspx');"><i class="icon icon-random"></i> <span>开放API接口</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('base/rulesmpapi.aspx');"><i class="icon icon-link"></i> <span>标准转发接口</span></a></li>
    <li class="menuli"><a href="javascript:GotoPage('base/rulesextend.aspx');"><i class="icon icon-list-alt"></i> <span>扩展开发接口</span></a></li>
    <%--<li class="menuli"><a href="javascript:GotoPage('base/mybutton.aspx');"><i class="icon icon-check-empty"></i> <span>自定义代码</span></a></li>--%>
    <li class="menuli"><a href="javascript:GotoPage('http://doc.wlniao.com/doc/weback/index.html');"><i class="icon icon-info-sign"></i> <span>关于程序</span></a></li>
  </ul>
</div>
<div id="content" style="margin-top:-38px;"></div>
<div class="row-fluid">
  <div id="footer" class="span12"> 2013 &copy; <a href="http://weback.wlniao.com/" target="_blank">Weback</a> &nbsp;&nbsp; 技术支持：<a href="http://www.wlniao.com/" target="_blank">Wlniao Studio</a> </div>
</div>
<script src="res/jquery.min.js"></script>
<script src="res/wlniao.js"></script>
<script src="res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    var iHeight = 0;
    function init() {
        var winHeight = $(window).height();
        iHeight = winHeight - 70;
        $("#content").height(iHeight);
    }
    init();
    var frameid = "iframepage";
    function setFrameHeight(height) {
        try {
            document.getElementById(frameid).height = height;
        } catch (e) { }
    }
    function iFrameHeight() {
        var ifm = document.getElementById(frameid);
        try {
            var subWeb = document.frames ? document.frames[frameid].document : ifm.contentDocument;
            if (ifm != null) {
                if (subWeb == null) {
                    ifm.height = ifm.ownerDocument.body.scrollHeight;
                    ifm.scrolling = "auto";
                } else {
                    ifm.height = subWeb.body.scrollHeight;
                }
            }
        } catch (e) {}
    }
    function GotoPage(url) {
        var now = new Date();
        var html = '<iframe id="' + frameid + '" src="' + url + '" frameborder="0" marginheight="0" marginwidth="0" scrolling="auto" width="100%" height="' + iHeight + '"></iframe> ';
        document.getElementById('content').innerHTML = html;
    }
    GotoPage('main.aspx');
</script>
</body>
</html>

