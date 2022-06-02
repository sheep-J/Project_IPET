using System.Collections.Generic;

namespace Project_IPET.Models
{
    public class CategoriesModel
    {
        /// <summary>
        /// 分類ID(PK)
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 分類圖片
        /// </summary>
        public byte[] CategoryPicture { get; set; }
        /// <summary>
        /// 產品數量
        /// </summary>
        public int ProductCount { get; set; }

        public List<SubCategoriesModel>SubCategories {get;set;}
    }
}
