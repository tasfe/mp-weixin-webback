using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCenterCMS.Web.Admin
{
    public partial class Install : System.Web.TemplateEngine
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AshxHelper helper = new AshxHelper(Context);
            #region 处理开始
            if (!IsPostBack)
            {
                InitRight();

                XCenter.Code.Domain.Sys.User _model = new XCenter.Code.Domain.Sys.User();
                _model.LoginName = "admin";
                _model.LoginPassword = System.Encryptor.Md5Encryptor32("admin").ToLower();
                db.insert(_model);
                cfgHelper.SetAppSettings("Administrator", "admin");
                cfgHelper.SetAppSettings("Install", "true");
            }

            #endregion 处理结束
        }

        public void InitRight()
        {
            try
            {
                XCenter.Code.Global.addRight("用户管理", "AccountManage", "管理权限", 0, 999, "管理网站用户");
                XCenter.Code.Global.addRight("用户权限", "AccountRightManage", "管理权限", 0, 998, "为用户设置权限");
                XCenter.Code.Global.addRight("日志查看", "OperateLogView", "管理权限", 0, 997, "管理日志查看");
                XCenter.Code.Global.addRight("备份管理", "BackupManage", "管理权限", 0, 969, "备份网站数据库");
                XCenter.Code.Global.addRight("栏目管理", "ClassManage", "管理权限", 0, 919, "设置网站内容栏目");
                XCenter.Code.Global.addRight("内容管理", "ArticleManage", "管理权限", 0, 909, "管理网站内容");
                XCenter.Code.Global.addRight("轮换图片", "FlashPicManage", "管理权限", 0, 918, "管理轮换图片（广告）");
                XCenter.Code.Global.addRight("友情链接", "FriendLinkManage", "管理权限", 0, 917, "管理友情链接");
                XCenter.Code.Global.addRight("云端应用", "WlniaoCloudApp", "增值服务", 0, 916, "未来鸟云端应用管理");
                XCenter.Code.Global.addRight("系统设置", "WebSiteSetting");
            }
            catch (Exception ex)
            {
                System.LogManager.GetLogger(this.GetType()).Info("权限初始化错误：" + ex.Message);
                System.LogManager.Flush();
            }
        }
    }
}
