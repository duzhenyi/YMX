namespace D.YMX.Utils
{
    public class CountryEntity
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品详情地址
        /// </summary>
        public string DetailUrl { get; set; }
        /// <summary>
        /// 关键字搜索产品地址
        /// </summary>
        public string KeywordsUrl { get; set; }
        /// <summary>
        /// 店铺产品地址
        /// </summary>
        public string ShopUrl { get; set; }
        /// <summary>
        /// 枚举
        /// </summary>
        public CountryEnum CountryType { get; set; }

        public string Mid { get; set; }
        public string PlanmId { get; set; }
        public string Lop { get; set; }
    }
}
