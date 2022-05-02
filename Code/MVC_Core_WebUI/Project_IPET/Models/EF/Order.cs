using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Order
    {
        public Order()
        {
            DonationDetails = new HashSet<DonationDetail>();
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

        public virtual Coupon Coupon { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual Member Member { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<DonationDetail> DonationDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
