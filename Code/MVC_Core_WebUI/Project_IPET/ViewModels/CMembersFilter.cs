
using Project_IPET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CMembersFilter
    {
        public string Keyword { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public int countOnePage { get; set; }
        public int totalPage { get; set; }
        public int totalMember { get; set; }
        //=============================
        //public PageModel Pagination { get; set; }
    }
}
