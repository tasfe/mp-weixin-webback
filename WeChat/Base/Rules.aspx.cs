using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class Rules : LoginPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "del":
                        try
                        {
                            if (Wlniao.WeChat.Model.Rules.findByField("StrGuid", helper.GetParam("Guid")).delete() <= 0)
                            {
                                helper.Result.Add("Sorry,删除失败");
                            }
                            else
                            {
                                List<Wlniao.WeChat.Model.RuleCode> listDel = Wlniao.WeChat.Model.RuleCode.findListByField("RuleGuid", helper.GetParam("Guid"));
                                if (listDel != null && listDel.Count > 0)
                                {
                                    foreach (Wlniao.WeChat.Model.RuleCode code in listDel)
                                    {
                                        if (code.delete() <= 0)
                                        {
                                            helper.Result.Add("Sorry,规则删除失败！");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "getlist":
                        int pageIndex = 0;
                        int pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }

                        System.DataPage<Wlniao.WeChat.Model.Rules> items = db.findPage<Wlniao.WeChat.Model.Rules>("DoMethod=''", pageIndex, pageSize);
                        try
                        {
                            foreach (Wlniao.WeChat.Model.Rules rule in items.Results)
                            {
                                try
                                {

                                }
                                catch { }
                            }
                        }
                        catch { }
                        List<Wlniao.WeChat.Model.Rules> list = null;
                        list = items.Results;
                        if (list == null)
                        {
                            list = new List<Wlniao.WeChat.Model.Rules>();
                        }
                        helper.Response("{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}");
                        break;
                }
            }
        }
    }
}