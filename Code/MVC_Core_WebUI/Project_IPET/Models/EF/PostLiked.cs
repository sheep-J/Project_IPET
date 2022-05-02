using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class PostLiked
    {
        public int PostLikedId { get; set; }
        public int PostId { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Post Post { get; set; }
    }
}
