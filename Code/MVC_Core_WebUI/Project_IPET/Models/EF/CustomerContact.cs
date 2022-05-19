using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class CustomerContact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactMail { get; set; }
        public string ContactSubject { get; set; }
        public string ContactMessage { get; set; }
        public DateTime? ContactDate { get; set; }
        public bool? ReplyStatus { get; set; }
        public string ReplyMessage { get; set; }
    }
}
