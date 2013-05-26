<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XCenterCMS.Web.Admin.Default" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitle")%></title><%=GetTemplate("admin/_tags")%>
        <style>
            legend{ font-size:12px; font-weight:bolder;}
            table.tableinfo tr td{ font-size:12px; text-align:right; vertical-align:text-top; line-height:1.8em;}
            table.tableinfo tr td input,table.tableinfo tr td textarea{ border:none;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <table id="main">
                <tr>
                    <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                    <td id="mainRight">
                        <fieldset style="border:solid 1px #aaa;padding:3px;">
                            <legend >网站信息统计</legend>
                            <div style="padding:5px;">
                            <table class="tableinfo" style="table-layout:fixed;">
                                <tr>
                                    <td style="width:90px;">用户数量：</td>
                                    <td style="width:350px;text-align:left;"><a href="javascript:GotoCode('sys/account.aspx','AccountManage');" style=" color:Green; text-decoration:underline;"><%=_UserCount %></a> 个 </td>
                                    <td style="width:90px;">网站总访问量：</td>
                                    <td style="width:350px;text-align:left;"><%=_VisitCount %> </td>
                                </tr>
                                <tr>
                                    <td>栏目数量：</td>
                                    <td style="text-align:left;"><a href="javascript:GotoCode('news/newsclass.aspx','ClassManage');" style=" color:Green; text-decoration:underline;"><%=_ClassCount%></a> 个 </td>
                                    <td>内容数量：</td>
                                    <td style="text-align:left;"><a href="javascript:GotoCode('news/articles.aspx','ArticleManage');" style=" color:Green; text-decoration:underline;"><%=_ContentCount %></a> 篇 </td>
                                </tr>
                                <tr>
                                    <td>页面缓存功能：</td>
                                    <td style="text-align:left;"><%=_HTMLCache%> </td>
                                    <td>云端应用功能：</td>
                                    <td style="text-align:left;"><%=_WlniaoCloud%> </td>
                                </tr>
                                <tr>
                                    <td>最近重启时间：</td>
                                    <td style="text-align:left;"><%=_LastStart%> </td>
                                    <td></td>
                                    <td style="text-align:left;"> </td>
                                </tr>
                            </table>
                            </div>
                        </fieldset>
                        <br />
                        <fieldset style="border:solid 1px #aaa;padding:3px;">
                            <legend >常用功能</legend>
                            <div style="padding:5px;">
                                <input type="button" value="重启网站" onclick="RestartWebSite();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>&nbsp;
                                <input type="button" value="清除缓存" onclick="ClearCache();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>&nbsp;
                                <input type="button" value="修改密码" onclick="TopGoto('sys/pwdset.aspx');" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>&nbsp;
                                <input type="button" value="网站首页" onclick="NewGoto('../');" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>&nbsp;
                            </div>
                        </fieldset>
                        <br />
                    <%if (_IsOnline)
                      { %>
                        <fieldset style="border:solid 1px #aaa;padding:3px;">
                            <legend >未来鸟云端应用服务</legend>
                            <div style="padding:5px;">
                            <table class="tableinfo" style="table-layout:fixed;">
                                <tr>
                                    <td style="width:90px;">应用名称：</td>
                                    <td>
                                        <input id="ClientName" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                    <td style="width:90px;">程序版本：</td>
                                    <td>
                                        <input id="Version" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >授权方式：</td>
                                    <td >
                                        <input id="AuthState" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                    <td>VIP到期：</td>
                                    <td>
                                        <input id="VipDate" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>用户名：</td>
                                    <td style="width:150px;" >
                                        <input id="LoginName" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                    <td>余额：</td>
                                    <td style="width:150px;" >
                                        <input id="Yue" class="mini-textbox" allowInput="false" style="width:350px;" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </fieldset>
                        <br />
                        <%}
                      else
                      { %>
                        <fieldset style="border:solid 1px #aaa;padding:3px;">
                            <legend >未来鸟云端应用服务</legend>
                            <div style=" padding:30px; font-size:12px; text-align:center;">
                                您尚未开启云端服务，<a href="javascript:GotoCode('sys/setting.aspx','WebSiteSetting');" style=" color:Green; text-decoration:underline;">点击这里</a> 进行设置！
                            </div>
                        </fieldset>
                        <br />
                        <%} %>
                        <fieldset style="border:solid 1px #aaa;padding:3px;">
                            <legend >服务器信息</legend>
                            <div style="padding:5px;">
                            <table class="tableinfo" style="table-layout:fixed;">
                                <tr>
                                    <td style="width:90px;">服务器名称：</td>
                                    <td>
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Server.MachineName.ToString() %>" />
                                    </td>
                                    <td style="width:90px;">操作系统：</td>
                                    <td>
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Environment.OSVersion.ToString() %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td >服务器IP：</td>
                                    <td >
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Request.ServerVariables.GetValues("LOCAL_ADDR")[0] %>" />
                                    </td>
                                    <td>服务器域名：</td>
                                    <td>
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Request.ServerVariables.GetValues("SERVER_NAME")[0] %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>.NET Framework 版本：</td>
                                    <td>
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=System.Environment.Version.ToString() %>" />
                                    </td>
                                    <td>IIS版本：</td>
                                    <td >
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Request.ServerVariables.GetValues("SERVER_SOFTWARE")[0] %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>相对路径：</td>
                                    <td>
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Request.ServerVariables.GetValues("PATH_INFO")[0] %>" />
                                    </td>
                                    <td>物理路径：</td>
                                    <td >
                                        <input class="mini-textbox" allowInput="false" style="width:350px;" value="<%=Request.ServerVariables.GetValues("APPL_PHYSICAL_PATH")[0] %>" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </fieldset>
                        <br />
                    </td>                   
                </tr>
            </table>
        </div>		
        <%=GetTemplate("admin/_footer")%>
    </body>
</html>

    <script type="text/javascript">
        mini.parse();
        function InitForm() {
            mini.get("ClientName").setValue("<%=_ClientName %>");
            mini.get("LoginName").setValue("<%=_LoginName %>");
            mini.get("AuthState").setValue("<%=_AuthState %>");
            mini.get("VipDate").setValue("<%=_VipDate %>");
            mini.get("Yue").setValue("<%=_Yue %>");
            mini.get("Version").setValue("<%=_Version %>");
        }
        InitForm();
        function RestartWebSite() {
            if (checkRight('WebSiteSetting')) {
                if (confirm("确定要重启网站？")) {
                    $.ajax({
                        url: "<%= WebRoot %>admin/sys/setting.aspx?handle=RestartWebSite",
                        type: "post",
                        data: {},
                        dataType: 'json',
                        success: function (data) {
                        }
                    });
                    reload();
                }
            }
        }
        function ClearCache() {
            if (checkRight('WebSiteSetting')) {
                if (confirm("确定要清除缓存？")) {
                    $.ajax({
                        url: "<%= WebRoot %>admin/sys/setting.aspx?handle=ClearCache",
                        type: "post",
                        data: {},
                        dataType: 'json',
                        success: function (data) {
                        }
                    });
                    reload();
                }
            }
        }
    </script>