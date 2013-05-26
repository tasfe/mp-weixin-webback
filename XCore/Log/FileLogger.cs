//------------------------------------------------------------------------------
//	文件名称：System\Log\FileLogger.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System;
namespace System.Log {

    /// <summary>
    /// 文件日志工具，所有日志会被写入磁盘
    /// </summary>
    internal partial class FileLogger : ILog
    {

        private LogLevel _levelSetting;
        private LogMessage _msg;

        public FileLogger() {
            _levelSetting = LogConfig.Instance.Level;
            _msg = new LogMessage();
        }

        public void Debug( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "debug";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Debug) {
                LoggerUtil.WriteFile( _msg );
            }
        }

        public void Info(String message)
        {
            Boolean isSql = false;
            if (message.StartsWith(LoggerUtil.SqlPrefix))
            {
                isSql = true;
                message = strUtil.TrimStart(message, LoggerUtil.SqlPrefix);
            }

            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "info";
            System.Diagnostics.Debug.Write(LoggerUtil.GetFormatMsg(_msg));
            if (_levelSetting >= LogLevel.Info)
            {
                LoggerUtil.WriteFile(_msg);
            }

            if (isSql) LoggerUtil.LogSqlCount();
        }
        public void Warn( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "warn";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Warn) {
                LoggerUtil.WriteFile( _msg );
            }
        }

        public void Error( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "error";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Error) {
                LoggerUtil.WriteFile( _msg );
            }
        }

        public void Fatal( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "fatal";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Fatal) {
                LoggerUtil.WriteFile( _msg );
            }
        }
        public void Code(string file, int line)
        {
            _msg.LogTime = DateTime.Now;
            _msg.Message = string.Format(" \r\nCodeFile:{0} \r\nCodeLine:{1} \r\n", file, line);
            _msg.LogLevel = "code";
            System.Diagnostics.Debug.Write(LoggerUtil.GetFormatMsg(_msg));
            LoggerUtil.WriteFileNow(_msg);
        }

        public String TypeName {
            set {
                _msg.TypeName = value;
            }
        }


    }
}

