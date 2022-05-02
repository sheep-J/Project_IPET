using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Region
    {
        public Region()
        {
            Pets = new HashSet<Pet>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
