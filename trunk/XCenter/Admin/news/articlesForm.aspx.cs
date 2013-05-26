using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class articlesForm : System.Web.LoginInPage
    {
        public string edContent = string.Empty;
        public string imgThumbPic = "../../xcenter/static/assets/defaultPic.png";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                try
                {
                    XCenter.Code.Domain.News.Article nc = db.findById<XCenter.Code.Domain.News.Article>(int.Parse(Request["id"]));
                    edContent = HttpUtility.HtmlDecode(nc.Content);
                    if (file.Exists(PathHelper.Map(nc.ThumbPic)))
                    {
                        imgThumbPic = nc.ThumbPic;
                    }
                    Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData> uploadedFiles = new Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData>();
                    string[] ids = nc.PicIds.Split(new char[] { '|' });
                    foreach (string id in ids)
                    {
                        XCenter.Code.Domain.UploadFile uf = db.findById<XCenter.Code.Domain.UploadFile>(int.Parse(id));
                        if (uf != null)
                        {
                            var fileData = new XCenter.Web.xcenter.Static.fileupload.FileData
                            {
                                Id = uf.Id,
                                FileName = uf.FileName,
                                FileSize = uf.FileSize,
                                MiniPath = uf.MiniPath,
                                SavePath = uf.SavePath,
                                FileTitle = uf.FileTitle,
                                FileKey = uf.FileKey,
                                Description = ""
                            };
                            uploadedFiles.Add(uf.FileKey, fileData);
                        }
                    }
                    System.Caching.SysCache.Put("UploadedFileHandler_Files", uploadedFiles);
                }
                catch { }
            }
            else
            {
                System.Caching.SysCache.Put("UploadedFileHandler_Files", new Dictionary<string, XCenter.Web.xcenter.Static.fileupload.FileData>());
            }
        }
    }
}