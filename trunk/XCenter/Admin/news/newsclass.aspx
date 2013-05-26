<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsclass.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.newsclass" %>
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
                        <h1><%=UIConfig("AdminTitleNewsClass")%></h1>
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
                        <div id="treegrid" class="mini-treegrid" style="width:100%;height:280px;" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=GetTreeGrid" treeColumn="classname" idField="Id" parentField="ParentId" resultAsTree="false" showCheckBox="true" checkRecursive="false">
                            <div property="columns">
                                <%--<div type="indexcolumn"></div>--%>
                                <div field="Id" width="20" headerAlign="center" align="center" allowSort="false" >ID</div>
                                <div field="ClassName" name="classname" width="380" headerAlign="center" allowSort="false">栏目名称</div>
                                <div field="Type" width="60" headerAlign="center" align="center" allowSort="false" renderer="onType">栏目类型</div>
                                <div field="Sort" width="40" headerAlign="center" align="center" allowSort="false">排序</div>
                                <div field="NavShow" width="50" headerAlign="center" align="center" allowSort="false" renderer="onNavShow">导航栏</div>
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
    function add() {
        mini.open({
            url: bootPATH + "../../../admin/news/newsclassForm.aspx",
            title: "新增栏目", width: 920, height: 580,
            maskOnLoad: false,
            onload: function () {
                try {
                    var iframe = this.getIFrameEl();
                    var data = { action: "new" };
                    iframe.contentWindow.SetData(data);
                } catch (e) { }
            },
            ondestroy: function (action) {
                try {
                    if (action == 'save') {
                        reload();
                    }
                } catch (er) { reload(); }
            }
        });
    }
    function edit() {
        var tree = mini.get("treegrid");
        var ids = tree.getValue().split(",");
        if (tree.getValue().length>0&&ids.length==1) {
            mini.open({
                url: bootPATH + "../../../admin/news/newsclassForm.aspx?action=edit&Id=" + ids[0],
                title: "编辑栏目信息", width: 920, height: 580,
                maskOnLoad: false,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "edit", id: ids[0] };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    try {
                        if (action != 'cancel') {
                            reload();
                        }
                    } catch (er) { reload(); }
                }
            });
        } else {
            MyalertMsg("请选中一条记录");
        }
    }
    function remove() {
        var tree = mini.get("treegrid");
        var id = tree.getValue();
        if (id.length > 0) {
            if (confirm("确定删除选中记录？")) {
                //tree.loading("操作中，请稍后......");
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    url: "<%= WebRoot %>admin/news/newsclass.aspx?handle=Del&ids=" + id,
                    success: function (data) {
                        if (data.success) {
                            MyalertMsg(data.msg, 'success', function () { reload(); });
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
    function onType(e) {
        try {
            if (e.value == "page")
                return "HTML单页";
            else if (e.value == "url")
                return "连接类型";
            else if (e.value == "list")
                return "文章列表";
            else if (e.value == "photo")
                return "图片相册";
            else
                return "普通栏目";
        } catch (e) { }
    }
    function onNavShow(e) {
        if (e.value=="true") return "显示";
        else return "隐藏";
    }
</script>