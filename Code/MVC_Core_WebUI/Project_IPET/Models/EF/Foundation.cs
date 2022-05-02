using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Foundation
    {
        public Foundation()
        {
            DonationDetails = new HashSet<DonationDetail>();
            ProjectDetails = new HashSet<ProjectDetail>();
        }

        public int FoundationId { get; set; }
        public string FoundationName { get; set; }

        public virtual ICollection<DonationDetail> DonationDetails { get; set; }
        public virtual ICollection<ProjectDetail> ProjectDetails { get; set; }
    }
}
