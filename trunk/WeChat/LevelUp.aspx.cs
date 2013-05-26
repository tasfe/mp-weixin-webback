using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class LevelUp : System.Web.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cfgHelper.SetAppSettings("Install", "true");
            Session["Account"] = null;
        }
    }
}
