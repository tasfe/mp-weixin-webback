<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="articles.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.articles" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleArticle")%></title><%=GetTemplate("admin/_tags")%>
        <style type="text/css">
            .iconThumbPic{ width:16px; height:16px; border:none; background-position:center center; background-repeat:no-repeat; background-image:url(../../xcenter/static/assets/icons/imageIcon.png); display:block;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">                
                <table id="main">
                    <tr>
                        <td id="mainLeft">
                            <div style="border:none; width:258px; height:500px; margin:0px; padding:0px 5px;">
                                <div id="leftTree" class="mini-outlooktree" url="<%= WebRoot %>admin/news/newsclass.aspx?handle=OutlookTree" expandOnLoad="true" resultAsTree="false" onnodeselect="onNodeSelect" textField="text" idField="id" parentField="pid" borderStyle="border:0;"></div>
                            </div>
                        </td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleArticle")%></h1>
                        <img id="PicShow" style=" height:auto; width:auto; display:none; background-color:#FFFFFF; border:5px solid #EEEEEE; margin:0 auto; position:fixed; left:33%; top:128; z-index:999;" src="" />
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
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:280px;" allowResize="false" url="<%= WebRoot %>admin/news/articles.aspx?handle=GetList" onRowdblclick="edit" idField="Id" multiSelect="true">
                            <div property="columns">
                                <%--<div type="indexcolumn"></div>--%>
                                <div field="Id" width="20" headerAlign="center" align="center" allowSort="false" >ID</div>
                                <div field="Pic" width="20" headerAlign="center" align="center" allowSort="false" renderer="onPic">图片</div>
                                <div field="Title" width="380" headerAlign="center" allowSort="false">标题</div>
                                <div field="OnTop" width="36" headerAlign="center" align="center" allowSort="false" renderer="onTop">置顶</div>
                                <div field="ClickNum" width="36" headerAlign="center" align="center" allowSort="false">点击</div>
                                <div field="AddTime" width="80" headerAlign="center" align="center" allowSort="false" renderer="onDataRenderer">发布时间</div>
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
    var selectid = 0;
    function onNodeSelect(e) {
        var node = e.node;
        selectid = node.id;
        grid.load({ classid: selectid });
    }
    var grid = mini.get("datagrid");
    if (checkRight('ArticleManage')) {
        grid.load();
        grid.sortBy("AddTime", "desc");
    } else {
        history.back();
    }
    function add() {
        if (checkRight('ArticleManage')) {
            mini.open({
                url: bootPATH + "../../../admin/news/articlesForm.aspx",
                title: "新增内容", width: 920, height: 580,
                maskOnLoad: false,
                onload: function () {
                    try {
                        var iframe = this.getIFrameEl();
                        var data = { action: "new" };
                        iframe.contentWindow.SetClassId(selectid);
                        iframe.contentWindow.SetData(data);
                    } catch (e) { }
                },
                ondestroy: function (action) {
                    try {
                        if (action != 'cancel') {
                            grid.reload();
                        }
                    } catch (er) {
                        grid.reload();
                    }
                }
            });
        }
    }
    function edit() {
        if (checkRight('ArticleManage')) {
            var row = grid.getSelected();
            if (row) {
                mini.open({
                    url: bootPATH + "../../../admin/news/articlesForm.aspx?action=edit&Id=" + row.Id,
                    title: "编辑内容", width: 920, height: 580,
                    maskOnLoad: false,
                    onload: function () {
                        var iframe = this.getIFrameEl();
                        var data = { action: "edit", id: row.Id };
                        iframe.contentWindow.SetClassId(0);
                        iframe.contentWindow.SetData(data);
                    },
                    ondestroy: function (action) {
                        try {
                            if (action != 'cancel') {
                                grid.reload();
                            }
                        } catch (er) {
                            grid.reload();
                        }
                    }
                });
            } else {
                MyalertMsg("请选中一条记录");
            }
        }
    }
    function remove() {
        if (checkRight('ArticleManage')) {
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
                        url: "<%= WebRoot %>admin/news/articles.aspx?handle=Del&ids=" + id,
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
        var key = mini.get("key").getValue();
        grid.load({ classid: selectid, key: key });
    }
    function onKeyEnter(e) {
        search();
    }
    function onTop(e) {
        if (e.value=="1") return "是";
        else return "";
    }
    function onPic(e) {
        if (e.value.length > 1) {
            return '<a class="iconThumbPic" onmouseover="ShowPic(\'' + e.value + '\');" onmouseout="HidePic();">&nbsp;</a>';
        }
        else return '';
    }
    function ShowPic(img) {
        var PicShow = $('#PicShow').attr('src', img);
        PicShow.css('display', 'block');
    }
    function HidePic() {
        var PicShow = $('#PicShow').css('display', 'none').attr('src', '');
    }
    function onDataRenderer(e) {
        return mini.formatDate(e.value, 'yyyy-MM-dd');
    }
</script>