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
    /// 规则命令
    /// </summary>
    public class RuleCode : ObjectBase<RuleCode>
    {
        private string _Guid;
        /// <summary>
        /// 所绑定的用户Guid
        /// </summary>
        [Column(Name = "StrGuid", Length = 50), Unique("Guid不能重复"), NotNull("Guid不能为空")]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _RuleGuid;
        /// <summary>
        /// 所属规则
        /// </summary>
        [Column(Length = 50)]
        public string RuleGuid
        {
            get { return _RuleGuid; }
            set { _RuleGuid = value; }
        }
        private string _Code;
        /// <summary>
        /// 编码
        /// </summary>
        [Column(Length = 100)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private int _HitCount;
        /// <summary>
        /// 命中次数
        /// </summary>
        public int HitCount
        {
            get { return _HitCount; }
            set { _HitCount = value; }
        }
        private string _HashCode;
        /// <summary>
        /// 哈希值
        /// </summary>
        [Column(Length = 50)]
        public string HashCode
        {
            get { return _HashCode; }
            set { _HashCode = value; }
        }
        private string _SepType;
        /// <summary>
        /// 分隔符
        /// </summary>
        [Column(Length = 10)]
        public string SepType
        {
            get { return _SepType; }
            set { _SepType = value; }
        }
        private string _Status;
        /// <summary>
        /// 规则状态 normal（正常），close（关闭），test（测试中）
        /// </summary>
        [Column(Length=20)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        

    }
}
