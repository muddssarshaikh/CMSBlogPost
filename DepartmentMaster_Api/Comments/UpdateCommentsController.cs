
using Microsoft.AspNetCore.Mvc;
using CommentsMaster_BL.CommentsMasterFeatures;
using CMSBlogMaster_BL.CommentsMasterFeatures;
using CMSBlogMaster_BL.Domain;

namespace CommentsMaster_Api.CommentsMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateCommentsMasterController : ControllerBase
    {
        private readonly UpdateCommentsMasterWrapper _updateCommentsMasterWrapper;
        private readonly ILogger<UpdateCommentsMasterController> _logger;
        public UpdateCommentsMasterController(UpdateCommentsMasterWrapper updateCommentsMasterWrapper, ILogger<UpdateCommentsMasterController> logger)
        {
            _updateCommentsMasterWrapper = updateCommentsMasterWrapper;
            _logger = logger;   
        }

        #region  Comments Master
        /// <summary>
        /// Update data to  Comments Master
        /// </summary>
        /// <param name="CommentsMaster"></param>
        /// <returns>Id of created  Comments Master master and it's collection</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentsMaster(int id, [FromBody] Comments CommentsMaster)
        {
            _logger.LogInformation("UpdateCommentsMasterController: UpdateCommentsMaster Started.");

            if (id != CommentsMaster.CommentId)
            {
                return BadRequest();
            }
            var result = await _updateCommentsMasterWrapper.UpdateCommentsMaster(CommentsMaster);

            _logger.LogInformation("UpdateCommentsMasterController: UpdateCommentsMaster End.");

            return Ok(result);
        }
        #endregion
    }
}
