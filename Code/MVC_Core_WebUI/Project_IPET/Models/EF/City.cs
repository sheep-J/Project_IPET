using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class City
    {
        public City()
        {
            Pets = new HashSet<Pet>();
            Regions = new HashSet<Region>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}
