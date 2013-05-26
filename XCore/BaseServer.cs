//------------------------------------------------------------------------------
//	文件名称：System\strUtil.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Security;
using System.Text;
namespace System {
    /// <summary>
    /// 基础服务
    /// </summary>
    public partial class BaseServer
    {
        public static Result SendSms(string smstext, string sendto)
        {
            Result result = new Result();
            try
            {
                string url = string.Format("{0}/api/smsapi.aspx?uid={1}&key={2}&smstext={3}&sendto={4}"
                     , cfgHelper.GetAppSettings("WlnServer"), cfgHelper.GetAppSettings("WlnUid"), cfgHelper.GetAppSettings("WlnKey"), smstext, sendto);
                string resultStr = System.Text.UTF8Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(url));
                SmsResult al = Json.ToObject<SmsResult>(resultStr);
                if (al != null && !al.success)
                {
                    result.Add(al.msg);
                }
            }
            catch(Exception ex)
            {
                result.Add(ex.Message);
            }
            return result;
        }
    }
    public class SmsResult
    {
		private bool _success;
		private string _msg;
        public bool success { 
			get{return _success;} 
			set{ _success=value;} 
		}
        public string msg { 
			get{return _msg;} 
			set{ _msg=value;} 
		}
	}
}

