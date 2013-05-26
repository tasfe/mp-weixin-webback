<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXin.aspx.cs" Inherits="WeChat.Base.WeiXin" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>微信帐号管理-Weback</title>
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wln/font/font-wlniao.css" />
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/weixin.aspx" class="current">微信帐号管理</a></div>   
  </div>
        <div class="container-fluid">
            <div class="row-fluid">
              <div class="span12">
                <div class="widget-box">
                     <div class="widget-title">
                        <div style=" width:300px; text-align:right; float:right; padding:3px;">
                            <a class="btn btn-primary" href="javascript:void(0);" onclick="reLoadData();" style=" float:right; margin-right:5px;">刷新</a> 
                            <a class="btn btn-primary" href="javascript:void(0);" onclick="Add()" style=" float:right; margin-right:5px;">新增</a> 
                            <a class="btn btn-primary" href="javascript:void(0);" onclick="Setting('','默认设置')" style=" float:right; display:none; margin-right:5px;">默认设置</a> 
                        </div>
                        <h5>我的微信公众帐号</h5>
                    </div>
                   
                    <table id="dataTable" class="wlntable">
                        <tr>
                            <td style="width:90px;" filed="WeChatName">名称</td>
                            <td style="width:90px;" filed="AccountName">帐号</td>
                            <td style="width:128px;" filed="AccountFirst">原始帐号</td>
                            <td style="width:auto;" filed="WeChatToken">Token</td>
                            <td style="width:120px;" filed="CreateTime">添加时间</td>
                            <%--<td style="width:60px;" filed="RequestCount">累计量</td>--%>
                            <td style="width:120px;" function="onAction">操作</td>
                        </tr>
                    </table>
                    <div style=" padding:5px 0px ;">
                        &nbsp;<font color="red">提示：</font>添加微信公众帐号后，请将微信公众帐号设置为开发模式，接口地址：http://<%=_website%>/wechatapi.aspx
                    </div>
                </div>
              </div>
            </div>
        </div>

</div>
<script src="../res/jquery.min.js"></script>
<script src="../res/wln/wln.js" type="text/javascript"></script>
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    wln.onAction = function (e) {
        str = '<a href="javascript:Setting(\'' + e.Id + '\',\'' + e.WeChatName + '\')">编辑</a>&nbsp;<a href="javascript:void(\'0\')" onclick="del(\'' + e.Id + '\',\'' + e.WeChatName + '\');">删除</a>';
        return str;
    }
    function reLoadData() {
        wln.loadTable('.wlntable', 'weixin.aspx', { 'action': 'getlist' });
    }
    function del(id, name) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>微信帐号删除后无法恢复，需谨慎操作；<br/>您确定要删除帐号“' + name + '”吗？',
            ok: function () {
                $.getJSON("weixin.aspx", { "action": "del", "id": id }, function (json) {
                    if (json.success) {
                        reLoadData();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,帐号删除成功！',
                            close: function () {
                                reLoadData();
                            }
                        });
                    } else {
                        $.dialog({
                            lock: true,
                            fixed: true,
                            icon: 'error',
                            content: json.msg
                        });
                    }
                });
            },
            okVal: '是,删除',
            cancelVal: '取消',
            cancel: true
        });
    }
    reLoadData();
    function Add() {
        art.dialog.open(wln.path + '../../base/setting.aspx?new=true', { title: '添加微信公众帐号', width: 580, height: 520, lock: true, drag: false, close: function () { reLoadData(); } });
    }
    function Setting(id,title) {
        art.dialog.open(wln.path + '../../base/setting.aspx?id=' + id, { title: title, width: 580, height: 520, lock: true, drag: false, close: function () { reLoadData(); } });
    }
</script>
</body>
</html>

