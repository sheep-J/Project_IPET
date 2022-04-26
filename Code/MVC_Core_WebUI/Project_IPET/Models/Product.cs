using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public bool? HotProduct { get; set; }
        public bool? ProductAvailable { get; set; }
        public int? Ranking { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
