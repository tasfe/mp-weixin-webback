//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\OracleFactory.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.Common;
namespace System.Data {
    /// <summary>
    /// oracle ���ݹ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public partial class OracleFactory : DbFactoryBase
    {
        // TODO
        internal override IDatabaseChecker GetDatabaseChecker() {
            throw new Exception( lang.get( "dbNotSupport" ) );
        }
    }
}