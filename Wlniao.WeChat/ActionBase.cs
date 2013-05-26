/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\ActionBase.cs
        运 行 库：2.0.50727.1882
        代码功能：执行API请求的基类

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Wlniao.WeChat
{
    /// <summary>
    /// 执行API请求的基类
    /// </summary>
    public abstract class ActionBase
    {
        private String msgText;
        private String msgArgs;
        /// <summary>
        /// 客户端ID
        /// </summary>
        public String ClientUser;
        /// <summary>
        /// 服务端ID
        /// </summary>
        public String ServerUser;
        /// <summary>
        /// Token
        /// </summary>
        public String Token;
        /// <summary>
        /// 消息内容
        /// </summary>
        public String MsgText;
        /// <summary>
        /// 参数（已除去命令符及首尾空格）
        /// </summary>
        public String MsgArgs;
    }
    
}
