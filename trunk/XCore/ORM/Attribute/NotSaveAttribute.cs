//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\NotSaveAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// ORM�ڱ������ݵ�ʱ�򣬻���Դ��� NotSave ��ע������
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Property | AttributeTargets.Class )]
    public class NotSaveAttribute : Attribute {}
}