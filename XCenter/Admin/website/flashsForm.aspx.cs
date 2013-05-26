using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class flashsForm : System.Web.LoginInPage
    {
        public string typelist = string.Empty;
        public string edContent = string.Empty;
        public string imgThumbPic = "../../xcenter/static/assets/defaultPic.png";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string FLASHPICTYPE = lang.getBySkin("FLASHPICTYPE");
                string[] types = FLASHPICTYPE.Split(new char[] { ';', ',', '|' });
                foreach (string type in types)
                {
                    typelist += string.Format("<option value=\"{0}\">{0}</option>", type);
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                try
                {
                    XCenter.Code.Domain.WebSite.FlashPic nc = db.findById<XCenter.Code.Domain.WebSite.FlashPic>(int.Parse(Request["id"]));
                    edContent = HttpUtility.HtmlDecode(nc.Description);
                    if (file.Exists(PathHelper.Map(nc.ThumbPic)))
                    {
                        imgThumbPic = nc.ThumbPic;
                    }
                }
                catch { }
            }
        }
    }
}