using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.tag
{
    public partial class tags : System.Web.LoginInPage
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
                        XCenter.Code.Domain.Tag _model = null;
                        try
                        {
                            _model = GetModelById(helper.GetParam("Id"));
                        }
                        catch { }
                        if (_model == null)
                        {
                            _model = new XCenter.Code.Domain.Tag();
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("TagName")))
                        {
                            success = false;
                            message = "标签名称未填写，请填写";
                        }
                        if (string.IsNullOrEmpty(helper.GetParam("Content")))
                        {
                            success = false;
                            message = "标签内容未填写，请填写";
                        }
                        _model.TagName = helper.GetParam("TagName");
                        _model.Content = helper.GetParam("Content");
                        _model.Comments = helper.GetParam("Comments");
                        if (success)
                        {
                            Result result = new Result();
                            if (_model.Id == 0)
                            {
                                if (CreateModel(_model.TagName, _model.Content, _model.Comments))
                                {
                                    XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "标签管理", string.Format("添加了新的标签【{0}】", _model.TagName));
                                    message = "Success，标签信息保存成功";
                                }
                                else
                                {
                                    success = false;
                                    message = result.ErrorsText;
                                }
                            }
                            else
                            {
                                if (UpdateModel(_model.Id.ToString(), _model.TagName, _model.Content, _model.Comments))
                                {
                                    XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "标签管理", string.Format("更新了标签【{0}】", _model.TagName));
                                    message = "Success，标签信息保存成功";
                                }
                                else
                                {
                                    success = false;
                                    message = result.ErrorsText;
                                }
                            }
                        }
                        System.TemplateEngine.TempInfo.UpdateTags();
                        helper.Add("success", success);
                        helper.Add("msg", message);
                        helper.Response();
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetOne")
                    {
                        #region
                        string id = helper.GetParam("id");
                        XCenter.Code.Domain.Tag _model = GetModelById(id);
                        helper.Response(System.Json.ToString(_model));
                        #endregion
                    }
                    else if (helper.GetParam("handle") == "GetList")
                    {
                        #region
                        string key = helper.GetParam("key");
                        int pageIndex = int.Parse(helper.GetParam("pageIndex"));
                        int pageSize = int.Parse(helper.GetParam("pageSize"));
                        int pagecount = 0;
                        int recordcount = 0;
                        string sortField = helper.GetParam("sortField");
                        string sortOrder = helper.GetParam("sortOrder");
                        string condition = string.Format("1=1");
                        List<XCenter.Code.Domain.Tag> items = GetModelList(pageIndex + 1, pageSize, out pagecount, out recordcount);
                        string json;
                        if (items.Count > 0)
                        {
                            json = "{total:" + recordcount + ",data:" + Json.ToStringList(items) + "}";
                        }
                        else
                        {
                            json = "{total:0,data:[]}";
                        }
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
                        String path = System.PathHelper.Map(cfgHelper.FrameworkRoot + "template/" + System.TemplateEngine.TeConfig.Instance.DefaultSkin + "/skin.xml");
                        foreach (string id in ids)
                        {
                            XCenter.Code.XmlParamter xpid = new XCenter.Code.XmlParamter("Id", id);
                            xpid.Direction = XCenter.Code.ParameterDirection.Equal;
                            if (XCenter.Code.XMLHelper.DeleteData(path, "Tag", xpid) > 0)
                            {
                                success++;
                            }
                            else
                            {
                                failed++;
                            }
                        }
                        System.TemplateEngine.TempInfo.UpdateTags();
                        helper.Add("success", true);
                        helper.Add("msg", string.Format("删除完毕！成功{0}条，失败{1}条。", success, failed));
                        helper.Response();
                        XCenter.Code.Common.Sys.OperateLogService.AddOperateLog(GetUserId(), "标签管理", string.Format("删除了{0}个标签", success));
                        #endregion
                    }
                }
                #endregion 处理结束
            }
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>对象的List集合</returns>
        public static List<XCenter.Code.Domain.Tag> GetModelList(Int32 pageindex, Int32 pagesize, out Int32 pagecount, out Int32 recordcount)
        {
            String path = System.PathHelper.Map(cfgHelper.FrameworkRoot + "template/" + System.TemplateEngine.TeConfig.Instance.DefaultSkin + "/skin.xml");
            if (!System.file.Exists(path))
            {
                XCenter.Code.XmlParamter xpname = new XCenter.Code.XmlParamter("TagName", "NoTag");
                XCenter.Code.XmlParamter xpcontent = new XCenter.Code.XmlParamter("Content", "You Hava No Tag");
                XCenter.Code.XmlParamter xpcomments = new XCenter.Code.XmlParamter("Comments", "System Default Data");
                XCenter.Code.XmlParamter xpaddtime = new XCenter.Code.XmlParamter("AddTime", DateTime.Now.ToString("yyyy-MM-dd"));
                XCenter.Code.XMLHelper.AddData(path, "Tag", xpname, xpcontent, xpcomments, xpaddtime);
            }
            System.Data.DataTable dt = XCenter.Code.XMLHelper.GetData(path, "Tag");
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    List<XCenter.Code.Domain.Tag> taglist = new List<XCenter.Code.Domain.Tag>();
                    recordcount = dt.Rows.Count;
                    pagecount = dt.Rows.Count / pagesize + 1;
                    pageindex = pageindex * pagesize - pagesize;
                    for (int i = 0; i < pagesize; i++)
                    {
                        try
                        {
                            XCenter.Code.Domain.Tag tag = new XCenter.Code.Domain.Tag();
                            tag.Id = Convert.ToInt32(dt.Rows[pageindex]["Id"]);
                            tag.TagName = dt.Rows[pageindex]["TagName"].ToString();
                            tag.Content = strUtil.HtmlDecode(dt.Rows[pageindex]["Content"].ToString());
                            tag.Comments = dt.Rows[pageindex]["Comments"].ToString();
                            tag.AddTime = Convert.ToDateTime(dt.Rows[pageindex]["AddTime"]);
                            taglist.Add(tag);
                            pageindex++;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    return taglist;
                }
                catch
                {
                    recordcount = 0;
                    pagecount = 1;
                    return null;
                }
            }
            else
            {
                recordcount = 0;
                pagecount = 1;
                return null;
            }
        }


        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="appid">序ID</param>
        /// <returns>Model对象</returns>
        public static XCenter.Code.Domain.Tag GetModelById(String id)
        {
            XCenter.Code.XmlParamter xpid = new XCenter.Code.XmlParamter("Id", id);
            xpid.Direction = XCenter.Code.ParameterDirection.Equal;
            String path = System.PathHelper.Map(cfgHelper.FrameworkRoot + "template/" + System.TemplateEngine.TeConfig.Instance.DefaultSkin + "/skin.xml");
            System.Xml.XmlNode xn = XCenter.Code.XMLHelper.GetDataOne(path, "Tag", xpid);
            if (xn == null)
            {
                return null;
            }
            else
            {
                XCenter.Code.Domain.Tag tag = new XCenter.Code.Domain.Tag();
                tag.Id = int.Parse(id);
                tag.TagName = xn.Attributes["TagName"].Value;
                tag.Content = strUtil.HtmlDecode(xn.Attributes["Content"].Value);
                tag.Comments = xn.Attributes["Comments"].Value;
                tag.AddTime = Convert.ToDateTime(xn.Attributes["AddTime"].Value);
                return tag;
            }
        }

        /// <summary>
        /// 创建一个新的对象
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns>true或false</returns>
        public static Boolean CreateModel(String name, String content, String comments)
        {
            XCenter.Code.XmlParamter xpname = new XCenter.Code.XmlParamter("TagName", name);
            XCenter.Code.XmlParamter xpcontent = new XCenter.Code.XmlParamter("Content", content);
            XCenter.Code.XmlParamter xpcomments = new XCenter.Code.XmlParamter("Comments", comments);
            XCenter.Code.XmlParamter xpaddtime = new XCenter.Code.XmlParamter("AddTime", DateTime.Now.ToString("yyyy-MM-dd"));
            String path = System.PathHelper.Map(cfgHelper.FrameworkRoot + "template/" + System.TemplateEngine.TeConfig.Instance.DefaultSkin + "/skin.xml");
            return XCenter.Code.XMLHelper.AddData(path, "Tag", xpname, xpcontent, xpcomments, xpaddtime) > 0;
        }
        /// <summary>
        /// 修改对象信息
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns>true或false</returns>
        public static Boolean UpdateModel(String id, String name, String content, String comments)
        {
            XCenter.Code.XmlParamter xpid = new XCenter.Code.XmlParamter("Id", id);
            XCenter.Code.XmlParamter xpname = new XCenter.Code.XmlParamter("TagName", name);
            XCenter.Code.XmlParamter xpcontent = new XCenter.Code.XmlParamter("Content", content);
            XCenter.Code.XmlParamter xpcomments = new XCenter.Code.XmlParamter("Comments", comments);
            xpid.Direction = XCenter.Code.ParameterDirection.Equal;
            xpname.Direction = XCenter.Code.ParameterDirection.Update;
            xpcontent.Direction = XCenter.Code.ParameterDirection.Update;
            xpcomments.Direction = XCenter.Code.ParameterDirection.Update;
            String path = System.PathHelper.Map(cfgHelper.FrameworkRoot + "template/" + System.TemplateEngine.TeConfig.Instance.DefaultSkin + "/skin.xml");
            return XCenter.Code.XMLHelper.UpdateData(path, "Tag", xpid, xpname, xpcontent, xpcomments) > 0;
        }

    }
}