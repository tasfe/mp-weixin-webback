//------------------------------------------------------------------------------
//	�ļ����ƣ�System\cvt.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Data;
namespace System {
    /// <summary>
    /// ��ͬ����֮����ֵת��
    /// </summary>
    public partial class cvt
    {
        /// <summary>
        /// ���������л�Ϊ xml (�ڲ����� .net ����Դ��� XmlSerializer)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToXML( Object obj ) {
            return EasyDB.SaveToString( obj );
        }
    }
}