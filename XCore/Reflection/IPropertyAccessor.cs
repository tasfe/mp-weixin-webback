//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Reflection\IPropertyAccessor.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��11�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.Reflection {
    /// <summary>
    /// ���Բ�������
    /// </summary>
    public interface IPropertyAccessor {
        Object Get( Object target );
        void Set( Object target, Object value );
    }
}