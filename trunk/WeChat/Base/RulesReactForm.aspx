<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesReactForm.aspx.cs" Inherits="WeChat.Base.RulesReactForm" %><!DOCTYPE html>
<html lang="zh">
<head>
    <title>自动应答管理-Weback</title>
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
                <label class="control-label" title="填写后将直接以纯文本的形式回复此处内容">优先应答:<br/><span style=" font-size:10px; color:#999999;">(可不填)</span></label>
                <div class="controls">
                    <script id="ReContent" type="text/plain">&nbsp;</script>
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
            <div class="control-group">
                <label class="control-label">应答方式:</label>
                <div class="controls">
                    <select id="SendMode" style=" margin-top:3px;">
                        <option value="sendnew">使用位于内容列表顶部的信息应答</option>
                        <option value="sendrandom">随机使用列表中的一条信息应答</option>
                        <option value="sendgroup">同时发送列表中的多条图文类信息</option>
                    </select>
                </div>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn btn-primary" onclick="submitRule();">保存</button>
                <button type="button" class="btn" onclick="art.dialog.close();">返回规则列表</button>
            </div>
            </form>
        </div>
        <%if (!string.IsNullOrEmpty(Request["guid"]))
          { %> 
            <div id="contentForm" style=" display:none;">
                <div class="control-title"><h5>应答内容编辑</h5></div>
                <form class="form-horizontal" action="javascript:void(0);">
                <div class="control-group">
                    <label class="control-label">标题:</label>
                    <div class="controls">                    
                        <table>
                            <tr>
                                <td><input type="text" id="Title" style=" width:210px;" /></td>
                                <td>
                                    <select id="ContentStatus" style=" width:60px;">
                                        <option value="normal">启用</option>
                                        <option value="close">停用</option>
                                        <option value="test">测试</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="control-group" id="divPicUrl" style=" display:none;">
                    <label class="control-label">图片:</label>
                    <div class="controls">
                        <input type="text" id="PicUrl" style=" display:none;" />                        
                        <table>
                            <tr>
                                <td style="width:250px;"><img id="imgPicUrl" src="#" style=" width:120px; height:60px; display:none;" />&nbsp;</td>
                                <td><span id="swfUploadPic"></span></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="control-group" id="divThumbPicUrl" style=" display:none;">
                    <label class="control-label">
                        小图:</label>
                    <div class="controls">
                        <input type="text" id="ThumbPicUrl" style=" display:none;" />
                        <table>
                            <tr>
                                <td style="width:250px;"><img id="imgThumbPicUrl" src="#" style=" width:52px; height:52px; display:none;" />&nbsp;</td>
                                <td><span id="swfUploadThumbPic"></span></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="control-group" id="divMusicUrl" style=" display:none;">
                    <label class="control-label">
                        声音:</label>
                    <div class="controls">
                        <input type="text" id="MusicUrl" style=" display:none;" />
                        <span id="swfUploadMusic"></span>
                        <br />
                        <span id="mp3MusicUrl"></span>
                        <table style=" width:90%;">
                            <tr>
                                <td></td>
                                <td style="width:100px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="control-group" id="divTextContent"  style=" display:none;">
                    <label class="control-label">文本:</label>
                    <div class="controls">
                        <script id="ListContent" type="text/plain">&nbsp;</script>
                    </div>
                </div>
                <div class="control-group" id="divLinkUrl" style=" display:none;">
                    <label class="control-label">
                        链接:</label>
                    <div class="controls">
                        <input type="text" id="LinkUrl" />
                    </div>
                </div>
                <div class="form-actions">
                    <button type="submit" class="btn btn-primary" onclick="submitContent();">保存</button>
                    <button type="button" class="btn" onclick="hideContent();">取消</button>
                </div>
                </form>
            </div>
        <%}%>
            </td>
            <td id="divRight">
    <%if (!string.IsNullOrEmpty(Request["guid"]))
      { %>
        <div class="control-title">
            <span style=" float:right; padding:3px;">
                <a class="btnH5" href="javascript:addContent('text');">新文本</a>
                <a class="btnH5" href="javascript:addContent('pictext');">新图文</a>
                <a class="btnH5" href="javascript:addContent('music');">新音乐</a>
            </span>
            <h5>内容列表</h5>
        </div>        
        <table id="dataTable" class="wlntable">
            <tr>
                <td style="width:20px;" function="onTop">&nbsp;</td>
                <td style="width:198px;" filed="Title">标题</td>
                <td style="width:60px;" function="onContentType">类型</td>
                <td style="width:60px;" filed="PushCount">累计量</td>
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
<script src="../res/ueditor/editor_all.js" type="text/javascript"></script>
<script src="../res/ueditor/editor_config.js" type="text/javascript"></script>
<script src="../res/SWFUpload/swfupload.js" type="text/javascript"></script>
<script src="../res/artDialog/jquery.artDialog.js?skin=twitter" type="text/javascript"></script>
<script src="../res/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script src="../res/jquery.jmp3.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        try {
            $('#divLeft').height($(window).height());
            setTimeout(function(){
                $('#divLeft').height($(window).height());
            },1000);
        } catch (e) { }
    })
    //实例化编辑器
    var ueReContent = UE.getEditor('ReContent');
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
            if ($('#ReContent').val().length > 300) {
                $.dialog({
                    lock: true,
                    fixed: true,
                    content: '注意：<br/>您填写的自动回复内容太长可能会导致发送失败；<br/>微信自动回复的最大中文长度为300字符；<br/>您是否要返回修改？',
                    ok: function () {
                        GoonSave();
                    },
                    okVal: '不,继续保存',
                    cancelVal: '返回修改',
                    cancel: function () { }
                });
            } else {
                GoonSave();
            }
        } catch (e) { }
    }
    function GoonSave() {
        $.getJSON("rulesreactform.aspx", { "action": "set", "guid": "<%=_Guid %>"
        , "RuleName": $('#RuleName').val()
        , "AccountFirst": $('#AccountFirst').val()
        , "ReContent": htmlEncode(ueReContent.getContent())
        , "SendMode": $('#SendMode').val()
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
        $.getJSON("rulesreactform.aspx", { "action": "get", "guid": "<%=_Guid %>" }, function (json) {
            $('#RuleName').val(json.RuleName);
            $('#AccountFirst').val(json.AccountFirst);
            var cfg = eval('(' + json.RuleConfig + ')');
            ueReContent.setContent(htmlDecode(cfg.ReContent));
            $('#SendMode').val(cfg.SendMode);
        });
    }
    init();
    
    <%if (!string.IsNullOrEmpty(Request["guid"]))
      { %>
      
    function reLoadData() {
        wln.loadTable('.wlntable', 'rulesreactform.aspx', { 'action': 'getcontentlist','RuleGuid': '<%=Request["guid"] %>' });
    }
    wln.onTop=function(e) {
        return '<a title="使当前内容置顶" href="javascript:stickContent(\'' + e.Guid + '\');" style="color:gray;">↑</a>';
    }
    wln.onContentType=function(e){
        if(e.ContentType=='text'){
            return '<a title="纯文本内容">文本</a>';
        }else if(e.ContentType=='pictext'){
            return '<a title="图文内容">图文</a>';
        }else if(e.ContentType=='music'){
            return '<a title="音乐媒体文件">音乐</a>';
        }
        return '<a title="纯文本内容">文本</a>';
    }
    wln.onAction=function(e) {
        return '<a href="javascript:editContent(\'' + e.Guid + '\',\'' + e.Title + '\',\'' + e.ContentType + '\',\'' + e.PicUrl + '\',\'' + e.ThumbPicUrl + '\',\'' + e.MusicUrl + '\',\'' + e.LinkUrl + '\',\'' + e.TextContent + '\',\'' + e.ContentStatus + '\');">编辑</a>&nbsp;<a href="javascript:delContent(\'' + e.Guid + '\');">删除</a>';
    }
    reLoadData();

    wln.wlnUpload('swfUploadPic', wln.path + '../../upload.ashx?filetype=pic', uploadProgressUpload, uploadSuccessPic);
    wln.wlnUpload('swfUploadThumbPic', wln.path + '../../upload.ashx?filetype=pic', uploadProgressUpload, uploadSuccessThumbPic);
    wln.wlnUpload('swfUploadMusic', wln.path + '../../upload.ashx?filetype=audio', uploadProgressUpload, uploadSuccessMusic);
    function uploadProgressUpload() {
    }
    var contentguid;
    var ContentType;
    var ueListContent = UE.getEditor('ListContent');
    function addContent(type) {
        $('#baseFrom').hide();
        $('#contentForm').show();

        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        if (type == 'pictext') {
            $('#divPicUrl').show();
            $('#divThumbPicUrl').show();
            $('#divLinkUrl').show();
            $('#divTextContent').show();
        } else if (type == 'music') {
            $('#divMusicUrl').show();
            $('#divTextContent').show();
        } else {
            $('#divTextContent').show();
        }
        $('#imgPicUrl').attr('src', '#').hide();
        $('#imgThumbPicUrl').attr('src', '#').hide();

        $('#Title').val('');
        $('#PicUrl').val('');
        $('#ThumbPicUrl').val('');
        $('#MusicUrl').val('');
        $('#LinkUrl').val('');
        $('#ContentStatus').val('normal');
        contentguid = '';
        ContentType = type;
    }
    function hideContent() {
        $('#baseFrom').show();
        $('#contentForm').hide();
        
        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        $('#imgPicUrl').attr('src', '#').hide();
        $('#imgThumbPicUrl').attr('src', '#').hide();

        $('#Title').val('');
        $('#PicUrl').val('');
        $('#ThumbPicUrl').val('');
        $('#MusicUrl').val('');
        $('#LinkUrl').val('');
        ueListContent.setContent('');
        $('#ContentStatus').val('normal');
        contentguid = '';
        ContentType = 'pictext';
    }
    function uploadSuccessMusic(fileobj, serverData) {
        var stringArray = serverData.split("|");
        if (stringArray[0] == "1") {
            $("#MusicUrl").val(stringArray[1]);

            $("#mp3MusicUrl").html('<span class="mp3">' + stringArray[1] + '</span>');
            $(".mp3").jmp3();

            art.dialog.tips(stringArray[2], 1);
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function uploadSuccessPic(fileobj, serverData) {
        var stringArray = serverData.split("|");
        if (stringArray[0] == "1") {
            $("#PicUrl").val(stringArray[1]);
            $('#imgPicUrl').attr('src', '..' + stringArray[1]).show();
            if ($("#ThumbPicUrl").val() == '') {
                if (stringArray.length > 3) {
                    $("#ThumbPicUrl").val(stringArray[3]);
                    $('#imgThumbPicUrl').attr('src', '..' + stringArray[3]).show();
                } else {
                    $("#ThumbPicUrl").val(stringArray[1]);
                    $('#imgThumbPicUrl').attr('src', '..' + stringArray[1]).show();
                }
            }
            art.dialog.tips(stringArray[2], 1);
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function uploadSuccessThumbPic(fileobj, serverData) {
        var stringArray = serverData.split("|");
        if (stringArray[0] == "1") {
            $("#ThumbPicUrl").val(stringArray[3]);
            $('#imgThumbPicUrl').attr('src', '..' + stringArray[3]).show();
            art.dialog.tips(stringArray[2], 1);
        }
        else {
            art.dialog.tips(stringArray[2], 3);
        }
    }
    function ajaxFileUpload(argnane,filetype, obfile_id, input_id) {
        $.ajax({
            type: "POST",
            url: "/upload.ashx",
            data: { "upfile": $("#" + obfile_id).val(), "filetype": filetype },
            success: function (data, status) {
                //alert(data);
            },
            error: function (data, status, e) {
                alert("上传失败:" + e.toString());
            }
        });
    }
    function editContent(guid, title, type, pic, thumbpic, music, link, text, status) {
        $('#baseFrom').hide();
        $('#contentForm').show();

        $('#divLinkUrl').hide();
        $('#divPicUrl').hide();
        $('#divThumbPicUrl').hide();
        $('#divMusicUrl').hide();
        $('#divTextContent').hide();
        if (type == 'pictext') {
            $('#divPicUrl').show();
            $('#divThumbPicUrl').show();
            $('#divLinkUrl').show();
            $('#divTextContent').show();
            if (pic) {
                $('#imgPicUrl').attr('src', '..' + pic).show();
            } else {
                $('#imgPicUrl').attr('src', '#').hide();
            }
            if (thumbpic) {
                $('#imgThumbPicUrl').attr('src', '..' + thumbpic).show();
            } else if (pic) {
                $('#imgThumbPicUrl').attr('src', '..' + pic).show();
            } else {
                $('#imgThumbPicUrl').attr('src', '#').hide();
            }
        } else if (type == 'music') {
            $('#divMusicUrl').show();
            $('#divTextContent').show();
            if (music) {
                $("#mp3MusicUrl").html('<span class="mp3">' + music + '</span>');
                $(".mp3").jmp3();
            }
        } else {
            $('#divTextContent').show();
        }
        $('#Title').val(title);
        $('#PicUrl').val(pic);
        $('#ThumbPicUrl').val(thumbpic);
        $('#MusicUrl').val(music);
        $('#LinkUrl').val(link);
        ueListContent.setContent(text);
        $('#ContentStatus').val(status);
        ContentType = type;
        contentguid = guid;
    }


    function submitContent() {
        if (!$('#Title').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,内容标题未填写，请填写！'
            });
            $('#Title').focus();
            return;
        }
        if (ContentType == 'text' && !ueListContent.hasContents()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,内容未填写，请填写！'
            });
            return;
        }
        if (ContentType == 'pictext' && !$('#PicUrl').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,图片未上传，请上传！'
            });
            $('#PicUrl').focus();
            return;
        }
        if (ContentType == 'music' && !$('#MusicUrl').val()) {
            $.dialog({
                time: 2,
                fixed: true,
                icon: 'error',
                content: 'Sorry,媒体文件未上传，请上传！'
            });
            $('#MusicUrl').focus();
            return;
        }
        $.getJSON("rulesreactform.aspx", { "action": "setcontent", "RuleGuid": "<%=_Guid %>"
        , "guid": contentguid
        , "ContentType": ContentType
        , "Title": $('#Title').val()
        , "PicUrl": $('#PicUrl').val()
        , "ThumbPicUrl": $('#ThumbPicUrl').val()
        , "MusicUrl": $('#MusicUrl').val()
        , "LinkUrl": $('#LinkUrl').val()
        , "TextContent": htmlEncode(ueListContent.getContent())
        , "ContentStatus": $('#ContentStatus').val()
        }, function (json) {
            if (json.success) {
                hideContent();
                $.dialog({
                    lock: true,
                    fixed: true,
                    icon: 'succeed',
                    content: 'Success,操作保存成功！',
                    ok: function () {
                    }, close: function () {
                            reLoadData();
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
    function delContent(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '注意：<br/>数据删除后无法恢复，需谨慎操作；<br/>您确定要删除当前内容吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "delcontent", "Guid": guid }, function (json) {
                    if (json.success) {
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
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
            cancel: true //为true等价于function(){}
        });
    }
    function stickContent(guid) {
        $.dialog({
            lock: true,
            fixed: true,
            content: '您确定要置顶当前内容吗？',
            ok: function () {
                $.getJSON("rulesform.aspx", { "action": "stickcontent", "Guid": guid }, function (json) {
                    if (json.success) {
                        $.dialog({
                            time: 2,
                            lock: true,
                            fixed: true,
                            icon: 'succeed',
                            content: 'Success,操作保存成功！',
                            close: function () {
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
            okVal: '是,置顶',
            cancelVal: '取消',
            cancel: true //为true等价于function(){}
        });
    }
    <%} %>
    function getHasBrValue(text) {
        var return_str = "";
        if (text != "") {
            var arr = text.split('<br/>');
            for (i = 0; i < arr.length; i++) {
                return_str += arr[i] + '\r\n';
            }
            text = return_str;
            return_str=''
            var arr = text.split('<br>');
            for (i = 0; i < arr.length; i++) {
                return_str += arr[i] + '\r\n';
            }
        }
        return return_str;
    }
    function getTextareaValue(text) {
        var return_str = "";
        if (text != "") {
            var arr = text.split('\r\n');
            for (i = 0; i < arr.length; i++) {
                return_str += arr[i] + '<br/>';
            }
        }
        return return_str;
    }

    function htmlEncode(str) {
        var div = document.createElement("div");
        var text = document.createTextNode(str);
        div.appendChild(text);
        return div.innerHTML;
    }
    function htmlDecode(str) {
        var div = document.createElement("div");
        div.innerHTML = str;
        return div.innerHTML;
    }
</script>
</body>
</html>

