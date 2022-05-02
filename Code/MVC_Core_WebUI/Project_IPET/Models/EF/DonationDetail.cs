using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class DonationDetail
    {
        public int DonationlDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int FoundationId { get; set; }

        public virtual Foundation Foundation { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
