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
    /// 规则
    /// </summary>
    public class Rules : ObjectBase<Rules>
    {
        /// <summary>
        /// Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        [Column(Length = 120)]
        public string RuleName { get; set; }

        /// <summary>
        /// 所属微信原始帐号
        /// </summary>
        [Column(Length = 50)]
        public string AccountFirst { get; set; }

        /// <summary>
        /// 规则类型 0、自动应答 1、自定义菜单 2、调用开放API 3、标准请求转发 4、自定义代码段 5、自编函数
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [LongText]
        public string RuleConfig { get; set; }
    }
    public class RuleConfig
    {
        /// <summary>
        /// 回调内容
        /// </summary>
        public string CallBackText { get; set; }
        /// <summary>
        /// 会话失效时间
        /// </summary>
        public string SessionTimeOut { get; set; }
        /// <summary>
        /// 会话是否需要强制保持（即遇到其它命令符后不会跳转）
        /// </summary>
        public int SessionKeep { get; set; }
    }
    public class RulesAutoConfig : RuleConfig
    {
        /// <summary>
        /// 优先回复内容
        /// </summary>
        public string ReContent { get; set; }
        /// <summary>
        /// 内容回复模式  SendNew（最新）,SendRandom(随机),SendGroup（组合仅图文）
        /// </summary>
        public string SendMode { get; set; }
    }
    public class RulesApiConfig : RuleConfig
    {
        /// <summary>
        /// API地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 基础参数
        /// </summary>
        public string BaseArgs { get; set; }
    }
    public class RulesMpApiConfig : RuleConfig
    {
        /// <summary>
        /// API地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 基础参数
        /// </summary>
        public string BaseArgs { get; set; }
    }
    public class RulesExtendConfig : RuleConfig
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        public string Assembly { get; set; }
        /// <summary>
        /// 需要执行的方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 基础参数
        /// </summary>
        public string BaseArgs { get; set; }
    }

}
