namespace Project_IPET.Models
{
    public class ProductListRequestModel
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        public PageModel Pagination { get; set; }
    }
}
