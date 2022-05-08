using System;

namespace Project_IPET.Models
{
    public class ProductModel
    {
        /// <summary>
        /// 商品Id(PK)
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 子分類Id(FK)
        /// </summary>
        public int SubCategoryID { get; set; }
        /// <summary>
        /// 品牌Id(FK)
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 品牌名稱
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 成本價
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 商品單價
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 商品庫存
        /// </summary>
        public int UnitsInStock { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否為熱門商品
        /// </summary>
        public bool HotProduct { get; set; }
        /// <summary>
        /// 商品上架狀態
        /// </summary>
        public bool ProductAvailable { get; set; }
        public int Ranking { get; set; }
        /// <summary>
        /// 商品圖片
        /// </summary>
        public byte[] ProductImage { get; set; }
        /// <summary>
        /// 商品圖片轉byte
        /// </summary>
        public string ProductImageBase64String { 
            get
            {
                if(ProductImage != null)
                {
                    return "data:image/jpg;base64," + Convert.ToBase64String(ProductImage, 0, ProductImage.Length);
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
