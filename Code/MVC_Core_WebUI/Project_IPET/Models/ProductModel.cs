using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// 商品圖片for list
        /// </summary>
        public byte[] ProductImage { get; set; }
        /// <summary>
        /// 商品圖片轉byte for list
        /// </summary>
        public string ProductImageBase64String
        {
            get
            {
                return ProductImage == null ? "" : "data:image/jpg;base64," + Convert.ToBase64String(ProductImage, 0, ProductImage.Length);
            }
        }

        /// <summary>
        /// 多張商品圖片 for detail
        /// </summary>
        public List<byte[]> ProductImages { get; set; }
        /// <summary>
        /// 多張商品圖片轉byte for detail
        /// </summary>
        public List<string> ProductImageBase64Strings { 
            get
            {
                return ProductImages == null ? new List<string>() : ProductImages.Select(ProductImage => "data:image/jpg;base64," + Convert.ToBase64String(ProductImage, 0, ProductImage.Length)).ToList();
            }
        }
    }
}
