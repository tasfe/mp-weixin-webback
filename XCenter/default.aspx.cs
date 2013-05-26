using System;
using System.Collections.Generic;
//|5|1|a|s|p|x
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenter.Web
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpApplication application = Context.ApplicationInstance;
            String path = "/";
            if (System.Data.KvTableUtil.GetBool("Statistical"))
            {
                System.Threading.ParameterizedThreadStart pts = new System.Threading.ParameterizedThreadStart(new XCenter.Code.MyModule().AddStatistical);
                System.Threading.Thread thread = new System.Threading.Thread(pts);
                thread.Start(application.Context);
            }
            new XCoreModule().BeginRequest(application, application.Context, application.Context.Response, path, System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileName(path));
        }
    }
}