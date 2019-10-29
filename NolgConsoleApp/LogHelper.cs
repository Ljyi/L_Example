using NLog;
using NLog.Config;
using System;
using System.IO;

namespace NolgConsoleApp
{
    public static class LogHelper
    {
        private static ILogger logger = GetLogger();
        private static ILogger GetLogger()
        {
            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Nlog.config"));
            return LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="debug"></param>
        public static void Debug(this string debug)
        {
            logger.Debug(debug);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="info"></param>
        public static void Info(this string info)
        {
            logger.Info(info);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="warn"></param>
        public static void Warn(this string warn)
        {
            logger.Warn(warn);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="error"></param>
        public static void Error(this string error)
        {
            logger.Error(error);
        }
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="fatale"></param>
        public static void Fatal(this string fatal)
        {
            logger.Fatal(fatal);
        }

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="trace"></param>
        public static void Trace(this string trace)
        {
            logger.Trace(trace);
        }
    }
}
