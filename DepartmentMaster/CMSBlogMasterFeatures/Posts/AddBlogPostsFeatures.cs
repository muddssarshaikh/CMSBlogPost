using CMSBlogMaster_BL.Database;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogPostsMaster_BL.BlogPostsMasterFeatures
{
    // Abstract class wrapper
    public abstract class AddBlogPostsMasterWrapper
    {
        public abstract Task<ActionResult> CreateBlogPostsMaster(BlogPosts BlogPostsMaster);
    }
    public class AddBlogPostsMasterFeatures : AddBlogPostsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<AddBlogPostsMasterFeatures> _logger;

        public AddBlogPostsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<AddBlogPostsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Create BlogPosts Master
        /// <summary>
        /// CreateBlogPostsMaster
        /// </summary>
        /// <param name="BlogPostsMaster"></param>
        /// <returns></returns>
        public override async Task<ActionResult> CreateBlogPostsMaster(BlogPosts BlogPostsMaster)
        {
            _logger.LogInformation("AddBlogPostsMasterFeatures: CreateBlogPostsMaster Started.");
           

            var createBlogPostsMaster = new BlogPosts
            {
                PostId = BlogPostsMaster.PostId,
                Title = BlogPostsMaster.Title,
                Content = BlogPostsMaster.Content,
                CategoryId = BlogPostsMaster.CategoryId,
            };

            _dbContext.BlogPosts.Add(createBlogPostsMaster);
            await _dbContext.SaveChangesAsync();
            BlogPostsMaster.CategoryId = createBlogPostsMaster.CategoryId;

            var response = new DataResponse
            {
                Message = ValidationMessages.RECORD_ADDED_SUCESSFULLY,
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true
            };

            _logger.LogInformation("AddBlogPostsMasterFeatures: CreateBlogPostsMaster end.");
            return new OkObjectResult(response);
        }
        #endregion
    }
}
