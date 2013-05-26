//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\ILogMsg.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.Log {
    /// <summary>
    /// ��־��Ϣ�ӿ�
    /// </summary>
    public interface ILogMsg {
        /// <summary>
        /// ��־�ĵȼ�
        /// </summary>
        String LogLevel { get; set; }
        /// <summary>
        /// ��־��ʱ��
        /// </summary>
        DateTime LogTime { get; set; }
        /// <summary>
        /// ��־������
        /// </summary>
        String Message { get; set; }
        /// <summary>
        /// �����־����������
        /// </summary>
        String TypeName { get; set; }
    }
}