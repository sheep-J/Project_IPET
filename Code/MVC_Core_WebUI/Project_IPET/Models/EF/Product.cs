using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Coupons = new HashSet<Coupon>();
            DonationDetails = new HashSet<DonationDetail>();
            MyFavorites = new HashSet<MyFavorite>();
            OrderDetails = new HashSet<OrderDetail>();
            PrjConnects = new HashSet<PrjConnect>();
            ProductImagePaths = new HashSet<ProductImagePath>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public bool HotProduct { get; set; }
        public bool? ProductAvailable { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<DonationDetail> DonationDetails { get; set; }
        public virtual ICollection<MyFavorite> MyFavorites { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PrjConnect> PrjConnects { get; set; }
        public virtual ICollection<ProductImagePath> ProductImagePaths { get; set; }
    }
}
