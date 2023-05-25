using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Numerics;
using System.Windows.Forms;

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

        /// <summary>
        /// 品牌名
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 产品尺寸
        /// </summary>
        public string ProductDimensions { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 特殊功能
        /// </summary>
        public string SpecialFeature { get; set; }
        /// <summary>
        /// 布料
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 产品推荐用途
        /// </summary>
        public string RecommendedUsesForProduct { get; set; }
        /// <summary>
        /// 饰面类型
        /// </summary>
        public string FinishType { get; set; }
        /// <summary>
        /// 房间类型
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 框架材料
        /// </summary>
        public string FrameMaterial { get; set; }
        /// <summary>
        /// 年龄范围
        /// </summary>
        public string AgeRangeDescription { get; set; }
        /// <summary>
        /// 背面样式
        /// </summary>
        public string BackStyle { get; set; }
        /// <summary>
        /// 单位计数
        /// </summary>
        public string UnitCount { get; set; }
        /// <summary>
        /// 项目重量
        /// </summary>
        public string ItemWeight { get; set; }
        /// <summary>
        /// 护理说明
        /// </summary>
        public string CareInstructions { get; set; }

        /// <summary>
        /// 最大推荐负载
        /// </summary>
        public string MaximumRecommendedLoad { get; set; }
        /// <summary>
        /// 阀座材料类型
        /// </summary>
        public string SeatMaterialType { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 原产国
        /// </summary>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// 项目型号
        /// </summary>
        public string ItemModelNumber { get; set; }
        /// <summary>
        /// 客户评论
        /// </summary>
        public string CustomerReviews { get; set; }

        /// <summary>
        /// 畅销排行榜
        /// </summary>
        public string BestSellersRank { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// 外形尺寸
        /// </summary>
        public string FormFactor { get; set; }
        /// <summary>
        /// 饰面类型
        /// </summary>
        public string FinishTypes { get; set; }
        /// <summary>
        /// 所需电池
        /// </summary>
        public string Batteries { get; set; } 
    }
}
