using Project_IPET.Enum;

namespace Project_IPET.Models
{
    public class ProductListRequestModel
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// Enum-排序
        /// </summary>
        public SortBy SortBy { get; set; }
        public PageModel Pagination { get; set; }
    }
}
