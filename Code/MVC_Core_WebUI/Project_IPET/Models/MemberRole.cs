using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class MemberRole
    {
        public MemberRole()
        {
            Members = new HashSet<Member>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
