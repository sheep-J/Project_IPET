using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CFrontWishListViewModel
    {
        public string ProductName { get; set; }
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ProductPrice { get; set; }
        //[DisplayFormat(DataFormatString = "{#,0}")]
        public int Quantity { get; set; }
        
    }
}
