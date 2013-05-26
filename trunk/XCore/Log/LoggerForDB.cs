//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Log\LoggerForDB.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System;
namespace System.Log {
    /// <summary>
    /// �洢�����ݿ����־(��δʵ�֣�����ʹ��)
    /// </summary>
    internal class LoggerForDB : ILog {
        private LogLevel _levelSetting;
        private LogMsg _msg;
        public LoggerForDB() {
            _levelSetting = LogConfig.Instance.Level;
            _msg = new LogMsg();
        }
        public void Debug( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "debug";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Debug)
                _msg.Insert();
        }
        public void Info( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "info";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Info)
                _msg.Insert();
        }
        public void Warn( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "warn";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Warn)
                _msg.Insert();
        }
        public void Error( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "error";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Error)
                _msg.Insert();
        }
        public void Fatal( String message ) {
            _msg.LogTime = DateTime.Now;
            _msg.Message = message;
            _msg.LogLevel = "fatal";
            System.Diagnostics.Debug.Write( LoggerUtil.GetFormatMsg( _msg ) );
            if (_levelSetting >= LogLevel.Fatal)
                _msg.Insert();
        }
        /// <summary>
        /// ��¼����ִ�������Ϣ��־
        /// </summary>
        /// <param name="message"></param>
        public void Code(String file, Int32 line)
        {
            _msg.LogTime = DateTime.Now;
            _msg.Message = string.Format(" \r\nCodeFile:{0} \r\nCodeLine:{1} \r\n", file, line);
            _msg.LogLevel = "code";
            System.Diagnostics.Debug.Write(LoggerUtil.GetFormatMsg(_msg));
            _msg.Insert();
        }
        public String TypeName {
            set { _msg.TypeName = value; }
        }
    }
}