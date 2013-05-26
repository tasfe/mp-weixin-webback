<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="statistical.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.statistical" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-访客记录</title><%=GetTemplate("admin/_tags")%>
        <style type="text/css">
            .iconThumbPic{ width:16px; height:16px; border:none; background-position:center center; background-repeat:no-repeat; background-image:url(../../xcenter/static/assets/icons/imageIcon.png); display:block;}
            .sUrl{ color:#111111;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">                
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1>访客记录</h1>
                        <img id="PicShow" style=" height:auto; width:auto; display:none; background-color:#FFFFFF; border:5px solid #EEEEEE; margin:0 auto; position:fixed; left:33%; top:128; z-index:999;" src="" />
                        <div style="width:100%;">
                            <div class="mini-toolbar" style="border-bottom:0;padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:100%;">
                                            <a class="mini-button" iconCls="icon-reload" onclick="reloadData()"><%=UIConfig("Reload")%></a>
                                        </td>
                                    </tr>
                                </table>           
                            </div>
                        </div>
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:476px;" allowResize="false" pageSize="18" url="<%= WebRoot %>admin/siteinfo/statistical.aspx?handle=GetList" idField="Id" multiSelect="true">
                            <div property="columns">
                                <%--<div type="indexcolumn"></div>--%>
                                <div field="Id" width="52" headerAlign="center" align="center" allowSort="false" >编号</div>
                                <div field="IsIndependent" width="36" headerAlign="center" align="center" allowSort="false" renderer="onTrueOrFalse">类型</div>
                                <div field="Address" width="118" align="center" headerAlign="center" allowSort="false">来源地区</div>
                                <div field="Url" width="280" headerAlign="center" allowSort="false" renderer="onUrl">访问的页面</div>
                                <div field="UrlReferrer" width="280" headerAlign="center" allowSort="false" renderer="onUrl">来源Url</div>
                                <div field="AccessTime" width="112" align="center" headerAlign="center" allowSort="false" dateFormat="MM月dd日 HH:mm:ss">时间</div>
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
    var grid = mini.get("datagrid");
    if (checkRight('StatisticalManage')) {
        grid.load();
        grid.sortBy("AccessTime", "desc");
    } else {
        history.back();
    }
    function reloadData() {
        grid.reload();
    }
    function Select() {
        var type = mini.get("FlashType").getValue();
        grid.load({ type: type });
    }
    function onUrl(e) {
        if (e.value.length > 0) return '<a class="sUrl" href="' + e.value + '" target="_blank">' + e.value + '</a>';
        else return "";
    }
    function onTrueOrFalse(e) {
        if (e.value=="1") return "新";
        else return "";
    }
</script>