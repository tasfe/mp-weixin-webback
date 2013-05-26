<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesReact.aspx.cs" Inherits="WeChat.Base.RulesReact" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>自定义互动规则-Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <link rel="stylesheet" href="../res/wln/font/font-wlniao.css" />
    <link href="../res/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
</head>
<body>

<div id="content" style=" margin:0px;">
  <div id="content-header">
    <div id="breadcrumb"> <a href="../main.aspx" title="返回首页" class="tip-bottom"><i class="icon-home"></i>未来鸟微信平台</a> <a href="../base/rulesreact.aspx" class="current">自动应答</a></div>   
  </div>

        <div class="container-fluid">
            <div class="row-fluid">
              <div class="span12">
                <div class="widget-box">
                     <div class="widget-title">
                        <div style="float:right; padding:3px;">
                            <a class="btn btn-primary" href="javascript:void(0);" onclick="add();">新建应答</a>                            
                        </div>
                        <h5>自动应答管理</h5>
                        <select id="AccountFirst" onchange="SetFirstId()" style=" margin-top:3px;">
                            <option value="">全部帐号</option>
                            <%=_WeiXin%>
                        </select>
                    </div>
                    <div id="datagrid" class="mini-datagrid" style="width:100%;" pageSize="18" showPageIndex="true" showLoading="false" showTotalCount="true" allowResize="false" url="rulesreact.aspx?action=getlist" idField="Guid" multiSelect="true" >
                        <div property="columns">
                            <div type="indexcolumn" width="18"></div>
                            <div field="RuleName" width="120" headerAlign="center" align="left" allowSort="false" renderer="onRuleName">规则名称</div>
                            <div field="AccountFirst" width="80" headerAlign="center" align="left" allowSort="false">所属帐号</div>
                            <div field="RuleConfig" width="270" headerAlign="center" align="left" allowSort="false" renderer="onReact">应答内容&方式</div>
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
    function trim(str) {
        for (var i = 0; i < str.length && str.charAt(i) == "  "; i++);
        for (var j = str.length; j > 0 && str.charAt(j - 1) == "  "; j--);
        if (i > j) return "";
        return str.substring(i, j).replace(/(^\s*)|(\s*$)/g, "");
    }
    mini.parse();
    var firstId = '';
    var grid = mini.get("datagrid");
    function onReact(e) {
        var cfg = eval('(' + e.value + ')');
        if (cfg.ReContent) {
            return '<a style="color:green;">' + trim(cfg.ReContent) + '</a>';
        } else {
            if (cfg.SendMode == 'sendnew') {
                return '<a style="color:#38BEF0;">使用位于内容列表顶部的信息应答</a>-【<a href="javascript:edit(\'' + e.Guid + '\');">管理</a>】';
            } else if (cfg.SendMode == 'sendrandom') {
                return '<a style="color:#38BEF0;">随机使用列表中的一条信息应答</a>-【<a href="javascript:edit(\'' + e.Guid + '\');">管理</a>】';
            } else if (cfg.SendMode == 'sendgroup') {
                return '<a style="color:#38BEF0;">同时发送列表中的多条图文类信息</a>-【<a href="javascript:edit(\'' + e.Guid + '\');">管理</a>】';
            }
        }
        return '<a style="color:gray;">' + cfg.ReContent + '</a>';
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
        art.dialog.open(wln.path + '../../base/rulesreactForm.aspx', { title: '添加应答', width: 430, height: 380, lock: true, drag: false, close: function () { ReloadData(); } });
    }
    function edit(guid) {
        art.dialog.open(wln.path + '../../base/rulesreactForm.aspx?Guid=' + guid, { title: '编辑应答', width: 980, height: 520, lock: true, drag: false, close: function () { ReloadData(); } });
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

