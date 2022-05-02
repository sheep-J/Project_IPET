using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
