using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class PostType
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        public int PostTypeId { get; set; }
        public string PostTypeName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
