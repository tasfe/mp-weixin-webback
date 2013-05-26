<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tagsForm.aspx.cs" Inherits="XCenterCMS.Web.Admin.tag.tagsForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=GetTemplate("admin/_base")%>
    <style type="text/css">
        td li{ display:inline; color:Blue; text-decoration:none;} 
    </style>
</head>
<body>
    <form id="form1" action="#">
        <input name="Id" class="mini-hidden" />
        <fieldset style="border:solid 1px #aaa;padding:3px;">
            <legend >基本信息</legend>
            <div style="padding:5px;">
            <table style="table-layout:fixed;">
                <tr>
                    <td style="width:70px;">标签名：</td>
                    <td style="width:370px;">
                        <input name="TagName" class="mini-textbox" required="true" />                      
                        <li><a href="javascript:NewsBuilder();">新闻列表</a></li>
                        <li><a href="javascript:ContentBuilder();">新闻类</a></li>
                        <li><a href="javascript:ClassBuilder();">栏目类</a></li>
                        <li><a href="javascript:FlashBuilder();">轮换图</a></li>
                        <li><a href="javascript:FriendLinkBuilder();">友情链接</a></li>
                    </td>
                </tr>
                <tr>
                    <td>标签内容：</td>
                    <td>
                        <input id="txtContent" name="Content" class="mini-textarea" required="true" style="width:350px; height:120px;"  />
                    </td>
                </tr>
                <tr>
                    <td>备注：</td>
                    <td>
                        <input name="Comments" class="mini-textarea" style="width:350px;"  />
                    </td>
                </tr>
            </table>
            </div>
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
        mini.parse();
        var form = new mini.Form("form1");
        function SaveData() {
            form.validate();
            if (form.isValid() == false) return;
            var s = form.getData();
            $.ajax({
                url: "<%= WebRoot %>admin/tag/tags.aspx?handle=SaveOne",
                data: {data:HTMLEnCode(mini.encode(s))},
                type: "POST",
                dataType: 'json',
                cache: false,
                success: function (data) {
                    if (data.success) {
                        MyalertMsg(data.msg, 'success');
                        CloseWindow("save");
                    } else {
                        MyalertMsg(data.msg, 'error');
                    }
                }
            });
        }
        ////////////////////
        //标准方法接口定义
        function SetData(model) {
            if (model.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                model = mini.clone(model);
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    url: "<%= WebRoot %>admin/tag/tags.aspx?handle=GetOne&id=" + model.id,
                    cache: false,
                    success: function (data) {
                        //绑定表单
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
        function ClassBuilder() {
            mini.open({
                url: bootPATH + "../../../admin/tag/builder/classbuilder.aspx",
                title: "标签管理", width: 640, height: 340,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (tags) {
                    if (tags.length > 0) {
                        var obj = mini.get("txtContent");
                        obj.setValue(tags);
                    }
                }
            });
        }
        function NewsBuilder() {
            mini.open({
                url: bootPATH + "../../../admin/tag/builder/newsbuilder.aspx",
                title: "标签管理", width: 640, height: 360,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (tags) {
                    if (tags.length > 0) {
                        var obj = mini.get("txtContent");
                        obj.setValue(tags);
                    }
                }
            });
        }
        function ContentBuilder() {
            mini.open({
                url: bootPATH + "../../../admin/tag/builder/contentbuilder.aspx",
                title: "标签管理", width: 640, height: 360,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (tags) {
                    if (tags.length > 0) {
                        var obj = mini.get("txtContent");
                        obj.setValue(tags);
                    }
                }
            });
        }
        function FlashBuilder() {
            mini.open({
                url: bootPATH + "../../../admin/tag/builder/flashbuilder.aspx",
                title: "标签管理", width: 640, height: 340,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (tags) {
                    if (tags.length > 0) {
                        var obj = mini.get("txtContent");
                        obj.setValue(tags);
                    }
                }
            });
        }
        function FriendLinkBuilder() {
            mini.open({
                url: bootPATH + "../../../admin/tag/builder/friendlinkbuilder.aspx",
                title: "标签管理", width: 640, height: 340,
                onload: function () {
                    var iframe = this.getIFrameEl();
                },
                ondestroy: function (tags) {
                    if (tags.length > 0) {
                        var obj = mini.get("txtContent");
                        obj.setValue(tags);
                    }
                }
            });
        }
    </script>
