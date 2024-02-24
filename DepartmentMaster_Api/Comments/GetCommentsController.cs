using Microsoft.AspNetCore.Mvc;
using CMSBlogMaster_BL.CommentsMasterFeatures;
using CMSBlogMaster_BL.Domain.Common;
using CMSBlog_Api_Contracts.Model;

namespace CommentsMaster_Api.CommentsMasterController
{
    [ApiController]
    public class GetCommentsMasterController : ControllerBase
    {
        private readonly GetCommentsMasterWrapper _getCommentsMasterWrapper;
        private readonly ILogger<GetCommentsMasterController> _logger;

        public GetCommentsMasterController(GetCommentsMasterWrapper getCommentsMasterWrapper, ILogger<GetCommentsMasterController> logger)
        {
            _getCommentsMasterWrapper = getCommentsMasterWrapper;
            _logger = logger;
        }


        #region Get  Comments Master
        /// <summary>
        /// Get  Comments Master
        /// </summary>
        /// <param name="PaginationQuery"></param>
        /// <returns> Comments Master master and it's collection</returns>
        [HttpGet("api/GetCommentsMaster")]
        public async Task<ActionResult<DataResponse>> GetCommentsMaster()
        {
            _logger.LogInformation("GetCommentsMasterController: GetCommentsMaster Started.");

            var CommentsMaster = await _getCommentsMasterWrapper.GetCommentsMaster();

            _logger.LogInformation("GetCommentsMasterController: GetCommentsMaster End.");

            return Ok(CommentsMaster);
        }
        #endregion


        #region Get  Comments By Id
        /// <summary>
        /// Get  Comments By Id
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: api/GetCommentsMasterById/1
        [HttpGet("api/GetCommentsMasterById/{id}")]
        public async Task<ActionResult<CommentsMasterModel>> GetCommentsMasterById(int id)
        {
            _logger.LogInformation("GetCommentsMasterController: GetCommentsMasterById Started.");

            var CommentsMasterById = await _getCommentsMasterWrapper.GetCommentsMasterById(id);

            if (CommentsMasterById == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetCommentsMasterController: GetCommentsMasterById End.");

            return Ok(CommentsMasterById);
        }
        #endregion


        #region Get  Comments By Post
        /// <summary>
        /// Get  Comments By Post
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: api/GetCommentsMasterById/1
        [HttpGet("api/GetCommentsMasterByPost/{id}")]
        public async Task<ActionResult<CommentsMasterModel>> GetCommentsMasterByPost(int id)
        {
            _logger.LogInformation("GetCommentsMasterController: GetCommentsMasterById Started.");

            var CommentsMasterById = await _getCommentsMasterWrapper.GetCommentsMasterByPost(id);

            if (CommentsMasterById == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetCommentsMasterController: GetCommentsMasterById End.");

            return Ok(CommentsMasterById);
        }
        #endregion

    }
}
