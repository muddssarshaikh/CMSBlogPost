
using Microsoft.AspNetCore.Mvc;
using CategorieMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.Domain;
using Microsoft.AspNetCore.Cors;

namespace Categorie_Api.CategorieController
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AddCategorieController : ControllerBase
    {
        private readonly AddCategorieMasterWrapper _addCategorieWrapper;
        private readonly ILogger<AddCategorieController> _logger;

        public AddCategorieController(AddCategorieMasterWrapper addCategorieWrapper, ILogger<AddCategorieController> logger)
        {
            _addCategorieWrapper = addCategorieWrapper;
            _logger = logger;
        }

        #region Create  Department Master
        /// <summary>
        /// Add  Department Master
        /// </summary>
        /// <param name="Categorie"></param>
        /// <returns></returns>Add  Department Master
        [HttpPost("api/AddCategories")]
        public async Task<ActionResult> CreateCategorie([FromBody] Categories Categorie)
        {
            _logger.LogInformation("AddCategorieController: CreateCategorie Started.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addCategorieWrapper.CreateCategorieMaster(Categorie);

            _logger.LogInformation("AddCategorieController: CreateCategorie end.");

            return Ok(result);
        }
        #endregion
    }
}
