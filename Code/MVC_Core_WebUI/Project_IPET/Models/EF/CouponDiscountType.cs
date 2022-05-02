using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class CouponDiscountType
    {
        public CouponDiscountType()
        {
            Coupons = new HashSet<Coupon>();
        }

        public int CouponDiscountTypeId { get; set; }
        public string CouponDiscountTypeName { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
