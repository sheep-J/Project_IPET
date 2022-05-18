using System;
using System.ComponentModel;

namespace Project_IPET.ViewModels
{
    public class CCommentViewModel
    {
        public int CommentId { get; set; }
        [DisplayName("產品名稱")]
        public string ProductName { get; set; }
        [DisplayName("訂單編號")]
        public int OrderId { get; set; }
        [DisplayName("評價會員")]
        public string MemberName { get; set; }

        public int MemberID { get; set; }
        [DisplayName("評價星級")]
        public int? Rating { get; set; }
        [DisplayName("評價日期")]
        public string CommentDate { get; set; }
        [DisplayName("評價內容")]
        public string CommentContent { get; set; }
        [DisplayName("回覆內容")]
        public string ReplyContent { get; set; }
        [DisplayName("屏蔽狀態")]
        public bool Banned { get; set; }
        [DisplayName("屏蔽內容")]
        public string BannedContent { get; set; }
        [DisplayName("回覆狀態")]
        public bool Reply { get; set; }
        [DisplayName("會員照片")]
        public string MemberImage { get; set; }
        public string FilterKeyword { get; set; }
        public string FilterRating { get; set; }

        public string FilterReply { get; set; }

        public string FilterBanned { get; set; }

        public DateTime? FilterCommentFristDate { get; set; }


        public DateTime? FilterCommentLastDate { get; set; }

        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }


    }
}
