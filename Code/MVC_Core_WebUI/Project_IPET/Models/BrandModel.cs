using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class BrandModel
    {
        /// <summary>
        /// 品牌Id(FK)
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 品牌名稱
        /// </summary>
        public string BrandName { get; set; }
    }
}
