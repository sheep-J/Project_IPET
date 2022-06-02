using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class ProductImagePath
    {
        public int ProductImagePathId { get; set; }
        public int ProductId { get; set; }
        public byte[] ProductImage { get; set; }
        public bool? IsMainImage { get; set; }

        public virtual Product Product { get; set; }
    }
}
