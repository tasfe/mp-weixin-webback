//------------------------------------------------------------------------------
//	文件名称：System\Log\ILog.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
namespace System
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public partial interface ILog
    {
        /// <summary>
        /// 调试信息日志
        /// </summary>
        /// <param name="message"></param>
        void Debug(String message);
        /// <summary>
        /// 普通信息日志
        /// </summary>
        /// <param name="message"></param>
        void Info(String message);
        /// <summary>
        /// 警告信息日志
        /// </summary>
        /// <param name="message"></param>
        void Warn(String message);
        /// <summary>
        /// 错误信息日志
        /// </summary>
        /// <param name="message"></param>
        void Error(String message);
        /// <summary>
        /// 崩溃信息日志
        /// </summary>
        /// <param name="message"></param>
        void Fatal(String message);
        /// <summary>
        /// 记录代码执行情况信息日志
        /// </summary>
        /// <param name="message"></param>
        void Code(String file,Int32 line);
        /// <summary>
        /// 输出日志的类型名称
        /// </summary>
        String TypeName { set; }
    }
}