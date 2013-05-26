//------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Mobirds.TemplateEngine\MyModule.cs
//	运 行 库：2.0.50727.1882
//	代码功能：请求处理程序
//	最后修改：2011年8月7日 23:35:52
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Security.Cryptography;
namespace System.TemplateEngine
{
    /// <summary>
    /// 请求处理程序，WebConfig中设置由本程序接管请求
    /// 如<add type="System.Web.XCoreModule" name="XCoreModule"/>
    /// 如需要使用CMS系统，则添加<add type="System.TemplateEngine.TeModule" name="TeModule"/>
    /// </summary>
    public class XCoreModule : IHttpModule, System.Web.SessionState.IRequiresSessionState
    {
        public void Dispose() { }
        private static string _HTMLCache = "";
        public bool HTMLCache
        {
            get
            {
                if (string.IsNullOrEmpty(_HTMLCache))
                {
                    _HTMLCache = System.Data.KvTableUtil.GetBool("HTMLCache").ToString();
                }
                return _HTMLCache == "true";
            }
        }
        protected List<KeyValue> parameters = new List<KeyValue>();
        public new void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            String path = context.Request.Path.ToLower();
            String directory = System.IO.Path.GetDirectoryName(path);
            String file = System.IO.Path.GetFileName(path);
            BeginRequest(application, context, context.Response, path, directory, file, "", "");
        }

        public void BeginRequest(HttpApplication application, HttpContext context, HttpResponse response, String path, String directory, String file, String skinname, String templatefile)
        {
            //当前请求的文件扩展名为接管的后缀名或无后缀名时，接管请求
            if ((System.IO.Path.GetExtension(file) == System.TemplateEngine.TeConfig.Instance.PageSuffix || file.IndexOf('.') < 0))
            {
                if (!System.IO.File.Exists(System.IO.PathTool.ServerMap("~/" + path)))
                {
                    if (string.IsNullOrEmpty(templatefile))
                    {
                        try
                        {
                            templatefile = GetTempletPath(path, skinname, context);
                        }
                        catch { }
                    }
                    DefaultModule(context, context.Response, path, directory, file, skinname, templatefile);
                }
            }
        }


        protected void DefaultModule(HttpContext context, HttpResponse response, String path, String directory, String file, String skinname, String templatefile)
        {
            string requestFile = Path.GetFileNameWithoutExtension(path);
            string requestPath = Path.GetDirectoryName(path);
            string html = string.Empty;
            string htmlpath = string.Empty;
            string rowurl = context.Request.RawUrl;
            if (HTMLCache)
            {
                if (string.IsNullOrEmpty(rowurl) || rowurl.IndexOf('.') < 0)
                {
                    htmlpath = PathHelper.Map(string.Format("{0}{1}{2}/{3}{4}", cfgHelper.FrameworkRoot, "htmlcache", rowurl, "default", System.TemplateEngine.TeConfig.Instance.PageSuffix));
                }
                else
                {
                    htmlpath = PathHelper.Map(cfgHelper.FrameworkRoot + "htmlcache/" + rowurl.Replace('?', '_'));
                }
                if (File.Exists(htmlpath))   //启用页面缓存
                {
                    html = FileEx.Read(htmlpath);
                }
            }
            if (string.IsNullOrEmpty(html))
            {
                if (string.IsNullOrEmpty(templatefile))
                {
                    templatefile = System.IO.PathTool.ServerMap(string.Format(@"{0}{1}\{2}\{3}{4}", cfgHelper.FrameworkRoot, System.TemplateEngine.TeConfig.Instance.TemplateFolder, requestPath, System.IO.Path.GetFileNameWithoutExtension(file), System.TemplateEngine.TeConfig.Instance.TemplatePageSuffix));
                }
                //页面文件不存在，根据模板生成网页文件
                if (File.Exists(templatefile))
                {
                    foreach (string key in context.Request.QueryString.AllKeys)
                        parameters.Add(KeyValue.Create(key, context.Request.QueryString[key]));
                    foreach (string key in context.Request.Form.AllKeys)
                        parameters.Add(KeyValue.Create(key, context.Request.Form[key]));
                    System.TemplateEngine.PageBase pagebase = new System.TemplateEngine.PageBase();
                    pagebase.ThisContext = context;
                    pagebase.Params = parameters.ToArray();
                    pagebase.SkinName = skinname;
                    html = pagebase.GetHtml(templatefile);
                    if (HTMLCache)
                    {
                        FileEx.Write(htmlpath, html, true);
                    }
                }
                else
                    context.RewritePath(cfgHelper.WebRoot + "404.htm");
            }
            response.Write(html);
            response.End();
        }

