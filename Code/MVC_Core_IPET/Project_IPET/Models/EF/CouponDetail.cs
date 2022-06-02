using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class CouponDetail
    {
        public int CouponDetailsId { get; set; }
        public int MemberId { get; set; }
        public int CouponId { get; set; }
        public int CouponQuantity { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Member Member { get; set; }
    }
}
