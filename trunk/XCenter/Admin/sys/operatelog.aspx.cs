using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class operatelog : System.Web.LoginInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    if (helper.GetParam("handle") == "GetList")
                    {
                        #region
                        string key = helper.GetParam("key");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        int userid = 0;
                        try
                        {
                            userid = cvt.ToInt(helper.GetParam("uid"));
                        }
                        catch { }
                        System.DataPage<XCenter.Code.Domain.Sys.SysLog> items = XCenter.Code.Common.Sys.OperateLogService.GetModelListByUserid(userid, pageIndex, pageSize);
                        List<XCenter.Code.Domain.Sys.SysLog> list = items.Results;
                        foreach (XCenter.Code.Domain.Sys.SysLog ol in list)
                        {
                            try
                            {
                                XCenter.Code.Domain.Sys.User ui = XCenter.Code.Common.Sys.UserService.GetModelById(ol.UserId);
                                if (string.IsNullOrEmpty(ui.Nickname))
                                {
                                    ol.NickName = ui.LoginName;
                                }
                                else
                                {
                                    ol.NickName = string.Format("{0}({1})", ui.LoginName, ui.Nickname);
                                }
                            }
                            catch { }
                        }
                        string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(list) + "}";
                        Response.Write(json);
                        Response.End();
                        #endregion
                    }
                }
                #endregion 处理结束
            }
        }
    }
}