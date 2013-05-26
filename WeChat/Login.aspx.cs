using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理开始
            if (!IsPostBack)
            {
                if (Request["do"] == "login")
                {
                    AshxHelper helper = new AshxHelper(Context);
                    helper.Result = Wlniao.WeChat.BLL.Sys.CheckLogin(Request["username"], Request["password"]);
                    if (helper.Result.IsValid)
                    {
                        Session["Account"] = Request["username"];
                    }
                    helper.ResponseResult();
                }
                else
                {
                    Session["Account"] = null;
                    if (System.Data.KvTableUtil.GetString("Install") != "true")
                    {
                        cfgHelper.SetAppSettings("Install", "false");
                        Response.Redirect("install.aspx");
                    }
                }
            }
            #endregion 处理结束
        }
    }
}