<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rightset.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.rightset" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleRight")%></title><%=GetTemplate("admin/_tags")%>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleRight")%></h1>
                        <div style="width:100%;">
                            <div class="mini-toolbar" style="border-bottom:0;padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:100%;">
                                            <span style="color:#111111; font-size:12px;">&nbsp;选择用户：</span>
                                            <input id="lookup2" name="look" class="mini-lookup" style="width:188px;" textField="LoginName" valueField="Id" popupWidth="auto" popup="#gridPanel" grid="#gridaccount" multiSelect="true" onhidepopup="loadwithuser()" />
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
                                            <span id="tipTitle" style=" color:Gray; font-size:12px;">为空则设置默认权限</span>
                                        </td>
                                        <td style="white-space:nowrap;">
                                            <a class="mini-button" iconCls="icon-reload" onclick="reload()"><%=UIConfig("Reload")%></a> 
                                        </td>
                                    </tr>
                                </table>           
                            </div>
                        </div>
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:280px;" allowResize="false" pageSize="250" ondrawgroup="onDrawGroup" oncellcommitedit="rowedit"
                            url="<%= WebRoot %>admin/sys/rightset.aspx?handle=GetList" idField="Id" allowCellEdit="true" allowCellSelect="false" multiSelect="false" showFooter="false">
                            <div property="columns">
                                <!--<div type="indexcolumn"></div>-->
                                <div field="RightName" width="88" headerAlign="center" allowSort="false">权限名称</div>
                                <div field="DefaultRightValue" width="30" type="checkboxcolumn" trueValue="1" falseValue="0" headerAlign="center" align="center" allowSort="false">操作</div>
                                <div field="Comments" width="588" headerAlign="center" align="left" allowSort="false">权限说明</div>
                                <div field="HasRightCode" width="90" headerAlign="center" allowSort="false">权限代码</div>
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
    if (checkRight('AccountRightManage')) {
        gridaccount.load();
    } else {
        history.back();
    }
    function onSearchClick(e) {
        if (checkRight('AccountRightManage')) {
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
    grid.sortBy("Sort", "asc");
    grid.groupBy("RightType", "desc");
    grid.setCollapseGroupOnLoad(true);
    grid.load();
    setTimeout(function () { grid.expandGroups(); }, 300);
    function rowedit(e) {
        var lookup2 = mini.get("lookup2");
        $.ajax({
            url: "<%= WebRoot %>admin/sys/rightset.aspx?handle=SaveOne&Id=" + e.record.Id + "&code=" + e.record.RightCode + "&value=" + e.value + "&uid=" + lookup2.value,
            data: { data: '' },
            type: "POST",
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data.success) {
                } else {
                    MyalertMsg(data.msg, 'error');
                }
            }
        });
    }
    function loadwithuser() {
        if (checkRight('AccountRightManage')) {
            var lookup2 = mini.get("lookup2");
            if (lookup2.value == '') {
                tipTitle.innerHTML = "设置默认权限";
            } else {
                tipTitle.innerHTML = "";
            }
            grid.load({ uid: lookup2.value });
            setTimeout(function () { grid.expandGroups(); }, 300);
        }
    }
    function reload() {
        if (checkRight('AccountRightManage')) {
            grid.reload();
            setTimeout(function () { grid.expandGroups(); }, 300);
        }
    }
    function onDrawGroup(e) {
        e.cellHtml = e.value + "：";
    }
</script>