namespace D.YMX.LogUtils
{
    /// <summary>
    /// 
    /// </summary>
    public interface INLogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        void Debug(Type source, string message, params object[] ps);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);
    }
}
