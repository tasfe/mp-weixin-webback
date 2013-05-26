/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\WeChatApi.cs
        运 行 库：2.0.50727.1882
        代码功能：解析微信服务器的请求和控制输出

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
	public class WeChatApi : Api
    {
		public WeChatApi ()
		{
			this.Load += new EventHandler (WeChatApi_Load);
		}
        public WeChatApi(string serverUser,string clientUser)
        {
            this.serverUser = serverUser;
            this.clientUser = clientUser;
        }

        void WeChatApi_Load(object sender, EventArgs e)
        {
            Response.Clear();
            try
            {
                //声明一个XMLDoc文档对象，LOAD（）xml字符串
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(new StreamReader(Request.InputStream).ReadToEnd());
                serverUser = doc.GetElementsByTagName("ToUserName")[0].InnerText.Trim();
                Wlniao.WeChat.Model.WeiXin weixin = Wlniao.WeChat.Model.WeiXin.findByField("AccountFirst", serverUser);
                if (weixin == null)
                {
                    weixin = Wlniao.WeChat.Model.WeiXin.findByField("AccountFirst", "");
                }
                clientUser = doc.GetElementsByTagName("FromUserName")[0].InnerText.Trim();
                string MsgType = doc.GetElementsByTagName("MsgType")[0].InnerText.Trim();
                string MsgId = "", Event = "", Content = "";
                try
                {
                    MsgId = doc.GetElementsByTagName("MsgId")[0].InnerText;
                }
                catch { }
                try
                {
                    Content = doc.GetElementsByTagName("Content")[0].InnerText;
                }
                catch { }
                try
                {
                    Event = doc.GetElementsByTagName("Event")[0].InnerText;
                    if (string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Event))
                    {
                        Content = Event;
                    }
                }
                catch { }
                if (true || CheckSignature(Context, weixin.WeChatToken))
                {
                    Wlniao.WeChat.Model.Fans fans = Wlniao.WeChat.BLL.Fans.Check(clientUser, serverUser, Content);
                    switch (MsgType.ToLower())
                    {
                        case "event":
                        case "text":
                            DoByContent(weixin, fans, Content, true);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ResponseMsg("Token签名错误:" + Context.Request.Url.ToString());
                }
            }
            catch(Exception ex)
            {
                Response.Write(Request.QueryString["echostr"]);
            }
            Response.End();
        }

        protected void DoByContent(Model.WeiXin weixin, Model.Fans fans, string Content, bool firstRun)
        {
            Wlniao.WeChat.Model.Rules rule = Wlniao.WeChat.BLL.Rules.GetRule(weixin.AccountFirst, Content, clientUser);
            bool onsession = false;
            if (fans.SessionTimeOut > 0 && fans.LastCmdTime < DateTools.GetNow().AddSeconds(0 - fans.SessionTimeOut))
            {
                if (Json.ToObject<Wlniao.WeChat.Model.RuleConfig>(rule.RuleConfig).SessionKeep == 1)
                {
                    onsession = true;
                }
            }
            if (rule != null && !onsession)
            {
                switch (rule.RuleType)
                {
                    case 0:
                        ResponseMsg(GetMsgByRule(rule, fans.AllowTest == 1));
                        break;
                    case 1:
                        break;
                    case 2:
                        try
                        {
                            string apiurl = Json.ToObject<Wlniao.WeChat.Model.RulesApiConfig>(rule.RuleConfig).ApiUrl;
                            if (!apiurl.Contains("?"))
                            {
                                apiurl += "?";
                            }
                            apiurl += "openid=" + clientUser + "&toid=" + serverUser + "&text=" + Content;
                            ResponseMsg(System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(apiurl)));
                        }
                        catch { }
                        break;
                    case 3:
                        try
                        {
                            StringBuilder sb = new StringBuilder();
                            String firstid = weixin.AccountFirst;
                            sb.AppendFormat("<xml>");
                            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", firstid);
                            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", clientUser);
                            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", Content);
                            sb.AppendFormat("</xml>");

                            string paramstr = "";
                            string apiurl = Json.ToObject<Wlniao.WeChat.Model.RulesMpApiConfig>(rule.RuleConfig).ApiUrl;
                            if (apiurl.IndexOf("signature") <= 0)
                            {
                                string timestamp = (DateTime.Now.Ticks / 1000000).ToString();
                                string[] arr = { weixin.WeChatToken, timestamp, timestamp };
                                Array.Sort(arr);     //字典排序
                                paramstr = "signature=" + System.Encryptor.GetSHA1(string.Join("", arr)).ToLower() + "&timestamp=" + timestamp + "&nonce=" + timestamp;
                            }
                            if (string.IsNullOrEmpty(paramstr))
                            {
                                if (apiurl.IndexOf('?') > 0)
                                {
                                    apiurl += "&" + paramstr;
                                }
                                else
                                {
                                    apiurl += "?" + paramstr;
                                }
                            }
                            byte[] byteArray = Encoding.UTF8.GetBytes(sb.ToString());
                            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(apiurl));
                            webRequest.Method = "POST";
                            //webRequest.ContentType = "application/x-www-form-urlencoded";
                            webRequest.ContentLength = byteArray.Length;
                            Stream newStream = webRequest.GetRequestStream();
                            newStream.Write(byteArray, 0, byteArray.Length);
                            newStream.Close();
                            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse();
                            StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                            if (string.IsNullOrEmpty(firstid))
                            {
                                ResponseMsg(php.ReadToEnd());
                            }
                            else
                            {
                                ResponseMsg(php.ReadToEnd().Replace(firstid, serverUser));
                            }
                        }
                        catch { }
                        break;
                    case 4:
                        break;
                    case 5:
                        Wlniao.WeChat.Model.RulesExtendConfig recfg = Json.ToObject<Wlniao.WeChat.Model.RulesExtendConfig>(rule.RuleConfig);
                        ResponseMsg(RunMethod(weixin.WeChatToken, recfg.Assembly, recfg.Method, Content, Content));
                        break;
                    default:
                        break;
                }
            }
            else if (rule == null && firstRun)
            {
                if (string.IsNullOrEmpty(weixin.DefaultCmd))
                {
                    ResponseMsg(RunMethod(weixin.WeChatToken,"", "Wlniao.WeChat.Method.Empty", Content, Content));
                }
                else if (firstRun)
                {
                    DoByContent(weixin, fans, (weixin.DefaultCmd + BLL.Rules.Separation[0] + Content).Trim(), false);
                }
            }
            else if (firstRun)
            {
                DoByContent(weixin, fans, (fans.KeyWords + BLL.Rules.Separation[0] + Content).Trim(), false);
            }
        }

		/// <summary>
		/// 根据参数和密码生成签名字符串
		/// </summary>
		/// <param name="parameters">API参数</param>
		/// <param name="secret">密码</param>
		/// <returns>签名字符串</returns>
        internal static bool CheckSignature(HttpContext context, String WeChatToken)
		{
			if (string.IsNullOrEmpty (WeChatToken)) {
				return true;
			} else {
				string[] arr = {
					WeChatToken,
					context.Request.QueryString ["timestamp"],
					context.Request.QueryString ["nonce"]
				};
				Array.Sort (arr);     //字典排序
				return System.Encryptor.GetSHA1 (string.Join ("", arr)).ToLower () == context.Request.QueryString ["signature"];
			}
		}
        public string GetMsgByRule(Model.Rules rule,Boolean canuseTest)
        {
            string msg = "";
            Wlniao.WeChat.Model.RulesAutoConfig config = Json.ToObject<Wlniao.WeChat.Model.RulesAutoConfig>(rule.RuleConfig);
            if (!string.IsNullOrEmpty(config.ReContent))
            {
                return strUtil.RemoveHtmlTag(config.ReContent);
            }
            string where = "RuleGuid='" + rule.Guid + "'";
            if (canuseTest)
            {
                where += " and (ContentStatus='normal' or ContentStatus='test')";
            }
            else
            {
                where += " and ContentStatus='normal'";
            }

            List<Model.RuleContent> listAll = Model.RuleContent.find(where + " order by LastStick desc").list();
            List<Model.RuleContent> listText = Model.RuleContent.find(where + " and ContentType='text' order by LastStick desc").list();
            List<Model.RuleContent> listPicText = Model.RuleContent.find(where + " and ContentType='pictext' order by LastStick desc").list();
            List<Model.RuleContent> listMusic = Model.RuleContent.find(where + " and ContentType='music' order by LastStick desc").list();
            if (config.SendMode == "sendgroup" && listPicText != null && listPicText.Count > 0)
            {
                config.ReContent = ResponsePicTextMsg(listPicText);
            }
            else if (listAll.Count > 0)
            {
                int i = 0;
                if (config.SendMode == "sendrandom")
                {
                    i = new Random().Next(0, listAll.Count);
                }
                if (listAll[i].ContentType == "text")
                {
                    msg = ResponseTextMsg(listAll[i].TextContent);
                    try
                    {
                        //更新推送次数
                        listAll[i].PushCount++;
                        listAll[i].update("PushCount");
                    }
                    catch { }
                }
                else if (listAll[i].ContentType == "music")
                {
                    msg = ResponseMusicMsg(listAll[i].Title, listAll[i].TextContent, listAll[i].MusicUrl, listAll[i].MusicUrl);
                    try
                    {
                        //更新推送次数
                        listAll[i].PushCount++;
                        listAll[i].update("PushCount");
                    }
                    catch { }
                }
                else if (listAll[i].ContentType == "pictext")
                {
                    List<Model.RuleContent> listTemp = new List<Model.RuleContent>();
                    listTemp.Add(listAll[i]);
                    msg = ResponsePicTextMsg(listTemp);
                }
            }
            return msg;
        }

        /// <summary>
        /// 输出内容
        /// </summary>
        /// <param name="msg"></param>
        protected void ResponseMsg(string msg)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(msg);
                if (doc.InnerText.Length < 50)
                {
                    doc = null;
                }
            }
            catch
            {
                doc = null;
            }
            if (doc == null)
            {
                HttpContext.Current.Response.Write(ResponseTextMsg(msg));
            }
            else
            {
                HttpContext.Current.Response.Write(doc.InnerXml);
            }
        }

		/// <summary>
		/// 回复文本内容
		/// </summary>
		/// <param name="to">接收者</param>
		/// <param name="from">消息来源</param>
		/// <param name="content">消息内容</param>
		/// <returns>生成的输出文本</returns>
		public string ResponseTextMsg (string content)
		{
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", DateTools.GetNow().Ticks);
            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strUtil.RemoveHtmlTag(content));
            sb.AppendFormat("<FuncFlag>0</FuncFlag>");
            sb.AppendFormat("</xml>");
            return sb.ToString();
		}
		/// <summary>
		/// 回复图文内容
		/// </summary>
		/// <param name="to">接收者</param>
		/// <param name="from">消息来源</param>
		/// <param name="content">消息内容</param>
		/// <returns>生成的输出文本</returns>
		public string ResponsePicTextMsg (List<Model.RuleContent> articles)
		{
			if (articles == null) {
				articles = new List<Model.RuleContent> ();
			}
			int count = 0;
			StringBuilder sbItems = new StringBuilder ();
			foreach (Model.RuleContent article in articles) {
				try {
					if (string.IsNullOrEmpty (article.Title) || string.IsNullOrEmpty (article.PicUrl) || string.IsNullOrEmpty (article.TextContent)) {
						continue;
					}
					StringBuilder sbTemp = new StringBuilder ();
					sbTemp.AppendFormat ("<item>");
					sbTemp.AppendFormat ("   <Title><![CDATA[{0}]]></Title>", article.Title);
					sbTemp.AppendFormat ("   <Description><![CDATA[{0}]]></Description>", strUtil.RemoveHtmlTag (strUtil.HtmlDecode (article.TextContent)));
					string urlTemp = article.PicUrl;
					if (count > 0) {
						urlTemp = string.IsNullOrEmpty (article.ThumbPicUrl) ? article.PicUrl : article.ThumbPicUrl;
					}
					sbTemp.AppendFormat ("   <PicUrl><![CDATA[{0}]]></PicUrl>", (!string.IsNullOrEmpty (urlTemp) && urlTemp.Contains ("http://")) ? urlTemp : ApiUrl + urlTemp);
					if (string.IsNullOrEmpty (article.LinkUrl)) {
						sbTemp.AppendFormat ("   <Url><![CDATA[{0}]]></Url>", ApiUrl + "/article.aspx?id=" + article.Id);
					} else {
						sbTemp.AppendFormat ("   <Url><![CDATA[{0}]]></Url>", article.LinkUrl);
					}
					sbTemp.AppendFormat ("   <FuncFlag>0</FuncFlag>");
					sbTemp.AppendFormat ("</item>");
					sbItems.Append (sbTemp.ToString ());
					count++;
					//更新推送次数
					article.PushCount++;
					article.update ("PushCount");
					if (count == 9) {
						break;
					}
				} catch {
				}
			}


			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("<xml>");
			sb.AppendFormat ("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
			sb.AppendFormat ("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
			sb.AppendFormat ("<CreateTime>{0}</CreateTime>", DateTools.GetNow ().Ticks);
			sb.AppendFormat ("<MsgType><![CDATA[news]]></MsgType>");
			sb.AppendFormat ("<ArticleCount>{0}></ArticleCount>", count);
			sb.AppendFormat ("<Articles>");
			sb.AppendFormat (sbItems.ToString ());
			sb.AppendFormat ("</Articles>");
			sb.AppendFormat ("<FuncFlag>0</FuncFlag>");
			sb.AppendFormat ("</xml>");
			return sb.ToString ();
		}
		/// <summary>
		/// 回复音乐内容
		/// </summary>
		/// <param name="to">接收者</param>
		/// <param name="from">消息来源</param>
		/// <param name="title">标题</param>
		/// <param name="description">描述信息</param>
		/// <param name="musicurl">音乐链接</param>
		/// <param name="hqmusicurl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
		/// <returns>生成的输出文本</returns>
		public string ResponseMusicMsg (string title, string description, string musicurl, string hqmusicurl)
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("<xml>");
			sb.AppendFormat ("<ToUserName><![CDATA[{0}]]></ToUserName>", clientUser);
			sb.AppendFormat ("<FromUserName><![CDATA[{0}]]></FromUserName>", serverUser);
			sb.AppendFormat ("<CreateTime>{0}</CreateTime>", DateTools.GetNow ().Ticks);
			sb.AppendFormat ("<MsgType><![CDATA[music]]></MsgType>");
			sb.AppendFormat ("<Music>");
			sb.AppendFormat ("   <Title><![CDATA[{0}]]></Title>", title);
			sb.AppendFormat ("   <Description><![CDATA[{0}]]></Description>", description);
			sb.AppendFormat ("   <MusicUrl><![CDATA[{0}]]></MusicUrl>", (!string.IsNullOrEmpty (musicurl) && musicurl.Contains ("http://")) ? musicurl : ApiUrl + musicurl);
			sb.AppendFormat ("   <HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", (!string.IsNullOrEmpty (hqmusicurl) && hqmusicurl.Contains ("http://")) ? hqmusicurl : ApiUrl + hqmusicurl);
			sb.AppendFormat ("   <FuncFlag>0</FuncFlag>");
			sb.AppendFormat ("</Music>");
			sb.AppendFormat ("</xml>");
			return sb.ToString ();
		}
	}
}
