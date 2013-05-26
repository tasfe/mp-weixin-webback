using System;
using System.Collections.Generic;
//~5~1~a~s~p~x
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin
{
    public partial class Default : System.Web.LoginInPage
    {
        public bool _IsOnline = false;
        public string _Version = "";
        public string _ClientName = "";
        public string _LoginName = "";
        public string _Nickname = "";
        public string _ProductName = "";
        public string _AuthState = "";
        public string _VipDate = "";
        public string _PastDate = "";
        public string _Comment = "";
        public string _Yue = "";
        public string _UserCount = "0";
        public string _VisitCount = "0";
        public string _ClassCount = "0";
        public string _ContentCount = "0";
        public string _HTMLCache = "未启用";
        public string _Statistical = "未启用";
        public string _WlniaoCloud = "未启用";
        public string _LastStart = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            #region 处理开始
            if (!IsPostBack)
            {
                Wlniao.Method.Client.Init();
                Wlniao.Model.ClientInfo client = null;
                client = Wlniao.Method.Client.GetInfo();
                if (client == null || string.IsNullOrEmpty(client.LoginName))
                {
                    Wlniao.Method.Client.Init();
                    client = Wlniao.Method.Client.GetInfo();
                }
                _Version = Wlniao.Configer.GetConfigValue("Version");
                _LastStart = Wlniao.Configer.GetConfigValue("LastStart");
                if (client != null && !string.IsNullOrEmpty(client.LoginName) && GetKeyValue("WlniaoCloud") == "true")
                {
                    _IsOnline = true;
                    _ClientName = client.ClientName;
                    _LoginName = client.LoginName;
                    _Nickname = client.NickName;
                    _ProductName = client.ProductName;
                    _AuthState = client.AuthState;
                    _VipDate = client.VipDate.ToString("yyyy-MM-dd HH:mm:ss");
                    _PastDate = client.PastDate.ToString("yyyy-MM-dd HH:mm:ss");
                    _Comment = client.Comment;
                    _Yue = string.Format("充值币：{0}   体验币：{1}   积分：{2}", client.CZB.ToString("F0"), client.TYB.ToString("F0"), client.Jifen.ToString("F0"));
                    if (!string.IsNullOrEmpty(client.Version))
                    {
                        _Version += string.Format(" (最新版本：{0})", client.Version);
                    }
                }
                _UserCount = db.count<XCenter.Code.Domain.Sys.User>().ToString();
                _ClassCount = db.count<XCenter.Code.Domain.News.NewsClass>().ToString();
                _ContentCount = db.count<XCenter.Code.Domain.News.Article>().ToString();
                if (GetKeyValue("Statistical") == "true")
                {
                    _Statistical = "已启用";
                    _VisitCount = db.count<XCenter.Code.Domain.Sys.Statistical>().ToString() + " 次";
                }
                else
                {
                    _VisitCount = "访问统计未<a href=\"javascript:GotoCode('sys/setting.aspx','WebSiteSetting');\" style=\" color:Green; text-decoration:underline;\">开启</a>";
                }
                if (GetKeyValue("HTMLCache") == "true")
                {
                    _HTMLCache = "已启用";
                }
                if (GetKeyValue("WlniaoCloud") == "true")
                {
                    _WlniaoCloud = "已启用";
                }
            }
            #endregion 处理结束

            //Response.Write(lang.getLangString() + "：" + lang.get("default"));
            //Domain.Sys.Account account = new Domain.Sys.Account();
            //account.LoginName = strUtil.Chs2PinyinSplit("谢超逸");
            //account.LoginPassword = Encryptor.Md5Encryptor32(Rand.Str_char(6));
            ////account.CreateTime = DateTools.GetNow();
            //db.insert(account);
        }
    }
}