using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
