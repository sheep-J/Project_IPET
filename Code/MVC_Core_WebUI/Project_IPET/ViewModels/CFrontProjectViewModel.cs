using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CFrontProjectViewModel
    {
        public int fId { get; set; }
        public string fTitle { get; set; }
        public string fDescription { get; set; }
        public string fContent { get; set; }
        public string fPrjImage { get; set; }
        public string fDeadline { get; set; }
        public string fDeadlineall { get; set; }
        public DateTime fLastDate { get; set; }
    }
}
