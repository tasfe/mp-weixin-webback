//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\ColumnAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {

    /// <summary>
    /// ��������ע�����ڱ�ʶ���������ݿ��ж�Ӧ�������ƺͳ���
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Property )]
    public class ColumnAttribute : Attribute {
        private String _columnName;
        private String _label;
        private int _length;
        public ColumnAttribute() {
            _length = 250;
        }
        public String Name {
            get {
                return _columnName;
            }
            set {
                _columnName = value;
            }
        }
        public String Label {
            get {
                return _label;
            }
            set {
                _label = value;
            }
        }
        public int Length {
            get {
                return _length;
            }
            set {
                _length = value;
            }
        }
        public Boolean LengthSetted() {
            return _length > 0 && _length != 250;
        }
    }
}