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

            // ��ȡJson�ļ�

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json"); //Ĭ�϶�ȡ����ǰ����Ŀ¼
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