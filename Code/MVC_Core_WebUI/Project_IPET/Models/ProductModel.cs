using System;

namespace Project_IPET.Models
{
    public class ProductModel
    {
        /// <summary>
        /// 商品Id(PK)
        /// </summary>
        public int ProdcutID { get; set; }
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
        public bool HotPrice { get; set; }
        public bool ProductAvaliable { get; set; }
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
                return "data:image/jpg;base64," + Convert.ToBase64String(ProductImage, 0, ProductImage.Length);
            }
        }
    }
}
