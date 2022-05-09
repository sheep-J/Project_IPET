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

        public List<OrderDetailModel> OrderItem { get; set; }
        public decimal CartTotal { get; set; }

        public decimal OrderTotal
        {
            get { return CartTotal + Frieght; }
        }
    }


    public class OrderDetailModel
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool Commented { get; set; }

        public decimal SubTotal { get; set; }

    }


    public class CartModel : OrderDetailModel
    {

        public CartModel() { }
        public CartModel(OrderDetailModel orderdetail)
        {
            this.OrderID = orderdetail.OrderID;
            this.ProductID = orderdetail.ProductID;
            this.UnitPrice = orderdetail.UnitPrice;
            this.Quantity = orderdetail.Quantity;
            this.SubTotal = orderdetail.SubTotal;
            this.Commented = orderdetail.Commented;
        }


        public Product Product { get; set; }

        public string Category { get; set; }

        public string imageSrc { get; set; }

    }
}