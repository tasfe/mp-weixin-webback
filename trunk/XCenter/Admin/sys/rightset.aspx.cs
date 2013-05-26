using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.sys
{
    public partial class rightset : System.Web.LoginInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    if (helper.GetParam("handle") == "SaveOne")
                    {
                        #region 保存
                        string ids = helper.GetParam("uid");
                        string code = helper.GetParam("code");
                        XCenter.Code.Domain.Sys.Right _model = null;
                        int rid = int.Parse(helper.GetParam("Id"));
                        bool success = true;
                        string message = string.Empty;
                        try
                        {
                            _model = db.findById<XCenter.Code.Domain.Sys.Right>(rid);
                            if (_model == null && rid > 0)
                            {
                                success = false;
                                message = "Sorry，权限点不存在";
                            }
                            else if (rid == 0)
                            {
                                _model = new XCenter.Code.Domain.Sys.Right();
                                _model.RightCode = code;
                            }
                            if (success)
                            {
                                Result result = new Result();
                                if (!string.IsNullOrEmpty(ids))
                                {
                                    string[] id = strUtil.Split(ids, ",");
                                    foreach (string i in id)
                                    {
                                        XCenter.Code.Domain.Sys.UserRight _userRight = null;
                                        try
                                        {
                                            _userRight = db.find<XCenter.Code.Domain.Sys.UserRight>(string.Format("UserId={0} and RightCode='{1}'", i, code)).first();
                                        }
                                        catch { }
                                        if (_userRight != null)
                                        {
                                            _userRight.DefaultRightValue = int.Parse(helper.GetParam("value"));
                                            result = db.update(_userRight);
                                        }
                                        else
                                        {
                                            _userRight = new XCenter.Code.Domain.Sys.UserRight();
                                            _userRight.HasUserId = int.Parse(i);
                                            _userRight.HasRightCode = _model.RightCode;
                                            _userRight.DefaultRightValue = int.Parse(helper.GetParam("value"));
                                            result = db.insert(_userRight);
                                        }
                                        try
                                        {
                                            XCenter.Code.Domain.Sys.User u = XCenter.Code.Common.Sys.UserService.GetModelById(_userRight.HasUserId);
                                            if (_userRight.DefaultRightValue == 1)
                                            {
                                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "权限设置", string.Format("为用户【{0}】添加了【{1}】权限", u.LoginName, _model.RightName));
                                            }
                                            else
                                            {
                                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "权限设置", string.Format("取消了用户【{0}】的【{1}】权限", u.LoginName, _model.RightName));
                                            }
                                        }
                                        catch { }
                                    }
                                    if (result.IsValid)
                                    {
                                        message = "Success，权限信息设置成功";
                                    }
                                    else
                                    {
                                        success = false;
                                        message = result.ErrorsText;
                                    }
                                }
                                else
                                {
                                    //用户名为空时，修改默认权限
                                    if (rid > 0)
                                    {
                                        _model.DefaultRightValue = int.Parse(helper.GetParam("value"));
                                        result = db.update(_model);
                                        if (result.IsValid)
                                        {
                                            message = "Success，权限信息设置成功";
                                        }
                                        else
                                        {
                                            success = false;
                                            message = result.ErrorsText;
                                        }
                                    }
                                    else
                                    {
                                        success = false;
                                        message = "Success，系统禁止设置默认的栏目管理权限";
                                    }
                                }
                            }
                        }
                        catch
                        {
                            success = false;
                            message = "Sorry，出现未知错误";
                        }
                        helper.Add("success", success);
                        helper.Add("msg", message);
                        helper.Response();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetList")
                    {
                        #region
                        string key = helper.GetParam("key");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string ids = helper.GetParam("uid");
                        string condition = string.Format("1=1 order by Sort desc");
                        System.DataPage<XCenter.Code.Domain.Sys.Right> items = db.findPage<XCenter.Code.Domain.Sys.Right>(condition, pageIndex, pageSize);
                        List<XCenter.Code.Domain.Sys.Right> list = items.Results;
                        #region
                        //栏目权限
                        System.DataPage<XCenter.Code.Domain.News.NewsClass> classitems = db.findPage<XCenter.Code.Domain.News.NewsClass>("1=1 order by Sort desc", 0, int.MaxValue);
                        if (classitems != null && classitems.Results != null)
                        {
                            foreach (XCenter.Code.Domain.News.NewsClass newsclass in classitems.Results)
                            {
                                XCenter.Code.Domain.Sys.Right righttemp = new XCenter.Code.Domain.Sys.Right();
                                if (newsclass.Type == "html")
                                {
                                    righttemp.Comments = "当前栏目类型为HTML单页";
                                }
                                righttemp.RightName = newsclass.ClassName;
                                righttemp.RightType = "栏目编辑";
                                righttemp.RightCode = "ClassEdit-" + newsclass.Id;
                                list.Add(righttemp);
                                righttemp = new XCenter.Code.Domain.Sys.Right();
                                if (newsclass.Type == "list")
                                {
                                    righttemp.Comments = "当前栏目类型为文章列表";
                                }
                                righttemp.RightName = newsclass.ClassName;
                                righttemp.RightType = "文章管理";
                                righttemp.RightCode = "ClassContent-" + newsclass.Id;
                                list.Add(righttemp);
                            }
                        }
                        #endregion
                        if (!string.IsNullOrEmpty(ids))
                        {
                            foreach (XCenter.Code.Domain.Sys.Right _model in list)
                            {
                                string[] id = strUtil.Split(ids, ",");
                                if (id.Length > 1)
                                {
                                    int value = -1;
                                    foreach (string i in id)
                                    {
                                        try
                                        {
                                            XCenter.Code.Domain.Sys.UserRight _userRight = db.find<XCenter.Code.Domain.Sys.UserRight>(string.Format("UserId={0} and RightCode='{1}'", i, _model.RightCode)).first();
                                            if (_userRight != null)
                                            {
                                                if (value >= 0 && _userRight.DefaultRightValue != value)
                                                {
                                                    value = _model.DefaultRightValue;
                                                    break;
                                                }
                                                value = _userRight.DefaultRightValue;
                                            }
                                            else
                                            {
                                                value = _model.DefaultRightValue;
                                                break;
                                            }
                                        }
                                        catch { }
                                    }
                                    _model.DefaultRightValue = value;
                                }
                                else
                                {
                                    try
                                    {
                                        XCenter.Code.Domain.Sys.UserRight _userRight = db.find<XCenter.Code.Domain.Sys.UserRight>(string.Format("UserId={0} and RightCode='{1}'", ids, _model.RightCode)).first();
                                        if (_userRight != null)
                                        {
                                            _model.DefaultRightValue = _userRight.DefaultRightValue;
                                        }
                                    }
                                    catch{}
                                }
                            }
                        }
                        string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(list) + "}";
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