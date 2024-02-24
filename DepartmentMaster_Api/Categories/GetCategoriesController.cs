using Microsoft.AspNetCore.Mvc;
using CMSBlogMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.Domain.Common;
using CMSBlog_Api_Contracts.Model;

namespace CategorieMaster_Api.CategorieMasterController
{
    [ApiController]
    public class GetCategorieMasterController : ControllerBase
    {
        private readonly GetCategorieMasterWrapper _getCategorieMasterWrapper;
        private readonly ILogger<GetCategorieMasterController> _logger;

        public GetCategorieMasterController(GetCategorieMasterWrapper getCategorieMasterWrapper, ILogger<GetCategorieMasterController> logger)
        {
            _getCategorieMasterWrapper = getCategorieMasterWrapper;
            _logger = logger;
        }


        #region Get  Categorie Master
        /// <summary>
        /// Get  Categorie Master
        /// </summary>
        /// <param name="PaginationQuery"></param>
        /// <returns> Categorie Master master and it's collection</returns>
        [HttpGet("api/GetCategorieMaster")]
        public async Task<ActionResult<DataResponse>> GetCategorieMaster()
        {
            _logger.LogInformation("GetCategorieMasterController: GetCategorieMaster Started.");

            var CategorieMaster = await _getCategorieMasterWrapper.GetCategorieMaster();

            _logger.LogInformation("GetCategorieMasterController: GetCategorieMaster End.");

            return Ok(CategorieMaster);
        }
        #endregion


        #region Get  Categorie By Id
        /// <summary>
        /// Get  Categorie By Id
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: api/GetCategorieMasterById/1
        [HttpGet("api/GetCategorieMasterById/{id}")]
        public async Task<ActionResult<CategorieMasterModel>> GetCategorieMasterById(int id)
        {
            _logger.LogInformation("GetCategorieMasterController: GetCategorieMasterById Started.");

            var CategorieMasterById = await _getCategorieMasterWrapper.GetCategorieMasterById(id);

            if (CategorieMasterById == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetCategorieMasterController: GetCategorieMasterById End.");

            return Ok(CategorieMasterById);
        }
        #endregion


    }
}
