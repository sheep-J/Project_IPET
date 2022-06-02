using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project_IPET.ViewModels
{
    public class CProjectCreateViewModel
    {
        public string fTitle { get; set; }
        public string fDes { get; set; }
        public int fFound { get; set; }
        public string fPhotoPath { get; set; }
        public IFormFile fPhoto { get; set; }
        public string fGoal { get; set; }
        public DateTime fStart { get; set; }
        public DateTime fEnd { get; set; }
        public string fContent { get; set; }
        public int[] fProds { get; set; }
    }
}
