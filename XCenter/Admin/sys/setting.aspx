<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="XCenterCMS.Web.Admin.sys.setting" %>
<html>
    <head>
        <title><%=GetKeyValue("ManageSiteTitle")%>-<%=UIConfig("AdminTitleSetting")%></title><%=GetTemplate("admin/_tags")%>
        <style>
         table tr td{ vertical-align:top; line-height:1.8em; vertical-align:text-top; font-size:12px;}
        </style>
    </head>
    <body>
        <div id="container"><%=GetTemplate("admin/_header")%>
            <div id="bgwrap">
                <table id="main">
                    <tr>
                        <td id="mainLeft"><%=GetTemplate("admin/_left")%></td> 
                        <td id="mainRight">
                        <h1><%=UIConfig("AdminTitleSetting")%></h1>
                        <div style="width:100%; min-height:460px;">
                            <div id="form1">
                                <fieldset style="border:solid 1px #ffffff;padding:3px;">
                                    <table style="table-layout:fixed; width:100%; font-size:10px; height:1.8em;">
                                        <tr style="">
                                            <td style="width:200px; text-align:right;">网站重启：</td>
                                            <td style="width:700px;">
                                                <input type="button" value="立即重启" onclick="RestartWebSite();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/><br />
                                                <span style="color:#aaaaaa;">某些设置修改后需要重新启动你的网站进程才会生效。</span> 
                                            </td>
                                        </tr>
                                        <tr style=" display:none;">
                                            <td style="text-align:right;">网站名称：</td>
                                            <td>
                                                <input name="Url" class="mini-textbox" style="width:80%;" />
                                                <span style="color:#aaaaaa;">设置您的网站名称</span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">启用页面缓存：</td>
                                            <td>
                                                <select id="HTMLCache" class="mini-radiobuttonlist" onValuechanged="UpdateKeyValue('HTMLCache');" >
                                                    <option value="true">是</option>
                                                    <option value="false">否</option>
                                                </select>
                                                <span style="color:#aaaaaa;">是否启用页面缓存，启用后台可以提高网站访问速度，但内容有修改后需要手动更新缓存。</span> 
                                            </td>
                                        </tr>
                                        <tr style="">
                                            <td style="text-align:right;">清除缓存：</td>
                                            <td>
                                                <input type="button" value="清除缓存" onclick="ClearCache();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/><br />
                                                <span style="color:#aaaaaa;">清除网站页面缓存功能。</span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">启用访客统计：</td>
                                            <td>
                                                <select id="Statistical" class="mini-radiobuttonlist" onValuechanged="UpdateKeyValue('Statistical');" >
                                                    <option value="true">是</option>
                                                    <option value="false">否</option>
                                                </select>
                                                <span style="color:#aaaaaa;">启用访客统计后系统会记录用户对每一个页面的访问，后期还将推出搜索引擎蜘蛛管理和推广效果监测。</span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">启用云端服务：</td>
                                            <td>
                                                <select id="WlniaoCloud" class="mini-radiobuttonlist" onValuechanged="WlniaoCloud();" >
                                                    <option value="true">是</option>
                                                    <option value="false">否</option>
                                                </select>
                                                <span style="color:#aaaaaa;">未来鸟工作室为您提供了更多云端应用服务，你可以在此选择是否启用这些功能。</span> 
                                                <table id="WlniaoCloudTable" style=" display:none;">
                                                    <tr>
                                                        <td style="text-align:right;width:70px;">AppKey：</td>
                                                        <td>
                                                            <input id="AppKey" class="mini-textbox" style="width:270px;" />
                                                            <input type="button" value="设置API" onclick="SettingAPI();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>
                                                            <input type="button" value="如何获取服务" onclick="Register();" style=" padding:3px 8px; border:1px solid #999999; cursor:pointer;"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;width:70px;">Secret：</td>
                                                        <td>
                                                            <input id="Secret" class="mini-textbox" style="width:270px;" />
                                                        </td>
                                                    </tr>
                                                    <tr style=" display:none;">
                                                        <td style="text-align:right;width:70px;">ApiUrl：</td>
                                                        <td>
                                                            <input id="ApiUrl" class="mini-textbox" style="width:270px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
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
    function submitForm() {

    }
    function InitForm() {
        mini.get("HTMLCache").setValue( "<%=GetKv("HTMLCache") %>");
        mini.get("Statistical").setValue( "<%=GetKv("Statistical") %>");
        mini.get("WlniaoCloud").setValue( "<%=GetKv("WlniaoCloud") %>");
        if(<%=GetKv("WlniaoCloud") %>){
            document.getElementById('WlniaoCloudTable').style.display = "";
        }else{
            document.getElementById('WlniaoCloudTable').style.display = "none";
        }
        mini.get("AppKey").setValue( "<%=_AppKey %>");
        mini.get("Secret").setValue( "<%=_Secret %>");
        mini.get("ApiUrl").setValue( "<%=_ApiUrl %>");
    }
    function RestartWebSite() {
        if(checkRight('WebSiteSetting')){
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
        if(checkRight('WebSiteSetting')){
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
    function SettingAPI() {
        var appkey = mini.get("AppKey").getValue();
        var secret = mini.get("Secret").getValue();
        var apiurl = mini.get("ApiUrl").getValue();
        $.ajax({
            url: "<%= WebRoot %>admin/sys/setting.aspx?handle=SettingAPI",
            type: "post",
            data: {appkey: appkey,secret:secret,apiurl:apiurl},
            dataType: 'json',
            success: function (data) {
                if(data.success){
                    MyalertMsg(data.msg, 'success');
                }else{
                    MyalertMsg(data.msg, 'error');
                }
            }
        });
    }
    function Register() {
        window.open('http://server.wlniao.com/help');
    }
    function UpdateKeyValue(key) {
        var value = mini.get(key).getValue();
        $.ajax({
            url: "<%= WebRoot %>admin/sys/setting.aspx?handle=UpdateKeyValue",
            type: "post",
            data: { key: key,value:value },
            dataType: 'json',
            success: function (data) {
            }
        });
    }
    function WlniaoCloud() {
        var value = mini.get("WlniaoCloud").getValue();
        if(value=="true"){
            document.getElementById('WlniaoCloudTable').style.display = "";
        }else{
            document.getElementById('WlniaoCloudTable').style.display = "none";
        }
        $.ajax({
            url: "<%= WebRoot %>admin/sys/setting.aspx?handle=UpdateKeyValue",
            type: "post",
            data: { key: "WlniaoCloud",value:value },
            dataType: 'json',
            success: function (data) {
            }
        });
    }
    InitForm();
</script>