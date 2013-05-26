using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace XCenter.Web.xcenter.Static.fileupload
{
    public partial class filedelete : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request.HttpMethod == "GET")
            {
                return;
            }
            if (Context.Request.HttpMethod == "DELETE")
            {
                XCenter.Code.Domain.UploadFile uploadfile = db.findById<XCenter.Code.Domain.UploadFile>(int.Parse(Context.Request["fid"]));
                if (uploadfile != null)
                {
                    try
                    {
                        string savePath = PathHelper.Map(uploadfile.SavePath);
                        string miniPath = PathHelper.Map(uploadfile.MiniPath);
                        if (db.delete(uploadfile)>0)
                        {
                            Dictionary<string, FileData> uploadedFiles = fileupload.GetSessionStore(HttpContext.Current);
                            if (uploadedFiles != null)
                            {
                                uploadedFiles.Remove(uploadfile.FileKey);
                            }
                            file.Delete(savePath);
                            file.Delete(miniPath);
                        }
                    }
                    catch { }
                }
                return;
            }
            if (Context.Request.HttpMethod != "POST")
            {
                Context.Response.StatusCode = 403;
                Context.Response.Write("<html><head></head><body><h1>403 - Forbidden</h1><p>Uploaded files must be POSTed.</p></body></html>");
                return;
            }
		}
	}
}