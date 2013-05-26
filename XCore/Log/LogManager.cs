//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\LogManager.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Web;
using System.IO;
using System.Log;
using System.Web;
namespace System
{
    /// <summary>
    /// ��־�������ͨ�����ڻ�ȡ��־����
    /// </summary>
    /// <example>
    /// һ������ĵ�һ�ж���
    /// <code>
    /// private static readonly ILog logger = LogManager.GetLogger( typeof( ObjectBase ) );
    /// </code>
    /// Ȼ�����������������ʹ��
    /// <code>
    /// logger.Info( "your message" );
    /// </code>
    /// </example>
    public class LogManager
    {
        private LogManager() { }
        /// <summary>
        /// ��ȡһ����־����
        /// </summary>
        /// <param name="type">��������</param>
        /// <returns>������־����</returns>
        public static ILog GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }
        /// <summary>
        /// ��ȡһ����־����
        /// </summary>
        /// <param name="typeName">��������</param>
        /// <returns>������־����</returns>
        public static ILog GetLogger(String typeName)
        {
            ILog log = getLogger();
            log.TypeName = typeName;
            return log;
        }
        /// <summary>
        /// ��ȡһ����־����
        /// </summary>
        /// <param name="typeName">��������</param>
        /// <returns>������־����</returns>
        public static ILog GetLogger()
        {
            ILog log = getLogger();
            log.TypeName = "Unknown Class";
            return log;
        }
        private static ILog getLogger()
        {
            if (LogConfig.Instance.Level == LogLevel.None)
                return new NullLogger();
            if (strUtil.IsNullOrEmpty(LogConfig.Instance.LoggerImpl))
                return new FileLogger();
            ILog log = null;
            String loggerImpl = LogConfig.Instance.LoggerImpl;
            if (strUtil.HasText(loggerImpl))
            {
                String[] strArray = loggerImpl.Split(new char[] { ',' });
                if (strArray.Length == 1)
                {
                    Type type = Type.GetType(strArray[0].Trim());
                    if (type != null)
                    {
                        log = rft.GetInstance(type) as ILog;
                    }
                    return log;
                }
                if (strArray.Length == 2)
                {
                    log = Assembly.Load(strArray[1].Trim()).CreateInstance(strArray[0].Trim()) as ILog;
                }
            }
            return log;
        }
        /// <summary>
        /// ��������־����д�����(�� web �У���־����ҳ�����������ʱ���һ��д�뵽���̵�)
        /// </summary>
        public static void Flush()
        {
            if (!SystemInfo.IsWeb) return;
            LoggerUtil.Flush();
        }
    }
}

