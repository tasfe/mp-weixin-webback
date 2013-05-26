<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="friendlinkForm.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.friendlinkForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=GetTemplate("admin/_base")%>
    <script src="../../xcenter/static/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="../../xcenter/static/ueditor/editor_all_min.js" type="text/javascript"></script>
    <script src="../../xcenter/static/fileupload/swfupload/swfupload.js" type="text/javascript"></script>
    <link href="../../xcenter/static/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" action="#">
        <input name="Id" class="mini-hidden" />
        <fieldset style="border:solid 1px #ffffff;padding:3px;">
            <table style="table-layout:fixed; width:100%;">
                <tr>
                    <td style="width:38px; text-align:right;">名称：</td>
                    <td style="width:370px;">
                        <input name="FriendLinkTitle" class="mini-textbox" required="true" style="width:360px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">分类：</td>
                    <td style=" margin:0px; padding:0px; border:none;">
                    <table style=" margin:0px; padding:0px; border:none;">
                        <tr>                        
                            <td style=" margin:0px; padding:0px; border:none;">
                                <select name="FriendLinkType" class="mini-combobox" style="width:90px;" >
                                    <%=typelist%>
                                </select>
                            </td>
                            <td>
                                <select name="OnTop" class="mini-radiobuttonlist" style="width:120px;" >
                                    <option value="0" selected="selected">普通</option>
                                    <option value="1">置顶</option>
                                </select>
                            </td>
                            <td>
                                <select name="OnStop" class="mini-radiobuttonlist" style="width:120px;" >
                                    <option value="0" selected="selected">正常</option>
                                    <option value="1">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr id="trUrl" style="">
                    <td style="text-align:right;">URL：</td>
                    <td style=" margin:0px; padding:0px; border:none;">
                    <table style=" margin:0px; padding:0px; border:none;">
                        <tr>                        
                            <td  style=" margin:0px; padding:0px; border:none;width:278px;text-align:left;"><input name="Url" class="mini-textbox" style="width:270px" /></td>
                            <td style="width:38px;text-align:right;">排序：</td>
                            <td style="width:38px;text-align:left;"><input name="Sort" class="mini-textbox" style="width:30px;" value="0" /></td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="text-align:right;">图片：</td>
                    <td valign="top">
                    <table>
                        <tr>
                            <td rowspan="2" style=" width:128px;" valign="top"><img id="imgThumbPic" style=" width:88px; height:31px; border:1px solid #AAAAAA;" src="<%=imgThumbPic %>" /></td>
                            <td valign="top">
                                <input id="fileupload1" class="mini-fileupload" style=" width:195px;" limitType="*.jpg;*.gif;*.png" limitSize="2MB" flashUrl="../../xcenter/static/fileupload/swfupload/swfupload.swf" uploadUrl="../news/upload.aspx?width=88&height=31" onuploadsuccess="onUploadSuccess1" />
                                <input id="hiPic" name="Pic" class="mini-hidden" />
                                <input id="hiThumbPic" name="ThumbPic" class="mini-hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <input type="button" value="上传图片" onclick="startUpload('fileupload1')"/>
                                <input type="button" value="清除图片" onclick="clearImg()"/>
                                <input type="button" value="重设" onclick="resetImg()"/>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr id="trContent" style="">
                    <td valign="top" style="text-align:right;">描述：</td>
                    <td>
                        <input name="Description" class="mini-textarea" style="width:360px; height:60px;"  />
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="text-align:center;padding:10px;">
            <a class="mini-button" onclick="onOk" style="width:60px;margin-right:20px;">确定</a>
            <a class="mini-button" onclick="resetForm" style="width:60px;margin-right:20px;">重置</a>
            <a class="mini-button" onclick="onCancel" style="width:60px;">关闭</a>
        </div>        
    </form>
</body>
</html>
    <script type="text/javascript">
        var editor;
        mini.parse();
        var form = new mini.Form("form1");
        var pidselectid = 0;
        function SaveData() {
            form.validate();
            if (form.isValid() == false) return;
            $.ajax({
                url: "<%= WebRoot %>admin/website/friendlink.aspx?handle=SaveOne",
                data: { data: mini.encode(form.getData()) },
                type: "POST",
                dataType: 'json',
                cache: false,
                success: function (data) {
                    if (data.success) {
                        MyalertMsg(data.msg, 'success', function () { CloseWindow("save"); });
                    } else {
                        MyalertMsg(data.msg, 'error');
                    }
                }
            });
        }
        //标准方法接口定义
        function SetClassId(selectid) {
            pidselectid = selectid;
        }
        //标准方法接口定义
        function SetData(model) {
            if (model.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                model = mini.clone(model);
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    url: "<%= WebRoot %>admin/website/friendlink.aspx?handle=GetOne&id=" + model.id,
                    cache: false,
                    success: function (data) {          //绑定表单                       
                        form.setData(data);             //设置多个控件数据
                        form.setChanged(false);
                    }
                });
            }
        }
        function onOk(e) {
            SaveData();
        }
        function CloseWindow(action) {
            if (action == "close" && form.isChanged()) {
                if (confirm("数据被修改了，是否先保存？")) {
                    return false;
                }
            }
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }
        function onCancel(e) {
            CloseWindow("cancel");
        }
        function resetForm() {
            var form = new mini.Form("form1");
            form.reset();
        }
        function clearForm() {
            var form = new mini.Form("form1");
            form.clear();
        }
        function onUploadSuccess1(e) {
            var str2 = e.serverData.split(';');
            if (str2.length == 2) {
                mini.get("hiPic").setValue(str2[0]);
                mini.get("hiThumbPic").setValue(str2[1]);
                document.getElementById('imgThumbPic').src = str2[1];
            }
        }
        function startUpload(id) {
            var fileupload = mini.get(id);
            fileupload.startUpload();
        }
        function clearImg(e) {
            mini.get("hiPic").setValue('');
            mini.get("hiThumbPic").setValue('ClearImg');
            document.getElementById('imgThumbPic').src = '../../xcenter/static/assets/defaultPic.png';
        }
        function resetImg(e) {
            mini.get("hiPic").setValue('');
            mini.get("hiThumbPic").setValue('');
            document.getElementById('imgThumbPic').src = '<%=imgThumbPic %>';
        }
    </script> 