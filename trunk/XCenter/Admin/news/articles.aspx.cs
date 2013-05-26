using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class articles : System.Web.LoginInPage
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
                        string data = HttpUtility.UrlDecode(Request["data"], new System.Text.UTF8Encoding());
                        helper.InitParam(System.Serialization.JSON.ToDictionary(data));
                        bool success = true;
                        string message = "";
                        XCenter.Code.Domain.News.Article _model = null;
                        try
                        {
                            _model = db.findById<XCenter.Code.Domain.News.Article>(int.Parse(helper.GetParam("Id")));
                        }
                        catch { }
                        if (_model == null)
                        {
                            _model = new XCenter.Code.Domain.News.Article();
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("Title")))
                        {
                            success = false;
                            message = "标题未填写，请填写";
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("ClassId")))
                        {
                            success = false;
                            message = "栏目未选择，请选择";
                        }
                        _model.Title = helper.GetParam("Title");
                        try
                        {
                            _model.ClassId = int.Parse(helper.GetParam("ClassId"));
                        }
                        catch { }
                        try
                        {
                            _model.OnTop = int.Parse(helper.GetParam("OnTop"));
                        }
                        catch { }
                        try
                        {
                            _model.ClickNum = int.Parse(helper.GetParam("ClickNum"));
                        }
                        catch { }
                        _model.Subtitle = helper.GetParam("Subtitle");
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
                        _model.Tags = helper.GetParam("Tags");
                        _model.Source = helper.GetParam("Source");

                        try
                        {
                            string ids = string.Empty;
                            Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData> uploadedFiles = XCenter.Web.xcenter.Static.fileupload.fileupload.GetSessionStore(HttpContext.Current);
                            Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData>.Enumerator em = uploadedFiles.GetEnumerator();
                            while (em.MoveNext())
                            {
                                ids += em.Current.Value.Id.ToString() + "|";
                            }
                            _model.PicIds = ids.Remove(ids.LastIndexOf('|'));
                        }
                        catch { }
                        try
                        {
                            _model.AddTime = Convert.ToDateTime(helper.GetParam("AddTime"));
                        }
                        catch
                        {
                            if (_model.Id <= 0)
                            {
                                _model.AddTime = DateTime.Now;
                            }
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("content")))
                        {
                            _model.Content = Request.Form["content"];
                        }
                        else
                        {
                            _model.Content = helper.GetParam("content");
                        }
                        _model.Templet = helper.GetParam("Templet");
                        if (success)
                        {
                            Result result;
                            if (_model.Id == 0)
                            {
                                result = db.insert(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "内容管理", string.Format("添加了新的内容【{0}】", _model.Title));
                            }
                            else
                            {
                                result = db.update(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "内容管理", string.Format("编辑了内容【{0}】", _model.Title));
                            }
                            if (result.IsValid)
                            {
                                message = "Success，内容信息保存成功";
                                #region 控制模版引擎中的附加内容
                                try
                                {
                                    TePage tepage = null;
                                    XCenter.Code.Domain.News.NewsClass nc = db.findById<XCenter.Code.Domain.News.NewsClass>(_model.ClassId);
                                    try
                                    {
                                        tepage = TePageUtil.GetByKey(nc.Type + "-" + nc.Id.ToString());
                                    }
                                    catch { }
                                    if (tepage != null)
                                    {
                                        tepage.Sid = nc.Type + "-" + _model.Id;
                                        tepage.Templet = nc.ContentTemplet;
                                        tepage.PageTitle = nc.PageTitle;
                                        tepage.KeyWords = nc.KeyWords;
                                        tepage.Type = nc.Type;
                                        tepage.ClassID = nc.Id.ToString();
                                        tepage.UpdateTime = DateTools.GetNow();
                                        TePageUtil.Edit(tepage);
                                    }
                                    else
                                    {
                                        tepage = new TePage();
                                        tepage.Sid = nc.Type + "-" + _model.Id;
                                        tepage.Templet = nc.ContentTemplet;
                                        tepage.PageTitle = nc.PageTitle;
                                        tepage.KeyWords = nc.KeyWords;
                                        tepage.Type = nc.Type;
                                        tepage.ClassID = nc.Id.ToString();
                                        tepage.UpdateTime = DateTools.GetNow();
                                        TePageUtil.Add(tepage);
                                    }
                                }
                                catch { }
                                System.Caching.SysCache.Put("UploadedFileHandler_Files", new Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData>());
                                #endregion
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
                        XCenter.Code.Domain.News.Article _model = db.findById<XCenter.Code.Domain.News.Article>(int.Parse(id));
                        helper.Response(System.Json.ToString(_model));
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetList")
                    {
                        #region
                        string key = helper.GetParam("key");
                        string classid = helper.GetParam("classid");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string condition = "1=1";
                        string like = " and Title like '%%'";
                        if (!string.IsNullOrEmpty(key))
                        {
                            //like = string.Format(" and (Title like '%{0}%' or Subtitle like '%{0}%' or Tags like '%{0}%' or Content like '%{0}%')", key);
                            like = string.Format(" and (Title like '%{0}%' or Subtitle like '%{0}%' or Tags like '%{0}%' or Source like '%{0}%')", key);
                        }
                        if (!string.IsNullOrEmpty(classid) && classid != "0")
                        {
                            condition = string.Format("{0} and ClassId={1}{2}{3}", condition, classid, like, " order by OnTop desc,AddTime desc");
                        }
                        else
                        {
                            condition = string.Format("{0}{1}{2}", condition, like, " order by OnTop desc,AddTime desc");
                        }
                        System.DataPage<XCenter.Code.Domain.News.Article> items = db.findPage<XCenter.Code.Domain.News.Article>(condition, pageIndex, pageSize);
                        //foreach (XCenter.Code.Domain.News.Article item in items.Results)
                        //{
                        //    if (file.Exists(PathHelper.Map(item.ThumbPic)))
                        //    {
                        //        item.ThumbPic = "是";
                        //    }
                        //}
                        string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}";
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
                            if (db.delete<XCenter.Code.Domain.News.Article>(int.Parse(id)) > 0)
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
                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "内容管理", string.Format("删除了{0}条内容", success));
                        helper.Response();
                        #endregion
                    }
                }
                #endregion 处理结束
            }
        }
    }
}