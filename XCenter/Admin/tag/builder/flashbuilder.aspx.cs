//----------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Web\admin\tags\builder\newsbuilder.aspx.cs
//	运 行 库：2.0.50727.1882
//	代码功能：新闻列表标签生成器
//	最后修改：2012年3月7日 11:25:31
//----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace XCenterCMS.Web.admin.tags.builder
{
    public partial class flashbuilder : System.Web.LoginInPage
    {
        public string typelist = string.Empty;
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
        }
    }
}