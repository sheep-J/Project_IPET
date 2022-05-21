using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class PetListModel
    {
        public class Request
        {
            public int CityID { get; set; }
            public string PetCategory { get; set; }
            public string PetGender { get; set; }
            public PageModel Pagination { get; set; }
        }

        public class Response
        {
            public List<PetModel> PetList { get; set; }
            public PageModel Pagination { get; set; }
        }
    }
}
