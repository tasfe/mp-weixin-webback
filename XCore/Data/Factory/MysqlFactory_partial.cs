//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\MysqlFactory_partial.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
namespace System.Data {
    /// <summary>
    /// mysql ���ݹ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public partial class MysqlFactory : DbFactoryBase
    {
        internal override IDatabaseChecker GetDatabaseChecker()
        {
            return new MysqlDatabaseChecker();
        }
    }
}