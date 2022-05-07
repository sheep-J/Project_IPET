using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class CartModel
    {
        /// <summary>
        /// 商品編號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品分類
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 商品售價
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 商品購買數量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 商品金額小計
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// 商品照片
        /// </summary>
        public string imageSrc { get; set; }

        public Product Product { get; set; }
    }

}
