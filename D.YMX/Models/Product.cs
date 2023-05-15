namespace D.YMX.Models
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product
    {
        public long Id { get; set; }

        /// <summary>
        /// Asin 
        /// </summary>
        public string Asin { get; set; }
        /// <summary>
        /// 网址 
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 标题 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 封面图 
        /// </summary>
        public string Img { get; set; } 
        /// <summary>
        /// 评论数 
        /// </summary>
        public string CommentTotal { get; set; }
        /// <summary>
        /// 星级 
        /// </summary>
        public string StartLevel { get; set; }
        /// <summary>
        /// 排名 
        /// </summary>
        public string Ranking { get; set; }
        /// <summary>
        /// 价格 
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 品牌名
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 是否注册
        /// </summary>
        public bool IsRegister { get; set; }
        /// <summary>
        /// 是否自发货，详情里看
        // Ship from == Amazon && Sold by == Amazon 是亚马逊自营模式
        // Ship from == Amazon && Sold by != Amazon 是FBA模式
        // Ship from != Amazon && Sold by == Ship from 是FBM模式    
        /// </summary>
        public string AutomaticShipping { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string Address { get; set; } 
        /// <summary>
        /// 是否僵尸产品，看有没有人卖，详情里最右侧
        /// </summary>
        public string IsOffline { get; set; }
        /// <summary>
        /// BSR
        /// </summary>
        public string Bsr { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public string ListingTime { get; set; }
        /// <summary>
        /// 优惠，分号隔开
        /// </summary>
        public string Preferentials { get; set; }
        /// <summary>
        /// 颜色名称
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// CSS颜色：
        /// </summary>
        public string Size { get; set; } 
    }
}
