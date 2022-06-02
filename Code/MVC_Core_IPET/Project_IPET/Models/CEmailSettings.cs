using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class CEmailSettings
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool ServerUseSsl { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
