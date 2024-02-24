using CMSBlogMaster_BL.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using System.Linq;


namespace CMSBlogMaster_BL.CommentsMasterFeatures
{
    // Abstract class wrapper
    public abstract class UpdateCommentsMasterWrapper
    {
        public abstract Task<IActionResult> UpdateCommentsMaster(Comments CommentsMaster);
    }
    public class UpdateCommentsMasterFeatures : UpdateCommentsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<UpdateCommentsMasterFeatures> _logger;

        public UpdateCommentsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<UpdateCommentsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Update Comments Master
        /// <summary>
        /// UpdateCommentsMaster
        /// </summary>
        /// <param name="CommentsMaster"></param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateCommentsMaster(Comments CommentsMaster)
        {
            _logger.LogInformation("UpdateCommentsMasterFeatures: UpdateCommentsMaster Started.");
           
            var existCommentsMaster = await _dbContext.Comments.FindAsync(CommentsMaster.PostId);

            if (existCommentsMaster == null)
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
                existCommentsMaster.Comment = CommentsMaster.Comment;
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation("UpdateCommentsMasterFeatures: UpdateCommentsMaster end.");

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