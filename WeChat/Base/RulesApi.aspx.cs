using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class RulesApi : LoginPage
    {
        protected string _WeiXin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _WeiXin = "";
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "getlist":
                        int pageIndex = 0;
                        int pageSize = int.MaxValue;
                        try
                        {
                            pageIndex = int.Parse(helper.GetParam("pageIndex"));
                            pageSize = int.Parse(helper.GetParam("pageSize"));
                        }
                        catch { }
                        System.DataPage<Wlniao.WeChat.Model.Rules> items = null;
                        if (string.IsNullOrEmpty(helper.GetParam("firstId")))
                        {
                            items = db.findPage<Wlniao.WeChat.Model.Rules>("RuleType=2 ", pageIndex, pageSize);
                        }
                        else
                        {
                            items = db.findPage<Wlniao.WeChat.Model.Rules>("RuleType=2 and (AccountFirst='' or AccountFirst='" + helper.GetParam("firstId") + "')", pageIndex, pageSize);
                        }
                        foreach (var item in items.Results)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(item.AccountFirst))
                                {
                                    item.AccountFirst = Wlniao.WeChat.Model.WeiXin.findByField("AccountFirst", item.AccountFirst).WeChatName;
                                }
                            }
                            catch { }
                        }
                        helper.Response("{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}");
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