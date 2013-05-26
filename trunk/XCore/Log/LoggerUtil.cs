//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\LoggerUtil.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Web;
namespace System.Log {
    /// <summary>
    /// ��־������
    /// </summary>
    public partial class LoggerUtil
    {
        private static Object objLock = new object();
        /// <summary>
        /// sql ��־��ǰ׺
        /// </summary>
        public static readonly String SqlPrefix = "sql=";
        /// <summary>
        /// ����־д�����
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteFile( ILogMsg msg ) {
            if (!SystemInfo.IsWeb || LogConfig.Instance.InRealTime)
            {
                writeFilePrivate(msg);
                return;
            }
            StringBuilder sb = CurrentRequest.getItem( "currentLogList" ) as StringBuilder;
            if (sb == null) {
                sb = new StringBuilder();
                CurrentRequest.setItem( "currentLogList", sb );
            }
            sb.AppendFormat( "{0} {1} {2} - {3} \r\n", msg.LogTime, msg.LogLevel, msg.TypeName, msg.Message );
        }
        /// <summary>
        /// ����־д�����
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteFileNow(ILogMsg msg)
        {
            writeFilePrivate(msg);
            return;
        }
        /// <summary>
        /// �� web ϵͳ�У���¼ sql ִ�еĴ���
        /// </summary>
        public static void LogSqlCount()
        {
            if (CurrentRequest.getItem("sqlcount") == null)
            {
                CurrentRequest.setItem("sqlcount", 1);
            }
            else
            {
                CurrentRequest.setItem("sqlcount", ((int)CurrentRequest.getItem("sqlcount")) + 1);
            }
        }
        private static void writeFilePrivate( ILogMsg msg ) {
            String formatMsg = GetFormatMsg( msg );
            writeContentToFile( formatMsg );
        }
        private static void writeContentToFile( String formatMsg ) {
            String logFilePath = LogConfig.Instance.FilePath;
            lock (objLock) {
                if (System.IO.File.Exists( logFilePath )) {
                    DateTime lastAccessTime = System.IO.File.GetLastWriteTime( logFilePath );
                    DateTime now = DateTime.Now;
                    if (cvt.IsDayEqual( lastAccessTime, now )) {
                        System.IO.FileEx.Append(logFilePath, formatMsg);
                    }
                    else {
                        String destFileName = getDestFileName( logFilePath );
                        System.IO.FileEx.Move(logFilePath, destFileName);
                        System.IO.FileEx.Write( logFilePath, formatMsg );
                    }
                }
                else {
                    System.IO.FileEx.Write(logFilePath, formatMsg);
                }
            }
        }
        private static String getDestFileName( string logFilePath ) {
            String ext = Path.GetExtension( logFilePath );
            String pathWithoutExt = strUtil.TrimEnd( logFilePath, ext );
            return pathWithoutExt + "_" + DateTime.Now.Subtract( TimeSpan.FromDays( 1 ) ).ToString( "yyyy.MM.dd" ) + ext;
        }
        public static String GetFormatMsg( ILogMsg logMsg ) {
            return String.Format( "{0} {1} {2} - {3} \r\n", logMsg.LogTime, logMsg.LogLevel, logMsg.TypeName, logMsg.Message );
        }
        /// <summary>
        /// ��������־����д�����
        /// </summary>
        internal static void Flush() {
            StringBuilder sb = CurrentRequest.getItem( "currentLogList" ) as StringBuilder;
            if (sb != null)
                writeContentToFile( sb.ToString() );
        }

        //��ȡ�����кŵĺ���
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }
        //��ȡ�����ļ��ĺ���
        public static string GetFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileName();
        }
        //��ȡ���뷽���ĺ���
        public static string GetMethod()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetMethod().Name;
        }
    }
}