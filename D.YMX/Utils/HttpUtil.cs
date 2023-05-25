using D.YMX.LogUtils;
using D.YMX.Models;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D.YMX.Utils
{
    public class ProxyObj
    {
        public bool success { get; set; }

        public List<Proxy> result { get; set; }
    }

    public static class HttpUtil
    {
        /// <summary>
        /// 定义Cookie容器
        /// </summary>
        public static Dictionary<string, CookieContainer> CookiesContainer { get; set; }

        ////API链接  在后台获取
        //const string proxyAPI = "http://15680505585.user.xiecaiyun.com/api/proxies?action=getJSON&key=NP77DDD613&count=&word=&rand=true&norepeat=true&detail=false&ltime=&idshow=false";
        ////后台用户名
        //public const string proxyusernm = "15680505585";
        ////后台密码
        //public const string proxypasswd = "*15680505585*";
        public static Proxy GetProxyIp()
        {
            WebClient wc = new WebClient();
            string body = wc.DownloadString(AppconfigUtil.ProxyUtil.ProxyUrl);
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream);
            writer.Write(body);
            writer.Flush();
            memoryStream.Position = 0;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProxyObj), new DataContractJsonSerializerSettings()
            {
                IgnoreExtensionDataObject = true
            });
            ProxyObj po = (ProxyObj)serializer.ReadObject(memoryStream);
            if (po.success && po.result.Count > 0)
            {
                return po.result[0];
            }
            throw new Exception("获取代理IP失败");
        }


        public static byte[] GetBytesFromUrl(string imgUrl, bool openProxy = false)
        {
            byte[] b;

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(imgUrl);

            if (AppconfigUtil.ProxyUtil != null && openProxy)
            {

                Proxy p = HttpUtil.GetProxyIp();
                myReq.Proxy = new WebProxy(p.ip, p.port);
                myReq.Proxy.Credentials = new NetworkCredential(AppconfigUtil.ProxyUtil.Account, AppconfigUtil.ProxyUtil.Pwd);
            }

            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }

        public static async Task<string> GetHtmlAsync(string url, bool openProxy = false)
        {
            try
            {
                //var fingerprint = Generate("www.amazon.com", 443);

                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true,
                    MaxConnectionsPerServer = int.MaxValue,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls |
                                   System.Security.Authentication.SslProtocols.Tls11 |
                                   System.Security.Authentication.SslProtocols.Tls12 |
                                   System.Security.Authentication.SslProtocols.Tls13,
                    //ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    //{
                    //    // 检查证书指纹是否匹配
                    //    var actualFingerprint = cert.GetCertHashString();
                    //    if (fingerprint.Equals(actualFingerprint, StringComparison.OrdinalIgnoreCase))
                    //    {
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }
                    //}
                    // 因为我们再上面把证书添加到本机受信任了 所以这行代码不需要，如果你不操作受信任证书的话，就需要
                    //ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                if (openProxy && AppconfigUtil.ProxyUtil != null)
                {
                    Proxy p = GetProxyIp();

                    handler.UseProxy = true;
                    handler.Proxy = new WebProxy(p.ip, p.port);
                    handler.Proxy.Credentials = new NetworkCredential(AppconfigUtil.ProxyUtil.Account, AppconfigUtil.ProxyUtil.Pwd);
                }

                var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(60);
                client.DefaultRequestVersion = new Version(2, 0);

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"));//"text/html;charset=UTF-8";
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64");
                client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");

                var response = await client.GetStringAsync(url);

                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    Stream myResponseStream = await response.Content.ReadAsStreamAsync();
                //    StreamReader myStreamReader = new StreamReader(myResponseStream);
                //    string retString = await myStreamReader.ReadToEndAsync();
                //    myStreamReader.Close();
                //    myResponseStream.Close();
                //    return retString;
                //}
                errorCount = 0;
                return response;
            }
            catch (Exception ex)
            {
                errorCount++;
                if (errorCount < 10)
                {
                    return await GetHtmlAsync(url, openProxy);
                }
                else
                {
                    NLogUtil.Log.Error(ex);
                    errorCount = 0;
                    return null;
                }
            }
        }
        private static int errorCount = 0;

        public static string Generate(string hostname, int port)
        {
            using (var client = new TcpClient(hostname, port))
            using (var stream = client.GetStream())
            using (var sslStream = new SslStream(stream))
            {
                sslStream.AuthenticateAsClient(hostname);

                var cert = sslStream.RemoteCertificate as X509Certificate2;
                if (cert == null)
                {
                    throw new Exception("Failed to get remote certificate.");
                }
                var md5 = cert.GetCertHashString();
                return md5;
                //var sha256 = cert.GetCertHashString(System.Security.Cryptography.HashAlgorithmName.SHA256);
                //return sha256.Replace(" ", "").ToLower();
            }
        }


        //public static async Task<string> GetHtml(string domain, string url, bool openProxy = false)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    // 谷歌 m,a,s,p
        //    request.Method = "GET";
        //    request.Headers.Add(":authority", domain);
        //    request.Headers.Add(":scheme", "https");
        //    request.Headers.Add("path", url);

        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
        //    request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");//定义gzip压缩页面支持
        //    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
        //    //cache-control:max-age=0

        //    request.ContentType = "application/json;charset=UTF-8"; //"text/html;charset=UTF-8";
        //    request.KeepAlive = true;
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64";

        //    // 因为我们再上面把证书添加到本机受信任了 所以这行代码不需要，如果你不操作受信任证书的话，就需要
        //    //httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        //    // 设置ja3指纹
        //    request.Headers.Add("tls-ja3", "771,4865-4866-4867-49195-49199-49196-49200-52393-52392-49171-49172-156-157-47-53,17513-10-18-11-51-13-27-0-35-65281-43-16-45-5-23-21,29-23-24,0");
        //    // 设置ja3proxy执行请求的超时
        //    request.Headers.Add("tls-timeout", "10");
        //    // 设置ja3proxy执行请求用代理，设置后请求目标服务器拿到的就是代理ip
        //    // client2.DefaultRequestHeaders.Add("tls-proxy","http://252.45.26.333:5543");

        //    if (openProxy)
        //    {
        //        Proxy p = GetProxyIp();
        //        request.Proxy = new WebProxy(p.ip, p.port);
        //        request.Proxy.Credentials = new NetworkCredential(proxyusernm, proxypasswd);
        //    }
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream myResponseStream = response.GetResponseStream();
        //    StreamReader myStreamReader = new StreamReader(myResponseStream);
        //    string retString = await myStreamReader.ReadToEndAsync();
        //    myStreamReader.Close();
        //    myResponseStream.Close();
        //    return retString;
        //}

        /// <summary>
        /// 异步创建爬
        /// </summary>
        /// <param name="uri">爬URL地址</param>
        /// <param name="proxy">代理服务器，没被封锁就别使用代理：60.221.50.118:8090</param>
        /// <returns>网页源代码</returns>
        public static async Task<string> GetHtmlAsync(string domain, string url, bool openProxy = false)
        {
            if (CookiesContainer == null)
            {
                CookiesContainer = new Dictionary<string, CookieContainer>();
            }
            if (!CookiesContainer.ContainsKey(domain))
            {
                CookiesContainer.Add(domain, null);
            }
            return await Task.Run(async () =>
            {
                var pageSource = string.Empty;
                try
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    //request.Accept = "*/*";
                    request.ServicePoint.Expect100Continue = false;//加快载入速度
                    request.ServicePoint.UseNagleAlgorithm = false;//禁止Nagle算法加快载入速度
                    request.AllowWriteStreamBuffering = false;//禁止缓冲加快载入速度
                    request.AllowAutoRedirect = false;//禁止自动跳转
                    //设置User-Agent，伪装成Google Chrome浏览器
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64";
                    request.ContentType = "text/html;charset=UTF-8";
                    //request.ContentType = "application/x-www-form-urlencoded";//定义文档类型及编码
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
                    // ContentType、UserAgent、Accept、Referer、Connection等不能直接加入Headers
                    request.Headers.Add("downlink", "10");
                    request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br"); //"gzip,deflate");//定义gzip压缩页面支持
                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");

                    request.Timeout = 5000;//定义请求超时时间为5秒
                    request.KeepAlive = true;//启用长连接
                    request.Method = "GET";//定义请求方式为GET              
                    if (openProxy && AppconfigUtil.ProxyUtil != null)
                    {
                        Proxy p = GetProxyIp();
                        var px = new WebProxy(p.ip, p.port);//设置代理服务器IP，伪装请求地址
                        px.Credentials = new NetworkCredential(AppconfigUtil.ProxyUtil.Account, AppconfigUtil.ProxyUtil.Pwd);
                        request.Proxy = px;
                    }
                    request.CookieContainer = CookiesContainer[domain];//附加Cookie容器
                    request.ServicePoint.ConnectionLimit = int.MaxValue;//定义最大连接数

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {//获取请求响应

                        foreach (System.Net.Cookie cookie in response.Cookies) CookiesContainer[domain].Add(cookie);//将Cookie加入容器，保存登录状态

                        if (response.ContentEncoding.ToLower().Contains("gzip"))//解压
                        {
                            using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    pageSource = reader.ReadToEnd();
                                }
                            }
                        }
                        else if (response.ContentEncoding.ToLower().Contains("deflate"))//解压
                        {
                            using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    pageSource = reader.ReadToEnd();
                                }

                            }
                        }
                        else
                        {
                            using (Stream stream = response.GetResponseStream())//原始
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {

                                    pageSource = reader.ReadToEnd();
                                }
                            }
                        }
                    }
                    request.Abort();
                    watch.Stop();
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID
                    var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间
                }
                catch (Exception ex)
                {
                    // 远程服务器返回错误: (503) 服务器不可用。
                    //throw new Exception(ex.Message, ex);
                    return await GetHtmlAsync(domain, url, openProxy);
                }
                return pageSource;
            });
        }

    }
}
