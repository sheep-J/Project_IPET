using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class RegionModel
    {
        /// <summary>
        /// 鄉鎮Id(PK)
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 鄉鎮名稱
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 縣市Id(FK)
        /// </summary>
        public int CityId { get; set; }
    }
}
