//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\AccessFactory_partial.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
namespace System.Data {
    /// <summary>
    /// access ���ݹ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public partial class AccessFactory : DbFactoryBase {
        internal override IDatabaseChecker GetDatabaseChecker() {
            return new AccessDatabaseChecker();
        }
    }
}