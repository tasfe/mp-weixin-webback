//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\LabelAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {

    /// <summary>
    /// label ��ע�����ڱ�������Զ�����
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Class )]
    public class LabelAttribute : Attribute {

        private String _label;

        public LabelAttribute( String lbl ) {
            _label = lbl;
        }

        public String Label {
            get {
                return _label;
            }
            set {
                _label = value;
            }
        }
    }
}

