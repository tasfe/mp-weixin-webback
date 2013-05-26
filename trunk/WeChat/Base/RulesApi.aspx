<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesApi.aspx.cs" Inherits="WeChat.Base.RulesApi" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>API规则-Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wln/font/font-wlniao.css" />
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/rulesapi.aspx" class="current">API接口</a></div>   
  </div>

        <div class="container-fluid">
            <div class="row-fluid">
              <div class="span12">
                <div class="widget-box">
                     <div class="widget-title">
                        <div style="float:right; padding:3px;">
                            <a class="btn btn-primary" href="javascript:void(0);" onclick="add();">添加接口</a>                            
                        </div>
                        <h5>API接口管理</h5>
                        <select id="AccountFirst" onchange="SetFirstId()" style=" margin-top:3px;">
                            <option value="">全部帐号</option>
                            <%=_WeiXin%>
                        </select>
                    </div>
                    <div id="datagrid" class="mini-datagrid" style="width:100%;" pageSize="18" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="rulesapi.aspx?action=getlist" idField="Guid" multiSelect="true" >
                        <div property="columns">
                            <div type="indexcolumn" width="18"></div>
                            <div field="RuleName" width="120" headerAlign="center" align="left" allowSort="false" renderer="onRuleName">接口名称</div>
                            <div field="AccountFirst" width="80" headerAlign="center" align="left" allowSort="false">所属帐号</div>
                            <div field="RuleConfig" width="270" headerAlign="center" align="left" allowSort="false" renderer="onApi">API地址</div>
                            <div field="Guid" width="88" headerAlign="center" align="center" allowSort="false" renderer="onOp">操作</div>
                        </div>
                    </div>
                </div>
              </div>
            </div>
        </div>

</div>
<script src="../res/jquery.min.js"></script>
<script src="../res/wln/wln.js" id="resTag" type="text/javascript"></script>
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script src="../res/miniui/miniui.js" type="text/javascript"></script>
<script type="text/javascript">
    mini.parse();
    var firstId = '';
    var grid = mini.get("datagrid");
    function onApi(e) {
        var cfg = eval('(' + e.value + ')');
        return '<a style="color:gray;">' + cfg.ApiUrl + '</a>';
    }
    function SetFirstId() {
        firstId = $('#AccountFirst').val();
        ReloadData();
    }
    function ReloadData() {
        grid.load({ 'firstId': firstId });
    }
    ReloadData();
    function onOp(e) {
        return '<a href="javascript:code(\'' + e.value + '\');">触发内容</a>&nbsp;<a href="javascript:edit(\'' + e.value + '\');">编辑</a>&nbsp;<a href="javascript:del(\'' + e.value + '\');">删除</a>';
    }
    function add() {
        art.dialog.open(wln.path + '../../base/rulesapiForm.aspx', { title: '添加接口', width: 430, height: 240, lock: true, drag: false, close: function () { ReloadData(); } });
    }
    function edit(guid) {
        art.dialog.open(wln.path + '../../base/rulesapiForm.aspx?Guid=' + guid, { title: '编辑接口', width: 430, height: 240, lock: true, drag: false, close: function () { ReloadData(); } });
    }
    function code(guid) {
        art.dialog.open(wln.path + '../../base/ruleskey.aspx?Guid=' + guid, { title: '触发内容管理', width: 980, height: 520, lock: true, drag: false, close: function () { ReloadData(); } });
    }
    function del(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前规则吗？',
            ok: function () {
                $.getJSON("rules.aspx", { "action": "del", "Guid": guid }, function (json) {
                    if (json.success) {
                        grid.reload();
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
                                ReloadData();
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
</script>
</body>
</html>

