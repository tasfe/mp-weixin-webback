//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\LogMessage.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System.Log {
    /// <summary>
    /// ��־��Ϣ
    /// </summary>
    public class LogMessage : ILogMsg {
        private String _level;
        private DateTime _logTime;
        private String _message;
        private String _typeName;
        /// <summary>
        /// ��־�ĵȼ�
        /// </summary>
        public String LogLevel {
            get { return _level; }
            set { _level = value; }
        }
        /// <summary>
        /// ��־��ʱ��
        /// </summary>
        public DateTime LogTime {
            get { return _logTime; }
            set { _logTime = value; }
        }
        /// <summary>
        /// ��־������
        /// </summary>
        public String Message {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// �����־����������
        /// </summary>
        public String TypeName {
            get { return _typeName; }
            set { _typeName = value; }
        }
    }
}

