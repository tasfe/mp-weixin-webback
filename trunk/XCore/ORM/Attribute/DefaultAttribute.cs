//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\DefaultAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// Ĭ��ֵ��ע��������û�б���ֵ��ʱ��ϵͳʹ�ô�Ĭ��ֵ�������ݿ�
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Property )]
    public class DefaultAttribute : Attribute {
        private Object _value;
        public DefaultAttribute( Object val ) {
            _value = val;
        }
        public Object Value {
            get { return _value; }
            set { _value = value; }
        }
    }
}