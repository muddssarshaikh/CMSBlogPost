
using Microsoft.AspNetCore.Mvc;
using BlogPostsMaster_BL.BlogPostsMasterFeatures;
using CMSBlogMaster_BL.BlogPostsMasterFeatures;
using CMSBlogMaster_BL.Domain;

namespace BlogPostsMaster_Api.BlogPostsMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateBlogPostsMasterController : ControllerBase
    {
        private readonly UpdateBlogPostsMasterWrapper _updateBlogPostsMasterWrapper;
        private readonly ILogger<UpdateBlogPostsMasterController> _logger;
        public UpdateBlogPostsMasterController(UpdateBlogPostsMasterWrapper updateBlogPostsMasterWrapper, ILogger<UpdateBlogPostsMasterController> logger)
        {
            _updateBlogPostsMasterWrapper = updateBlogPostsMasterWrapper;
            _logger = logger;   
        }

        #region  BlogPosts Master
        /// <summary>
        /// Update data to  BlogPosts Master
        /// </summary>
        /// <param name="BlogPostsMaster"></param>
        /// <returns>Id of created  BlogPosts Master master and it's collection</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPostsMaster(int id, [FromBody] BlogPosts BlogPostsMaster)
        {
            _logger.LogInformation("UpdateBlogPostsMasterController: UpdateBlogPostsMaster Started.");

            if (id != BlogPostsMaster.CategoryId)
            {
                return BadRequest();
            }
            var result = await _updateBlogPostsMasterWrapper.UpdateBlogPostsMaster(BlogPostsMaster);

            _logger.LogInformation("UpdateBlogPostsMasterController: UpdateBlogPostsMaster End.");

            return Ok(result);
        }
        #endregion
    }
}
