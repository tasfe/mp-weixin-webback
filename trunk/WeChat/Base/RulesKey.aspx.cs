using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class RulesKey : LoginPage
    {
        protected string _Guid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _Guid = Request["guid"];
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "setcode":
                        Wlniao.WeChat.Model.RuleCode codeSet = Wlniao.WeChat.Model.RuleCode.findByField("StrGuid", _Guid);
                        if (codeSet == null)
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.AddRuleCode(helper.GetParam("Code"), helper.GetParam("RuleGuid"), helper.GetParam("SepType"));
                        }
                        else
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.EditRuleCode(_Guid, helper.GetParam("Code"), helper.GetParam("RuleGuid"), helper.GetParam("SepType"), helper.GetParam("Status"));
                        }
                        helper.ResponseResult();
                        break;
                    case "delcode":
                        try
                        {
                            if (Wlniao.WeChat.Model.RuleCode.findByField("StrGuid", helper.GetParam("Guid")).delete() <= 0)
                            {
                                helper.Result.Add("Sorry，删除失败！");
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

                        System.DataPage<Wlniao.WeChat.Model.RuleCode> items = db.findPage<Wlniao.WeChat.Model.RuleCode>("RuleGuid='" + helper.GetParam("RuleGuid") + "'", pageIndex, pageSize);
                        List<Wlniao.WeChat.Model.RuleCode> list = items.Results;
                        if (list == null)
                        {
                            list = new List<Wlniao.WeChat.Model.RuleCode>();
                        }
                        foreach (Wlniao.WeChat.Model.RuleCode rulecode in list)
                        {
                            rulecode.Code = rulecode.Code.Replace("#", " ").Replace("$", " ").TrimStart().TrimEnd().Replace(" ", ",");
                        }
                        helper.Response(list);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}