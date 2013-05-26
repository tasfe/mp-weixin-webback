<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwdset.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.pwdset" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleChangePassword")%></title><%=GetTemplate("admin/_tags")%>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleChangePassword")%></h1>
                        <div style="width:100%; min-height:460px;">
                            <div id="form1">
                                <input name="id" class="mini-hidden" />
                                <table>
                                    <tr>
                                        <td>
                                            <label for="oldPwd$text">旧密码：</label>
                                        </td>
                                        <td>
                                            <input id="oldPwd"  name="oldPwd" class="mini-password" required="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="newPwd$text">新密码：</label>
                                        </td>
                                        <td>
                                            <input id="newPwd" name="newPwd" class="mini-password" required="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="rePwd$text">确&nbsp;&nbsp;认：</label>
                                        </td>
                                        <td>
                                            <input id="rePwd" name="rePwd" class="mini-password" required="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <input value="<%=UIConfig("Submit")%>" type="button" onclick="submitForm()" style=" padding:2px 10px; cursor:pointer;" />
                                            <input value="<%=UIConfig("Cancel")%>" type="button" onclick="resetForm()" style=" padding:2px 10px; cursor:pointer;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        </td>                   
                    </tr>
                </table>
            </div>
        </div>		
        <%=GetTemplate("admin/_footer")%>
    </body>
</html>
<script type="text/javascript">
    mini.parse();
    function submitForm() {

        //提交表单数据
        var form = new mini.Form("#form1");
        form.validate();
        if (form.isValid() == false) return;
        $.ajax({
            url: "<%= WebRoot %>admin/sys/pwdset.aspx?handle=ChangePwd",
            type: "post",
            data: { data: mini.encode(form.getData()) },
            dataType: 'json',
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
    function resetForm() {
        var form = new mini.Form("#form1");
        form.reset();
    }
</script>