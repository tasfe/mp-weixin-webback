<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsclassForm.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.newsclassForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=GetTemplate("admin/_base")%>
    <script src="../../xcenter/static/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="../../xcenter/static/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="../../xcenter/static/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" action="#">
        <input name="Id" class="mini-hidden" />
        <fieldset style="border:solid 1px #ffffff;padding:3px;">
            <table style="table-layout:fixed; width:100%;">
                <tr>
                    <td style="width:70px;">栏目名称：</td>
                    <td style="width:140px;">
                        <input name="ClassName" class="mini-textbox" required="true" />
                    </td>
                    <td style="width:50px;">父栏目：</td>
                    <td style="width:100px;" >
                        <input id="ParentId" name="ParentId" class="mini-treeselect" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTree" multiSelect="false" 
                            textField="ClassName" valueField="Id" parentField="ParentId" checkRecursive="true" 
                            showFolderCheckBox="false" style="width:90px;"  />
                    </td>
                    <td style="width:40px;">类型：</td>
                    <td style="width:100px;" >
                        <select name="Type" class="mini-combobox" required="true" style="width:90px;" >
                            <option value="list">文章列表</option>
                            <option value="page">HTML单页</option>
                            <option value="photo">图片相册</option>
                            <option value="url">连接类型</option>
                        </select>
                    </td>
                    <td style="width:40px;">排序：</td>
                    <td style="width:40px;" >
                        <input name="Sort" class="mini-textbox" style="width:30px;" />
                    </td>
                    <td style="width:90px;">导航栏显示：</td>
                    <td style="width:90px;" >
                        <select name="NavShow" class="mini-radiobuttonlist" >
                            <option value="false" selected="selected">否</option>
                            <option value="true">是</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td valign="top">栏目内容：</td>
                    <td colspan="9">
                        <script type="text/plain" id="editor" name="Content"><%=edContent %></script>
                    </td>
                </tr>
                <tr id="trPath" style="">
                    <td style="text-align:right;">访问路径：</td>
                    <td colspan="9">
                        <input name="Path" class="mini-textbox" style="width:500px" />&nbsp;<span style=" color:Gray;">如:/product.aspx</span>
                    </td>
                </tr>
                <tr id="trTemplet" style="">
                    <td style="text-align:right;">栏目模板：</td>
                    <td colspan="9">
                        <input id="Templet" name="Templet" class="mini-treeselect" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTemplate" multiSelect="false" 
                            textField="FileName" valueField="Path" parentField="DicPath" checkRecursive="true" 
                            showFolderCheckBox="false" style="width:500px;"  />
                    </td>
                </tr>
                <tr id="trContentTemplet" style="">
                    <td style="text-align:right;">内容模板：</td>
                    <td colspan="9">
                        <input id="ContentTemplet" name="ContentTemplet" class="mini-treeselect" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTemplate" multiSelect="false" 
                            textField="FileName" valueField="Path" parentField="DicPath" checkRecursive="true" 
                            showFolderCheckBox="false" style="width:500px;"  />
                    </td>
                </tr>
            </table>
        </fieldset>
        <div style="text-align:center;padding:10px;">
            <a class="mini-button" onclick="onOk" style="width:60px;margin-right:20px;">确定</a>
            <a class="mini-button" onclick="onCancel" style="width:60px;">关闭</a>
        </div>        
    </form>
</body>
</html>
    <script type="text/javascript">
        var editor;
        mini.parse();
        var form = new mini.Form("form1");
        function SaveData() {
            form.validate();
            if (form.isValid() == false) return;
            var str = HTMLEnCode(document.getElementById('ueditor_textarea_Content').value);
            $.ajax({
                url: "<%= WebRoot %>admin/news/newsclass.aspx?handle=SaveOne",
                data: { data: mini.encode(form.getData()), content: str },
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
        function SetData(model) {
            if (model.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                model = mini.clone(model);
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    url: "<%= WebRoot %>admin/news/newsclass.aspx?handle=GetOne&id=" + model.id,
                    cache: false,
                    success: function (data) {          //绑定表单                       
                        form.setData(data);             //设置多个控件数据
                        form.setChanged(false);
                    }
                });
            }
            $(document).ready(function () {
                editor = new baidu.editor.ui.Editor({
                    toolbars: [['FullScreen', 'Source', 'Undo', 'Redo', 'Bold', 'Italic', 'Underline', 'ForeColor', 'BackColor', 'FontFamily', 'FontSize', '|', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyJustify', 'Link', '|', 'InsertImage', 'Emotion', 'InsertVideo', 'Attachment', 'InsertTable', 'DeleteTable', 'InsertParagraphBeforeTable', 'InsertRow', 'DeleteRow', 'InsertCol', 'DeleteCol', 'MergeCells', 'MergeRight', 'MergeDown', 'SplittoCells', 'SplittoRows', 'SplittoCols', '|', 'StrikeThrough', 'BlockQuote', 'PastePlain', 'Map', 'GMap', 'Preview']],
                    minFrameHeight: 360, //最小高度
                    autoHeightEnabled: false, //是否自动长高
                    wordCount: false    //关闭字数统计
                });
                editor.render('editor');
            });
        }
        function onOk(e) {
            try {
                editor.sync();
                //var a = document.getElementById('ueditor_textarea_Content').value;
            } catch (e) { }
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
        function initForm() {
            document.getElementById('trPath').style.display = "none";
            document.getElementById('trTemplet').style.display = "none";
            document.getElementById('trContentTemplet').style.display = "none";
            if (checkRight('Administrator')) {
                document.getElementById('trPath').style.display = "";
                document.getElementById('trTemplet').style.display = "";
                document.getElementById('trContentTemplet').style.display = "";
            }
        }
        initForm();
    </script>