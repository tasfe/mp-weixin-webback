//------------------------------------------------------------------------------
//	�ļ����ƣ�System\ORM\Attribute\DateTimeAttribute.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.ORM {
    /// <summary>
    /// ʱ����ע�����Ա��������������ݡ�
    /// ���ݿ�洢��ʱ��ʹ���������ͽ��д洢
    /// </summary>
    [Serializable, AttributeUsage( AttributeTargets.Property )]
    public class DateTimeAttribute : Attribute{ }
}