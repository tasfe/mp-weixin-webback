/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\Model\Fans.cs
        运 行 库：2.0.50727.1882
        代码功能：订阅者信息实体类定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;

namespace Wlniao.WeChat.Model
{
    public class Fans : ObjectBase<Fans>
    {
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Column(Length = 30)]
        public string NickName { get; set; }
        /// <summary>
        /// 所绑定的用户Sid
        /// </summary>
        [Column(Length = 50)]
        public string Sid { get; set; }

        /// <summary>
        /// 所属微信原始帐号
        /// </summary>
        [Column(Length = 50)]
        public string AccountFirst { get; set; }
        /// <summary>
        /// 微信OpenId
        /// </summary>
        [Column(Length = 50),Unique("微信OpenId不唯一")]
        public string WeChatOpenId { get; set; }
        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime BindTiem { get; set; }
        /// <summary>
        /// 订阅是否有效
        /// </summary>
        /// <summary>
        /// 订阅是否有效
        /// </summary>
        public int Subscribe { get; set; }
        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime SubscribeTime { get; set; }
        /// <summary>
        /// 是否新粉丝
        /// </summary>
        public int IsNewFans { get; set; }
        /// <summary>
        /// 是否允许使用测试功能
        /// </summary>
        public int AllowTest { get; set; }
        /// <summary>
        /// 正在继续的命令符
        /// </summary>
        [LongText]
        public string KeyWords { get; set; }
        /// <summary>
        /// 最后接收的参数
        /// </summary>
        [LongText]
        public string LastArgs { get; set; }
        /// <summary>
        /// 最后来访时间
        /// </summary>
        public DateTime LastVisit { get; set; }
        /// <summary>
        /// 当前流程超时时间
        /// </summary>
        public int SessionTimeOut { get; set; }
        /// <summary>
        /// 流程是否强制保持
        /// </summary>
        public int SessionKeep { get; set; }
        /// <summary>
        /// 最后记录的命令时间
        /// </summary>
        public DateTime LastCmdTime { get; set; }

    }
}
