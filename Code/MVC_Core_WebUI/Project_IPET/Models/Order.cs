using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int TransactionTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public int? CouponId { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        public string ShippedTo { get; set; }
        public decimal Frieght { get; set; }
        public string PayDate { get; set; }
        public string OrderName { get; set; }
        public string OrderPhone { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
