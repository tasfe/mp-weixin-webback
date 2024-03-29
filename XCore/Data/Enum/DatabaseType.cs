//------------------------------------------------------------------------------
//	文件名称：System\Data\Enum\DatabaseType.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.Data {
    /// <summary>
    /// XCenter ORM 支持的数据库类型
    /// </summary>
    public enum DatabaseType {
        Access,
        SqlServer,
        SqlServer2000,
        MySql,
        SQLite,
        Oracle,
        Other
    }
}