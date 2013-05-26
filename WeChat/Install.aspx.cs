using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat
{
    public partial class Install : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            #region 处理开始
            if (!IsPostBack)
            {
                if (Request["do"] == "init")
                {
                    if (System.Data.KvTableUtil.GetString("Install") == "true")
                    {
                        helper.Result.Add("Sorry,您的程序已经执行过安装操作！");
                    }
                    else
                    {
                        string ormconfig = "";
                        switch (helper.GetParam("dbtype"))
                        {
                            case "access":
                                ormconfig = "{ \"ConnectionStringTable\":{ \"default\":\"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=xcore.mdb\" }, \"DbType\":{ \"default\":\"access\" }, \"AssemblyList\":[ \"WeChat\", \"Wlniao.WeChat\", \"Wlniao.WeChat.Extend\", \"XCore\" ], \"IsCheckDatabase\":true, \"TablePrefix\":\"\", \"ContextCache\":true, \"ApplicationCache\":false, \"ApplicationCacheMinutes\":0, \"ApplicationCacheManager\":\"\", \"MetaDLL\":\"\", \"Mapping\":[  ], \"Interceptor\":[  ] }";
                                break;
                            case "mysql":
                                ormconfig = "{ \"ConnectionStringTable\":{ \"default\":\"Server=" + helper.GetParam("server") + ";Database=" + helper.GetParam("dbname") + ";Uid=" + helper.GetParam("username") + ";Pwd=" + helper.GetParam("password") + "\" }, \"DbType\":{ \"default\":\"mysql\" }, \"AssemblyList\":[ \"WeChat\", \"Wlniao.WeChat\", \"Wlniao.WeChat.Extend\", \"XCore\" ], \"IsCheckDatabase\":true, \"TablePrefix\":\"\", \"ContextCache\":true, \"ApplicationCache\":false, \"ApplicationCacheMinutes\":0, \"ApplicationCacheManager\":\"\", \"MetaDLL\":\"\", \"Mapping\":[  ], \"Interceptor\":[  ] }";
                                break;
                            case "mssql":
                                ormconfig = "{ \"ConnectionStringTable\":{ \"default\":\"Server=" + helper.GetParam("server") + ";Database=" + helper.GetParam("dbname") + ";Uid=" + helper.GetParam("username") + ";Pwd=" + helper.GetParam("password") + "\" }, \"DbType\":{ \"default\":\"sqlserver\" }, \"AssemblyList\":[ \"WeChat\", \"Wlniao.WeChat\", \"Wlniao.WeChat.Extend\", \"XCore\" ], \"IsCheckDatabase\":true, \"TablePrefix\":\"\", \"ContextCache\":true, \"ApplicationCache\":false, \"ApplicationCacheMinutes\":0, \"ApplicationCacheManager\":\"\", \"MetaDLL\":\"\", \"Mapping\":[  ], \"Interceptor\":[  ] }";
                                break;
                            case "accessE":
                                break;
                            default:
                                helper.Result.Add("Sorry,未指定数据库类型！");
                                break;
                        }
                        if (helper.Result.IsValid && helper.GetParam("dbtype") != "accessE")
                        {
                            try
                            {
                                string ormfile = System.Data.DbConfig.getConfigPath();
                                if (file.Exists(ormfile))
                                {
                                    file.Delete(ormfile);
                                }
                                file.Write(ormfile, ormconfig, true);
                                System.Data.DbConfig.ReLoad();
                                System.Data.KvTableUtil.Save("ConnectionTest", "true");
                                if (System.Data.KvTableUtil.GetString("ConnectionTest") != "true")
                                {
                                    helper.Result.Add("Sorry,数据库连接失败！");
                                }
                            }
                            catch
                            {
                                helper.Result.Add("Sorry,数据库连接失败！");
                            }
                        }
                        if (helper.Result.IsValid)
                        {
                            string username = "admin";
                            string password = "admin";

                            Wlniao.WeChat.Model.WeiXin weixin = new Wlniao.WeChat.Model.WeiXin();
                            weixin.WeChatName = "";
                            weixin.WeChatToken = "";
                            weixin.AccountName = "";
                            weixin.AccountFirst = "";
                            weixin.CreateTime = DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss");
                            helper.Result.Join(weixin.insert());
                            if (password.Length != 32)
                            {
                                password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                            }
                            Wlniao.WeChat.Model.Manager manager = new Wlniao.WeChat.Model.Manager();
                            manager.ManagerUsername = username;
                            manager.ManagerPassword = password;
                            manager.IsAdministrator = 1;
                            manager.AccountFirst = weixin.AccountFirst;
                            helper.Result.Join(manager.insert());
                            if (helper.Result.IsValid)
                            {
                                System.Data.KvTableUtil.Save("Install", "true");
                            }
                        }
                    }
                    helper.ResponseResult();
                }
            }

            #endregion 处理结束
        }
    }
}
