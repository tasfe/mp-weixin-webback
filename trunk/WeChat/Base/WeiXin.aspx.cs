using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.Base
{
    public partial class WeiXin : LoginPage
    {
        protected string _website = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (helper.GetParam("action").ToLower())
                {
                    case "getlist":
                        List<Wlniao.WeChat.Model.WeiXin> items = db.find<Wlniao.WeChat.Model.WeiXin>("AccountFirst<>''").list();
                        helper.Response(items);
                        break;
                    case "get":
                        Wlniao.WeChat.Model.WeiXin model = null;
                        try
                        {
                            if (string.IsNullOrEmpty(Request["id"]) && Request["new"] != "true")
                            {
                                model = Wlniao.WeChat.Model.WeiXin.findByField("AccountFirst", "");
                            }
                            else
                            {
                                model = Wlniao.WeChat.Model.WeiXin.findById(Convert.ToInt32(Request["id"]));
                            }
                        }
                        catch
                        {
                            model = new Wlniao.WeChat.Model.WeiXin();
                        }
                        helper.Response(model);
                        break;
                    case "set":
                        Wlniao.WeChat.Model.WeiXin weixin = null;
                        try
                        {
                            if (Request["new"] == "true")
                            {
                                if (string.IsNullOrEmpty(helper.GetParam("WeChatName")))
                                {
                                    helper.Result.Add("请填写微信公众帐号名称");
                                }
                                else if (string.IsNullOrEmpty(helper.GetParam("AccountFirst")))
                                {
                                    helper.Result.Add("请填写微信原始帐号");
                                }
                                else
                                {
                                    weixin = new Wlniao.WeChat.Model.WeiXin();
                                    weixin.CreateTime = DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            else if (string.IsNullOrEmpty(Request["id"]))
                            {
                                weixin = Wlniao.WeChat.Model.WeiXin.findByField("AccountFirst", "");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(helper.GetParam("WeChatName")))
                                {
                                    helper.Result.Add("请填写微信公众帐号名称");
                                }
                                else if (string.IsNullOrEmpty(helper.GetParam("AccountFirst")))
                                {
                                    helper.Result.Add("请填写微信原始帐号");
                                }
                                else
                                {
                                    weixin = Wlniao.WeChat.Model.WeiXin.findById(Convert.ToInt32(Request["id"]));
                                }
                            }
                        }
                        catch { weixin = null; }
                        if (helper.Result.IsValid)
                        {
                            if (weixin != null)
                            {
                                weixin.WeChatName = helper.GetParam("WeChatName");
                                weixin.AccountName = helper.GetParam("AccountName");
                                weixin.AccountFirst = helper.GetParam("AccountFirst");
                                weixin.WeChatToken = helper.GetParam("WeChatToken");
                                weixin.DefaultCmd = helper.GetParam("DefaultCmd");
                                weixin.Appid = helper.GetParam("Appid");
                                weixin.Secret = helper.GetParam("Secret");

                                helper.Result.Join(weixin.save());
                                try
                                {
                                    if (!string.IsNullOrEmpty(helper.GetParam("Appid")) || !string.IsNullOrEmpty(helper.GetParam("Secret")))
                                    {
                                        string temp = "";
                                        Wlniao.WeChat.WeixinMP.MP.Init(helper.GetParam("Appid"), helper.GetParam("Secret"), out temp);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    helper.Result.Add("设置保存成功！但APPID校验失败，错误：" + ex.Message);
                                }
                            }
                            else
                            {
                                helper.Result.Add("Sorry,您的设置不能保存！");
                            }
                        }
                        helper.ResponseResult();
                        break;
                    case "del":
                        Wlniao.WeChat.Model.WeiXin del = Wlniao.WeChat.Model.WeiXin.findById(Convert.ToInt32(Request["id"]));
                        if (del == null || del.Id <= 0)
                        {
                            helper.Result.Add("当前帐号已删除！");
                        }
                        else if (string.IsNullOrEmpty(del.AccountFirst))
                        {
                            helper.Result.Add("默认设置无法删除！");
                        }
                        else if (del.delete() <= 0)
                        {
                            helper.Result.Add("帐号删除失败！");
                        }
                        helper.ResponseResult();
                        break;
                    default:
                        if (Request.Url.Port == 80)
                        {
                            _website = Request.Url.Host;
                        }
                        else
                        {
                            _website = Request.Url.Host + ":" + Request.Url.Port;
                        }
                        _website += Request.Url.AbsolutePath.Replace("/base/weixin.aspx", "");
                        break;
                }
            }
        }
    }
}