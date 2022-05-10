using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_IPET.Models
{
    public class CBFrontPostFilterFactory
    {
        private readonly MyProjectContext _context;

        public CBFrontPostFilterFactory(MyProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> PostFilter(PostFilterModel PostFilters)
        {
            IEnumerable<Post> datas = null;

            datas = from p in _context.Posts
                    select p;
            
            if (!string.IsNullOrEmpty(PostFilters.Keyword)) 
            {
                datas = from p in datas
                        where p.Member.Name == PostFilters.Keyword
                        where p.PostType.PostTypeName == PostFilters.Keyword
                        where p.PostDate == PostFilters.Keyword
                        where p.PostContent == PostFilters.Keyword
                        where p.Tag == PostFilters.Keyword
                        where p.Title == PostFilters.Keyword
                        select p;
               
            }

            if (!string.IsNullOrEmpty(PostFilters.PostType))
            {
                datas = from p in datas
                        where p.PostType.ToString() == PostFilters.PostType
                        select p;
            }

            if (!string.IsNullOrEmpty(PostFilters.PostFristDate) && !string.IsNullOrEmpty(PostFilters.PostLastDate))
            {
                datas = from p in datas
                        where DateTime.ParseExact(p.PostDate, "yyyy/mm/dd :G(zh-TW)", null) >= DateTime.ParseExact(PostFilters.PostFristDate, "yyyy/mm/dd :G(zh-TW)", null)
                           && DateTime.ParseExact(p.PostDate, "yyyy/mm/dd :G(zh-TW)", null) <= DateTime.ParseExact(PostFilters.PostLastDate, "yyyy/mm/dd :G(zh-TW)", null)
                        select p;
            }

            if (!string.IsNullOrEmpty(PostFilters.Tag))
            {
                datas = from p in datas
                        where p.Tag == PostFilters.Tag
                        select p;
            }
            
            return datas;
        }


















    }
}
