using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class statistical : System.Web.LoginInPage
    {
        public string typelist = string.Empty;
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
                        string classid = helper.GetParam("classid");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        int recordCount = 0;

                        bool independent=false;
                        string ip="";
                        DateTime begin=DateTime.MinValue; 
                        DateTime end=DateTime.MinValue;

                        System.DataPage<XCenter.Code.Domain.Sys.Statistical> items = XCenter.Code.Common.Sys.StatisticalService.GetModelList(pageIndex, pageSize, independent, ip, begin, end);
                        recordCount = items.RecordCount;
                        string json = "{total:" + recordCount + ",data:" + Json.ToStringList(items.Results) + "}";
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