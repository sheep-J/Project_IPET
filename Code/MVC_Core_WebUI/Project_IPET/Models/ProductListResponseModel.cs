using System.Collections.Generic;

namespace Project_IPET.Models
{
    public class ProductListResponseModel
    {
        public List<ProductModel> ProductList { get; set; }
        public PageModel Pagination { get; set; }
    }
}
