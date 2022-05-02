using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class NotifiesType
    {
        public NotifiesType()
        {
            Notifies = new HashSet<Notify>();
        }

        public int NotifyTypeId { get; set; }
        public string NotifyName { get; set; }

        public virtual ICollection<Notify> Notifies { get; set; }
    }
}
