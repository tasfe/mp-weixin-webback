using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin
{
    public partial class Login : System.Web.TemplateEngine
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            #region 处理开始
            if (!IsPostBack)
            {
                if (helper.GetParam("handle") == "loginin")
                {
                    string inputstr = helper.GetParam("inputstr");
                    string password = helper.GetParam("password");
                    XCenter.Code.Domain.Sys.User _model = null;
                    try
                    {
                        _model = db.find<XCenter.Code.Domain.Sys.User>(string.Format("LoginName = '{0}' and LoginPassword='{1}'", inputstr, System.Encryptor.Md5Encryptor32(password).ToLower())).first();
                        if (_model != null && _model.LoginName == inputstr)
                        {
                            Session["LoginIn"] = inputstr;
                            Session["UserId"] = _model.Id;
                            helper.Add("success", true);
                            helper.Add("code", "success");
                            helper.Add("msg", "Success!您已登录成功");
                            _model.LoginCount = _model.LoginCount + 1;
                            db.update(_model);
                            if (string.IsNullOrEmpty(_model.Nickname))
                            {
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(_model.Id, "系统基本功能", string.Format("用户【{0}】已成功登陆系统。", _model.LoginName));
                            }
                            else
                            {
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(_model.Id, "系统基本功能", string.Format("用户【{0}({1})】已成功登陆系统。", _model.LoginName, _model.Nickname));
                            }
                        }
                        else
                        {
                            helper.Add("success", false);
                            helper.Add("code", "false");
                            helper.Add("msg", "Sorry!您的用户名或密码错误");
                            XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(_model.Id, "系统基本功能", string.Format("用户【{0}】登陆失败。", inputstr));
                        }
                    }
                    catch
                    {
                        helper.Add("success", false);
                        helper.Add("code", "false");
                        helper.Add("msg", "Sorry!您的用户名或密码错误");
                    }
                    helper.Response();
                }
                else
                {
                    if (cfgHelper.GetAppSettings("Install") != "true")  //进入初始化步骤
                    {                        
                        Response.Redirect("Install.aspx");
                    }
                }
            }
            #endregion 处理结束
        }
    }
}