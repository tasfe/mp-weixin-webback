<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="articlesForm.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.articlesForm" %>
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
                    <td style="width:50px; text-align:right;">标题：</td>
                    <td style="width:520px;">
                        <input name="Title" class="mini-textbox" required="true" style="width:500px" />
                    </td>
                    <td style="width:67px; text-align:right;">所属栏目：</td>
                    <td style="width:97px;" >                  
                        <input id="ClassId" name="ClassId" class="mini-treeselect" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTree" multiSelect="false" 
                            textField="ClassName" valueField="Id" parentField="ParentId" checkRecursive="true" required="true"
                            showFolderCheckBox="false" style="width:98px;" />
                    </td>
                    <td style="width:43px; text-align:right;">置顶：</td>
                    <td style="width:80px;" >
                        <select name="OnTop" class="mini-radiobuttonlist" >
                            <option value="1">是</option>
                            <option value="0" selected="selected">否</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">功能：</td>
                    <td>
                        <input class="mini-checkbox" text="综合文本" value="true" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trContent").style.display = "";}else{document.getElementById("trContent").style.display = "none";}' /> 
                        <input class="mini-checkbox" text="图片" value="false" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trPic").style.display = "";}else{document.getElementById("trPic").style.display = "none";}' /> 
                        <input class="mini-checkbox" text="图册" value="false" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trUploadPic").style.display = "";}else{document.getElementById("trUploadPic").style.display = "none";}' /> 
                        <input class="mini-checkbox" text="外链、来源" value="false" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trUrl").style.display = "";}else{document.getElementById("trUrl").style.display = "none";}' /> 
                        <input id="cbkTemplet" style=" display:none;" class="mini-checkbox" text="模版" value="false" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trTemplet").style.display = "";}else{document.getElementById("trTemplet").style.display = "none";}' /> 
                        <input class="mini-checkbox" text="副标题" value="false" trueValue="true" falseValue="false" oncheckedchanged='if(this.value=="true"){document.getElementById("trSubtitle").style.display = "";}else{document.getElementById("trSubtitle").style.display = "none";}' /> 
                    </td>
                    <td style="text-align:right;">发表时间：</td>
                    <td>
                        <input name="AddTime" class="mini-datepicker" vtype="date:yyyy-MM-dd" style="width:98px;" />
                    </td>
                    <td style="text-align:right;">
                        <input name="ClickNum" class="mini-textbox" style="width:43px; direction:rtl;" value="0" />
                    </td>
                    <td>次点击
                    </td>
                </tr>
                <tr id="trSubtitle" style="">
                    <td style="text-align:right;">副标题：</td>
                    <td>
                        <input name="Subtitle" class="mini-textbox" style="width:500px" />
                    </td>
                    <td style="text-align:right;">关键字：</td>
                    <td colspan="3">
                        <input name="Tags" class="mini-textbox" style="width:168px" /><span style=" color:#AAAAAA;">用","分隔</span>
                    </td>
                </tr>
                <tr id="trPic" style="">
                    <td valign="top" style="text-align:right;">图片：</td>
                    <td valign="top" colspan="6">
                    <table>
                        <tr>
                            <td rowspan="2" style=" width:80px;" valign="top"><img id="imgThumbPic" style=" width:80px; height:80px; border:1px solid #AAAAAA;" src="<%=imgThumbPic %>" /></td>
                            <td valign="top">
                                <input id="fileupload1" class="mini-fileupload" style=" width:413px;" limitType="*.jpg;*.gif;*.png" limitSize="2MB" flashUrl="../../xcenter/static/fileupload/swfupload/swfupload.swf" uploadUrl="upload.aspx" onuploadsuccess="onUploadSuccess1" />
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
                <tr id="trUrl" style="">
                    <td style="text-align:right;">URL：</td>
                    <td>
                        <input name="Url" class="mini-textbox" style="width:500px" />
                    </td>
                    <td style="text-align:right;">来源：</td>
                    <td colspan="3">
                        <input name="Source" class="mini-textbox" style="width:218px" />
                    </td>
                </tr>
                <tr id="trContent" style="">
                    <td valign="top" style="text-align:right;">内容：</td>
                    <td colspan="5">
                        <script type="text/plain" id="editor" name="Content"><%=edContent %></script>
                    </td>
                </tr>
                <tr id="trUploadPic" style="">
                    <td valign="top" style="text-align:right;">图册：</td>
                    <td valign="top" id="tdUploadPic" colspan="5">
                        <iframe id="frmUploadPic" width="99%" scrolling="no" allowtransparency="true" frameborder="0" style=" border: 0px none; background-color:transparent; height:100%; " src="../../xcenter/static/fileupload/imageupload.html"></iframe>
                    </td>
                </tr>
                <tr id="trTemplet" style="">
                    <td style="text-align:right;">模板：</td>
                    <td>
                        <input id="Templet" name="Templet" class="mini-treeselect" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTemplate" multiSelect="false" 
                            textField="FileName" valueField="Path" parentField="DicPath" checkRecursive="true" 
                            showFolderCheckBox="false" style="width:500px;"  />
                    </td>
                    <td style="text-align:right;"></td>
                    <td>
                    </td>
                    <td style="text-align:right;"></td>
                    <td>
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
            var str = HTMLEnCode(document.getElementById('ueditor_textarea_Content').value);
            $.ajax({
                url: "<%= WebRoot %>admin/news/articles.aspx?handle=SaveOne",
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
                    url: "<%= WebRoot %>admin/news/articles.aspx?handle=GetOne&id=" + model.id,
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
        function initForm() {
            setTimeout(function () {
                if (pidselectid > 0) {
                    var pidselect = mini.get("ParentId");
                    pidselect.setValue(pidselectid);
                }
            }, 1000);
            document.getElementById('trSubtitle').style.display = "none";
            document.getElementById('trUploadPic').style.display = "none";
            document.getElementById('trUrl').style.display = "none";
            document.getElementById('trPic').style.display = "none";
            document.getElementById('trTemplet').style.display = "none";
            if (checkRight('Administrator')) {
                document.getElementById('cbkTemplet').style.display = "";
                //document.getElementById('trTemplet').style.display = "";
            }
        }
        initForm();

        //** iframe自动适应页面 **//
        //如果用户的浏览器不支持iframe是否将iframe隐藏 yes 表示隐藏，no表示不隐藏     
        var iframehide="yes"
        function dyniframesize() {
            var iframedom = document.getElementById('frmUploadPic');
            var tddom = document.getElementById('tdUploadPic');
            if (iframedom) {
                iframedom.style.display = "block"
                if (iframedom.contentDocument && iframedom.contentDocument.body.offsetHeight) //如果用户的浏览器是NetScape
                {
                    iframedom.height = iframedom.contentDocument.body.offsetHeight + 5;
                    tddom.height = iframedom.contentDocument.body.offsetHeight + 5;
                }
                else if (iframedom.Document && iframedom.Document.body.scrollHeight) //如果用户的浏览器是IE
                {
                    iframedom.height = iframedom.Document.body.scrollHeight + 5;
                    tddom.height = iframedom.Document.body.scrollHeight + 5;
                }
            }
            //根据设定的参数来处理不支持iframe的浏览器的显示问题     
            if (iframedom && iframehide == "no") {
                iframedom.style.display = "block"
            }
        }
        if (window.addEventListener)     
            window.addEventListener("load", dyniframesize, false)     
        else if (window.attachEvent)     
            window.attachEvent("onload", dyniframesize)     
        else    
            window.onload=dyniframesize     
    </script> 