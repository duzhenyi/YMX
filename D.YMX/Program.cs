using D.YMX.LogUtils;
using D.YMX.Utils;
using Microsoft.Extensions.Configuration;
using NLog;
using Org.BouncyCastle.Asn1.Crmf;
using System.Configuration;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace D.YMX
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            NLogUtil.AddNLogUtil();

            // 读取Json文件

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json"); //默认读取：当前运行目录
            IConfigurationRoot configuration = builder.Build();
            
            string url= configuration.GetSection("url").Value;
            string account = configuration.GetSection("account").Value;
            string pwd = configuration.GetSection("pwd").Value;

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(pwd))
            {
                JsonConfigUtil.ProxyUtil = new ProxyConfig()
                {
                    ProxyUrl = url,
                    Account = account,
                    Pwd = pwd,
                };
            }
            Application.Run(new FrmImgTest());
        }
    }
}