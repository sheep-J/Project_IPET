using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponDetails = new HashSet<CouponDetail>();
            Orders = new HashSet<Order>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public int CouponDiscountTypeId { get; set; }
        public int CouponDiscount { get; set; }
        public int CouponDiscountCondition { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public int CouponQuantityIssued { get; set; }
        public string CouponCode { get; set; }
        public int ProductId { get; set; }

        public virtual CouponDiscountType CouponDiscountType { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<CouponDetail> CouponDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
