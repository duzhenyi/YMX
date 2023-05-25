using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;

namespace D.YMX.LogUtils
{
    /// <summary>
    /// 使用 NLog 日志框架，集成原生 ILogger 接口做日志记录；
    /// </summary>
    public static class NLogUtil
    {
        // NLog 日志
        public static INLogService Log = null;

        /// <summary>
        /// 添加日志中间件
        /// </summary>
        /// <param name="services"></param>
        public static void AddNLogUtil()
        {
            if (Log == null)
            {
                Log = new NLogService();
            }
        }
    }
}
