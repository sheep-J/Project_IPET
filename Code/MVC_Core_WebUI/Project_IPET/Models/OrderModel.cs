using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class OrderModel
    {
        public decimal Frieght { get; set; }

        public List<CartModel> OrderItem { get; set; }

        public decimal CartTotal { get; set; }

        public decimal OrderTotal
        {
            get { return CartTotal + Frieght; }
        }
    }

    public class CartModel
    {
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public Product Product { get; set; }

        public string Category { get; set; }

        public string imageSrc { get; set; }

    }
}