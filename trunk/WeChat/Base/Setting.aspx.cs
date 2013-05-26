using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Setting : LoginPage
    {
        protected string _website = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Url.Port == 80)
                {
                    _website = Request.Url.Host;                    
                }
                else
                {
                    _website = Request.Url.Host + ":" + Request.Url.Port;
                }
                _website += Request.Url.AbsolutePath.Replace("/base/setting.aspx", "");
            }
        }
    }
}