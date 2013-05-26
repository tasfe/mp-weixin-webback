using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin
{
    public partial class Logout : System.Web.TemplateEngine
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginIn"] = null;
        }
    }
}