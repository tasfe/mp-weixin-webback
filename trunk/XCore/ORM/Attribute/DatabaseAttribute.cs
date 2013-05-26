//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\DatabaseAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// ���ݿ���ע
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Class )]
    public class DatabaseAttribute : Attribute {
        private String _dbName;
        public DatabaseAttribute( String dbName ) {
            _dbName = dbName;
        }
        public String Database {
            get {
                return _dbName;
            }
            set {
                _dbName = value;
            }
        }
    }
}