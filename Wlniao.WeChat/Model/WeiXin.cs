/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Model\Rules.cs
        运 行 库：2.0.50727.1882
        代码功能：API处理规则实体类定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.WeChat.Model
{
    /// <summary>
    /// 微信公众帐号列表
    /// </summary>
    public class WeiXin : ObjectBase<WeiXin>
    {
        /// <summary>
        /// 微信原始帐号
        /// </summary>
        [Column(Length = 50), Unique("原始帐号已存在")]
        public string AccountFirst { get; set; }
        /// <summary>
        /// 微信名称
        /// </summary>
        [Column(Length = 50)]
        public string WeChatName { get; set; }
        /// <summary>
        /// 微信帐号
        /// </summary>
        [Column(Length = 50)]
        public string AccountName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Column(Length = 50)]
        public string WeChatToken { get; set; }

        /// <summary>
        /// Appid
        /// </summary>
        [Column(Length = 50)]
        public string Appid { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        [Column(Length = 50)]
        public string Secret { get; set; }

        /// <summary>
        /// Default
        /// </summary>
        [Column(Length = 50)]
        public string DefaultCmd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public String CreateTime { get; set; }
        /// <summary>
        /// 请求总次数
        /// </summary>
        public int RequestCount { get;set; }
    }
}
