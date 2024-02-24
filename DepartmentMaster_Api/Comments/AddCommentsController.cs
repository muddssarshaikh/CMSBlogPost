
using Microsoft.AspNetCore.Mvc;
using CommentsMaster_BL.CommentsMasterFeatures;
using CMSBlogMaster_BL.Domain;
using Microsoft.AspNetCore.Cors;

namespace Comments_Api.CommentsController
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AddCommentsController : ControllerBase
    {
        private readonly AddCommentsMasterWrapper _addCommentsWrapper;
        private readonly ILogger<AddCommentsController> _logger;

        public AddCommentsController(AddCommentsMasterWrapper addCommentsWrapper, ILogger<AddCommentsController> logger)
        {
            _addCommentsWrapper = addCommentsWrapper;
            _logger = logger;
        }

        #region Create  Department Master
        /// <summary>
        /// Add  Department Master
        /// </summary>
        /// <param name="Comments"></param>
        /// <returns></returns>Add  Department Master
        [HttpPost("api/AddCommentss")]
        public async Task<ActionResult> CreateComments([FromBody] Comments Comments)
        {
            _logger.LogInformation("AddCommentsController: CreateComments Started.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addCommentsWrapper.CreateCommentsMaster(Comments);

            _logger.LogInformation("AddCommentsController: CreateComments end.");

            return Ok(result);
        }
        #endregion
    }
}
