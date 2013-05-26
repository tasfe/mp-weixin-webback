//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Factory\DbTypeChecker.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data;
using System.Reflection;
namespace System.Data
{
    /// <summary>
    /// ������ݿ����͵Ĺ���
    /// </summary>
    public class DbTypeChecker
    {

        public static DatabaseType GetDatabaseType(IDbCommand cmd)
        {
            if (cmd is SqlCommand)
            {
                return DatabaseType.SqlServer;
            }
            if (cmd is OleDbCommand)
            {
                return DatabaseType.Access;
            }
            if (cmd.GetType() == MysqlFactory.mySqlCommandType)
            {
                return DatabaseType.MySql;
            }
            if (cmd is OracleCommand)
            {
                return DatabaseType.Oracle;
            }
            return DatabaseType.Other;
        }

        public static DatabaseType GetDatabaseType(IDbConnection cn)
        {
            if (cn is OleDbConnection)
            {
                return DatabaseType.Access;
            }
            if (cn is SqlConnection)
            {
                return DatabaseType.SqlServer;
            }
            if (cn.GetType() == MysqlFactory.mySqlConnectionType)
            {
                return DatabaseType.MySql;
            }
            if (cn is OracleConnection)
            {
                return DatabaseType.Oracle;
            }
            return DatabaseType.Other;
        }

        public static DatabaseType GetDatabaseType(String connectionString)
        {
            if (!strUtil.IsNullOrEmpty(connectionString))
            {
                if (connectionString.ToLower().IndexOf("oledb") > 0)
                {
                    return DatabaseType.Access;
                }
                if (connectionString.ToLower().IndexOf("uid") > 0)
                {
                    return DatabaseType.SqlServer;
                }
                if (connectionString.ToLower().IndexOf("initial catalog") > 0)
                {
                    return DatabaseType.SqlServer;
                }
                if (connectionString.ToLower().IndexOf("user id") > 0)
                {
                    return DatabaseType.MySql;
                }
            }
            return DatabaseType.Access;
        }

        public static DatabaseType GetFromString(String typeString)
        {

            try
            {

                return (DatabaseType)Enum.Parse(typeof(DatabaseType), typeString, true);

            }
            catch (Exception ex)
            {
                throw new Exception("���ݿ��������ô���" + ex.Message);
            }

        }

    }
}