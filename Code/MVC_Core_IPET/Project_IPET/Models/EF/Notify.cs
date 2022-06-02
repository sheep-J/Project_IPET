using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Notify
    {
        public int NotifyId { get; set; }
        public string NotifyContent { get; set; }
        public int NotifyTypeId { get; set; }
        public int MemberId { get; set; }
        public bool ReadStatus { get; set; }

        public virtual Member Member { get; set; }
        public virtual NotifiesType NotifyType { get; set; }
    }
}
