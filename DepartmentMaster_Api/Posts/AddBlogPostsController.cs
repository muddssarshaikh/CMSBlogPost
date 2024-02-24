
using Microsoft.AspNetCore.Mvc;
using BlogPostsMaster_BL.BlogPostsMasterFeatures;
using CMSBlogMaster_BL.Domain;
using Microsoft.AspNetCore.Cors;

namespace BlogPosts_Api.BlogPostsController
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AddBlogPostsController : ControllerBase
    {
        private readonly AddBlogPostsMasterWrapper _addBlogPostsWrapper;
        private readonly ILogger<AddBlogPostsController> _logger;

        public AddBlogPostsController(AddBlogPostsMasterWrapper addBlogPostsWrapper, ILogger<AddBlogPostsController> logger)
        {
            _addBlogPostsWrapper = addBlogPostsWrapper;
            _logger = logger;
        }

        #region Create  Department Master
        /// <summary>
        /// Add  Department Master
        /// </summary>
        /// <param name="BlogPosts"></param>
        /// <returns></returns>Add  Department Master
        [HttpPost("api/AddBlogPostss")]
        public async Task<ActionResult> CreateBlogPosts([FromBody] BlogPosts BlogPosts)
        {
            _logger.LogInformation("AddBlogPostsController: CreateBlogPosts Started.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addBlogPostsWrapper.CreateBlogPostsMaster(BlogPosts);

            _logger.LogInformation("AddBlogPostsController: CreateBlogPosts end.");

            return Ok(result);
        }
        #endregion
    }
}
