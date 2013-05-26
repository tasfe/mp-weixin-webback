/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Api.cs
        运 行 库：2.0.50727.1882
        代码功能：Api程序基础类

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using Wlniao.WeChat.BLL;

namespace Wlniao.WeChat
{
    public class Api : System.Web.UI.Page
    {
        private static string _ApiUrl = "";
        protected static string ApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_ApiUrl))
                {
                    if (HttpContext.Current.Request.Url.Port == 80)
                    {
                        _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host;
                    }
                    else
                    {
                        _ApiUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    }
                }
                return _ApiUrl;
            }
        }
        internal string serverUser = "";
        internal string clientUser = "";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="method">执行的方法</param>
        /// <param name="msgText">消息内容</param>
        /// <param name="msgArgs">参数(已除去命令符及首尾空格)</param>
        /// <returns></returns>
        public string RunMethod(string token, string assembly, string method, String msgText, String msgArgs)
        {
            String content = "";             //方法执行结果
            try
            {
                String classname = method.Substring(0, method.LastIndexOf('.'));        //获取类名
                String methodname = method.Substring(method.LastIndexOf('.') + 1);      //获取方法名
                Type type = null;
                try
                {
                    if (string.IsNullOrEmpty(assembly))
                    {
                        assembly = "Wlniao.WeChat.Extend";
                    }
                    else
                    {
                        assembly = assembly.ToLower().Replace(".dll", "");
                    }
                    if (method.Split(new char[] { '.' }).Length > 1)
                    {
                        type = Type.GetType(String.Format("{0}, {1}", classname, assembly), false, true);
                        if (type == null)
                        {
                            type = Type.GetType(String.Format("{0}, Wlniao.WeChat", classname), false, true);
                        }
                    }
                    else
                    {
                        type = Type.GetType(String.Format("Wlniao.WeChat.Extend.{0}, {1}", classname, assembly), false, true);
                        if (type == null)
                        {
                            type = Type.GetType(String.Format("Wlniao.WeChat.Method.{0}, Wlniao.WeChat", classname), false, true);
                        }
                    }
                }
                catch { }
                try
                {
                    ActionBase action = (ActionBase)Activator.CreateInstance(type);
                    action.ClientUser = clientUser;
                    action.ServerUser = serverUser;
                    action.Token = token;
                    action.MsgText = msgText;
                    action.MsgArgs = msgArgs;
                    content = type.InvokeMember(methodname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, action, new object[] { }).ToString();
                }
                catch{}
            }
            catch { }
            return content;
        }
        protected static void ResponseMsg(string msg)
        {
            HttpContext.Current.Response.Write(msg);
        }
    }
}
