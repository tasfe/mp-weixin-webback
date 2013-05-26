<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="app.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.app" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleWlniaoCloud")%></title><%=GetTemplate("admin/_tags")%>
        <style>
            legend{ font-size:12px; font-weight:bolder;}
            table.tableinfo tr td{ font-size:12px; text-align:right; vertical-align:text-top; line-height:1.8em;}
            table.tableinfo tr td input,table.tableinfo tr td textarea{ border:none;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight" align="left">
                        <%if (_IsOnline)
                          { %>
                                <iframe src="apps.aspx" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" id="ifm" name="ifm" onload="javascript:dyniframesize('ifm');" width="100%"></iframe> 
                            <br />
                            <%}
                          else
                          { %>
                            <fieldset style="border:solid 1px #aaa;padding:3px; margin:50px 0px 210px 0px;">
                                <legend >未来鸟云端应用服务</legend>
                                <div style=" padding:50px; font-size:12px; text-align:center;">
                                    您尚未开启云端服务，<a href="javascript:GotoCode('setting.aspx','WebSiteSetting');" style=" color:Green; text-decoration:underline;">点击这里</a> 进行设置！
                                </div>
                            </fieldset>
                            <br />
                            <%} %>
                        </td>                   
                    </tr>
                </table>
            </div>
        </div>		
        <%=GetTemplate("admin/_footer")%>
    </body>
</html>
<script language="javascript" type="text/javascript">
    function OpenApps(url, height) {
        document.getElementById('ifm').style.height = height;
        document.getElementById('ifm').src = url;
    }
    function Init() {
        document.getElementById('ifm').style.height = 430;
    }
    Init();
</script> 