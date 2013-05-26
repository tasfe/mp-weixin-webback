using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace XCenter.Web.xcenter.Static.fileupload
{
	public partial class fileupload : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, FileData> uploadedFiles = GetSessionStore(HttpContext.Current);
            if (Context.Request.HttpMethod == "GET")
            {
                WriteFileListJson(Context, uploadedFiles);
                return;
            }
            if (Context.Request.HttpMethod != "POST")
            {
                Context.Response.StatusCode = 403;
                Context.Response.Write("<html><head></head><body><h1>403 - Forbidden</h1><p>Uploaded files must be POSTed.</p></body></html>");
                return;
            }
            if (Context.Request.Files.Count == 0)
            {
                throw new Exception("File missing from form post");
            }
            if (Context.Request.Files.Count > 1)
            {
                throw new NotImplementedException("Currently only supports single file at a time.");
            }
            foreach (var key in Context.Request.Files.AllKeys)
            {
                var file = Context.Request.Files[key];
                string miniPath = string.Empty;
                var savePath = SaveUploadToDisk(file, out miniPath);
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
                    FileTitle=Path.GetFileNameWithoutExtension(fileName),
                    FileKey = Encryptor.Md5Encryptor32(savePath),
                    Description = ""
                };
                db.insert(uploadfile);
                var fileData = new FileData
                {
                    Id = uploadfile.Id,
                    FileName = fileName,
                    FileSize = file.ContentLength,
                    MiniPath = miniPath,
                    SavePath = savePath,
                    FileTitle = Path.GetFileNameWithoutExtension(fileName),
                    FileKey = Encryptor.Md5Encryptor32(savePath),
                    Description = ""
                };
                uploadedFiles.Add(uploadfile.FileKey, fileData);
            }
            WriteFileListJson(Context, uploadedFiles);
		}
        /// <summary>
        /// Writes the file list as JSON to the httpcontect, setting content type and encoding.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="uploadedFiles">The uploaded files to write out.</param>
        private static void WriteFileListJson(HttpContext context, Dictionary<string, FileData> uploadedFiles)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            List<System.Collections.Hashtable> al = new List<System.Collections.Hashtable>();
            Dictionary<string, FileData>.Enumerator em = uploadedFiles.GetEnumerator();
            while (em.MoveNext())
            {
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("id", em.Current.Value.Id);
                ht.Add("name", em.Current.Value.FileName);
                ht.Add("title", em.Current.Value.FileTitle);
                ht.Add("size", em.Current.Value.FileSize);
                ht.Add("url", em.Current.Value.SavePath);
                ht.Add("dese", em.Current.Value.Description);
                ht.Add("thumbnail_url", em.Current.Value.MiniPath);
                ht.Add("key", em.Current.Value.FileKey);
                ht.Add("delete_type", "DELETE");
                ht.Add("delete_url", "filedelete.aspx?fid=" + em.Current.Value.Id);
                ht.Add("error", false);
                al.Add(ht);
            }
            String str = Json.ToStringEx(al);
            context.Response.Write(str);
            context.Response.End();
        }

        /// <summary>
        /// Saves the uploaded file to the temp folder with the right extension and a temporary file name.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The path to the saved file</returns>
        internal static string SaveUploadToDisk(HttpPostedFile file, out string minipath)
        {
            minipath = "";
            int width = 0;
            try
            {
                width = Convert.ToInt32(lang.getBySkin("THUMBPICWIDTH"));
            }
            catch { width = 80; }
            int height = 0;
            try
            {
                height = Convert.ToInt32(lang.getBySkin("THUMBPICHEIGHT"));
            }
            catch { height = 80; }
            return SaveUploadToDisk(file, width, height, out minipath);
        }
        /// <summary>
        /// Saves the uploaded file to the temp folder with the right extension and a temporary file name.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The path to the saved file</returns>
        internal static string SaveUploadToDisk(HttpPostedFile file,int width,int height, out string minipath)
        {
            if (width == 0)
            {
                try
                {
                    width = Convert.ToInt32(lang.getBySkin("THUMBPICWIDTH"));
                }
                catch { width = 80; }
            }
            if (height == 0)
            {
                try
                {
                    height = Convert.ToInt32(lang.getBySkin("THUMBPICHEIGHT"));
                }
                catch { height = 80; }
            }
            string filename = Rand.Str(9).ToLower(); ;
            string extension = Path.GetExtension(file.FileName);
            var path = string.Format("{0}upload/uploadpic/{1}/{2}{3}", cfgHelper.FrameworkRoot, DateTools.GetNow().ToString("yyyy-MM"), filename, extension);
            System.IO.Directory.CreateDirectory(PathHelper.Map(Path.GetDirectoryName(path)));
            file.SaveAs(PathHelper.Map(path));
            if (".jpg|.jpeg|.gif|.png".IndexOf(extension) >= 0)
            {
                minipath = string.Format("{0}upload/uploadpic/{1}/{2}_thumb{3}", cfgHelper.FrameworkRoot, DateTools.GetNow().ToString("yyyy-MM"), filename, extension);
                Util.MakeThumbnail(PathHelper.Map(path), PathHelper.Map(minipath), width, height, "W");
            }
            else
            {
                minipath = path;
            }
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// Unzips the file, loops through the slides and adds them as pages
        /// </summary>
        /// <param name="zipFilePath">The path to the zip file containing the presentation.</param>
        private void ProcessPresentation(string zipFilePath)
        {

        }

        /// <summary>
        /// Finds the next unused unique (numbered) filename.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="inUse">Function that will determine if the name is already in use</param>
        /// <returns>The original filename if it wasn't already used, or the filename with " (n)"
        /// added to the name if the original filename is already in use.</returns>
        private static string NextUniqueFilename(string fileName, Dictionary<string, FileData> uploadedFiles)
        {
            Dictionary<string, FileData>.Enumerator em = uploadedFiles.GetEnumerator();
            while (em.MoveNext())
            {
                if (em.Current.Value.FileName == fileName)
                {
                    var name = Path.GetFileNameWithoutExtension(fileName);
                    var extension = Path.GetExtension(fileName);
                    if (name == null)
                    {
                        throw new Exception("File name without extension returned null.");
                    }
                    var nextUniqueFilename = Rand.Str(9, true).ToLower() + extension;
                    Dictionary<string, FileData>.Enumerator emIn = uploadedFiles.GetEnumerator();
                    while (emIn.MoveNext())
                    {
                        if (emIn.Current.Value.FileName == nextUniqueFilename)
                        {
                            nextUniqueFilename = Rand.Str(9, true).ToLower() + extension;
                        }
                    }
                    fileName = nextUniqueFilename;
                }
            }
            return fileName;
        }
        /// <summary>
        /// Get session storage for list of uploaded files. Creates dictionary if missing.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <returns></returns>
        public static Dictionary<string, FileData> GetSessionStore(HttpContext context)
        {
            if (System.Caching.SysCache.Get("UploadedFileHandler_Files") == null)
            {
                System.Caching.SysCache.Put("UploadedFileHandler_Files", new Dictionary<string, FileData>());
            }
            return (Dictionary<string, FileData>)System.Caching.SysCache.Get("UploadedFileHandler_Files");
            
        }
	}

    /// <summary>
    /// Container for all the data we need to know about an uploaded file,
    /// including data for redisplay in the uploader, and where the temp file
    /// is saved.
    /// </summary>
    public class FileData
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileTitle { get; set; }
        public long FileSize { get; set; }
        public string MiniPath { get; set; }
        public string SavePath { get; set; }
        public string FileKey { get; set; }
        public string Description { get; set; }
    }
}