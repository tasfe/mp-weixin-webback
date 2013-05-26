<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.account" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleAccount")%></title><%=GetTemplate("admin/_tags")%>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleAccount")%></h1>
                        <div style="width:100%;">
                            <div class="mini-toolbar" style="border-bottom:0;padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:100%;">
                                            <a class="mini-button" iconCls="icon-add" onclick="add()"><%=UIConfig("Add")%></a>
                                            <a class="mini-button" iconCls="icon-edit" onclick="edit()"><%=UIConfig("Edit")%></a>
                                            <a class="mini-button" iconCls="icon-remove" onclick="remove()"><%=UIConfig("Del")%></a>
                                            <a class="mini-button" iconCls="icon-reload" onclick="reload()"><%=UIConfig("Reload")%></a>   
                                        </td>
                                        <td style="white-space:nowrap;">
                                            <input id="key" class="mini-textbox" emptyText="<%=UIConfig("InputSearchKey")%>" style="width:150px;" onenter="onKeyEnter"/>   
                                            <a class="mini-button" iconCls="icon-search" onclick="search()"><%=UIConfig("Search")%></a>
                                        </td>
                                    </tr>
                                </table>           
                            </div>
                        </div>
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:280px;" allowResize="false" url="<%= WebRoot %>admin/sys/account.aspx?handle=GetList" onRowdblclick="edit" idField="Id" multiSelect="true">
                            <div property="columns">
                                <!--<div type="indexcolumn"></div>        -->
                                <div field="Id" width="20" headerAlign="center" allowSort="false" type="checkcolumn" >ID</div>
                                <div field="LoginName" width="110" headerAlign="center" allowSort="false">用户名</div>
                                <div field="Nickname" width="110" headerAlign="center" allowSort="false">姓名</div>
                                <div field="IsAdmin" width="50" headerAlign="center" align="center" allowSort="false" renderer="onIsAdmin">权限</div>
                                <div field="Mobile" width="80" headerAlign="center" align="center" allowSort="false">手机号</div>
                                <div field="QQ" width="80" headerAlign="center" align="center" allowSort="false">QQ号</div>
                                <div field="Email" width="110" headerAlign="center" align="center" allowSort="false">电子邮箱</div>
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
    if (checkRight('AccountManage')) {
        grid.load();
        grid.sortBy("CreateDate", "desc");
    } else {
        history.back();
    }
    function add() {
        if (checkRight('AccountManage')) {
            mini.open({
                url: bootPATH + "../../../admin/sys/accountForm.aspx",
                title: "新增用户", width: 500, height: 410,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "new" };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }
    }
    function edit() {
        if (checkRight('AccountManage')) {
            var row = grid.getSelected();
            if (row) {
                mini.open({
                    url: bootPATH + "../../../admin/sys/accountForm.aspx?action=edit&Id=" + row.Id,
                    title: "【" + row.LoginName + "】-资料管理", width: 500, height: 410,
                    onload: function () {
                        var iframe = this.getIFrameEl();
                        var data = { action: "edit", id: row.Id };
                        iframe.contentWindow.SetData(data);
                    },
                    ondestroy: function (action) {
                        grid.reload();
                    }
                });
            } else {
                alert("请选中一条记录");
            } 
        }
    }
    function remove() {
        if (checkRight('AccountManage')) {
            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.Id);
                    }
                    var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        url: "<%= WebRoot %>admin/sys/account.aspx?handle=Del&ids=" + id,
                        success: function (data) {
                            if (data.success) {
                                grid.reload();
                                MyalertMsg(data.msg, 'success');
                            } else {
                                MyalertMsg(data.msg, 'error');
                            }
                        },
                        error: function () {
                        }
                    });
                }
            } else {
                MyalertMsg("请选中一条记录");
            } 
        }
    }
    function search() {
        if (checkRight('AccountManage')) {
            var key = mini.get("key").getValue();
            grid.load({ key: key });
        }
    }
    function onKeyEnter(e) {
        search();
    }
    /////////////////////////////////////////////////
    function onDataRenderer(e) {
        var value = e.value;
        if (value > Date.parse('1900-1-1')) {
            return mini.formatDate(value, 'yyyy-MM-dd');
        } else {
            return "";
        }
    }
    function onIsAdmin(e) {
        if (e.value=="true") return "管理员";
        else return "用户";
    }
    function onTrueOrFalse(e) {
        if (e.value) return "是";
        else return "否";
    }
</script>