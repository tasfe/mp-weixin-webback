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
    /// 管理员列表
    /// </summary>
    public class Manager : ObjectBase<Manager>
    {
        /// <summary>
        /// 管理员帐号
        /// </summary>
        [Column(Length = 50), Unique("管理员帐号已存在"), NotNull("管理员帐号未填写")]
        public string ManagerUsername { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        [Column(Length = 50)]
        public string ManagerPassword { get; set; }
        /// <summary>
        /// 1、超级管理员 0、普通管理员
        /// </summary>
        public int IsAdministrator { get; set; }

        /// <summary>
        /// 所属微信原始帐号
        /// </summary>
        [Column(Length = 50)]
        public string AccountFirst { get; set; }
    }
}
