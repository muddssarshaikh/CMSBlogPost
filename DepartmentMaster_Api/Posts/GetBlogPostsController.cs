using Microsoft.AspNetCore.Mvc;
using CMSBlogMaster_BL.BlogPostsMasterFeatures;
using CMSBlogMaster_BL.Domain.Common;
using CMSBlog_Api_Contracts.Model;

namespace BlogPostsMaster_Api.BlogPostsMasterController
{
    [ApiController]
    public class GetBlogPostsMasterController : ControllerBase
    {
        private readonly GetBlogPostsMasterWrapper _getBlogPostsMasterWrapper;
        private readonly ILogger<GetBlogPostsMasterController> _logger;

        public GetBlogPostsMasterController(GetBlogPostsMasterWrapper getBlogPostsMasterWrapper, ILogger<GetBlogPostsMasterController> logger)
        {
            _getBlogPostsMasterWrapper = getBlogPostsMasterWrapper;
            _logger = logger;
        }


        #region Get  BlogPosts Master
        /// <summary>
        /// Get  BlogPosts Master
        /// </summary>
        /// <param name="PaginationQuery"></param>
        /// <returns> BlogPosts Master master and it's collection</returns>
        [HttpGet("api/GetBlogPostsMaster")]
        public async Task<ActionResult<DataResponse>> GetBlogPostsMaster()
        {
            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMaster Started.");

            var BlogPostsMaster = await _getBlogPostsMasterWrapper.GetBlogPostsMaster();

            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMaster End.");

            return Ok(BlogPostsMaster);
        }
        #endregion


        #region Get  BlogPosts By Id
        /// <summary>
        /// Get  BlogPosts By Id
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: api/GetBlogPostsMasterById/1
        [HttpGet("api/GetBlogPostsMasterById/{id}")]
        public async Task<ActionResult<BlogPostsMasterModel>> GetBlogPostsMasterById(int id)
        {
            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMasterById Started.");

            var BlogPostsMasterById = await _getBlogPostsMasterWrapper.GetBlogPostsMasterById(id);

            if (BlogPostsMasterById == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMasterById End.");

            return Ok(BlogPostsMasterById);
        }
        #endregion

        #region Get  BlogPosts By category
        /// <summary>
        /// Get  BlogPosts By categorycategory
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: api/GetBlogPostsMasterById/1
        [HttpGet("api/GetBlogPostsMasterByCategory/{id}")]
        public async Task<ActionResult<BlogPostsMasterModel>> GetBlogPostsMasterByCategory(int id)
        {
            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMasterById Started.");

            var BlogPostsMasterById = await _getBlogPostsMasterWrapper.GetBlogPostsMasterById(id);

            if (BlogPostsMasterById == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetBlogPostsMasterController: GetBlogPostsMasterById End.");

            return Ok(BlogPostsMasterById);
        }
        #endregion


    }
}
