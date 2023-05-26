using D.YMX.LogUtils;
using D.YMX.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D.YMX.Utils
{
    public static class ApiUtil
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetTotalAsync(CountryEntity countryEntity, string keyWords, bool openProxy = false)
        {
            try
            {
                // 1. 拼接搜索页面
                var url = countryEntity.KeywordsUrl.Replace("{QID}", DateTime.Now.Ticks.ToString()).Replace("{KEY_WORDS}", HttpUtility.UrlEncode(keyWords)).Replace("{PAGE}", "1");
                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, openProxy);
                if (!string.IsNullOrEmpty(res))
                {
                    var checkRes = await CheckCacptchImg(res, countryEntity, openProxy);
                    if (checkRes == EnumCheckCaptcha.OK)
                    {
                        return await GetTotalAsync(countryEntity, keyWords);
                    }
                    else if (checkRes == EnumCheckCaptcha.No)
                    {
                        return 0;
                    }

                    // 解析Html
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(res);
                    // 获取总页数  
                    return countryEntity.Instance().GetTotalPage(doc);
                }
            }
            catch (Exception ex)
            {
                NLogUtil.Log.Error("【GetTotalAsync函数异常：】" + ex);
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="countryEntity"></param>
        /// <param name="page"></param>
        public static async Task<List<string>> GetAsinListAsync(CountryEntity countryEntity, string keyWords, string page,bool openProxy=false)
        {
            try
            {
                // 1. 拼接搜索页面
                var url = countryEntity.KeywordsUrl.Replace("{QID}", DateTime.Now.Ticks.ToString()).Replace("{KEY_WORDS}", HttpUtility.UrlEncode(keyWords)).Replace("{PAGE}", page);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, openProxy);
                if (!string.IsNullOrEmpty(res))
                {
                    var checkRes = await CheckCacptchImg(res, countryEntity, openProxy);
                    if (checkRes == EnumCheckCaptcha.OK)
                    {
                        return await GetAsinListAsync(countryEntity, keyWords, page);
                    }
                    else if (checkRes == EnumCheckCaptcha.No)
                    {
                        return null;
                    }
                }
                // 3. 解析Html
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(res);

                // 4. 获取所有数据的asins
                var asins = countryEntity.Instance().GetAsinByList(doc);
                if (asins.Count > 0)
                {
                    return asins;
                }
            }
            catch (Exception ex)
            {
                NLogUtil.Log.Error("【GetAsinListAsync函数异常：】" + ex);
                return null;
            }
            return null;
        }

        /// <summary>
        /// 获取尺寸 & 颜色的Asin
        /// </summary>
        /// <param name="countryEntity"></param>
        /// <param name="asin"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetAllAsins(CountryEntity countryEntity, string asin, bool openProxy = false)
        {
            try
            {
                // 1. 拼接URL
                var url = countryEntity.DetailUrl.Replace("{ASIN}", asin);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, openProxy);
                if (!string.IsNullOrEmpty(res))
                {
                    var checkRes = await CheckCacptchImg(res, countryEntity, openProxy);
                    if (checkRes == EnumCheckCaptcha.None)
                    {
                        return countryEntity.Instance().GetAllAsins(res);
                    }
                    else if (checkRes == EnumCheckCaptcha.OK)
                    {
                        return await GetAllAsins(countryEntity, asin);
                    }
                    else if (checkRes == EnumCheckCaptcha.No)
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NLogUtil.Log.Error("【GetAllAsins函数异常：】" + ex);
                return null;
            }
            return null;
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="countryEntity"></param>
        /// <param name="asin"></param>
        /// <returns></returns>
        public static async Task<Product> GetDetailAsync(CountryEntity countryEntity, string asin, bool openProxy = false)
        {
            try
            {
                // 1. 拼接URL
                var url = countryEntity.DetailUrl.Replace("{ASIN}", asin);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, openProxy);

                if (!string.IsNullOrEmpty(res))
                {
                    var checkRes = await CheckCacptchImg(res, countryEntity, openProxy);
                    if (checkRes == EnumCheckCaptcha.None)
                    {
                        // 3. 解析Html
                        return countryEntity.Instance().GetDetail(res);
                    }
                    else if (checkRes == EnumCheckCaptcha.OK)
                    {
                        return await GetDetailAsync(countryEntity, asin);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                NLogUtil.Log.Error("【GetDetailAsync函数异常：】" + ex);
                return null;
            }
            return null;
        }

        /// <summary>
        /// 人机验证码
        /// </summary>
        /// <param name="html"></param>
        /// <param name="countryEntity"></param>
        /// <returns></returns>
        private static async Task<EnumCheckCaptcha> CheckCacptchImg(string html, CountryEntity countryEntity, bool openProxy = false)
        {
            try
            {
                if (html.Contains("Enter the characters you see below"))
               {// 需要输入验证码，重新请求

                    // 获取验证码  url = validateCaptcha?amzn=Rth+q2/q0PFHYp/RpglARg==&amzn-r=/dp/B0BWZW448X?th=1&psc=1&field-keywords=验证码
                    var captchaImgUrl = countryEntity.Instance().GetCaptcha(html);
                    // 存储到本地
                    var localCaptchaImgUrl = ImgUtil.SaveCaptchaImage(captchaImgUrl,openProxy);
                    // 根据字模，算出跟哪个最相近
                    var captcha = ImgUtil.GetCaptchaImage(localCaptchaImgUrl);

                    if (!string.IsNullOrEmpty(captcha))
                    {
                        var res = await HttpUtil.GetHtmlAsync($"{countryEntity.Domain}/validateCaptcha?amzn=Rth+q2/q0PFHYp/Rpg?th=1&psc=1&field-keywords=" + captcha, openProxy);
                        return EnumCheckCaptcha.OK;
                    }
                    else
                    {
                        NLogUtil.Log.Warn($"没有匹配到人机验证码：{captchaImgUrl},本地地址:{localCaptchaImgUrl}");
                    }
                }
                return EnumCheckCaptcha.None;
            }
            catch (Exception ex)
            {
                NLogUtil.Log.Error("【CheckCacptchImg函数异常：】" + ex);
                return EnumCheckCaptcha.No;
            }
        }

        public enum EnumCheckCaptcha
        {
            None,
            OK,
            No,
        }
    }
}
