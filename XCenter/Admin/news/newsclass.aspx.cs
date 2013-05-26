using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class newsclass : System.Web.LoginInPage
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
                        XCenter.Code.Domain.News.NewsClass _model = null;
                        try
                        {
                            _model = db.findById<XCenter.Code.Domain.News.NewsClass>(int.Parse(helper.GetParam("Id")));
                        }
                        catch { }
                        if (_model == null)
                        {
                            _model = new XCenter.Code.Domain.News.NewsClass();
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("ClassName")))
                        {
                            success = false;
                            message = "栏目名称未填写，请填写";
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("Type")))
                        {
                            success = false;
                            message = "栏目类型未选择，请选择";
                        }
                        _model.ClassName = helper.GetParam("ClassName");
                        _model.ParentId = helper.GetParam("ParentId");
                        _model.KeyWords = helper.GetParam("KeyWords");
                        _model.PageTitle = helper.GetParam("PageTitle");
                        _model.Url = helper.GetParam("Url");
                        _model.Type = helper.GetParam("Type");
                        try
                        {
                            _model.Sort = int.Parse(helper.GetParam("Sort"));
                        }
                        catch { _model.Sort = 0; }
                        _model.NavShow = helper.GetParam("NavShow");
                        if (string.IsNullOrEmpty(helper.GetParam("content")))
                        {
                            _model.Content = Request.Form["content"];
                        }
                        else
                        {
                            _model.Content = helper.GetParam("content");
                        }
                        _model.Path = helper.GetParam("Path");
                        _model.Templet = helper.GetParam("Templet");
                        _model.ContentTemplet = helper.GetParam("ContentTemplet");
                        if (success)
                        {
                            Result result;
                            if (_model.Id == 0)
                            {
                                result = db.insert(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "栏目管理", string.Format("添加了新栏目【{0}】", _model.ClassName));
                            }
                            else
                            {
                                result = db.update(_model);
                                XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "栏目管理", string.Format("更新了栏目【{0}】", _model.ClassName));
                            }
                            if (result.IsValid)
                            {
                                message = "Success，栏目信息保存成功";
                                #region 控制模版引擎中的附加内容
                                try
                                {
                                    TePage tepage = null;
                                    try
                                    {
                                        tepage = TePageUtil.GetByKey(_model.Type + "-" + _model.Id.ToString());
                                    }
                                    catch { }
                                    if (tepage != null)
                                    {
                                        tepage.Sid = _model.Type + "-" + _model.Id;
                                        tepage.Templet = _model.Templet;
                                        tepage.Path = _model.Path;
                                        tepage.PageTitle = _model.PageTitle;
                                        tepage.KeyWords = _model.KeyWords;
                                        tepage.Type = _model.Type;
                                        tepage.ClassID = _model.Id.ToString();
                                        tepage.UpdateTime = DateTools.GetNow();
                                        TePageUtil.Edit(tepage);
                                    }
                                    else
                                    {
                                        tepage = new TePage();
                                        tepage.Sid = _model.Type + "-" + _model.Id;
                                        tepage.Templet = _model.Templet;
                                        tepage.Path = _model.Path;
                                        tepage.PageTitle = _model.PageTitle;
                                        tepage.KeyWords = _model.KeyWords;
                                        tepage.Type = _model.Type;
                                        tepage.ClassID = _model.Id.ToString();
                                        tepage.UpdateTime = DateTools.GetNow();
                                        TePageUtil.Add(tepage);
                                    }
                                }
                                catch { }
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
                        XCenter.Code.Domain.News.NewsClass _model = db.findById<XCenter.Code.Domain.News.NewsClass>(int.Parse(id));
                        helper.Response(System.Json.ToString(_model));
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
                        string condition = string.Format("1=1 order by Sort desc");
                        System.DataPage<XCenter.Code.Domain.News.NewsClass> items = db.findPage<XCenter.Code.Domain.News.NewsClass>(condition, pageIndex, pageSize);
                        string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}";
                        Response.Write(json);
                        Response.End();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetTree")
                    {
                        #region
                        string key = helper.GetParam("key");
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string condition = string.Format("1=1 order by Sort desc");
                        System.DataPage<XCenter.Code.Domain.News.NewsClass> items = db.findPage<XCenter.Code.Domain.News.NewsClass>(condition, 0, int.MaxValue);

                        Response.Write(Json.ToStringList(items.Results));
                        Response.End();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "OutlookTree")
                    {
                        #region
                        string key = helper.GetParam("key");
                        string condition = string.Format("1=1 order by Sort desc");
                        System.DataPage<XCenter.Code.Domain.News.NewsClass> items = db.findPage<XCenter.Code.Domain.News.NewsClass>(condition, 0, int.MaxValue);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendFormat("{{\"id\":\"0\",\"text\":\"栏目列表\"}}");
                        foreach (XCenter.Code.Domain.News.NewsClass item in items.Results)
                        {
                            sb.AppendFormat(",{{\"id\":\"{0}\",\"text\":\"{1}\"{2}}}", item.Id, item.ClassName, string.Format(",\"pid\":\"{0}\"", string.IsNullOrEmpty(item.ParentId) ? "0" : item.ParentId));
                        }
                        Response.Write("[" + sb.ToString() + "]");
                        Response.End();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetTreeGrid")
                    {
                        #region
                        string key = helper.GetParam("key");
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string condition = string.Format("1=1 order by Sort desc");
                        System.DataPage<XCenter.Code.Domain.News.NewsClass> items = db.findPage<XCenter.Code.Domain.News.NewsClass>(condition, 0, int.MaxValue);
                        //string json = "{total:" + items.RecordCount + ",data:" + Json.ToStringList(items.Results) + "}";
                        //Response.Write(Json.ToStringList(items.Results));
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        foreach (XCenter.Code.Domain.News.NewsClass item in items.Results)
                        {
                            if (sb.Length > 0)
                            {
                                sb.AppendFormat(",{{\"Id\":\"{0}\",\"Type\":\"{1}\",\"ParentId\":\"{2}\",\"ClassName\":\"{3}\",\"Sort\":\"{4}\",\"NavShow\":\"{5}\"}}", item.Id, item.Type, string.IsNullOrEmpty(item.ParentId) ? "-1" : item.ParentId, item.ClassName, item.Sort, item.NavShow);
                            }
                            else
                            {
                                sb.AppendFormat("{{\"Id\":\"{0}\",\"Type\":\"{1}\",\"ParentId\":\"{2}\",\"ClassName\":\"{3}\",\"Sort\":\"{4}\",\"NavShow\":\"{5}\"}}", item.Id, item.Type, string.IsNullOrEmpty(item.ParentId) ? "-1" : item.ParentId, item.ClassName, item.Sort, item.NavShow);
                            }
                        }
                        Response.Write("[" + sb.ToString() + "]");
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
                            if (db.delete<XCenter.Code.Domain.News.NewsClass>(int.Parse(id)) > 0)
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
                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "栏目管理","删除了栏目。");
                        helper.Response();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetTemplate")
                    {
                        List<TemplateFile> list = new List<TemplateFile>();
                        string skinpath = PathHelper.Map(cfgHelper.FrameworkRoot + System.TemplateEngine.TeConfig.Instance.TemplateFolder + "\\" + System.TemplateEngine.TeConfig.Instance.DefaultSkin);

                        string[] temppaths = System.IO.Directory.GetDirectories(skinpath, "*", System.IO.SearchOption.AllDirectories);
                        foreach (string temppath in temppaths)
                        {
                            if (System.IO.Directory.GetFiles(temppath, "*" + System.TemplateEngine.TeConfig.Instance.TemplatePageSuffix, System.IO.SearchOption.AllDirectories).Length > 0)
                            {
                                TemplateFile tf = new TemplateFile();
                                tf.DicPath = "";
                                tf.Path = temppath.Replace(skinpath, "");
                                tf.FileName = temppath.Replace(skinpath, "");
                                list.Add(tf);
                            }
                        }
                        temppaths = System.IO.Directory.GetFiles(skinpath, "*" + System.TemplateEngine.TeConfig.Instance.TemplatePageSuffix, System.IO.SearchOption.AllDirectories);
                        foreach (string temppath in temppaths)
                        {
                            TemplateFile tf = new TemplateFile();
                            tf.FileName = System.IO.Path.GetFileName(temppath);
                            tf.Path = temppath.Replace(skinpath, "");
                            tf.DicPath = System.IO.Path.GetDirectoryName(temppath).Replace(skinpath, "");
                            list.Add(tf);
                        }
                        Response.Write(Json.ToStringList(list));
                        Response.End();
                    }
                }
                #endregion 处理结束
            }
        }
        public class TemplateFile
        {
            public string FileName { get; set; }
            public string Path { get; set; }
            public string DicPath { get; set; }
        }
    }
}