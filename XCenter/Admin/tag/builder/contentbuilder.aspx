<%------------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Web\admin\tags\builder\contentbuilder.aspx
//	运 行 库：2.0.50727.1882
//	代码功能：新闻内容标签生成器
//	最后修改：2012年3月7日 11:25:31
//------------------------------------------------------------------------------------%>
<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="contentbuilder.aspx.cs" Inherits="XCenterCMS.Web.admin.tags.builder.contentbuilder" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>内容标签</title>
    <link href="base.css" rel="stylesheet" type="text/css" />
    <%=GetTemplate("admin/_base")%>
    <script type="text/javascript">
        var labeltype = 'Content';
        var contentid = '0';
        var titlenumber = '';
        var contentnumber = '';
        var dserdefined = '';
        var dserdefinedpic = '';
        var hashtml = false;
        function returnValue() {
            var rvalue = '[XCY:UnLoop';
            var rvalueend = '[/XCY:UnLoop]';
            if (labeltype != '') {
                rvalue += ',XCY:LabelType=' + labeltype;
            }
            if (hashtml) {
                rvalue += ',XCY:HasHTML=True';
            }
            titlenumber = document.getElementById("TitleNumber").value;
            if (titlenumber != '') {
                if (checkIsNumber(titlenumber) || titlenumber == '0') {
                    alert('标题显示字数只能为正整数');
                    return false;
                } else {
                    rvalue += ',XCY:TitleNumer=' + titlenumber;
                }
            }
            contentnumber = document.getElementById("ContentNumber").value;
            if (contentnumber != '') {
                if (checkIsNumber(contentnumber) || contentnumber == '0') {
                    alert('内容显示字数只能为正整数');
                    return false;
                } else {
                    rvalue += ',XCY:ContentNumer=' + contentnumber;
                }
            }
            contentid = document.getElementById("ContentId").value;
            if (contentid != '') {
                rvalue += ',XCY:ContentId=' + contentid;
            } else {
                rvalue += ',XCY:ContentId=0';
            }
            dserdefinedpic = document.getElementById("PicDefined").value;
            //rvalue += ',XCY:PicTemplate=' + dserdefinedpic.replace(',','');
            rvalue += ']';
            dserdefined = document.getElementById("UserDefined").value;
            if (dserdefined != '') {
                rvalue += dserdefined;
            } else {
                alert('标签样式尚未定义');
                return false;
            }
            rvalue += rvalueend;
            if (dserdefinedpic.length > 0) {
                rvalue += '[PIC]' + dserdefinedpic + '[/PIC]';
            }
            return rvalue;
        }
        function selectListType(listtype) {
            labeltype = listtype;
        }
        function checkIsNumber(str) {
            var re = /^[0-9]*$$/;
            if (re.test(str) == false) {
                return true;
            }
            return false;
        }
        var start = 0;
        var end = 0;
        function selectFieldSet(field) {
            if (field != 'select') {
                var textBox = document.getElementById('UserDefined');
                var pre = textBox.value.substr(0, start);
                var post = textBox.value.substr(end);
                textBox.value = pre + '{#' + field + '}' + post;
                var textLength = document.getElementById("UserDefined").value.length;
                start = textLength;
                end = textLength;
            }
        }
        function selectPicFieldSet(field) {
            if (field != 'select') {
                var textBox = document.getElementById('PicDefined');
                var pre = textBox.value.substr(0, start);
                var post = textBox.value.substr(end);
                textBox.value = pre + '{#' + field + '}' + post;
                var textLength = document.getElementById("PicDefined").value.length;
                start = textLength;
                end = textLength;
            }
        }
        function selectIsHTML(field) {
            if (field != 'Yes') {
                hashtml = false;
            } else {
                hashtml = true;
            }
        }
        function savePos(textBox) {
            //如果是Firefox(1.5)的话，方法很简单    
            if (typeof (textBox.selectionStart) == "number") {
                start = textBox.selectionStart;
                end = textBox.selectionEnd;
            }
            //下面是IE(6.0)的方法，麻烦得很，还要计算上'\n'    
            else if (document.selection) {
                var range = document.selection.createRange();
                if (range.parentElement().id == textBox.id) {
                    // create a selection of the whole textarea    
                    var range_all = document.body.createTextRange();
                    range_all.moveToElementText(textBox);
                    //两个range，一个是已经选择的text(range)，一个是整个textarea(range_all)    
                    //range_all.compareEndPoints()比较两个端点，如果range_all比range更往左(further to the left)，则             
                    //返回小于0的值，则range_all往右移一点，直到两个range的start相同。    
                    // calculate selection start point by moving beginning of range_all to beginning of range    
                    for (start = 0; range_all.compareEndPoints("StartToStart", range) < 0; start++)
                        range_all.moveStart('character', 1);
                    // get number of line breaks from textarea start to selection start and add them to start    
                    // 计算一下\n    
                    for (var i = 0; i <= start; i++) {
                        if (textBox.value.charAt(i) == '\n')
                            start++;
                    }
                    // create a selection of the whole textarea    
                    var range_all = document.body.createTextRange();
                    range_all.moveToElementText(textBox);
                    // calculate selection end point by moving beginning of range_all to end of range    
                    for (end = 0; range_all.compareEndPoints('StartToEnd', range) < 0; end++)
                        range_all.moveStart('character', 1);
                    // get number of line breaks from textarea start to selection end and add them to end    
                    for (var i = 0; i <= end; i++) {
                        if (textBox.value.charAt(i) == '\n')
                            end++;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" width="90">内容类型：</td>
                <td align="left" valign="middle">
				    <select name="ListType" id="ListType" class="form" onchange="javascript:selectListType(this.value);" style="width:100px;">
	                    <option value="Content">普通新闻</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" width="90">新闻ID：</td>
                <td align="left" valign="middle">                    
				    <input name="ContentId" type="text" value="" id="ContentId" class="table_input" />
                </td>
            </tr>
            <tr>
                <td align="right" width="90">标题显示字数：</td>
                <td align="left" valign="middle">                    
				    <input name="TitleNumber" type="text" value="" id="TitleNumber" class="table_input" style="width:40px;" />
                    &nbsp;&nbsp;内容显示字数：
				    <input name="ContentNumber" type="text" value="" id="ContentNumber" class="table_input" style="width:40px;" />
                    &nbsp;&nbsp;
				    <select name="IsHTML" id="IsHTML" class="form" onchange="javascript:selectIsHTML(this.value);" style="min-width:100px;">
	                    <option value="IsHTML">是否包含HTML标记</option>
	                    <option value="Yes">是</option>
	                    <option value="No">否</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle">定义样式：</td>
                <td align="left" valign="middle" style=" line-height:1em;">
                    <div>
				        <select class="form" onchange="javascript:selectFieldSet(this.value);" style="width:140px;">
	                        <option value="select">选择基本参数</option>
	                        <option value="NewsID">文章ID</option>
	                        <option value="NewsTitle">文章标题</option>
	                        <option value="nNewsTitle">文章标题全称</option>
	                        <option value="NewsSubTitle">文章副标题</option>
	                        <option value="Url">外链Url</option>
	                        <option value="PicList">图片集</option>
	                        <option value="Property">文章属性</option>
	                        <option value="Content">文章内容</option>
	                        <option value="ClassName">所属栏目名称</option>
	                        <option value="ClassId">所属栏目ID</option>
	                        <option value="UserName">所属用户昵称</option>
	                        <option value="UserId">所属用户ID</option>
	                        <option value="Pic">图片Url</option>
	                        <option value="ThumbPic">缩略图Url</option>
	                        <option value="Files">附件Url</option>
	                        <option value="Tags">文章标签</option>
	                        <option value="ClickNum">点击数</option>
	                        <option value="Date">添加时间(完整)</option>
	                        <option value="DateLong">添加时间(yyyy年mm月dd日)</option>
	                        <option value="DateShort">添加时间(yyyy-mm-dd)</option>
	                        <option value="TimeLong">添加时间(时:分:秒)</option>
	                        <option value="TimeShort">添加时间(hh-mm-ss)</option>
	                        <option value="Dateyyyy">添加时间-年(yyyy)</option>
	                        <option value="DateMM">添加时间-月(MM)</option>
	                        <option value="Datedd">添加时间-日(dd)</option>
	                        <option value="DateEn">添加时间(Monday, November 12, 2012)</option>
	                        <option value="DateEnShort">添加时间(Apr 07 2012)</option>
                        </select>
                    </div>                 
				    <textarea rows="4" cols="1" style=" width:99%"  name="UserDefined" id="UserDefined" onKeydown="savePos(this)" onKeyup="savePos(this)" onmousedown="savePos(this)" onmouseup="savePos(this)" onfocus="savePos(this)" ></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle">图片样式：</td>
                <td align="left" valign="middle" style=" line-height:1em;">
                    <div>
				        <select class="form" onchange="javascript:selectPicFieldSet(this.value);" style="width:140px;">
	                        <option value="select">选择图片参数</option>
	                        <option value="Pic_FileId">附件ID</option>
	                        <option value="Pic_FileName">文件名</option>
	                        <option value="Pic_FileTitle">标题</option>
	                        <option value="Pic_Description">描述文本</option>
	                        <option value="Pic_SavePath">文件Url</option>
	                        <option value="Pic_MiniPath">压缩后文件Url</option>
                        </select>
                    </div>                 
				    <textarea rows="4" cols="1" style=" width:99%"  name="PicDefined" id="PicDefined" onKeydown="savePos(this)" onKeyup="savePos(this)" onmousedown="savePos(this)" onmouseup="savePos(this)" onfocus="savePos(this)" ></textarea>
                </td>
            </tr>
        </table>
    </form>
    <div style="text-align:center;padding:10px;">
        <a class="mini-button" onclick="onOk" style="width:60px;margin-right:20px;">确定</a>
        <a class="mini-button" onclick="onCancel" style="width:60px;">关闭</a>
    </div> 
</body>
</html>
    <script type="text/javascript">
        mini.parse();
        var form = new mini.Form("form1");
        function onOk(e) {
            form.validate();
            if (form.isValid() == false) return;
            if (window.CloseOwnerWindow)
                return window.CloseOwnerWindow(returnValue());
            else window.close();
        }
        function onCancel(e) {
            if (window.CloseOwnerWindow)
                return window.CloseOwnerWindow('');
            else window.close();
        }
    </script>

