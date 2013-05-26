/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Method\Wlniao.cs
        运 行 库：2.0.50727.1882
        代码功能：系统常用方法

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using BLL = Wlniao.WeChat.BLL;
namespace Wlniao.WeChat.Method
{
    public class Method : ActionBase
    {
        /// <summary>
        /// 空函数
        /// </summary>
        /// <returns></returns>
        public string Empty()
        {
            string msg = "";
            try
            {
                string url = "http://weback.azurewebsites.net/defaultapi.aspx";
                if (!url.Contains("?"))
                {
                    url += "?";
                }
                url += "openid=" + ClientUser + "&toid=" + ServerUser + "&text=" + MsgText;
                msg = System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(url));
            }
            catch { }
            return msg;
        }
        /// <summary>
        /// 被用户关注时触发的事件
        /// </summary>
        /// <returns></returns>
        public string Subscribe()
        {
            BLL.Fans.Subscribe(ClientUser);
            return "";
        }
        /// <summary>
        /// 用户取消关注时触发的事件
        /// </summary>
        /// <returns></returns>
        public string UnSubscribe()
        {
            BLL.Fans.UnSubscribe(ClientUser);
            return "";
        }
        public string RunCode()
        {
            CodeDomProvider codeDomProvider = new CSharpCodeProvider();
            CompilerParameters options = new CompilerParameters();
            options.ReferencedAssemblies.Add("system.dll");
            options.ReferencedAssemblies.Add("XCore.dll");
            options.GenerateExecutable = false;
            options.GenerateInMemory = true;
            CompilerResults results = codeDomProvider.CompileAssemblyFromSource(options, new String[] { "" });
            String message = "";
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    message = message + error.ToString() + "\r\n";
                }
                throw new Exception(message);
            }
            return ReflectionUtil.CallMethod(results.CompiledAssembly.CreateInstance("Weback.RunCode"), "Run").ToString();
        }
    }
}
