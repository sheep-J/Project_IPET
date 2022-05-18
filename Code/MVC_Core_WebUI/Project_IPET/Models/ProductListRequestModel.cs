using Project_IPET.Enum;
using System.Collections.Generic;

namespace Project_IPET.Models
{
    public class ProductListRequestModel
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public List<int> BrandIds { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// Enum-排序
        /// </summary>
        public SortBy SortBy { get; set; }
        public PageModel Pagination { get; set; }
    }
}
