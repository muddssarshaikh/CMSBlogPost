using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBlog_Api_Contracts.Model
{
    public class BlogPostsMasterModel
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
