using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
