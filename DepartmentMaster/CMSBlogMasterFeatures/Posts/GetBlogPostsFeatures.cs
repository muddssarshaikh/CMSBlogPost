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

namespace CMSBlogMaster_BL.BlogPostsMasterFeatures
{
    // Abstract class wrapper
    public abstract class GetBlogPostsMasterWrapper
    {
        public abstract Task<DataResponse> GetBlogPostsMaster();

        public abstract Task<BlogPostsMasterModel> GetBlogPostsMasterById(int id);
        public abstract Task<BlogPostsMasterModel> GetBlogPostsMasterByCategory(int id);
    }
    public class GetBlogPostsMasterFeatures : GetBlogPostsMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<GetBlogPostsMasterFeatures> _logger;

        public GetBlogPostsMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<GetBlogPostsMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Get BlogPosts Master
        /// <summary>
        /// Get data From BlogPosts Master 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public override async Task<DataResponse> GetBlogPostsMaster()
        {
            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMaster Started.");

            var BlogPostsMasterDetails = from dm in _dbContext.BlogPosts
                                         select new BlogPostsMasterModel()
                                         {
                                             PostId = dm.PostId,
                                             Title = dm.Title,
                                             Content = dm.Content,
                                             CategoryId = dm.CategoryId,
                                         };
            var response = new DataResponse
            {
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true,
                BlogPostsMasterCollection = BlogPostsMasterDetails
            };

            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMaster End.");
            return await Task.FromResult(response);
        }
        #endregion


        #region Get BlogPosts Master By ID
        /// <summary>
        ///  Get data From BlogPosts Master by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public override async Task<BlogPostsMasterModel> GetBlogPostsMasterById(int id)
        {
            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMasterById Started.");
            var BlogPostsMasterById = await _dbContext.BlogPosts.Where(s => s.PostId == id).Select(s =>
                                    new BlogPostsMasterModel()
                                    {
                                        PostId = s.PostId,
                                        Title = s.Title,
                                        Content = s.Content,
                                        CategoryId = s.CategoryId,
                                    }).ToListAsync();

            if (BlogPostsMasterById.FirstOrDefault() == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMasterById End.");

            return await Task.FromResult(BlogPostsMasterById.First());
        }
        #endregion

        #region Get BlogPosts Master By Category
        /// <summary>
        ///  Get data From BlogPosts Master by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public override async Task<BlogPostsMasterModel> GetBlogPostsMasterByCategory(int id)
        {
            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMasterById Started.");
            var BlogPostsMasterById = await _dbContext.BlogPosts.Where(s => s.CategoryId == id).Select(s =>
                                    new BlogPostsMasterModel()
                                    {
                                        PostId = s.PostId,
                                        Title = s.Title,
                                        Content = s.Content,
                                        CategoryId = s.CategoryId,
                                    }).ToListAsync();

            if (BlogPostsMasterById.FirstOrDefault() == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            _logger.LogInformation("GetBlogPostsMasterFeatures: GetBlogPostsMasterById End.");

            return await Task.FromResult(BlogPostsMasterById.First());
        }
        #endregion

    }

}
