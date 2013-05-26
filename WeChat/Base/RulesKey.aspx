<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesKey.aspx.cs" Inherits="WeChat.Base.RulesKey" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>匹配项-Weback</title>
    <style type="text/css">
        *{ margin:0px; padding:0px;}body{width:100%; height:100%; background-color:#fafafa;}
        #divForm{width:100%; height:100%; overflow:hidden;}
        #divLeft{ height:100%;width:430px;background-color:#ffffff; vertical-align:top; }
        #divRight{height:100%;width:auto;text-align:center; vertical-align:top;}
        .control-title{ height:40px; text-align:center;}
        .control-title h5{ background-color:#f0f0f0; border-bottom:1px solid #dedede; padding:5px;}
        .control-group{ clear:both;}
        .control-label{ float:left; width:80px; text-align:right; font-size:12px; padding-top:5px;}
        .controls{ float:left; width:270px; text-align:left; min-height:30px;}
        .controls input,.controls textarea,.controls select,.controls option,.controls button{ width:100%; line-height:21px;}
        .form-actions{ text-align:center; clear:both;}
        .btn{ background-color:#acacac; padding:3px 9px; border:1px solid #a0a0a0; font-size:14px; font-family:微软雅黑; color:#fefefe;}
        .btn.btn-primary{ background-color:#0569CE; border:1px solid #471083;}.form-actions .btn:hover{ background-color:#0044CC; border:1px solid #471083;}
        .btnH5{font-size:14px; font-family:微软雅黑; color:#0569CE; text-decoration:none;}.btnH5:hover{ color:#FF5400; text-decoration:underline;}
    </style>
</head>
<body>
<div id="divForm">
    <table style=" width:100%;border-collapse:collapse; border:none;">
        <tr>
            <td id="divLeft">
        <div id="baseFrom">
            <div class="control-title">
            <span style=" float:right; padding:3px;">
                <a class="btnH5" href="javascript:addCode();">新增</a>
            </span>
            <h5>触发内容管理</h5>
            </div>                            
            <form class="form-horizontal" action="javascript:void(0);">
            <div class="control-group">
                <label class="control-label">
                    内容</label>
                <div class="controls">
                    <input type="text" class="grd-white" id="Code" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">
                    方式</label>
                <div class="controls">
                    <div data-toggle="buttons-radio" class="btn-group">
                        <select id="SepType">
                            <option value="#">命令触发模式</option>
                            <option value="$">关键词触发模式</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">
                    状态</label>
                <div class="controls">
                    <select id="Status">
                        <option value="normal">启用</option>
                        <option value="close">停用</option>
                        <option value="test">测试</option>
                    </select>
                </div>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn btn-primary" onclick="submitCode();">保存</button>
                <button type="button" class="btn" onclick="art.dialog.close();">返回规则列表</button>
            </div>
            </form>
        </div>
            </td>
            <td id="divRight">
    <%if (!string.IsNullOrEmpty(Request["guid"]))
      { %>
        <div class="control-title">
            <h5>内容列表</h5>
        </div>        
        <table id="dataTable" class="wlntable">
            <tr>
                <td style="width:60px;" filed="PushCount">&nbsp;</td>
                <td style="width:198px;" filed="Code">内容</td>
                <td style="width:60px;" function="onSepType">类型</td>
                <td style="width:60px;" function="onStatus">状态</td>
                <td style="width:80px;" function="onAction">操作</td>
            </tr>
        </table>
    <%}%>
            </td>
        </tr>
    </table>
    <div style=" clear:both;"></div>
</div>
<script src="../res/jquery.min.js"></script>
<script src="../res/wln/wln.js" type="text/javascript"></script>
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        try {
            $('#divLeft').height($(window).height());
            setTimeout(function(){
                $('#divLeft').height($(window).height());
            },1000);
        } catch (e) { }
    })

    var codeguid;
    function addCode() {
        $('#Code').val('');
        $('#Status').val('normal');
        codeguid = '';
    }
    function editCode(guid, code, septype, status) {
        $('#Code').val(code);
        $('#Status').val(status);
        $('#SepType').val(septype);
        codeguid = guid;
    }
    function reLoadData() {
        wln.loadTable('.wlntable', 'ruleskey.aspx', { 'action': 'getlist', 'RuleGuid': '<%=Request["guid"] %>' });
    }
    wln.onSepType = function (e) {
        if (e.SepType == '$') {
            return '<a title="关键词触发模式">关键词触发</a>';
        } else {
            return '<a title="命令触发模式">命令触发</a>';
        }
    }
    wln.onStatus = function (e) {
        if (e.SepType == 'close') {
            return '<a title="停用">停用</a>';
        } else if(e.SepType == 'test'){
            return '<a title="测试">测试</a>';
        } else {
            return '<a title="正常">正常</a>';
        }
    }
    wln.onAction = function (e) {
        return '<a href="javascript:editCode(\'' + e.Guid + '\',\'' + e.Code + '\',\'' + e.SepType + '\',\'' + e.Status + '\');">编辑</a>&nbsp;<a href="javascript:delCode(\'' + e.Guid + '\');">删除</a>';
    }
    reLoadData();

    function submitCode() {
        $.getJSON("ruleskey.aspx", { "action": "setcode", "RuleGuid": "<%=_Guid %>"
        , "guid": codeguid
        , "Code": $('#Code').val()
        , "SepType": $('#SepType').val()
        , "Status": $('#Status').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                    }, close: function () {
                        reLoadData();
                        addCode();
                    }
                });
            } else {
                $.dialog({
                    fixed: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
        return false;
    }
    function delCode(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前匹配项吗？',
            ok: function () {
                $.getJSON("ruleskey.aspx", { "action": "delcode", "Guid": guid }, function (json) {
                    if (json.success) {
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
                                reLoadData();
                                addCode();
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
            cancel: true //为true等价于function(){}
        });
    }

</script>
</body>
</html>

