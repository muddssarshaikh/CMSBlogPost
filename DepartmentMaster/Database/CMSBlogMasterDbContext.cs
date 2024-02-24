using CMSBlogMaster_BL.Domain;
using Microsoft.EntityFrameworkCore;


namespace CMSBlogMaster_BL.Database
{
    public partial class CMSBlogMasterDbContext : DbContext
    {
        public CMSBlogMasterDbContext(DbContextOptions<CMSBlogMasterDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<BlogPosts> BlogPosts { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }

    }
}
