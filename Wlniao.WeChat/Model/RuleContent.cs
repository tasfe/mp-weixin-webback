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
    /// 规则内容
    /// </summary>
    public class RuleContent : ObjectBase<RuleContent>
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
        private string _ContentType;
        /// <summary>
        /// 内容类型 text（文本）,pictext(图文) ,music（音乐）
        /// </summary>
        [Column(Length = 30)]
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }
        private string _Title;
        /// <summary>
        /// 标题
        /// </summary>
        [Column(Length = 180)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _LinkUrl;
        /// <summary>
        /// 外链地址
        /// </summary>
        [Column(Length = 120)]
        public string LinkUrl
        {
            get { return _LinkUrl; }
            set { _LinkUrl = value; }
        }
        private string _PicUrl;
        /// <summary>
        /// 图片外链地址
        /// </summary>
        [Column(Length = 120)]
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        private string _ThumbPicUrl;
        /// <summary>
        /// 小图外链地址
        /// </summary>
        [Column(Length = 120)]
        public string ThumbPicUrl
        {
            get { return _ThumbPicUrl; }
            set { _ThumbPicUrl = value; }
        }
        private string _MusicUrl;
        /// <summary>
        /// 声音文件外链地址
        /// </summary>
        [Column(Length = 120)]
        public string MusicUrl
        {
            get { return _MusicUrl; }
            set { _MusicUrl = value; }
        }
        private string _TextContent;
        /// <summary>
        /// 文本内容
        /// </summary>
        [LongText]
        public string TextContent
        {
            get { return _TextContent; }
            set { _TextContent = value; }
        }
        private int _PushCount;
        /// <summary>
        /// 推送次数
        /// </summary>
        public int PushCount
        {
            get { return _PushCount; }
            set { _PushCount = value; }
        }
        private string _ContentStatus;
        /// <summary>
        /// 规则状态 normal（正常），close（关闭），test（测试中）
        /// </summary>
        [Column(Length = 20)]
        public string ContentStatus
        {
            get { return _ContentStatus; }
            set { _ContentStatus = value; }
        }

        private DateTime _LastStick;
        /// <summary>
        /// 最后置顶的时间
        /// </summary>
        public DateTime LastStick
        {
            get { return _LastStick; }
            set { _LastStick = value; }
        }

    }
}
