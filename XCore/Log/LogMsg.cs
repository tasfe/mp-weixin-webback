//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\LogMsg.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System;
namespace System.Log {
    /// <summary>
    /// ��־��Ϣ(��δʵ�֣�����ʹ��)
    /// </summary>
    internal partial class LogMsg : ILogMsg
    {
        private int _id;
        private String _level;
        private DateTime _logTime;
        private String _message;
        private String _typeName;
        public void Insert() {}
        /// <summary>
        /// ��־�ı��
        /// </summary>
        public int Id {
            get { return _id; }
            set { _id = value; }
        }
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