        protected string GetTempletPath(string path, string skinname, HttpContext context)
        {
            string templatefile = string.Empty;
            string requestFile = Path.GetFileNameWithoutExtension(path);
            string requestPath = Path.GetDirectoryName(path);
            if (path.IndexOf('.') < 0)
            {
                requestFile = "default";
                requestPath = Path.GetDirectoryName(path + "/");
            }
            string tempid = context.Request["id"];
            string[] strparams = requestFile.ToLower().Split(new char[] { '-', '_' });
            if (string.IsNullOrEmpty(tempid) && strparams.Length > 1)
            {
                tempid = strparams[1];
            }
            System.TemplateEngine.ClassBase cb = null;
            System.TemplateEngine.ArticleBase ab = null;
            if (TeConfig.Instance.ClassPage.Contains(strparams[0])||strparams.Length==1)
            {
                try
                {
                    Type type = Type.GetType("XCenter.TemplateEngine.Builder.Sys, XCenter.Code", false, true);
                    Action builder = (Action)Activator.CreateInstance(type);
                    builder.CurrentPath = path;
                    try
                    {
                        builder.CurrentClassID = strparams[1];
                    }
                    catch { }
                    cb = Json.ToObject<System.TemplateEngine.ClassBase>(type.InvokeMember("GetNewsClass", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, builder, new object[] { }).ToString());
                }
                catch { }
            }
            if (TeConfig.Instance.ArticlePage.Contains(strparams[0]))
            {
                try
                {
                    Type type = Type.GetType("XCenter.TemplateEngine.Builder.Sys, XCenter.Code", false, true);
                    Action builder = (Action)Activator.CreateInstance(type);
                    builder.CurrentNewsID = tempid;
                    ab = Json.ToObject<System.TemplateEngine.ArticleBase>(type.InvokeMember("GetArticle", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, builder, new object[] { }).ToString());
                }
                catch { }
            }
            if (cb!=null)
            {
                try
                {
                    AddParameter(KeyValue.Create("classid", cb.ClassId), ref parameters);
                    if (string.IsNullOrEmpty(cb.ClassTemplate))
                    {
                        if (!string.IsNullOrEmpty(requestFile))
                        {
                            templatefile = PathHelper.Map(string.Format(@"{5}{0}\{1}{2}\{3}{4}", TeConfig.Instance.TemplateFolder, skinname, requestPath, cb.ClassType, TeConfig.Instance.TemplatePageSuffix, cfgHelper.FrameworkRoot));
                        }
                        else
                        {
                            templatefile = PathHelper.Map(string.Format(@"{5}{0}\{1}{2}\{3}{4}", TeConfig.Instance.TemplateFolder, skinname, requestPath, requestFile, TeConfig.Instance.TemplatePageSuffix, cfgHelper.FrameworkRoot));
                        }
                    }
                    else
                    {
                        templatefile = PathHelper.Map(string.Format(@"{0}{1}\{2}{3}\{4}", cfgHelper.FrameworkRoot, TeConfig.Instance.TemplateFolder, skinname, requestPath, cb.ClassTemplate));
                        if (!file.Exists(templatefile))
                        {
                            templatefile = PathHelper.Map(string.Format(@"{0}{1}\{2}{3}\{4}", cfgHelper.FrameworkRoot, TeConfig.Instance.TemplateFolder, skinname, "", cb.ClassTemplate));
                        }
                    }
                    AddParameter(KeyValue.Create("module", "ClassContent"), ref parameters);
                }
                catch { }
            }
            else if (ab != null)
            {
                try
                {
                    AddParameter(KeyValue.Create("classid", ab.ClassId), ref parameters);
                    try
                    {
                        Type type = Type.GetType("XCenter.TemplateEngine.Builder.Sys, XCenter.Code", false, true);
                        Action builder = (Action)Activator.CreateInstance(type);
                        builder.CurrentClassID = ab.ClassId;
                        cb = Json.ToObject<System.TemplateEngine.ClassBase>(type.InvokeMember("GetNewsClass", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, builder, new object[] { }).ToString());
                        if (!string.IsNullOrEmpty(cb.ContentTemplate))
                        {
                            templatefile = PathHelper.Map(string.Format(@"{0}{1}\{2}{3}{4}", cfgHelper.FrameworkRoot, TeConfig.Instance.TemplateFolder, skinname, requestPath, cb.ContentTemplate));
                            if (!file.Exists(templatefile))
                            {
                                templatefile = PathHelper.Map(string.Format(@"{0}{1}\{2}{3}{4}", cfgHelper.FrameworkRoot, TeConfig.Instance.TemplateFolder, skinname, "", cb.ContentTemplate));
                            }
                        }
                    }
                    catch { }
                    AddParameter(KeyValue.Create("module", "Content"), ref parameters);
                }
                catch { }
            }
            else
            {
                try
                {
                    AddParameter(KeyValue.Create("module", strparams[0]), ref parameters);
                }
                catch { }
            }
            try
            {
                AddParameter(KeyValue.Create("id", tempid), ref parameters);
                AddParameter(KeyValue.Create("file", strparams[0]), ref parameters);
                if (string.IsNullOrEmpty(templatefile))
                    templatefile = PathHelper.Map(string.Format(@"{0}{1}\{2}{3}\{4}{5}", cfgHelper.FrameworkRoot, TeConfig.Instance.TemplateFolder, skinname, requestPath, strparams[0], TeConfig.Instance.TemplatePageSuffix));
                else
                    templatefile = PathHelper.Map(templatefile);
            }
            catch { }
            return templatefile;
        }

        #region 将一个参数加入参数队列
        /// <summary>
        /// 将一个参数加入参数队列
        /// </summary>
        /// <param name="lp">标签参数</param>
        /// <param name="list">列表</param>
        public void AddParameter(KeyValue AblerParam, ref List<KeyValue> list)
        {
            bool flag = true;
            foreach (KeyValue p in list)
            {
                if (p.Key.Equals(AblerParam.Key))
                    flag = false; break;
            }
            if (flag)
                list.Add(AblerParam);
        }
        #endregion
    }
}
