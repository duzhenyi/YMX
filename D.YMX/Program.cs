using D.YMX.LogUtils;
using D.YMX.Utils;
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

            var account = ConfigurationManager.AppSettings["PROXY-ACCOUNT"];
            var pwd = ConfigurationManager.AppSettings["PROXY-PWD"];
            var url = ConfigurationManager.AppSettings["PROXY-URL"];
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(pwd))
            {
                AppconfigUtil.ProxyUtil = new ProxyConfig()
                {
                    ProxyUrl = url,
                    Account = account,
                    Pwd = pwd,
                };
            }

            Application.Run(new FrmMain());
        }
    }
}