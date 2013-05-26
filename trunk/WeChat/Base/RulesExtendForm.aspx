<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesExtendForm.aspx.cs" Inherits="WeChat.Base.RulesExtendForm" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>扩展开发接口-Weback</title>
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
            <div class="control-title"><h5>应答规则</h5></div>                            
            <form class="form-horizontal" action="javascript:void(0);">
            <div class="control-group">
                <label class="control-label" title="当前应答规则的名称，不是内容">名称:</label>
                <div class="controls">
                    <input id="RuleName" type="text" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" title="">所在程序集:</label>
                <div class="controls">
                    <input id="Assembly" type="text" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" title="完整的类名+方法名">方法名:</label>
                <div class="controls">
                    <input id="Method" type="text" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">所属帐号:</label>
                <div class="controls">
                    <select id="AccountFirst" style=" margin-top:3px;">
                        <option value="">全部帐号</option>
                        <%=_WeiXin%>
                    </select>
                </div>
            </div>
            <div class="control-group" style=" display:none;">
                <label class="control-label" title="默认参数">参数:</label>
                <div class="controls">
                    <input id="BaseArgs" type="text" />
                </div>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn btn-primary" onclick="submitRule();">保存</button>
                <button type="button" class="btn" onclick="art.dialog.close();">返回规则列表</button>
            </div>
            </form>
        </div>
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
    function submitRule() {
        try {
            if (!$('#RuleName').val()) {
                $.dialog({
                    fixed: true,
                    icon: 'error',
                    content: '规则名称未填写',
                    ok: function () {
                        $('#RuleName').focus();
                    }
                });
                return false;
            }
            GoonSave();
        } catch (e) { }
    }
    function GoonSave() {
        $.getJSON("rulesextendform.aspx", { "action": "set", "guid": "<%=_Guid %>"
        , "RuleName": $('#RuleName').val()
        , "AccountFirst": $('#AccountFirst').val()
        , "Assembly": $('#Assembly').val()
        , "Method": $('#Method').val()
        , "BaseArgs": $('#BaseArgs').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                        if ('<%=_Guid %>' == '') {
                            art.dialog.close();
                        }
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
    }
    function init() {
        $.getJSON("rulesextendform.aspx", { "action": "get", "guid": "<%=_Guid %>" }, function (json) {
            $('#RuleName').val(json.RuleName);
            $('#AccountFirst').val(json.AccountFirst);
            if (json.RuleConfig) {
                var cfg = eval('(' + json.RuleConfig + ')');
                $('#Assembly').val(cfg.Assembly);
                $('#Method').val(cfg.Method);
                $('#BaseArgs').val(cfg.BaseArgs);
            }
        });
    }
    init();    
</script>
</body>
</html>

