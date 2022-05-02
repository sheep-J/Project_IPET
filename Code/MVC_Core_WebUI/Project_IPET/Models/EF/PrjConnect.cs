using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class PrjConnect
    {
        public int FId { get; set; }
        public int? PrjId { get; set; }
        public int? ProductId { get; set; }

        public virtual ProjectDetail Prj { get; set; }
        public virtual Product Product { get; set; }
    }
}
