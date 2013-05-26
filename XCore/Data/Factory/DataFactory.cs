//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\DataFactory.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
namespace System.Data {
    /// <summary>
    /// ���ݹ��������Բ��ÿ������ݿ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public partial class DataFactory
    {
        public static IDbConnection GetConnection(String connectionString, DatabaseType dbtype)
        {
            return DbFactoryBase.Instance(dbtype).GetConnection(connectionString);
        }

        public static IDbCommand GetCommand(String CommandText, IDbConnection cn)
        {
            return DbFactoryBase.Instance(cn).GetCommand(CommandText);
        }

        internal static IDatabaseChecker GetDatabaseChecker(DatabaseType dbtype)
        {
            return DbFactoryBase.Instance(dbtype).GetDatabaseChecker();
        }

        public static IDatabaseDialect GetDialect(DatabaseType dbtype)
        {
            return DbFactoryBase.Instance(dbtype).GetDialect();
        }

        public static Object SetParameter(IDbCommand cmd, String parameterName, Object parameterValue)
        {
            return DbFactoryBase.Instance(cmd).SetParameter(cmd, parameterName, parameterValue);
        }

        public static DbDataAdapter GetAdapter(IDbCommand cmd)
        {
            return DbFactoryBase.Instance(cmd).GetAdapter();
        }

        public static DbDataAdapter GetAdapter(String CommandText, IDbConnection cn)
        {
            return DbFactoryBase.Instance(cn).GetAdapter(CommandText);
        }

    }


}
