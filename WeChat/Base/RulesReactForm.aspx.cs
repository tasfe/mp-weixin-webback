using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class RulesReactForm : LoginPage
    {
        protected string _Guid = "";
        protected string _WeiXin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _Guid = Request["guid"];
            _WeiXin = "";
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "get":
                        Wlniao.WeChat.Model.Rules rulesGet = Wlniao.WeChat.BLL.Rules.Get(_Guid);
                        if (rulesGet == null)
                        {
                            rulesGet = new Wlniao.WeChat.Model.Rules();
                        }
                        helper.Response(rulesGet);
                        break;
                    case "set":
                        Wlniao.WeChat.Model.Rules rulesSet = Wlniao.WeChat.BLL.Rules.Get(_Guid);
                        if (rulesSet == null)
                        {
                            rulesSet = new Wlniao.WeChat.Model.Rules();
                            rulesSet.Guid = Guid.NewGuid().ToString();
                            rulesSet.RuleType = 0;
                        }
                        rulesSet.RuleName = helper.GetParam("RuleName");
                        rulesSet.AccountFirst = helper.GetParam("AccountFirst");
                        Wlniao.WeChat.Model.RulesAutoConfig config = new Wlniao.WeChat.Model.RulesAutoConfig();
                        config.ReContent = helper.GetParam("ReContent");
                        config.SendMode = helper.GetParam("SendMode");
                        rulesSet.RuleConfig = Json.ToStringEx(config);
                        if (rulesSet.Id > 0)
                        {
                            helper.Result = rulesSet.update();
                        }
                        else
                        {
                            helper.Result = rulesSet.insert();
                        }
                        helper.ResponseResult();
                        break;
                    case "setcontent":
                        Wlniao.WeChat.Model.RuleContent codeContent = Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", _Guid);
                        if (codeContent == null)
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.AddRuleContent(helper.GetParam("RuleGuid"), helper.GetParam("ContentType"), helper.GetParam("Title"), helper.GetParam("TextContent").Replace("<br/>", "\n"), helper.GetParam("PicUrl"), helper.GetParam("ThumbPicUrl"), helper.GetParam("MusicUrl"), helper.GetParam("LinkUrl"), helper.GetParam("ContentStatus"));
                        }
                        else
                        {
                            helper.Result = Wlniao.WeChat.BLL.Rules.EditRuleContent(_Guid, helper.GetParam("ContentType"), helper.GetParam("Title"), helper.GetParam("TextContent").Replace("<br/>", "\n"), helper.GetParam("PicUrl"), helper.GetParam("ThumbPicUrl"), helper.GetParam("MusicUrl"), helper.GetParam("LinkUrl"), helper.GetParam("ContentStatus"));
                        }
                        helper.ResponseResult();
                        break;
                    case "delcontent":
                        try
                        {
                            if (Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", helper.GetParam("Guid")).delete() <= 0)
                            {
                                helper.Result.Add("Sorry，删除内容失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "stickcontent":
                        try
                        {
							Wlniao.WeChat.Model.RuleContent stick = Wlniao.WeChat.Model.RuleContent.findByField("StrGuid", helper.GetParam("Guid"));
                            stick.LastStick = DateTools.GetNow();
                            stick.update("LastStick");
                        }
                        catch (Exception ex)
                        {
                            helper.Result.Add("错误：" + ex.Message);
                        }
                        helper.ResponseResult();
                        break;
                    case "getcontentlist":
                        int pageIndex = 0;
                        int pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }

                        System.DataPage<Wlniao.WeChat.Model.RuleContent> itemsContent = db.findPage<Wlniao.WeChat.Model.RuleContent>("RuleGuid='" + helper.GetParam("RuleGuid") + "' order by LastStick asc", pageIndex, pageSize);
                        List<Wlniao.WeChat.Model.RuleContent> listContent = itemsContent.Results;
                        if (listContent == null)
                        {
                            listContent = new List<Wlniao.WeChat.Model.RuleContent>();
                        }
                        foreach (Wlniao.WeChat.Model.RuleContent rulecontent in listContent)
                        {
                            rulecontent.TextContent = rulecontent.TextContent.Replace("\n", "<br/>");
                        }
                        helper.Response(listContent);
                        break;
                    default:
                        List<Wlniao.WeChat.Model.WeiXin> weixinS = db.find<Wlniao.WeChat.Model.WeiXin>("AccountFirst<>''").list();
                        foreach (Wlniao.WeChat.Model.WeiXin weixin in weixinS)
                        {
                            _WeiXin += string.Format("<option value=\"" + weixin.AccountFirst + "\">" + weixin.WeChatName + "</option>");
                        }
                        break;
                }
            }
        }
    }
}