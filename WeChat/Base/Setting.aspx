<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WeChat.Base.Setting" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>基础设置-Weback</title>
    <meta charset="UTF-8" />
    <link rel="stylesheet" href="../res/bootstrap.min.css" />
    <link rel="stylesheet" href="../res/wlniao-style.css" />
    <style type="text/css">
        .control-group {border-top:0px solid #ffffff;}
    </style>
</head>
<body>
<div id="content" style=" margin:0px;">
  <div class="container-fluid">
                    <form class="form-horizontal" action="javascript:void(0);" style=" margin-right:75px;">
                    <div>&nbsp;</div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label"><font color="red">*</font>微信公众名称</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="WeChatName" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">微信帐号</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="AccountName" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label"><font color="red">*</font>微信原始号</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="AccountFirst" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">
                            Token</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="WeChatToken" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">
                            请设置接口为</label>
                        <div class="controls">
                            http://<%=_website%>/wechatapi.aspx
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">
                            无匹配回复</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="DefaultCmd" placeholder="" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">
                            Appid</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="Appid" title="在公众平台申请内测资格，审核通过后可获得" style=" width:300px;" />
                        </div>
                    </div>
                    <div class="control-group" style="border:none;">
                        <label class="control-label">
                            Secret</label>
                        <div class="controls">
                            <input type="text" class="grd-white" id="Secret" title="在公众平台申请内测资格，审核通过后可获得" style=" width:300px;" />
                        </div>
                    </div>
                    </form>
                    <div class="form-actions" style=" text-align:right;">
                        <button type="submit" class="btn btn-primary" onclick="submitForm();">
                            保存</button>
                        <button type="button" class="btn" onclick="art.dialog.close();">取消</button>
                    </div>
  </div>
</div>
<script src="../res/jquery.min.js"></script>
<script src="../res/wln/wln.js"></script>
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    function submitForm() {
        $.getJSON("weixin.aspx", { "action": "set"
        , "id": '<%=Request["id"] %>'
        , "new": '<%=Request["new"] %>'
        , "WeChatName": $('#WeChatName').val()
        , "AccountName": $('#AccountName').val()
        , "AccountFirst": $('#AccountFirst').val()
        , "WeChatToken": $('#WeChatToken').val()
        , "DefaultCmd": $('#DefaultCmd').val()
        , "Appid": $('#Appid').val()
        , "Secret": $('#Secret').val()
        }, function (json) {
            if (json.success) {
                $.dialog({
                    time: 2,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                        art.dialog.close();
                    }
                });
            } else {
                $.dialog({
                    fixed: true,
                    lock: true,
                    icon: 'error',
                    content: json.msg
                });
            }
        });
        return false;
    }
    function init() {
        $.getJSON("weixin.aspx", { "action": "get", "id": '<%=Request["id"] %>', "new": '<%=Request["new"] %>' }, function (json) {        
            $('#WeChatName').val(json.WeChatName);
            $('#AccountName').val(json.AccountName);
            $('#AccountFirst').val(json.AccountFirst);
            $('#WeChatToken').val(json.WeChatToken);
            $('#DefaultCmd').val(json.DefaultCmd);
            $('#Appid').val(json.Appid);
            $('#Secret').val(json.Secret);
        });
    }
    init();
</script>
</body>
</html>

