//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Data\DbChecker\IDatabaseChecker.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ORM;
namespace System.Data {
    internal interface IDatabaseChecker {
        String ConnectionString { get; set; }
        DatabaseType DatabaseType { get; set; }
        void CheckDatabase();
        void CheckTable( MappingClass mapping, String db );
        List<String> GetTables();
    }
}