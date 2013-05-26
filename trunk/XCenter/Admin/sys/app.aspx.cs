using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class app : System.Web.LoginInPage
    {
        public bool _IsOnline = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Wlniao.Method.Client.Init();
                    Wlniao.Model.ClientInfo client = null;
                    client = Wlniao.Method.Client.GetInfo();
                    if (client != null && !string.IsNullOrEmpty(client.LoginName) && GetKeyValue("WlniaoCloud") == "true")
                    {
                        _IsOnline = true;
                    }
                }
                catch { }
            }
        }
    }
}