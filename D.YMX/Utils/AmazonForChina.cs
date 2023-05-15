using D.YMX.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace D.YMX.Utils
{
    /// <summary>
    /// 中国
    /// </summary>
    public class AmazonForChina : IAmazon
    {
        #region 根据关键字查询的列表页面

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="lastDataIndex"></param>
        /// <returns></returns>
        public int GetTotalPage(HtmlAgilityPack.HtmlDocument doc)
        {
            var productNodes = doc.DocumentNode.SelectNodes("//span[@data-component-type='s-search-results']/div[1]/div[@data-component-type='s-search-result']");
            var lastDataIndex = productNodes[productNodes.Count - 1].Attributes["data-index"].Value;
            if (lastDataIndex != null)
            {
                //  获取分页Html 
                var pageNodes = doc.DocumentNode.SelectNodes($"//span[@data-component-type='s-search-results']/div[1]/div[@data-index='{int.Parse(lastDataIndex) + 1}']/div[1]/div[1]/span[1]/span");
                if (pageNodes != null)
                {
                    if (pageNodes[pageNodes.Count - 1].InnerText != null)
                    {
                        return int.Parse(pageNodes[pageNodes.Count - 1].InnerText);
                    }

                }
            }

            return 0;
        }

        /// <summary>
        /// 获取Asin
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<string> GetAsinByList(HtmlAgilityPack.HtmlDocument doc)
        {
            var list = new List<string>();
            var productNodes = doc.DocumentNode.SelectNodes("//span[@data-component-type='s-search-results']/div[1]/div[@data-component-type='s-search-result']");
            foreach (var productNode in productNodes)
            {
                list.Add(productNode.Attributes["data-asin"].Value);
            }
            return list;
        }

        /// <summary>
        /// 返回所有颜色，尺寸的详情Asins
        /// </summary>
        /// <param name="detailHtml"></param>
        /// <returns></returns>
        public List<string> GetAllAsins(string detailHtml)
        {
            ////// 抓取颜色，看是否是多个，是多个全部抓取Asin后直接返回
            ////var existsColors = false;
            ////var colorNode = doc.DocumentNode.SelectSingleNode("//form[@id='inline-twister-expander-content-size_name']");
            ////if (colorNode != null)
            ////{
            ////    var totalCount = colorNode.Attributes["data-totalvariationcount"].Value;
            ////    if (!string.IsNullOrEmpty(totalCount) && int.Parse(totalCount) > 0)
            ////    {// 存在多个颜色/尺寸
            ////        existsColors = true;
            ////    }
            ////}

            ////if (existsColors)
            ////{// 存在多个颜色/尺寸, 抓取每个asin
            ////    var list = new List<string>();

            ////    return new Tuple<bool, Product, List<string>>(true, null, list);
            ////}
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(detailHtml);

            var jsNode = doc.DocumentNode.SelectNodes("//script[@type='text/javascript']");
            if (jsNode != null)
            {
                foreach (var js in jsNode)
                {
                    //(?<= "dimensionValuesDisplayData"\s *:\s *){ [^{ }]*}
                    string pattern = @"\s*""dimensionValuesDisplayData""\s*:\s*(\{[^\}]+\})";
                    Match match = Regex.Match(js.InnerText, pattern);
                    if (match.Success)
                    {
                        string colorAsinStr = match.Value.Replace("\r", "").Replace("dimensionValuesDisplayData\" : {", "").Replace("}", "").Replace("\"", "").Trim();
                        var colorAsins = colorAsinStr.Split(']');
                        var list = new List<string>();
                        foreach (var item in colorAsins)
                        {
                            var itemAttry = item.Split(':');
                            if (itemAttry.Length > 0 && !string.IsNullOrEmpty(itemAttry[0]))
                            {
                                list.Add(itemAttry[0].Trim(','));
                            }
                            //var asin = itemAttry[0];
                            //var colorAttry = itemAttry[1].Split(',');
                            //var color = colorAttry[0].Trim('[');
                            //var size = colorAttry[1];
                        }
                        return list;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取产品详情根据列表数据
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private Tuple<string, Product> GetProductByList(HtmlNode productNode, string index)
        {
            var contentNode = productNode.SelectSingleNode($"//div[1]/div[@cel_widget_id='MAIN-SEARCH_RESULTS-{index}']/div[1]/div[1]");

            var prod = new Product();
            prod.Asin = productNode.Attributes["data-asin"].Value;
            var dataIndex = productNode.Attributes["data-index"].Value;


            // 封面图图片，链接
            var productImgNode = contentNode.SelectSingleNode("./div[1]/span[@data-component-type='s-product-image']/a[1]");
            if (productImgNode != null)
            {
                prod.Domain = HttpUtility.UrlDecode(productImgNode.Attributes["href"].Value);
                prod.Img = productImgNode.SelectSingleNode("./div[1]/img[1]").Attributes["src"].Value;
                //prod.ImgAlt = productImgNode.SelectSingleNode("./div[1]/img[1]").Attributes["alt"].Value;
            }

            // 内容
            var productDetailNodes = contentNode.SelectNodes("./div[3]/div");
            if (productDetailNodes != null)
            {
                foreach (var node in productDetailNodes)
                {
                    if (node.SelectSingleNode("./span[@data-component-type='s-color-swatch']") != null)
                    {
                        continue;
                    }

                    // 名称
                    var titleNode = node.SelectSingleNode("./h2[1]");
                    if (titleNode != null)
                    {
                        prod.Name = titleNode.InnerText;
                        continue;
                    }

                    // 星级， 4.4 颗星，最多 5 颗星
                    var startLevelNode = node.SelectSingleNode("./div[1]/span[1]");
                    if (startLevelNode != null && startLevelNode.Attributes["aria-label"] != null)
                    {
                        prod.StartLevel = startLevelNode.Attributes["aria-label"].Value;
                    }

                    // 评论数
                    var commentTotalNode = node.SelectSingleNode("./div[1]/span[2]");
                    if (commentTotalNode != null && commentTotalNode.Attributes["aria-label"] != null)
                    {
                        prod.CommentTotal = commentTotalNode.Attributes["aria-label"].Value;
                        continue;
                    }

                    // 价格，¥1,240.04
                    var priceNode = node.SelectSingleNode("./div[1]/a[1]/span[1]/span[@class='a-offscreen']");
                    if (priceNode != null)
                    {
                        prod.Price = priceNode.InnerHtml;
                        continue;
                    }

                    // 优惠
                    var preferentialNodes = node.SelectNodes("./div[2]/span");
                    if (preferentialNodes != null)
                    {
                        foreach (var item in preferentialNodes)
                        {
                            if (!string.IsNullOrEmpty(item.InnerHtml))
                            {
                                prod.Preferentials += item + ";";
                            }
                        }
                    }

                    // 颜色
                    //var productColors = node.SelectNodes("./span[1]/div[1]/div[@class='a-section s-color-swatch-outer-circle s-color-swatch-pad']");
                    //if (productColors != null)
                    //{
                    //    foreach (var colorNode in productColors)
                    //    {
                    //        var colorHtml = colorNode.SelectSingleNode("./div[1]/div[1]/a[1]");
                    //        var href = HttpUtility.UrlDecode(colorHtml.Attributes["href"].Value);
                    //        prod.ColorUrls += href + ";";
                    //        var colorName = colorHtml.Attributes["aria-label"].Value;
                    //        prod.ColorNames += colorName + ";";
                    //        // background-color: #7E6D5A
                    //        var color = colorHtml.SelectSingleNode("./span[1]").Attributes["style"].Value;
                    //        prod.CssColors += color + ";";
                    //    }
                    //    continue;
                    //}


                }
            }


            return new Tuple<string, Product>(dataIndex, prod);
        }

        #endregion

        #region 商品详情页面


        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="detailHtml"></param>
        /// <returns>item1:是否存在多个Asin,单个Asin返回参数,返回多个Asin</returns>
        public Product GetDetail(string detailHtml)
        {
            var product = new Product();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(detailHtml);

            // 1. 名称 id="productTitle"
            product.Name = doc.DocumentNode.SelectSingleNode("//span[@id='productTitle']").InnerText;

            // 2. 基本信息 id="detailBullets_feature_div"
            var basicNode = doc.DocumentNode.SelectSingleNode("//div[@id='detailBullets_feature_div']");
            if (basicNode != null)
            {
                var basicNodes = basicNode.SelectNodes("./ul[1]/li");
                if (basicNodes != null)
                {
                    var dic = new Dictionary<string, string>();
                    foreach (var itemNode in basicNodes)
                    {
                        // 制造商是否已停产,制造商,ASIN，型号/款式，部门
                        var key = itemNode.SelectSingleNode("./span[1]/span[1]");
                        var value = itemNode.SelectSingleNode("./span[1]/span[2]");
                        if (key != null && value != null)
                        {
                            dic.Add(key.InnerText.Replace("\n", "").Replace(" ", ""), value.InnerText);
                        }
                    }
                }
            }

            // 3. 排名，评分，评论数 id="detailBulletsWrapper_feature_div" > ul 2个 
            var otherNode = doc.DocumentNode.SelectSingleNode("//div[@id='detailBulletsWrapper_feature_div']");
            if (otherNode != null)
            {
                var rankingNode = otherNode.SelectSingleNode("./ul[1]/li[1]");
                if (rankingNode != null)
                {// 亚马逊热销商品排名
                    product.Ranking = rankingNode.SelectSingleNode("./span[1]").InnerText.Trim();
                }

                var startLevelNode = otherNode.SelectSingleNode("./ul[2]/li[1]");
                if (startLevelNode != null)
                {
                    product.StartLevel = startLevelNode.SelectSingleNode("//span[@id='acrPopover']").Attributes["title"].Value;

                    product.CommentTotal = startLevelNode.SelectSingleNode("//a[@id='acrCustomerReviewLink']").InnerText;
                }
            }

            // 4. 价格
            var priceNode = doc.DocumentNode.SelectSingleNode("//div[@id='corePrice_feature_div']/div[1]/span[1]/span[1]");
            if (priceNode != null)
            {
                product.Price = priceNode.InnerText;
            }

            // 5. 是否自动发货，货物来自哪里
            var addressNode = doc.DocumentNode.SelectSingleNode("//div[@id='merchant-info']/a[1]/span[1]");
            if (addressNode != null)
            {
                product.Address = addressNode.InnerText;
                if (product.Address.Contains("亚马逊"))
                {
                    product.AutomaticShipping = "亚马逊自营模式";
                }
            }

            // 6. 颜色 
            var colorNode = doc.DocumentNode.SelectSingleNode("//span[@id='inline-twister-expanded-dimension-text-color_name']");
            if (colorNode != null)
            {
                product.Color = colorNode.InnerText;
            }
            // 7. 尺寸
            var sizeNode = doc.DocumentNode.SelectSingleNode("//span[@id='inline-twister-expanded-dimension-text-size_name']");
            if (sizeNode != null)
            {
                product.Size = sizeNode.InnerText;
            }


            return product;

        }

        public string GetCaptcha(string detailHtml)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
