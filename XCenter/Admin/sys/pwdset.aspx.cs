using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class pwdset : System.Web.LoginInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    if (helper.GetParam("handle").ToLower() == "changepwd")
                    {
                        #region 保存
                        string data = HttpUtility.UrlDecode(Request["data"], new System.Text.UTF8Encoding());
                        helper.InitParam(System.Serialization.JSON.ToDictionary(data));
                        bool success = true;
                        string message = "";
                        XCenter.Code.Domain.Sys.User _model = null;
                        try
                        {
                            _model = db.find<XCenter.Code.Domain.Sys.User>(string.Format("LoginName='{0}'", GetUserName())).first();
                            if (_model != null)
                            {
                                if (string.IsNullOrEmpty(helper.GetParam("oldPwd")))
                                {
                                    success = false;
                                    message = "旧密码未填写，请填写";
                                }
                                else if (string.IsNullOrEmpty(helper.GetParam("newPwd")))
                                {
                                    success = false;
                                    message = "新密码未填写，请填写";
                                }
                                else if (string.IsNullOrEmpty(helper.GetParam("rePwd")))
                                {
                                    success = false;
                                    message = "请再次输入新密码";
                                }
                                else if (helper.GetParam("newPwd") != helper.GetParam("rePwd"))
                                {
                                    success = false;
                                    message = "两次输入的密码不一致，请返回修改";
                                }
                                if (_model.LoginPassword == System.Encryptor.Md5Encryptor32(helper.GetParam("oldPwd")).ToLower())
                                {
                                    _model.LoginPassword = helper.GetParam("newPwd");
                                    if (_model.LoginPassword.Length != 32)
                                    {
                                        _model.LoginPassword = System.Encryptor.Md5Encryptor32(_model.LoginPassword).ToLower();
                                    }
                                }
                                else
                                {
                                    success = false;
                                    message = "您输入的旧密码不正确，请重试";
                                }
                                if (success)
                                {
                                    Result result = db.update(_model);
                                    if (result.IsValid)
                                    {
                                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "密码修改", string.Format("用户【{0}】修改了登陆密码", _model.LoginName));
                                        message = "密码设置成功，请使用新密码登录";
                                    }
                                    else
                                    {
                                        success = false;
                                        message = result.ErrorsText;
                                    }
                                }
                            }
                            else
                            {
                                success = false;
                                message = "获取当前登录信息失败，请重新登录";
                            }
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            message = ex.Message;
                        }
                        helper.Add("success", success);
                        helper.Add("msg", message);
                        helper.Response();
                        #endregion
                    }
                }
                #endregion 处理结束
            }
        }
    }
}