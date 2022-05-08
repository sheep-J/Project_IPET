using Microsoft.AspNetCore.Mvc.Rendering;
using Project_IPET.Models.EF;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project_IPET.ViewModels
{
    public class CPostViewModel
    {
      

        public int PostId { get; set; }

        [DisplayName("貼文標題:")]
        public string Title { get; set; }
        [DisplayName("貼文內容:")]
        public string PostContent { get; set; }
        public string PostDate { get; set; }
        public int LikeCount { get; set; }
        public string Tag { get; set; }
        public string MemberName { get; set; }
        public int MemberId { get; set; }
        public string MemberImage { get; set; }
        public int ReplyConut { get; set; }
        [DisplayName("貼文分類:")]
        public string PostType { get; set; }

        [DisplayName("貼文圖片:")]
        public string PostImage { get; set; }

      
    }
}
