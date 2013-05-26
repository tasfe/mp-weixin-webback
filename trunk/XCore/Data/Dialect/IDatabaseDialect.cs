//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\Dialect\IDatabaseDialect.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.Data {
    /// <summary>
    /// �������ݿ�������﷨����ӿ�
    /// </summary>
    public interface IDatabaseDialect {
        String GetConnectionItem( String connectionString, ConnectionItemType connectionItem );
        String GetLimit( String sql, int limit );
        String GetLimit( String sql );
        String GetParameter( String parameterName );
        String GetParameterAdder( String parameterName );
        String GetTimeQuote();
        String GetLeftQuote();
        String GetRightQuote();
        String Top { get; }
    }
}