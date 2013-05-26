<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accountForm.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.accountForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=GetTemplate("admin/_base")%>
</head>
<body>
    <form id="form1" action="#">
        <input name="Id" class="mini-hidden" />
        <fieldset style="border:solid 1px #aaa;padding:3px;">
            <legend >基本信息</legend>
            <div style="padding:5px;">
            <table style="table-layout:fixed;">
                <tr>
                    <td style="width:70px;">用户名：</td>
                    <td style="width:150px;">
                        <input name="LoginName" class="mini-textbox" required="true" />
                    </td>
                    <td style="width:70px;">密码：</td>
                    <td style="width:150px;" >
                        <input name="LoginPassword" class="mini-textbox" required="true" />
                    </td>
                </tr>
                <tr>
                    <td>姓名：</td>
                    <td>
                        <input name="Nickname" class="mini-textbox" required="true" onvalidation="onChineseValidation" />
                    </td>
                    <td>手机号码：</td>
                    <td>
                        <input name="Mobile" class="mini-textbox" vtype="int;rangeLength:11,11;" />
                    </td>
                </tr>
                <tr>
                    <td>QQ号码：</td>
                    <td>
                        <input name="QQ" class="mini-textbox" vtype="int" />
                    </td>
                    <td>Email：</td>
                    <td>
                        <input name="Email" class="mini-textbox" vtype="email;rangeLength:5,20;" />
                    </td>
                </tr>
                <tr>
                    <td >纪念日：</td>
                    <td >
                        <input name="Birthday" class="mini-datepicker" vtype="date:yyyy-MM-dd" />
                    </td>
                    <td>权限：</td>
                    <td>
                        <select name="IsAdmin" class="mini-radiobuttonlist" >
                            <option value="false" selected="selected">普通用户</option>
                            <option value="true">管理员</option>
                        </select>
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
            $.ajax({
                url: "<%= WebRoot %>admin/sys/account.aspx?handle=SaveOne",
                data: {data:mini.encode(form.getData())},
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
                    url: "<%= WebRoot %>admin/sys/account.aspx?handle=GetOne&id=" + model.id,
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
        ////////////////////////////////////////
        function onEnglishValidation(e) {
            if (e.isValid) {
                if (isEnglish(e.value) == false) {
                    e.errorText = "必须输入英文";
                    e.isValid = false;
                }
            }
        }
        function onEnglishAndNumberValidation(e) {
            if (e.isValid) {
                if (isEnglishAndNumber(e.value) == false) {
                    e.errorText = "必须输入英文+数字";
                    e.isValid = false;
                }
            }
        }
        function onChineseValidation(e) {
            if (e.isValid) {
                if (isChinese(e.value) == false) {
                    e.errorText = "必须输入中文";
                    e.isValid = false;
                }
            }
        }
        function onIDCardsValidation(e) {
            if (e.isValid) {
                var pattern = /\d*/;
                if (e.value.length < 15 || e.value.length > 18 || pattern.test(e.value) == false) {
                    e.errorText = "必须输入15~18位数字";
                    e.isValid = false;
                }
            }
        }
        ////////////////////////////////////
        /* 是否英文 */
        function isEnglish(v) {
            var re = new RegExp("^[a-zA-Z\_]+$");
            if (re.test(v)) return true;
            return false;
        }
        /* 是否英文+数字 */
        function isEnglishAndNumber(v) {
            var re = new RegExp("^[0-9a-zA-Z\_]+$");
            if (re.test(v)) return true;
            return false;
        }
        /* 是否汉字 */
        function isChinese(v) {
            var re = new RegExp("^[\u4e00-\u9fa5]+$");
            if (re.test(v)) return true;
            return false;
        }
    </script>
