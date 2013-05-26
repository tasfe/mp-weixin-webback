/*------------------------------------------------------------------------------
        程序名称：Weback微信公众帐号管理系统
        源码作者：谢超逸 © Wlniao  http://www.xiechaoyi.com
        
 
        文件名称：Wlniao.WeChat\BLL\Sys.cs
        运 行 库：2.0.50727.1882
        代码功能：系统数据存储方法定义

        最后修改：2013年4月11日 07:30:00
        修改备注：
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
namespace Wlniao.WeChat.BLL
{
    public class Sys
    {
        public static Result AddWeiXin(String WeChatName, String AccountName, String AccountFirst, String WeChatToken, String username, String password, Boolean supper)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(WeChatName))
            {
                result.Add("Sorry,微信公众帐号名称未填写");
            }
            else if (string.IsNullOrEmpty(AccountFirst))
            {
                result.Add("Sorry,微信原始帐号未填写");
            }
            else
            {
                Model.WeiXin weixin = new Model.WeiXin();
                weixin.WeChatName = WeChatName;
                weixin.WeChatToken = WeChatToken;
                weixin.AccountName = AccountName;
                weixin.AccountFirst = AccountFirst;
                weixin.CreateTime = DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss");
                result.Join(weixin.insert());
                if (!string.IsNullOrEmpty(username))
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        result.Add("Sorry,管理员密码不能为空");
                    }
                    else
                    {
                        if (password.Length != 32)
                        {
                            password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                        }
                        Model.Manager manager = new Model.Manager();
                        manager.ManagerUsername = username;
                        manager.ManagerPassword = password;
                        manager.IsAdministrator = 0;
                        manager.AccountFirst = weixin.AccountFirst;
                        result.Join(manager.insert());
                    }
                }
            } 
            return result;
        }
        public static Result CheckLogin(String username, String password)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(username))
            {
                result.Add("Sorry,管理员账号不能为空");
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Add("Sorry,管理员密码不能为空");
            }
            else
            {
                if (password.Length != 32)
                {
                    password = Encryptor.Md5Encryptor32(Encryptor.Md5Encryptor32(password));
                }
                Model.Manager manager = Model.Manager.findByField("ManagerUsername", username);
                if (manager == null)
                {
                    result.Add("Sorry,管理员账号不存在");
                }
                else if (manager.ManagerPassword != password)
                {
                    result.Add("Sorry,您输入的密码不正确");
                }
            }
            return result;
        }
    }
}
