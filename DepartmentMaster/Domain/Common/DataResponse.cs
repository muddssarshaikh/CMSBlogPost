using CMSBlog_Api_Contracts.Model;
using System.Diagnostics.CodeAnalysis;

namespace CMSBlogMaster_BL.Domain.Common
{
    [ExcludeFromCodeCoverageAttribute]

    public class DataResponse
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public object Result { get; set; }
        public bool IsSuccess { get; set; }

        public IEnumerable<CategorieMasterModel> CategorieMasterCollection { get; set; }
        public IEnumerable<BlogPostsMasterModel> BlogPostsMasterCollection { get; set; }
        public IEnumerable<CommentsMasterModel> CommentsMasterCollection { get; set; }
    }
}
