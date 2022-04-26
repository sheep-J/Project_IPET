using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public int? Rating { get; set; }
        public string CommentContent { get; set; }
        public string CommentDate { get; set; }
        public bool Banned { get; set; }
        public string BannedContent { get; set; }
        public int? CommentReplyId { get; set; }
        public string ReplyContent { get; set; }
        public bool Reply { get; set; }
        public string CommentImage { get; set; }

        public virtual CommentReply CommentReply { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Product Product { get; set; }
    }
}
