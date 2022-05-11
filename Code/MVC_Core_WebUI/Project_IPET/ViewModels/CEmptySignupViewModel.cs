using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CEmptySignupViewModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string CurrentPwd { get; set; }
        public string ConfirmPwd { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
}
