using CMSBlogMaster_BL.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using System.Linq;


namespace CMSBlogMaster_BL.BlogPostsMasterFeatures
{
    // Abstract class wrapper
    public abstract class UpdateBlogPostsMasterWrapper
    {
        public abstract Task<IActionResult> UpdateBlogPostsMaster(BlogPosts BlogPostsMaster);
    }
    public class UpdateBlogPostsMasterFeatures : UpdateBlogPostsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<UpdateBlogPostsMasterFeatures> _logger;

        public UpdateBlogPostsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<UpdateBlogPostsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Update BlogPosts Master
        /// <summary>
        /// UpdateBlogPostsMaster
        /// </summary>
        /// <param name="BlogPostsMaster"></param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateBlogPostsMaster(BlogPosts BlogPostsMaster)
        {
            _logger.LogInformation("UpdateBlogPostsMasterFeatures: UpdateBlogPostsMaster Started.");
           
            var existBlogPostsMaster = await _dbContext.BlogPosts.FindAsync(BlogPostsMaster.PostId);

            if (existBlogPostsMaster == null)
            {
                var badResponse = new DataResponse
                {
                    Message = ValidationMessages.RECORD_NOT_FOUND,
                    StatusCode = ErrorStatusCode.STATUS_BadRequest,
                    IsSuccess = false
                };
                return new BadRequestObjectResult(badResponse);
            }
            else
            {
                existBlogPostsMaster.Title = BlogPostsMaster.Title;
                existBlogPostsMaster.Content = BlogPostsMaster.Content;
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation("UpdateBlogPostsMasterFeatures: UpdateBlogPostsMaster end.");

            var successResponse = new DataResponse
            {
                Message = ValidationMessages.RECORD_UPDATED_SUCESSFULLY,
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true
            };
            return new OkObjectResult(successResponse);
        }
        #endregion
    }
}