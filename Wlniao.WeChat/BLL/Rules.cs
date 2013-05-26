﻿/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\BLL\Rules.cs
        运 行 库：2.0.50727.1882
        代码功能：API规则存储方法定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Wlniao.WeChat.BLL
{
    public class Rules : System.ORM.CommonBase<Model.Rules>
	{
        public static Model.Rules Get(string Guid)
        {
            Model.Rules rules = new Model.Rules();
            try
            {
                rules = Model.Rules.findByField("StrGuid", Guid);
            }
            catch { return null; }
            return rules;
        }
        private static string[] separation = null;
        public static string[] Separation
        {
            get {
                try
                {
                    if (separation == null)
                    {
                        string temp = System.Tool.GetConfiger("Separation");
                        if (!string.IsNullOrEmpty(temp))
                        {
                            separation = temp.Split(new char[] { ',' });
                        }
                        else
                        {
                            separation = new string[] { " ", "#" };
                        }
                    }
                }
                catch
                {
                    separation = new string[] { " ", "#" };
                }
                return separation;
            }
        }

        public static Model.Rules GetRule(string AccountFirst, string Text, string clientUser)
        {
            try
            {
                string Code = Text.Split(Separation, StringSplitOptions.RemoveEmptyEntries)[0];
                Model.RuleCode rulecode = null;
                try
                {
                    rulecode = Model.RuleCode.find("Status <>'close' and Code like'%#" + Code + "#%'").first();
                }
                catch { }
                if (rulecode == null)
                {
                    Code = "";
                    Result result = strUtil.CheckSensitiveWords(Text, KeyWords);
                    int HitCount = result.Errors.Count;
                    if (HitCount > 0)
                    {
                        string temp = "(" + strUtil.Join("|", result.Errors.ToArray()) + ")";
                        for (; HitCount > 0; HitCount--)
                        {
                            rulecode = null;
                            try
                            {
                                Code = result.Errors[HitCount-1];
                                rulecode = Model.RuleCode.find("Status <>'close' and Code like'%$" + Code + "$%'").first();
                                if (rulecode != null)
                                {
                                    Result t = strUtil.CheckSensitiveWords(rulecode.Code, temp);
                                    List<string> ary = new List<string>();
                                    for (int i = 0; i < t.Errors.Count; i++)
                                    {
                                        if (!ary.Contains(t.Errors[i]))
                                        {
                                            ary.Add(t.Errors[i]);
                                        }
                                    }
                                    if (ary.Count < rulecode.HitCount)
                                    {
                                        rulecode = null;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                if (rulecode == null ||( rulecode.Status == "test" && Fans.GetBy("WeChatOpenId", clientUser).AllowTest != 1))
                {
                    return null;
                }
                else
                {
                    Model.Rules temp = Get(rulecode.RuleGuid);
                    //temp.GoOnCmd = Code;
                    //string msgArgs = Text;
                    //if (!string.IsNullOrEmpty(temp.GoOnCmd) && Text.StartsWith(temp.GoOnCmd))
                    //{
                    //    msgArgs = msgArgs.Substring(temp.GoOnCmd.Length).Trim();
                    //}
                    //BLL.Fans.SetSession(clientUser, temp.GoOnCmd, temp.DoMethod, msgArgs, temp.CallBackText);
                    if (string.IsNullOrEmpty(temp.AccountFirst) || temp.AccountFirst == AccountFirst)
                    {
                        return temp;
                    }
                }
            }
            catch { }
            return null;
        }
        private static string _keywords = "";
        private static string KeyWords
        {
            get
            {
                if (string.IsNullOrEmpty(_keywords))
                {
                    UpdateKeyWords("$");
                }
                return _keywords;
            }
        }
        protected static void UpdateKeyWords(string sepType)
        {
            List<string> list = new List<string>();
            List<Model.RuleCode> rclist = Model.RuleCode.findListByField("SepType", sepType);
            foreach (Model.RuleCode rc in rclist)
            {
                try
                {
                    string[] codes = rc.Code.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string code in codes)
                    {
                        if (!string.IsNullOrEmpty(code) && !list.Contains(code))
                        {
                            list.Add(code);
                        }
                    }
                }
                catch { }
            }
            _keywords = strUtil.Join("|", list.ToArray());

        }
        public static Result AddRuleCode(string Code, string RuleGuid, string sepType)
        {
            return AddRuleCode(Code, RuleGuid, sepType, "normal");
        }
        public static Result AddRuleCode(string Code, string RuleGuid, string sepType, string Status)
        {
            List<Model.RuleCode> rulecodes = new List<Model.RuleCode>();
            if (string.IsNullOrEmpty(sepType))
            {
                if (Code.IndexOf("$") > 0)
                {
                    sepType = "$";
                }
                if (string.IsNullOrEmpty(sepType))
                {
                    sepType = "#";
                }
            }
            string[] codes = Code.Split(new string[] { sepType, ",", ";", " ", "，", "；" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            foreach (string code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    list.Add(code);
                }
            }
            list.Sort();
            Code = strUtil.Join(sepType, list.ToArray());
            Code = sepType + Code + sepType;

            Model.RuleCode rulecode = new Model.RuleCode();
            rulecode.Guid = Guid.NewGuid().ToString();
            rulecode.Code = Code;
            rulecode.RuleGuid = RuleGuid;
            rulecode.SepType = sepType;
            rulecode.Status = Status;
            rulecode.HitCount = list.Count;
            rulecode.HashCode = Encryptor.Md5Encryptor32(Code);
            _keywords = "";
            return rulecode.insert();
        }
        public static Result EditRuleCode(string Guid, string Code, string RuleGuid, string sepType, string Status)
        {
            Model.RuleCode rulecode = Model.RuleCode.findByField("StrGuid", Guid);
            if (rulecode == null && rulecode.Id <= 0)
            {
                Result result = new Result();
                result.Add("你操作的内容不存在或已删除！");
                return result;
            }
            List<Model.RuleCode> rulecodes = new List<Model.RuleCode>();
            if (string.IsNullOrEmpty(sepType))
            {
                if (Code.IndexOf("$") > 0)
                {
                    sepType = "$";
                }
                if (string.IsNullOrEmpty(sepType))
                {
                    sepType = "#";
                }
            }
            string[] codes = Code.Split(new string[] { sepType, ",", ";", " ", "，", "；" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            foreach (string code in codes)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    list.Add(code);
                }
            }
            list.Sort();
            Code = strUtil.Join(sepType, list.ToArray());
            Code = sepType + Code + sepType;

            rulecode.Code = Code;
            rulecode.RuleGuid = RuleGuid;
            rulecode.SepType = sepType;
            rulecode.HitCount = list.Count;
            if (!string.IsNullOrEmpty(Status))
            {
                rulecode.Status = Status;
            }
            rulecode.HashCode = Encryptor.Md5Encryptor32(Code);
            _keywords = "";
            return rulecode.update();
        }
        public static Result AddRuleContent(string RuleGuid, string ContentType, string Title, string TextContent, string PicUrl, string ThumbPicUrl, string MusicUrl, string LinkUrl, string ContentStatus)
        {
            Model.RuleContent rulecontent = new Model.RuleContent();
            rulecontent.Guid = Guid.NewGuid().ToString();
            rulecontent.RuleGuid = RuleGuid;
            rulecontent.ContentType = ContentType;
            rulecontent.Title = Title;
            rulecontent.TextContent = TextContent;
            rulecontent.PicUrl = PicUrl;
            if (string.IsNullOrEmpty(ThumbPicUrl))
            {
                ThumbPicUrl = PicUrl;
            }
            rulecontent.ThumbPicUrl = ThumbPicUrl;
            rulecontent.MusicUrl = MusicUrl;
            rulecontent.LinkUrl = LinkUrl;
            rulecontent.ContentStatus = ContentStatus;
            rulecontent.PushCount = 0;
            return rulecontent.insert();
        }
        public static Result EditRuleContent(string Guid, string ContentType, string Title, string TextContent, string PicUrl, string ThumbPicUrl, string MusicUrl, string LinkUrl, string ContentStatus)
        {
            Model.RuleContent rulecontent = Model.RuleContent.findByField("StrGuid", Guid);
            if (rulecontent == null && rulecontent.Id <= 0)
            {
                Result result = new Result();
                result.Add("你操作的内容不存在或已删除！");
                return result;
            }
            else
            {
                rulecontent.ContentType = ContentType;
                rulecontent.Title = Title;
                rulecontent.TextContent = TextContent;
                rulecontent.PicUrl = PicUrl;
                if (string.IsNullOrEmpty(ThumbPicUrl))
                {
                    ThumbPicUrl = PicUrl;
                }
                rulecontent.ThumbPicUrl = ThumbPicUrl;
                rulecontent.MusicUrl = MusicUrl;
                rulecontent.LinkUrl = LinkUrl;
                rulecontent.ContentStatus = ContentStatus;
                return rulecontent.update();
            }
        }

    }
}
