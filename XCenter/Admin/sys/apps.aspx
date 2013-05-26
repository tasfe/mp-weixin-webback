<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="apps.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.apps" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleWlniaoCloud")%></title>
        <style>
            .applist{ list-style:none; width:820px; text-decoration:none;}
            .applist li{ height:64px; list-style:none; float:left; width:400px; cursor:pointer;}
            .applist li table{ width:100%;}
            .applist li table td{ font-size:12px;}
            .appdesc{ vertical-align:top;}
            .appdesc h1{ color:#666666; font-weight:bolder; font-size:12px; line-height:1em; margin:3px 0px; padding:0px; }
            .appdesc span{ color:#aaaaaa; display:block; height:43px; line-height:1.3em; overflow:inherit;}
        </style>
    </head>
    <body>
        <ul class="applist">            
            <%=_applist %>
        </ul>
    </body>
</html>
<script>
    function OpenApp(url, height) {
        parent.OpenApps(url, height);
    }
</script>