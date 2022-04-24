namespace Project_IPET.Models
{
    public class ProductModel
    {
        public int ProdcutID { get; set; }
        public string ProductName { get; set; }
        public int SubCategoryID { get; set; }
        public int BrandID { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public bool HotPrice { get; set; }
        public bool ProductAvaliable { get; set; }
        public int Ranking { get; set; }
        public string ProductImage { get; set; }
    }
}
