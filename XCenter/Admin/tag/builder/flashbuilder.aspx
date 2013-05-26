<%------------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Web\admin\tags\builder\newsbuilder.aspx
//	运 行 库：2.0.50727.1882
//	代码功能：新闻列表标签生成器
//	最后修改：2012年3月7日 11:25:31
//------------------------------------------------------------------------------------%>
<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="flashbuilder.aspx.cs" Inherits="XCenterCMS.Web.admin.tags.builder.flashbuilder" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>轮换图片标签</title>
    <link href="base.css" rel="stylesheet" type="text/css" />
    <%=GetTemplate("admin/_base")%>
    <script type="text/javascript">
        var labeltype = 'FlashPic';
        var flashtype = '';
        var number = '';
        var titlenumber = '';
        var contentnumber = '';
        var dserdefined = '';
        var hashtml = false;
        function returnValue() {
            var rvalue = '[XCY:Loop';
            var rvalueend = '[/XCY:Loop]';
            number = document.getElementById("Number").value;
            if (number != '') {
                if (checkIsNumber(number)) {
                    MyalertMsg('循环次数只能为正整数');
                    return false;
                } else {
                    if (number == '1' || number == '0') {
                        rvalue = '[XCY:UnLoop';
                        rvalueend = '[/XCY:UnLoop]';
                    } else {
                        rvalue += ',XCY:Number=' + number;
                    }
                }
            } else {
                MyalertMsg('循环次数不能为空值');
                return false;
            }
            if (labeltype != '') {
                rvalue += ',XCY:LabelType=' + labeltype;
            }
            if (flashtype != '') {
                rvalue += ',XCY:FlashType=' + flashtype;
            }
            if (hashtml) {
                rvalue += ',XCY:HasHTML=True';
            }
            titlenumber = document.getElementById("TitleNumber").value;
            if (titlenumber != '') {
                if (checkIsNumber(titlenumber) || titlenumber == '0') {
                    MyalertMsg('标题显示字数只能为正整数');
                    return false;
                } else {
                    rvalue += ',XCY:TitleNumer=' + titlenumber;
                }
            }
            contentnumber = document.getElementById("ContentNumber").value;
            if (contentnumber != '') {
                if (checkIsNumber(contentnumber) || contentnumber == '0') {
                    MyalertMsg('描述显示字数只能为正整数');
                    return false;
                } else {
                    rvalue += ',XCY:ContentNumer=' + contentnumber;
                }
            }
            rvalue += ']';
            dserdefined = document.getElementById("UserDefined").value;
            if (dserdefined != '') {
                rvalue += dserdefined;
            } else {
                alert('标签样式尚未定义');
                return false;
            }
            rvalue += rvalueend;
            return rvalue;
        }
        function selectFlashType(thistype) {
            flashtype = thistype;
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
                <td align="right" width="90">列表类型：</td>
                <td align="left" valign="middle">
				    <select name="FlashType" id="FlashType" class="form" onchange="javascript:selectFlashType(this.value);" style="width:100px;">
                        <%=typelist%>
                    </select>
                    &nbsp;&nbsp;图片数量：
				    <input name="Number" type="text" value="10" id="Number" class="table_input" style="width:40px;" />
                </td>
            </tr>
            <tr>
                <td align="right" width="90">标题显示字数：</td>
                <td align="left" valign="middle">                    
				    <input name="TitleNumber" type="text" value="" id="TitleNumber" class="table_input" style="width:40px;" />
                    &nbsp;&nbsp;描述显示字数：
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
	                        <option value="FlashTitle">图片标题</option>
	                        <option value="nFlashTitle">图片标题全称</option>
	                        <option value="Description">描述内容</option>
	                        <option value="Url">链接地址</option>
	                        <option value="Pic">图片Url</option>
	                        <option value="ThumbPic">缩略图Url</option>
	                        <option value="FlashType">图片分类</option>
	                        <option value="Sort">顺序</option>
	                        <option value="Id">图片ID</option>
                        </select>
                    </div>                 
				    <textarea rows="4" cols="1" style=" width:99%"  name="UserDefined" id="UserDefined" onKeydown="savePos(this)" onKeyup="savePos(this)" onmousedown="savePos(this)" onmouseup="savePos(this)" onfocus="savePos(this)" ></textarea>
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
