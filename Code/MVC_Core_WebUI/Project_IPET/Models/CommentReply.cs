using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class CommentReply
    {
        public CommentReply()
        {
            Comments = new HashSet<Comment>();
        }

        public int CommentReplyId { get; set; }
        public string ReplyContent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
