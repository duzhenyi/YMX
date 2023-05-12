﻿using D.YMX.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace D.YMX.Utils
{
    public static class ApiUtil
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetTotalAsync(CountryEntity countryEntity, string keyWords)
        {
            try
            {
                // 1. 拼接搜索页面
                var url = countryEntity.KeywordsUrl.Replace("{QID}", DateTime.Now.Ticks.ToString()).Replace("{KEY_WORDS}", HttpUtility.UrlEncode(keyWords)).Replace("{PAGE}", "1");
                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, true);
                if (!string.IsNullOrEmpty(res))
                {
                    // 解析Html
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(res);
                    // 获取总页数  
                    return countryEntity.Instance().GetTotalPage(doc);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="countryEntity"></param>
        /// <param name="page"></param>
        public static async Task<List<string>> GetAsinListAsync(CountryEntity countryEntity, string keyWords, string page)
        {
            try
            {
                // 1. 拼接搜索页面
                var url = countryEntity.KeywordsUrl.Replace("{QID}", DateTime.Now.Ticks.ToString()).Replace("{KEY_WORDS}", HttpUtility.UrlEncode(keyWords)).Replace("{PAGE}", page);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, true);

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
        public static async Task<List<string>> GetAllAsins(CountryEntity countryEntity, string asin)
        {
            try
            {
                // 1. 拼接URL
                var url = countryEntity.DetailUrl.Replace("{ASIN}", asin);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, true);
                if (!string.IsNullOrEmpty(res))
                {
                    if (res.Contains("Enter the characters you see below"))
                    {// 需要输入验证码，重新请求

                        // 获取验证码  url = validateCaptcha?amzn=Rth+q2/q0PFHYp/RpglARg==&amzn-r=/dp/B0BWZW448X?th=1&psc=1&field-keywords=验证码
                        var captchaImgUrl = countryEntity.Instance().GetCaptcha(res);
                        // 存储到本地
                        var localCaptchaImgUrl = ImgUtil.GetCaptchaImage(captchaImgUrl);
                        // 图片切分

                        // 图片二值化
                        Bitmap bitmap = new Bitmap(localCaptchaImgUrl);
                        if (bitmap != null)
                        {
                             
                        }
                        // 根据字模，算出跟哪个最相近

                        return await GetAllAsins(countryEntity, asin);
                    }
                    return countryEntity.Instance().GetAllAsins(res);
                }
            }
            catch (Exception)
            {
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
        public static async Task<Product> GetDetailAsync(CountryEntity countryEntity, string asin)
        {
            try
            {
                // 1. 拼接URL
                var url = countryEntity.DetailUrl.Replace("{ASIN}", asin);

                // 2. 获取分页的搜索产品
                var res = await HttpUtil.GetHtmlAsync(url, true);

                if (!string.IsNullOrEmpty(res))
                {
                    // 3. 解析Html
                    return countryEntity.Instance().GetDetail(res);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
