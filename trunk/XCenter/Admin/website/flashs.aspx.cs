using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class flashs : System.Web.LoginInPage
    {
        public string typelist = string.Empty;
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
                        string data = HttpUtility.UrlDecode(Request["data"], new System.Text.UTF8Encoding());
                        helper.InitParam(System.Serialization.JSON.ToDictionary(data));
                        bool success = true;
                        string message = "";
                        XCenter.Code.Domain.WebSite.FlashPic _model = null;
                        try
                        {
                            _model = db.findById<XCenter.Code.Domain.WebSite.FlashPic>(int.Parse(helper.GetParam("Id")));
                        }
                        catch { }
                        if (_model == null)
                        {
                            _model = new XCenter.Code.Domain.WebSite.FlashPic();
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("FlashTitle")))
                        {
                            success = false;
                            message = "标题未填写，请填写";
                        }
                        _model.FlashTitle = helper.GetParam("FlashTitle");
                        _model.Description = helper.GetParam("Description");
                        _model.Url = helper.GetParam("Url");
                        try
                        {
                            _model.FlashType = helper.GetParam("FlashType");
                        }
                        catch { }
                        try
                        {
                            _model.OnTop = int.Parse(helper.GetParam("OnTop"));
                        }
                        catch { }
                        try
                        {
                            _model.OnStop = int.Parse(helper.GetParam("OnStop"));
                        }
                        catch { }
                        try
                        {
                            _model.Sort = int.Parse(helper.GetParam("Sort"));
                        }
                        catch { }
                        _model.Url = helper.GetParam("Url");
                        if (!string.IsNullOrEmpty(helper.GetParam("ThumbPic")))
                        {
                            if (helper.GetParam("ThumbPic") == "ClearImg")
                            {
                                _model.Pic = "";
                                _model.ThumbPic = "";
                            }
                            else
                            {
                                _model.Pic = helper.GetParam("Pic");
                                _model.ThumbPic = helper.GetParam("ThumbPic");
                            }
                        }
                        if (success)
                        {
                            Result result;
                            if (_model.Id == 0)
                            {
                                result = db.insert(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "轮换图片", string.Format("添加了新的轮换图片【{0}】", _model.FlashTitle));
                            }
                            else
                            {
                                result = db.update(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "轮换图片", string.Format("编辑了轮换图片【{0}】", _model.FlashTitle));
                            }
                            if (result.IsValid)
                            {
                                message = "Success，内容信息保存成功";
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
                    else if (helper.GetParam("handle") == "GetOne")
                    {
                        #region
                        string id = helper.GetParam("id");
                        XCenter.Code.Domain.WebSite.FlashPic _model = db.findById<XCenter.Code.Domain.WebSite.FlashPic>(int.Parse(id));
                        helper.Response(System.Json.ToString(_model));
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetList")
                    {
                        #region
                        string classid = helper.GetParam("classid");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        int recordCount = 0;
                        string type = helper.GetParam("type");
                        List<XCenter.Code.Domain.WebSite.FlashPic> items = XCenter.Code.Common.WebSite.FlashPicService.GetModelListByType(type, pageIndex, pageSize, out recordCount);
                        string json = "{total:" + recordCount + ",data:" + Json.ToStringList(items) + "}";
                        Response.Write(json);
                        Response.End();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "Del")
                    {
                        #region
                        string idstr = helper.GetParam("ids");
                        string[] ids = idstr.Split(new char[] { ',' });
                        int success = 0, failed = 0;
                        foreach (string id in ids)
                        {
                            if (db.delete<XCenter.Code.Domain.WebSite.FlashPic>(int.Parse(id)) > 0)
                            {
                                success++;
                            }
                            else
                            {
                                failed++;
                            }
                        }
                        helper.Add("success", true);
                        helper.Add("msg", string.Format("删除完毕！成功{0}条，失败{1}条。", success, failed));
                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "轮换图片", string.Format("删除了{0}条轮换图片", success));
                        helper.Response();
                        #endregion
                    }
                    else
                    {
                        try
                        {
                            string FLASHPICTYPE = lang.getBySkin("FLASHPICTYPE");
                            string[] types = FLASHPICTYPE.Split(new char[] { ';', ',', '|' });
                            foreach (string type in types)
                            {
                                typelist += string.Format("<option value=\"{0}\">{0}</option>", type);
                            }
                        }
                        catch { }
                    }
                }
                #endregion 处理结束
            }
        }
    }
}