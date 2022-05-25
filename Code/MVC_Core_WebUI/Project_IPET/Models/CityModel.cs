using System.Collections.Generic;

namespace Project_IPET.Models
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        public List<RegionModel>Regions {get;set;}
    }
}
