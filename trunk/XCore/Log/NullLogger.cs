//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\NullLogger.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System;
namespace System.Log
{
    /// <summary>
    /// ʹ�� null ģʽ����־����
    /// </summary>
    internal class NullLogger : ILog
    {
        /// <summary>
        /// ������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Debug(String message) { }
        /// <summary>
        /// ��ͨ��Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Info(String message) { }
        /// <summary>
        /// ������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Warn(String message) { }
        /// <summary>
        /// ������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Error(String message) { }
        /// <summary>
        /// ������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(String message) { }
        /// <summary>
        /// ��¼����ִ�������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Code(String file, Int32 line) { }
        /// <summary>
        /// �����־����������
        /// </summary>
        public String TypeName { set { } }
    }
}