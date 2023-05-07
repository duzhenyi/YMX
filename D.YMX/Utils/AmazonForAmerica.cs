using D.YMX.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace D.YMX.Utils
{
    /// <summary>
    /// 美国
    /// </summary>
    public class AmazonForAmerica : IAmazon
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
            var pageNode = doc.DocumentNode.SelectSingleNode("//ul[@class='a-pagination']/li[@class='a-normal']/a[1]");
            if (pageNode != null)
            {
                return int.Parse(pageNode.InnerText);
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
            var productNodes = doc.DocumentNode.SelectNodes("//div[@id='gridItemRoot']/div[1]/div[@class='zg-grid-general-faceout']/div");
            foreach (var productNode in productNodes)
            {
                list.Add(productNode.Attributes["id"].Value);
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
                    product.AutomaticShipping = true;
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

        #endregion
    }
}
