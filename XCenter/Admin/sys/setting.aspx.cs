using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class setting : System.Web.LoginInPage
    {
        protected string _AppKey = "";
        protected string _Secret = "";
        protected string _ApiUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    Wlniao.Model.ClientInfo client = null;
                    switch (helper.GetParam("handle").ToLower())
                    {
                        case "updatekeyvalue":
                            System.Data.KvTableUtil.Save(Request["key"], Request["value"]);
                            helper.Response("");
                            break;
                        case "restartwebsite":
                            XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "网站管理", string.Format("于{0}重启网站", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss")));
                            cfgHelper.SetAppSettings("LastStart", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss"));
                            helper.Response("");
                            break;
                        case "clearcache":
                            System.IO.Directory.Delete(PathHelper.Map(cfgHelper.FrameworkRoot + "htmlcache"), true);
                            helper.Response("");
                            break;
                        case "settingapi":
                            string appkey = Request["appkey"];
                            string secret = Request["secret"];
                            string apiurl = Request["apiurl"];
                            try
                            {
                                Wlniao.Method.Client.Init(appkey, secret, apiurl);
                                client = Wlniao.Method.Client.GetInfo();
                                if (client == null || string.IsNullOrEmpty(client.LoginName))
                                {
                                    Wlniao.Method.Client.Init(appkey, secret, apiurl);
                                    client = Wlniao.Method.Client.GetInfo();
                                }
                            }
                            catch { }
                            if (client != null && !string.IsNullOrEmpty(client.LoginName))
                            {
                                cfgHelper.SetAppSettings("AppKey", appkey);
                                cfgHelper.SetAppSettings("Secret", secret);
                                cfgHelper.SetAppSettings("ApiUrl", apiurl);
                                helper.Add("success", true);
                                helper.Add("msg", "API信息设置成功");
                            }
                            else
                            {
                                helper.Add("success", false);
                                helper.Add("msg", "API信息错误，设置失败");
                            }
                            helper.Response();
                            break;
                        default:
                            try
                            {
                                Wlniao.Method.Client.Init(cfgHelper.GetAppSettings("AppKey"), cfgHelper.GetAppSettings("Secret"), cfgHelper.GetAppSettings("ApiUrl"));
                                client = Wlniao.Method.Client.GetInfo();
                                if (client == null || string.IsNullOrEmpty(client.LoginName))
                                {
                                    Wlniao.Method.Client.Init(cfgHelper.GetAppSettings("AppKey"), cfgHelper.GetAppSettings("Secret"), cfgHelper.GetAppSettings("ApiUrl"));
                                    client = Wlniao.Method.Client.GetInfo();
                                }
                                if (client == null || string.IsNullOrEmpty(client.LoginName))
                                {
                                    System.Data.KvTableUtil.Save("WlniaoCloud", "false");
                                }
                            }
                            catch { }
                            _AppKey = cfgHelper.GetAppSettings("AppKey");
                            _Secret = cfgHelper.GetAppSettings("Secret");
                            _ApiUrl = cfgHelper.GetAppSettings("ApiUrl");
                            break;
                    }
                }
                #endregion 处理结束
            }
        }

        public string GetKv(string key)
        {
            return System.Data.KvTableUtil.GetString(key);
        }
    }
}