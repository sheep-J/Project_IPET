using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models
{
    public partial class Post
    {
        public Post()
        {
            InverseReplyToPostNavigation = new HashSet<Post>();
            PostLikeds = new HashSet<PostLiked>();
        }

        public int PostId { get; set; }
        public int MemberId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string PostDate { get; set; }
        public int PostTypeId { get; set; }
        public bool Banned { get; set; }
        public string BannedContent { get; set; }
        public int LikeCount { get; set; }
        public string PostImage { get; set; }
        public int? ReplyToPost { get; set; }
        public string Tag { get; set; }

        public virtual Member Member { get; set; }
        public virtual PostType PostType { get; set; }
        public virtual Post ReplyToPostNavigation { get; set; }
        public virtual ICollection<Post> InverseReplyToPostNavigation { get; set; }
        public virtual ICollection<PostLiked> PostLikeds { get; set; }
    }
}
