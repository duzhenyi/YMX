using D.YMX.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

        //API链接  在后台获取
        const string proxyAPI = "http://15680505585.user.xiecaiyun.com/api/proxies?action=getJSON&key=NP77DDD613&count=&word=&rand=false&norepeat=false&detail=false&ltime=&idshow=false";
        //后台用户名
        const string proxyusernm = "15680505585";
        //后台密码
        const string proxypasswd = "15680505585";


        public static Proxy GetIp()
        {
            WebClient wc = new WebClient();
            string body = wc.DownloadString(proxyAPI);
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



        public static async Task<string> GetHtmlAsync(string url, bool openProxy = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.ContentType = "application/json;charset=UTF-8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");//定义gzip压缩页面支持

            // 因为我们再上面把证书添加到本机受信任了 所以这行代码不需要，如果你不操作受信任证书的话，就需要
            //httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // 设置ja3指纹
            request.Headers.Add("tls-ja3", "771,4865-4866-4867-49195-49199-49196-49200-52393-52392-49171-49172-156-157-47-53,17513-10-18-11-51-13-27-0-35-65281-43-16-45-5-23-21,29-23-24,0");
            // 设置ja3proxy执行请求的超时
            request.Headers.Add("tls-timeout", "10");
            // 设置ja3proxy执行请求用代理，设置后请求目标服务器拿到的就是代理ip
            // client2.DefaultRequestHeaders.Add("tls-proxy","http://252.45.26.333:5543");

            //request.ContentType = "text/html;charset=UTF-8";
            //request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            //request.Headers.Add("downlink", "10");
            //request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64");

            if (openProxy)
            {
                Proxy p = GetIp();
                request.Proxy = new WebProxy(p.ip, p.port);
                request.Proxy.Credentials = new NetworkCredential(proxyusernm, proxypasswd);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream);
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

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
                    if (openProxy)
                    {
                        Proxy p = GetIp();
                        var px = new WebProxy(p.ip, p.port);//设置代理服务器IP，伪装请求地址
                        px.Credentials = new NetworkCredential(proxyusernm, proxypasswd);
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
