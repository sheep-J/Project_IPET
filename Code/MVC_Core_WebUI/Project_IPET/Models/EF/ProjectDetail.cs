using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class ProjectDetail
    {
        public ProjectDetail()
        {
            PrjConnects = new HashSet<PrjConnect>();
        }

        public int PrjId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Goal { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public string PrjContent { get; set; }
        public string PrjImage { get; set; }
        public int? FoundationId { get; set; }

        public virtual Foundation Foundation { get; set; }
        public virtual ICollection<PrjConnect> PrjConnects { get; set; }
    }
}
