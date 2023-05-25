using NLog;

namespace D.YMX.LogUtils
{
    /// <summary>
    /// 写日志
    /// </summary>
    public class NLogService : INLogService
    {
        /// <summary>
        /// 
        /// </summary>
        public Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        public void Debug(Type source, string message, params object[] ps)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Debug(message, ps);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
         {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }
    }
}
