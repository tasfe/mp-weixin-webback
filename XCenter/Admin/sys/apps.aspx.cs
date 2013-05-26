using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class apps : System.Web.LoginInPage
    {
        public string _applist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Wlniao.Method.Client.Init();
                try
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    List<Wlniao.Model.AppInfo> applist = Wlniao.Method.Client.GetApps();
                    if (applist != null)
                    {
                        foreach (Wlniao.Model.AppInfo app in applist)
                        {
                            sb.AppendFormat("<li onclick=\"OpenApp('{0}',{1});\"><table><tr><td width=\"64\"><img src=\"{2}\" style=\" width:64px; height:64px; border:none;\" /></td><td class=\"appdesc\"><h1>{3}</h1><span>{4}</span></td></tr></table></li>", app.Url, app.Height, app.Icons, app.AppName, strUtil.Ellipsis(app.Comment, 150, "..."));
                        }
                    }
                    _applist = sb.ToString();
                }
                catch { }
            }
        }
    }
}