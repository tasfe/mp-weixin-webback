using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class newsclassForm : System.Web.LoginInPage
    {
        public string edContent = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                try
                {
                    XCenter.Code.Domain.News.NewsClass nc = db.findById<XCenter.Code.Domain.News.NewsClass>(int.Parse(Request["id"]));
                    edContent = HttpUtility.HtmlDecode(nc.Content);
                }
                catch { }
            }
        }
    }
}