//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\DbFactoryBase.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
namespace System.Data {
    /// <summary>
    /// ���ݹ���������࣬���Բ��ÿ������ݿ�������ȡ Connection, Command, DataAdapter
    /// </summary>
    public abstract partial class DbFactoryBase
    {
        internal abstract IDatabaseChecker GetDatabaseChecker();
        public static DbFactoryBase Instance(String connectionString)
        {
            DbFactoryBase result = Instance(DbTypeChecker.GetDatabaseType(connectionString));
            result.cn = result.GetConnection(connectionString);
            return result;
        }

        public static DbFactoryBase Instance(IDbConnection cn)
        {
            DbFactoryBase result = Instance(DbTypeChecker.GetDatabaseType(cn));
            result.cn = cn;
            return result;
        }

        public static DbFactoryBase Instance(IDbCommand cmd)
        {
            DbFactoryBase result = Instance(DbTypeChecker.GetDatabaseType(cmd));
            result.cmd = cmd;
            return result;
        }


        public static DbFactoryBase Instance(DatabaseType dbtype)
        {

            if (dbtype == DatabaseType.SqlServer) return new MsSqlDbFactory();
            if (dbtype == DatabaseType.SqlServer2000) return new MsSqlDbFactory();
            if (dbtype == DatabaseType.Access) return new AccessFactory();
            if (dbtype == DatabaseType.MySql) return new MysqlFactory();
            if (dbtype == DatabaseType.Oracle) return new OracleFactory();

            throw new Exception(lang.get("dbNotSupport"));
        }

        public abstract IDbConnection GetConnection(String connectionString);
        public abstract IDbCommand GetCommand(String CommandText);
        public abstract IDatabaseDialect GetDialect();
        public abstract Object SetParameter(IDbCommand cmd, String parameterName, Object parameterValue);
        public abstract DbDataAdapter GetAdapter();
        public abstract DbDataAdapter GetAdapter(String CommandText);


        protected IDbConnection cn;
        protected IDbCommand cmd;


        protected virtual void setTransaction(IDbCommand cmd)
        {
            DbContext.setTransaction(cmd);
        }

        protected virtual Object processValue(Object parameterValue)
        {

            if (parameterValue is DateTime)
            {
                DateTime time = (DateTime)parameterValue;
                if ((time < new DateTime(1800, 1, 1)) || (time > new DateTime(9000, 1, 1)))
                {
                    parameterValue = DateTime.Now;
                }
            }
            else if (parameterValue is string)
            {
                parameterValue = parameterValue.ToString().Trim();
            }
            else if (parameterValue is int && (int)parameterValue == 0)
            {
                parameterValue = Convert.ToInt32(parameterValue);
            }

            return parameterValue;

        }

    }
}