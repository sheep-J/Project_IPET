using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_IPET.Models
{
    public class CCommentFilterFactory
    {
        private readonly MyProjectContext _context;

        public CCommentFilterFactory(MyProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<CCommentViewModel> CommentFilter(CCommentViewModel CCommentFilters)
        {
            IEnumerable<CCommentViewModel> datas = null;
            datas = _context.Comments
                    .Select(n => new CCommentViewModel
                    {
                        CommentId = n.CommentId,
                        ProductName = n.Product.ProductName,
                        OrderId = n.OrderDetail.OrderId,
                        MemberName = n.OrderDetail.Order.Member.Name,
                        Rating = n.Rating,
                        CommentDate = n.CommentDate,
                        CommentContent = n.CommentContent,
                        ReplyContent = n.ReplyContent,
                        Reply = n.Reply,
                        BannedContent = n.BannedContent,
                        Banned = n.Banned,

                    });

            if (CCommentFilters != null)
            {

                if (CCommentFilters.FilterKeyword != null)
                {
                    datas = datas.Where(c => c.MemberName.Contains(CCommentFilters.FilterKeyword) ||
                                             c.CommentDate.Contains(CCommentFilters.FilterKeyword) ||
                                             c.CommentContent.Contains(CCommentFilters.FilterKeyword) ||
                                             c.ReplyContent.Contains(CCommentFilters.FilterKeyword)
                                        )
                                  .Select(p => p);

                }

                if (CCommentFilters.FilterCommentFristDate != null && CCommentFilters.FilterCommentLastDate != null)
                {
                    datas = datas.Where(p => DateTime.Parse(p.CommentDate) > CCommentFilters.FilterCommentFristDate
                                         && DateTime.Parse(p.CommentDate).AddDays(-1) <= CCommentFilters.FilterCommentLastDate)
                                 .Select(p => p);
                }

                if (CCommentFilters.Rating != null)
                {
                    datas = datas.Where(c => c.Rating == CCommentFilters.FilterRating)
                                 .Select(p => p);
                }

                //if (CCommentFilters.FilterMemberName != null)
                //{
                //    datas = datas.Where(c => c.MemberName == CCommentFilters.FilterMemberName)
                //                 .Select(p => p);
                //}



                if (CCommentFilters.FilterBanned)
                {
                    datas = datas.Where(b =>b.Banned ==CCommentFilters.FilterBanned)
                                 .Select(p => p);
                }

                if (CCommentFilters.FilterReply)
                {
                    datas = datas.Where(b => b.Banned == CCommentFilters.FilterBanned)
                                .Select(p => p);
                }
            }

            return datas.ToList().OrderByDescending(p => DateTime.Parse(p.CommentDate));

        }
    }

}
