using System;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin.news
{
    public partial class upload : System.Web.LoginInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AshxHelper helper = new AshxHelper(Context);
                #region 处理开始
                if (!IsPostBack)
                {
                    foreach (var key in Context.Request.Files.AllKeys)
                    {
                        var file = Context.Request.Files[key];
                        int width = 0;
                        int height = 0;
                        width = cvt.ToInt(helper.GetParam("width"));
                        height = cvt.ToInt(helper.GetParam("height"));
                        string miniPath = string.Empty;
                        var savePath = XCenter.Web.xcenter.Static.fileupload.fileupload.SaveUploadToDisk(file, width, height, out miniPath);
                        if (string.IsNullOrEmpty(miniPath))
                        {
                            miniPath = savePath;
                        }
                        var fileName = file.FileName;
                        var uploadfile = new XCenter.Code.Domain.UploadFile
                        {
                            FileName = fileName,
                            FileSize = file.ContentLength,
                            MiniPath = miniPath,
                            SavePath = savePath,
                            FileTitle = System.IO.Path.GetFileNameWithoutExtension(fileName),
                            FileKey = Encryptor.Md5Encryptor32(savePath),
                            Description = ""
                        };
                        db.insert(uploadfile);
                        helper.Add("savepath", savePath);
                        helper.Add("minipath", miniPath);
                        helper.Response(savePath + ";" + miniPath);
                    }
                }
                #endregion 处理结束
            }
        }
    }
}