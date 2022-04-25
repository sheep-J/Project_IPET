namespace Project_IPET.Models
{
    public class SubCategoriesModel
    {
        /// <summary>
        /// 子分類Id(PK)
        /// </summary>
        public int SubCategoryId { get; set;}
        /// <summary>
        /// 分類Id(FK)
        /// </summary>
        public int CategoryId { get; set;}
        /// <summary>
        /// 子分類名稱
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// 子分類圖片
        /// </summary>
        public byte[] SubCategoryPicture { get; set; }

    }
}
