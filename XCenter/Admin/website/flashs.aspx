<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="flashs.aspx.cs" Inherits="XCenterCMS.Web.Admin.news.flashs" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleFlashPic")%></title><%=GetTemplate("admin/_tags")%>
        <style type="text/css">
            .iconThumbPic{ width:16px; height:16px; border:none; background-position:center center; background-repeat:no-repeat; background-image:url(../../xcenter/static/assets/icons/imageIcon.png); display:block;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">                
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleFlashPic")%></h1>
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
                                            <select id="FlashType" name="FlashType" class="mini-combobox" style="width:90px;" onvaluechanged="Select()" >
                                                <%=typelist%>
                                            </select>
                                        </td>
                                    </tr>
                                </table>           
                            </div>
                        </div>
                        <div id="datagrid" class="mini-datagrid" style="width:100%;height:360px;" allowResize="false" url="<%= WebRoot %>admin/website/flashs.aspx?handle=GetList" onRowdblclick="edit" idField="Id" multiSelect="true">
                            <div property="columns">
                                <%--<div type="indexcolumn"></div>--%>
                                <div field="Id" width="20" headerAlign="center" align="center" allowSort="false" >ID</div>
                                <div field="Pic" width="35" headerAlign="center" align="center" allowSort="false" renderer="onPic">图片</div>
                                <div field="FlashType" width="80" headerAlign="center" allowSort="false">分类</div>
                                <div field="FlashTitle" width="380" headerAlign="center" allowSort="false">标题</div>
                                <div field="OnTop" width="36" headerAlign="center" align="center" allowSort="false" renderer="onTrueOrFalse">置顶</div>
                                <div field="Sort" width="41" headerAlign="center" align="center" allowSort="false">排序</div>
                                <div field="OnStop" width="36" headerAlign="center" align="center" allowSort="false" renderer="onTrueOrFalse">停用</div>
                                <div field="Url" width="280" headerAlign="center" allowSort="false">连接地址</div>
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
    if (checkRight('FlashPicManage')) {
        grid.load();
        grid.sortBy("Sort", "asc");
    } else {
        history.back();
    }
    function Select() {
        var type = mini.get("FlashType").getValue();
        grid.load({ type: type });
    }
    function add() {
        if (checkRight('FlashPicManage')) {
            mini.open({
                url: bootPATH + "../../../admin/website/flashsForm.aspx",
                title: "新增轮换图片", width: 460, height: 398,
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
        if (checkRight('FlashPicManage')) {
            var row = grid.getSelected();
            if (row) {
                mini.open({
                    url: bootPATH + "../../../admin/website/flashsForm.aspx?action=edit&Id=" + row.Id,
                    title: "编辑轮换图片", width: 460, height: 398,
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
        if (checkRight('FlashPicManage')) {
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
                        url: "<%= WebRoot %>admin/website/flashs.aspx?handle=Del&ids=" + id,
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
    function onTrueOrFalse(e) {
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
</script>