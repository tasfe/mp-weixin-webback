//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Dialect\AccessDialect.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Web;
using System.Web;
namespace System.Data {
    /// <summary>
    /// access �����﷨������
    /// </summary>
    [Serializable]
    public class AccessDialect : IDatabaseDialect {
        public String GetConnectionItem( String connectionString, ConnectionItemType connectionItem ) {
            String str = connectionItem.ToString().ToLower().Replace( "database", "data source" ).Replace( "userid", "user id" );
            String[] arrItem = connectionString.ToLower().Split( new char[] { ';' } );
            foreach (String item in arrItem) {
                if (item.Trim().ToLower().StartsWith( str ))
                    return item.Replace( str, "" ).Replace( "=", "" ).Replace( " ", "" );
            }
            return null;
        }
        public String GetLimit( String sql, int limit ) {
            return sql.ToLower().Replace( "select ", "select top " + limit + " " );
        }
        public String GetLimit( String sql) {
            return sql;
        }
        public String GetTimeQuote() {
            return "#";
        }
        public String GetParameter( String parameterName ) {
            return "?";
        }
        public String GetParameterAdder( String parameterName ) {
            return ("@" + parameterName);
        }
        public static String MapPath( String connectionString ) {
            if (SystemInfo.IsWeb==false)
                return connectionString;
            String connectionItem = new AccessDialect().GetConnectionItem( connectionString, ConnectionItemType.Database );
            String newValue = PathHelper.Map( connectionItem );
            return connectionString.Replace( connectionItem, newValue );
        }
        public String Top {
            get { return "top"; }
        }
        public string GetLeftQuote() {
            return "[";
        }
        public string GetRightQuote() {
            return "]";
        }
    }
}