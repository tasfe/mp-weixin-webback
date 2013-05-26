using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class RulesApiForm : LoginPage
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
                            rulesSet.RuleType = 2;
                        }
                        rulesSet.RuleName = helper.GetParam("RuleName");
                        rulesSet.AccountFirst = helper.GetParam("AccountFirst");
                        Wlniao.WeChat.Model.RulesApiConfig config = new Wlniao.WeChat.Model.RulesApiConfig();
                        config.ApiUrl = helper.GetParam("ApiUrl");
                        config.BaseArgs = helper.GetParam("BaseArgs");
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