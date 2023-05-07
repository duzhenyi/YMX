using D.YMX.Models;
using D.YMX.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace D.YMX
{
    public partial class FrmKeyWords : Form
    {
        private string _country;
        public FrmKeyWords(string keyWords, string country)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(keyWords))
            {
                checkBoxLeft.Items.Add(keyWords, false);
            }
            this._country = country;
        }

        private string stkeywd;
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBoxLeft.SelectedIndex < 0)
                {
                    MessageBox.Show("关键词不能为空！");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取异常，已经停止！");
            }
        }


        public async void GetKeywords()
        {
            checked
            {
                for (; ; )
                {
                    try
                    {
                        CountryEntity yaMaXunCountry = YaMaXunUtil.GetCountryCode(this._country);
                        string nkeyword = HttpUtility.UrlEncode(this.stkeywd);

                        bool flag = Cache.Keywords.Count > 0;
                        if (flag)
                        {
                            nkeyword = HttpUtility.UrlEncode(Cache.Keywords[1]);
                            Cache.Keywords.RemoveAt(1);
                        }

                        string url = string.Concat(new string[]
                        {
                            "https://completion.",
                            this._country,
                            "/api/2017/suggestions?limit=11&prefix=",
                            nkeyword,
                            "&suggestion-type=WIDGET&suggestion-type=KEYWORD&page-type=Gateway&alias=aps&site-variant=desktop&version=3&event=onKeyPress&wc=&last-prefix=",
                            nkeyword,
                            "&lop=",
                            yaMaXunCountry.Lop,
                            "&avg-ks-time=654&fb=1&mid=",
                           yaMaXunCountry.Mid ,
                            "&plain-mid=",
                           yaMaXunCountry.PlanmId,
                            "&client-info=amazon-search-ui"
                        });

                        string html = await HttpUtil.GetHtmlAsync(url, url);//RuntimeHelpers.GetObjectValue();
                        if (html != null)
                        {
                            KeyWords keyWords = JsonConvert.DeserializeObject<KeyWords>(html);
                            foreach (var item in keyWords.suggestions)
                            {
                                if (!Cache.Keywords.Contains(item.value))
                                {
                                    Cache.Keywords.Add(item.value);
                                }

                                this.Invoke(new MethodInvoker(delegate
                                {
                                    checkBoxRight.Items.Add(item.value);
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            MessageBox.Show("获取中断，已经停止！");
                        }));

                        break;
                    }
                    Thread.Sleep(2000);
                    Application.DoEvents();
                }
            }
        }

        //public static string GetHtml(string url, string Method = "GET", string formdata = "", string cookie = "", string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36", string Referer = "", string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9", string ContentType = "text/html; charset=utf-8", bool AllowAutoRedirect = true, bool KeepAlive = true)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    try
        //    {
        //        //Referer = request.Host;
        //        request.Referer = "https://" + Referer;
        //        request.Proxy = null;
        //        request.UserAgent = UserAgent;
        //        bool flag5 = cookie != null;
        //        if (flag5)
        //        {
        //            string[] wk = Strings.Split(cookie, ";", -1, CompareMethod.Binary);
        //            foreach (string scookie in wk)
        //            {
        //                if (!string.IsNullOrEmpty(scookie))
        //                {
        //                    bool flag7 = Strings.InStr(scookie, "=", CompareMethod.Binary) > 1;
        //                    Cookie ck;
        //                    if (flag7)
        //                    {
        //                        string dcookie = Strings.Split(scookie, "=", -1, CompareMethod.Binary)[0];
        //                        string ecookie = Strings.Split(scookie, dcookie + "=", -1, CompareMethod.Binary)[1].Replace(",", "%2c");
        //                        Console.WriteLine(scookie);
        //                        ck = new Cookie(Strings.Replace(Strings.Trim(dcookie.ToString()), "\r\n", "", 1, -1, CompareMethod.Binary), Strings.Replace(Strings.Trim(ecookie.ToString()), "\r\n", "", 1, -1, CompareMethod.Binary), "/", request.Host);
        //                        YaMaXunUtil.Cookies.Add(ck);
        //                    }
        //                }
        //            }
        //            request.CookieContainer = YaMaXunUtil.Cookies;
        //        }
        //        request.Method = Method;
        //        request.Accept = Accept;
        //        request.ContentType = ContentType;
        //        request.AllowAutoRedirect = AllowAutoRedirect;
        //        request.KeepAlive = KeepAlive;
        //        if (Method == "POST")
        //        {
        //            byte[] strbyte = Encoding.ASCII.GetBytes(formdata);
        //            request.ContentLength = (long)strbyte.Length;
        //            Stream stream = request.GetRequestStream();
        //            stream.Write(strbyte, 0, strbyte.Length);
        //            stream.Flush();
        //        }
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        YaMaXunUtil.Cookies.Add(response.Cookies);
        //        Stream res = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(res, Encoding.GetEncoding("utf-8"));
        //        string html = reader.ReadToEnd();
        //        request.Abort();
        //        request = null;
        //        reader.Close();
        //        res.Close();
        //        response.Close();
        //        return html;
        //    }
        //    catch (Exception ex)
        //    {
        //        request.Abort();
        //        request = null;
        //    }
        //    return null;
        //}

        private void btnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                //bool isAlive = this.caijikeysc.IsAlive;
                //if (isAlive)
                //{
                //    this.caijikeysc.Abort();
                //}
                GC.Collect();
                GC.WaitForFullGCApproach();
                MessageBox.Show("已经停止");
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class KeyWords
    {
        public string alias { get; set; }
        public string prefix { get; set; }
        public string suffix { get; set; }

        public List<Suggestions> suggestions { get; set; }
        public string suggestionTitleId { get; set; }
        public string responseId { get; set; }
        public bool shuffled { get; set; }
    }
    public class Suggestions
    {
        public string suggType { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string refTag { get; set; }
        public string candidateSources { get; set; }
        public string strategyId { get; set; }
        public string strategyApiType { get; set; }
        public double prior { get; set; }
        public bool ghost { get; set; }
        public bool help { get; set; }
    }
}
