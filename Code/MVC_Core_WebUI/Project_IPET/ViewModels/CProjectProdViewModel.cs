using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CProjectProdViewModel
    {
        public int fId { get; set; }
        public string fName { get; set; }
        public string fDes { get; set; }
        public int fPrice { get; set; }
        public int fQuantity { get; set; }
        public byte[] fImg { get; set; }
    }
}
