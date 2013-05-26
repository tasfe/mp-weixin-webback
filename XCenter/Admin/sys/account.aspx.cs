using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class account : System.Web.LoginInPage
    {
        protected override void OnLoad(EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            if (helper.GetParam("handle").ToLower() == "checkright")
            {
                bool success = XCenter.Code.Global.checkRightHandle(helper.GetParam("code"));
                if (success)
                {
                    helper.Add("success", success);
                    helper.Response();
                }
            }
            else if (helper.GetParam("handle").ToLower() == "loginshow")
            {
                bool success = false;
                string message = string.Empty;
                if (System.Web.HttpContext.Current.Session["LoginIn"] == null || string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["LoginIn"].ToString()))
                {
                    helper.Add("code", "logout");
                    message = lang.get("YouLoginIsTimeOut");
                }
                else
                {
                    string loginname = System.Web.HttpContext.Current.Session["LoginIn"].ToString();
                    XCenter.Code.Domain.Sys.User ui = XCenter.Code.Common.Sys.UserService.GetModelByLoginName(loginname);
                    if (ui != null)
                    {
                        success = true;
                        helper.Add("code", "success");
                        helper.Add("logincount", ui.LoginCount);
                        if (string.IsNullOrEmpty(ui.Nickname))
                        {
                            helper.Add("username", loginname);
                        }
                        else
                        {
                            helper.Add("username", ui.Nickname);
                        }
                    }
                    else
                    {
                        helper.Add("code", "usernotexists");
                        message = lang.get("YouLoginIsTimeOut");
                        helper.Add("username", loginname);
                        helper.Add("logincount", 0);
                    }
                }
                helper.Add("success", success);
                helper.Add("msg", message);
                helper.Response();
            }
            else
            {
                base.OnLoad(e);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    if (helper.GetParam("handle").ToLower() == "saveone")
                    {
                        #region 保存
                        string data = HttpUtility.UrlDecode(Request["data"], new System.Text.UTF8Encoding());
                        helper.InitParam(System.Serialization.JSON.ToDictionary(data));
                        bool success = true;
                        string message = "";
                        XCenter.Code.Domain.Sys.User _model = null;
                        try
                        {
                            _model = db.findById<XCenter.Code.Domain.Sys.User>(int.Parse(helper.GetParam("Id")));
                        }
                        catch { }
                        if (_model == null)
                        {
                            _model = new XCenter.Code.Domain.Sys.User();
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("LoginName")))
                        {
                            success = false;
                            message = "用户名未填写，请填写";
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("Nickname")))
                        {
                            success = false;
                            message = "用户姓名未填写，请填写";
                        }
                        else if (_model.Id == 0 && string.IsNullOrEmpty(helper.GetParam("LoginPassword")))
                        {
                            success = false;
                            message = "用户密码未填写，请填写";
                        }
                        _model.LoginName = helper.GetParam("LoginName");
                        _model.LoginPassword = helper.GetParam("LoginPassword");
                        if (_model.LoginPassword.Length != 32)
                        {
                            _model.LoginPassword = System.Encryptor.Md5Encryptor32(_model.LoginPassword).ToLower();
                        }
                        _model.Nickname = helper.GetParam("Nickname");
                        _model.Mobile = helper.GetParam("Mobile");
                        _model.QQ = helper.GetParam("QQ");
                        _model.Email = helper.GetParam("Email");
                        try
                        {
                            _model.Birthday = Convert.ToDateTime(helper.GetParam("Birthday"));
                        }
                        catch { }
                        _model.IsAdmin = helper.GetParam("IsAdmin");
                        if (success)
                        {
                            Result result;
                            if (_model.Id == 0)
                            {
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "用户管理", string.Format("添加新用户【{0}】。", _model.LoginName));
                                result = db.insert(_model);
                            }
                            else
                            {
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "用户管理", string.Format("更新用户【{0}】的账号信息。", _model.LoginName));
                                result = db.update(_model);
                            }
                            if (result.IsValid)
                            {
                                message = "Success，用户信息保存成功";
                            }
                            else
                            {
                                success = false;
                                message = result.ErrorsText;
                            }
                        }
                        helper.Add("success", success);
                        helper.Add("msg", message);
                        helper.Response();
                        #endregion
                    }
                    else if (helper.GetParam("handle").ToLower() == "getone")
                    {
                        #region
                        string id = helper.GetParam("id");
                        XCenter.Code.Domain.Sys.User _model = db.findById<XCenter.Code.Domain.Sys.User>(int.Parse(id));
                        helper.Response(System.Json.ToString(_model));
                        #endregion
                    }
                    else if (helper.GetParam("handle").ToLower() == "getlist")
                    {
                        #region
                        string key = helper.GetParam("key");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string condition = string.Format("1=1");
                        if (!string.IsNullOrEmpty(key))
                        {
                            condition = string.Format("LoginName like '%{0}%' or Nickname like '%{0}%'", key);
                        }
                        System.DataPage<XCenter.Code.Domain.Sys.User> items = db.findPage<XCenter.Code.Domain.Sys.User>(condition, pageIndex, pageSize);
                        string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}";
                        Response.Write(json);
                        Response.End();
                        #endregion
                    }
                    else if (helper.GetParam("handle").ToLower() == "del")
                    {
                        #region
                        string idstr = helper.GetParam("ids");
                        string[] ids = idstr.Split(new char[] { ',' });
                        int success = 0, failed = 0;
                        string message = string.Empty;
                        foreach (string id in ids)
                        {
                            XCenter.Code.Domain.Sys.User _model = db.findById<XCenter.Code.Domain.Sys.User>(int.Parse(id));
                            if (_model.LoginName != cfgHelper.GetAppSettings("Administrator"))
                            {
                                if (db.delete<XCenter.Code.Domain.Sys.User>(int.Parse(id)) > 0)
                                {
                                    success++;
                                }
                                else
                                {
                                    failed++;
                                }
                            }
                            else
                            {
                                message = "，超级管理员禁止删除";
                                failed++;
                            }
                        }
                        helper.Add("success", true);
                        helper.Add("msg", string.Format("删除完毕！成功{0}条，失败{1}条{2}。", success, failed, message));
                        helper.Response();
                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "用户管理", string.Format("删除{0}个用户", success));
                        #endregion
                    }
                }
                #endregion 处理结束
            }
        }
    }
}