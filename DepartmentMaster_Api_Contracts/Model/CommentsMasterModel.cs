namespace CMSBlog_Api_Contracts.Model
{
    public class CommentsMasterModel
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string? Comment { get; set; }
    }
}
