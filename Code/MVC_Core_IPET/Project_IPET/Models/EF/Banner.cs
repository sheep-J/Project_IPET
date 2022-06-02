using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public string BannerName { get; set; }
        public byte[] BannerImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
