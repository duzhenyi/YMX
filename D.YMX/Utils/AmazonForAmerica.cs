using D.YMX.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
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
            var pageNode = doc.DocumentNode.SelectSingleNode("//span[@class='s-pagination-item s-pagination-disabled']");
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
            var productNodes = doc.DocumentNode.SelectNodes("//div[@data-component-type='s-search-result']");
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
        /// 获取验证码
        /// https://images-na.ssl-images-amazon.com/captcha/tinytuux/Captcha_ajrcrcceni.jpg
        /// </summary>
        /// <param name="detailHtml"></param>
        /// <returns></returns>
        public string GetCaptcha(string detailHtml)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(detailHtml);

            var imgNode = doc.DocumentNode.SelectSingleNode("//div[@class='a-row a-text-center']/img[1]");
            if (imgNode != null)
            {
                return imgNode.Attributes["src"].Value;
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
            var dic = new Dictionary<string, string>();
            var basicNode = doc.DocumentNode.SelectSingleNode("//table[@id='productDetails_detailBullets_sections1']");
            if (basicNode != null)
            {
                var basicNodes = basicNode.SelectNodes("./tr");
                if (basicNodes != null)
                {
                    foreach (var itemNode in basicNodes)
                    {
                        // 制造商是否已停产,制造商,ASIN，型号/款式，部门
                        var key = itemNode.SelectSingleNode("./th[1]");
                        var value = itemNode.SelectSingleNode("./td[1]");
                        if (key != null && value != null && !dic.ContainsKey(key.InnerText.Trim()))
                        {
                            dic.Add(key.InnerText.Trim(), value.InnerText.Trim());
                        }
                    }

                    foreach (var itemNode in dic)
                    {
                        if (itemNode.Key == "Brand")
                        {// 品牌名
                            product.Brand = itemNode.Value;
                        }
                        else if (itemNode.Key == "ProductDimensions")
                        {// 产品尺寸
                            product.ProductDimensions = itemNode.Value;
                        }
                        else if (itemNode.Key == "Style")
                        {// 风格
                            product.Style = itemNode.Value;
                        }
                        else if (itemNode.Key == "Special Feature")
                        {// 特殊功能
                            product.SpecialFeature = itemNode.Value;
                        }
                        else if (itemNode.Key == "Material")
                        {// 布料
                            product.Material = itemNode.Value;
                        }
                        else if (itemNode.Key == "Recommended Uses For Product")
                        {// 产品推荐用途
                            product.RecommendedUsesForProduct = itemNode.Value;
                        }
                        else if (itemNode.Key == "Finish Type")
                        {// 饰面类型
                            product.FinishType = itemNode.Value;
                        }
                        else if (itemNode.Key == "Room Type")
                        {// 房间类型
                            product.RoomType = itemNode.Value;
                        }
                        else if (itemNode.Key == "Frame Material")
                        {// 框架材料
                            product.FrameMaterial = itemNode.Value;
                        }
                        else if (itemNode.Key == "Age Range (Description)")
                        {// 年龄范围
                            product.AgeRangeDescription = itemNode.Value;
                        }
                        else if (itemNode.Key == "Back Style")
                        {// 背面样式
                            product.BackStyle = itemNode.Value;
                        }
                        else if (itemNode.Key == "Unit Count")
                        {// 单位计数
                            product.UnitCount = itemNode.Value;
                        }
                        else if (itemNode.Key == "Item Weight")
                        {// 项目重量
                            product.ItemWeight = itemNode.Value;
                        }
                        else if (itemNode.Key == "Care instructions")
                        {// 护理说明
                            product.CareInstructions = itemNode.Value;
                        }
                        else if (itemNode.Key == "Maximum recommended load")
                        {// 最大推荐负载
                            product.MaximumRecommendedLoad = itemNode.Value;
                        }
                        else if (itemNode.Key == "Seat Material Type")
                        {// 阀座材料类型
                            product.SeatMaterialType = itemNode.Value;
                        }
                        else if (itemNode.Key == "Department")
                        {// 部门
                            product.Department = itemNode.Value;
                        }
                        else if (itemNode.Key == "Manufacturer")
                        {// 制造商
                            product.Manufacturer = itemNode.Value;
                        }
                        else if (itemNode.Key == "Country of Origin")
                        {// 原产国
                            product.CountryOfOrigin = itemNode.Value;
                        }
                        else if (itemNode.Key == "Item model number")
                        {// 项目型号
                            product.ItemModelNumber = itemNode.Value;
                        }
                        else if (itemNode.Key == "Customer Reviews")
                        {// 客户评论
                            product.CustomerReviews = itemNode.Value;
                        }
                        else if (itemNode.Key == "Best Sellers Rank")
                        {// 畅销排行榜
                            product.BestSellersRank = itemNode.Value;
                        }
                        else if (itemNode.Key == "Volume")
                        {// 体积
                            product.Volume = itemNode.Value;
                        }
                        else if (itemNode.Key == "Form Factor")
                        {// 外形尺寸
                            product.FormFactor = itemNode.Value;
                        }
                        else if (itemNode.Key == "Finish types")
                        {// 饰面类型
                            product.FinishTypes = itemNode.Value;
                        }
                        else if (itemNode.Key == "Batteries")
                        {// 所需电池
                            product.Batteries = itemNode.Value;
                        }

                    }
                }
            }

            // 3. 评分
            var startNode = doc.DocumentNode.SelectSingleNode("//span[@id='acrPopover']");
            if (startNode != null)
            {
                var startLevelNode = startNode.SelectSingleNode("./span[1]/a[1]/span[1]");
                if (startLevelNode != null)
                {
                    product.StartLevel = startLevelNode.InnerText;
                }
            }
            // 4. 评论数 
            var commentNode = doc.DocumentNode.SelectSingleNode("//span[@id='acrCustomerReviewText']");
            if (commentNode != null)
            {
                product.CommentTotal = commentNode.InnerText;
            }

            // 4. 价格
            var priceNode = doc.DocumentNode.SelectSingleNode("//div[@id='corePrice_feature_div']/div[1]/span[1]/span[1]");
            if (priceNode != null)
            {
                product.Price = priceNode.InnerText;
            }

            // 5. 是否自动发货，货物来自哪里
            var shipsFrom = string.Empty;
            var soldBy = string.Empty;

            var addressValuesNodes = doc.DocumentNode.SelectNodes("//div[@id='tabular-buybox']/div[1]/div[@class='tabular-buybox-text']");
            if (addressValuesNodes != null)
            {
                foreach (var addressNode in addressValuesNodes)
                {
                    if (addressNode.Attributes["tabular-attribute-name"].Value == "Ships from")
                    {
                        shipsFrom = addressNode.InnerText;
                    }

                    if (addressNode.Attributes["tabular-attribute-name"].Value == "Sold by")
                    {
                        soldBy = addressNode.InnerText;
                    }
                }
            }

            if (!string.IsNullOrEmpty(shipsFrom) && !string.IsNullOrEmpty(soldBy))
            {
                product.Address = shipsFrom + "/" + soldBy;
                shipsFrom = shipsFrom.Replace("\n", "").Trim();
                soldBy = soldBy.Replace("\n", "").Trim();
                if (shipsFrom == "Amazon" && soldBy == "Amazon")
                {// Ship from == Amazon && Sold by == Amazon 是亚马逊自营模式
                    product.AutomaticShipping = "亚马逊自营模式";
                }
                else if (shipsFrom == "Amazon" && soldBy != "Amazon")
                {//Ship from == Amazon && Sold by != Amazon 是FBA模式
                    product.AutomaticShipping = "FBA模式";
                }
                else
                {// Ship from != Amazon && Sold by == Ship from 是FBM模式
                    product.AutomaticShipping = "FBM模式";
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

        //美国商标局：http://tmsearch.uspto.gov/
        //欧洲商标局：https://euipo.europa.eu/
        //英国商标局：http://www.ipo.gov.uk/
        //日本商标局：http://www.jpo.go.jp/
        #endregion
    }
}
