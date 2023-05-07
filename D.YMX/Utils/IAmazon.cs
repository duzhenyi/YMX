using D.YMX.Models;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace D.YMX.Utils
{
    public interface IAmazon
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        int GetTotalPage(HtmlAgilityPack.HtmlDocument doc);

        /// <summary>
        /// 获取列表显示的商品asin
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        List<string> GetAsinByList(HtmlAgilityPack.HtmlDocument doc);

        /// <summary>
        /// 获取商品所有颜色尺寸的asin
        /// </summary>
        /// <param name="detailHtml"></param>
        /// <returns></returns>
        List<string> GetAllAsins(string detailHtml);

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="detailHtml"></param>
        /// <returns></returns>
        Product GetDetail(string detailHtml);
    }
}
