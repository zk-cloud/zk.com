using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Utility
{
    public static class LogHelper
    {
        private static ILog _log = LogManager.GetLogger(typeof(LogHelper));

        #region 日志记录


        /// <summary>
        /// 调试日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public static void Debug(object msg)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(msg);
            }

        }

        /// <summary>
        /// 调试日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        /// <param name="ex">输出异常</param>
        public static void Debug(object msg, Exception ex)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(msg, ex);
            }
        }


        /// <summary>
        /// 信息日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public static void Info(object msg)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(msg);
            }
        }

        /// <summary>
        /// 信息日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        /// <param name="ex">输出异常</param>
        public static void Info(object msg, Exception ex)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(msg, ex);
            }
        }

        /// <summary>
        /// 警告日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public static void Warn(object msg)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(msg);
            }
        }

        /// <summary>
        /// 警告日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        /// <param name="ex">输出异常</param>
        public static void Warn(object msg, Exception ex)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(msg, ex);
            }
        }

        /// <summary>
        /// 错误日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public static void Error(object msg)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(msg);
            }
        }

        /// <summary>
        /// 错误日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        /// <param name="ex">输出异常</param>
        public static void Error(object msg, Exception ex)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(msg, ex);
            }
        }

        /// <summary>
        /// 致命日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        public static void Fatal(object msg)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(msg);
            }
        }

        /// <summary>
        /// 致命日志输出
        /// </summary>
        /// <param name="msg">输出内容</param>
        /// <param name="ex">输出异常</param>
        public static void Fatal(object msg, Exception ex)
        {

            if (_log.IsFatalEnabled)
            {
                _log.Fatal(msg, ex);
            }
        }


        #endregion
    }
}
