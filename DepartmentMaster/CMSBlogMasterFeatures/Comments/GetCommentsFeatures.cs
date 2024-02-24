using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMSBlogMaster_BL.Domain.Common;
using CMSBlogMaster_BL.Domain.Extension;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using CMSBlogMaster_BL.Database;
using CMSBlogMaster_BL.Domain;
using System.Runtime.Serialization;
using System.Linq;
using CMSBlog_Api_Contracts.Model;

namespace CMSBlogMaster_BL.CommentsMasterFeatures
{
    // Abstract class wrapper
    public abstract class GetCommentsMasterWrapper
    {
        public abstract Task<DataResponse> GetCommentsMaster();
        public abstract Task<CommentsMasterModel> GetCommentsMasterByPost(int id);
        public abstract Task<CommentsMasterModel> GetCommentsMasterById(int id);
    }
    public class GetCommentsMasterFeatures : GetCommentsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<GetCommentsMasterFeatures> _logger;

        public GetCommentsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<GetCommentsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Get Comments Master
        /// <summary>
        /// Get data From Comments Master 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public override async Task<DataResponse> GetCommentsMaster()
        {
            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMaster Started.");

            var CommentsMasterDetails = from dm in _dbContext.Comments
                                         select new CommentsMasterModel()
                                         {
                                             PostId = dm.PostId,
                                             CommentId = dm.CommentId,
                                             Comment = dm.Comment,
                                         };
            var response = new DataResponse
            {
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true,
                CommentsMasterCollection = CommentsMasterDetails
            };

            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMaster End.");
            return await Task.FromResult(response);
        }
        #endregion


        #region Get Comments Master By ID
        /// <summary>
        ///  Get data From Comments Master by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public override async Task<CommentsMasterModel> GetCommentsMasterById(int id)
        {
            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMasterById Started.");
            var CommentsMasterById = await _dbContext.Comments.Where(s => s.CommentId == id).Select(s =>
                                    new CommentsMasterModel()
                                    {
                                        PostId = s.PostId,
                                        CommentId = s.CommentId,
                                        Comment = s.Comment,
                                    }).ToListAsync();

            if (CommentsMasterById.FirstOrDefault() == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMasterById End.");

            return await Task.FromResult(CommentsMasterById.First());
        }
        #endregion

        #region Get Comments Master By Post
        /// <summary>
        ///  Get data From Comments Master by Post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public override async Task<CommentsMasterModel> GetCommentsMasterByPost(int id)
        {
            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMasterById Started.");
            var CommentsMasterById = await _dbContext.Comments.Where(s => s.PostId == id).Select(s =>
                                    new CommentsMasterModel()
                                    {
                                        PostId = s.PostId,
                                        CommentId = s.CommentId,
                                        Comment = s.Comment,
                                    }).ToListAsync();

            if (CommentsMasterById.FirstOrDefault() == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            _logger.LogInformation("GetCommentsMasterFeatures: GetCommentsMasterById End.");

            return await Task.FromResult(CommentsMasterById.First());
        }
        #endregion

    }

}
