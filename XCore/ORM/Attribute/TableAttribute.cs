//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\TableAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// ��������ע�����ڱ�ʶ���������ݿ��ж�Ӧ�ı�����
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Class )]
    public class TableAttribute : Attribute {
        private String _tableName;
        public TableAttribute( String tableName ) {
            _tableName = tableName;
        }
        public String TableName {
            get {
                return _tableName;
            }
            set {
                _tableName = value;
            }
        }
    }
}