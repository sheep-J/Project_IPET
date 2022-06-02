using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Orders = new HashSet<Order>();
        }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
