<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="operatelog.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.operatelog" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleCOperateLogView")%></title><%=GetTemplate("admin/_tags")%>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleCOperateLogView")%></h1>
                        <div style="width:100%;">
                            <div class="mini-toolbar" style="border-bottom:0;padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:100%;">
                                            <span style="color:#111111; font-size:12px;">&nbsp;选择用户：</span>
                                            <input id="lookup2" name="look" class="mini-lookup" style="width:188px;" textField="LoginName" valueField="Id" popupWidth="auto" popup="#gridPanel" grid="#gridaccount" multiSelect="false" onhidepopup="loadwithuser()" />
                                                <div id="gridPanel" class="mini-panel" title="header" iconCls="icon-add" style="width:450px;height:210px;" 
                                                    showToolbar="true" showCloseButton="true" showHeader="false" bodyStyle="padding:0" borderStyle="border:0">
                                                    <div property="toolbar" style="padding:5px;padding-left:8px;text-align:center;">   
                                                        <span>姓名：</span>    
                                                        <input id="accountText" class="mini-textbox" style="width:160px;" onenter="onSearchClick"/>
                                                        <a class="mini-button" onclick="onSearchClick"><%=UIConfig("Search")%></a>
                                                        <a class="mini-button" onclick="onCloseClick"><%=UIConfig("Close")%></a>
                                                    </div>
                                                    <div id="gridaccount" class="mini-datagrid" style="width:100%;height:100%;" borderStyle="border:0" showPageSize="false" showPageIndex="false" url="<%= WebRoot %>admin/sys/account.aspx?handle=GetList">
                                                        <div property="columns">
                                                            <div type="checkcolumn">#</div>
                                                            <div field="LoginName" width="120" headerAlign="center" allowSort="true">用户名</div>    
                                                            <div field="Nickname" width="120" headerAlign="center" allowSort="true">姓名</div>
                                                            <div field="CreateTime" width="100" headerAlign="center" dateFormat="yyyy-MM-dd" allowSort="true">创建日期</div>                
                                                        </div>
                                                    </div>  
                                                </div>  
                                            <span id="tipTitle" style=" color:Gray; font-size:12px;">为空则查看所有操作记录</span>
                                        </td>
                                        <td style="white-space:nowrap;">
                                            <a class="mini-button" iconCls="icon-reload" onclick="reload()"><%=UIConfig("Reload")%></a> 
                                        </td>
                                    </tr>
                                </table>           
                            </div>
                        </div>
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:476px;" allowResize="false" pageSize="18" 
                            url="<%= WebRoot %>admin/sys/operatelog.aspx?handle=GetList" idField="Id" allowCellEdit="true" allowCellSelect="false" multiSelect="false" showFooter="true">
                            <div property="columns">
                                <!--<div type="indexcolumn"></div>-->
                                <div field="NickName" width="118" headerAlign="center" align="center" allowSort="false">用户</div>
                                <div field="LogModule" width="118" headerAlign="center" allowSort="false">功能模块</div>
                                <div field="OperateDescription" width="518" headerAlign="center" align="left" allowSort="false">操作描述</div>
                                <div field="LogOperateTime" width="118" headerAlign="center" allowSort="false" renderer="onDataRenderer">时间</div>
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
    var gridaccount = mini.get("gridaccount");
    var accountText = mini.get("accountText");
    if (checkRight('OperateLogView')) {
        gridaccount.load();
    } else {
        history.back();
    }
    function onSearchClick(e) {
        if (checkRight('OperateLogView')) {
            gridaccount.load({
                key: accountText.value
            });
        }
    }
    function onCloseClick(e) {
        var lookup2 = mini.get("lookup2");
        lookup2.hidePopup();
    }

    var grid = mini.get("datagrid");
    grid.sortBy("OperateTime", "asc");
    grid.load();
    function loadwithuser() {
        if (checkRight('OperateLogView')) {
            var lookup2 = mini.get("lookup2");
            if (lookup2.value == '') {
                tipTitle.innerHTML = "查看全部日志";
            } else {
                tipTitle.innerHTML = "";
            }
            grid.load({ uid: lookup2.value });
        }
    }
    function reload() {
        if (checkRight('OperateLogView')) {
            grid.reload();
            setTimeout(function () { grid.expandGroups(); }, 300);
        }
    }
    function onDataRenderer(e) {
        return mini.formatDate(new Date(e.value), 'yyyy-MM-dd HH:mm:ss');
    }
</script>