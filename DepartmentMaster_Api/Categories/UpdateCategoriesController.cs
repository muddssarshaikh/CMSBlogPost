
using Microsoft.AspNetCore.Mvc;
using CategorieMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.Domain;

namespace CategorieMaster_Api.CategorieMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateCategorieMasterController : ControllerBase
    {
        private readonly UpdateCategorieMasterWrapper _updateCategorieMasterWrapper;
        private readonly ILogger<UpdateCategorieMasterController> _logger;
        public UpdateCategorieMasterController(UpdateCategorieMasterWrapper updateCategorieMasterWrapper, ILogger<UpdateCategorieMasterController> logger)
        {
            _updateCategorieMasterWrapper = updateCategorieMasterWrapper;
            _logger = logger;   
        }

        #region  Categorie Master
        /// <summary>
        /// Update data to  Categorie Master
        /// </summary>
        /// <param name="CategorieMaster"></param>
        /// <returns>Id of created  Categorie Master master and it's collection</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategorieMaster(int id, [FromBody] Categories CategorieMaster)
        {
            _logger.LogInformation("UpdateCategorieMasterController: UpdateCategorieMaster Started.");

            if (id != CategorieMaster.CategoryId)
            {
                return BadRequest();
            }
            var result = await _updateCategorieMasterWrapper.UpdateCategorieMaster(CategorieMaster);

            _logger.LogInformation("UpdateCategorieMasterController: UpdateCategorieMaster End.");

            return Ok(result);
        }
        #endregion
    }
}
