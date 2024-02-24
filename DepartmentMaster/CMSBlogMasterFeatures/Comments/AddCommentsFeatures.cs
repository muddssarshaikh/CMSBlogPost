using CMSBlogMaster_BL.Database;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommentsMaster_BL.CommentsMasterFeatures
{
    // Abstract class wrapper
    public abstract class AddCommentsMasterWrapper
    {
        public abstract Task<ActionResult> CreateCommentsMaster(Comments CommentsMaster);
    }
    public class AddCommentsMasterFeatures : AddCommentsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<AddCommentsMasterFeatures> _logger;

        public AddCommentsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<AddCommentsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Create Comments Master
        /// <summary>
        /// CreateCommentsMaster
        /// </summary>
        /// <param name="CommentsMaster"></param>
        /// <returns></returns>
        public override async Task<ActionResult> CreateCommentsMaster(Comments CommentsMaster)
        {
            _logger.LogInformation("AddCommentsMasterFeatures: CreateCommentsMaster Started.");
           

            var createCommentsMaster = new Comments
            {
                CommentId = CommentsMaster.CommentId,
                Comment = CommentsMaster.Comment,
                PostId = CommentsMaster.PostId,
                
            };

            _dbContext.Comments.Add(createCommentsMaster);
            await _dbContext.SaveChangesAsync();
            CommentsMaster.CommentId = createCommentsMaster.CommentId;

            var response = new DataResponse
            {
                Message = ValidationMessages.RECORD_ADDED_SUCESSFULLY,
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true
            };

            _logger.LogInformation("AddCommentsMasterFeatures: CreateCommentsMaster end.");
            return new OkObjectResult(response);
        }
        #endregion
    }
}
