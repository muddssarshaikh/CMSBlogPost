using CMSBlogMaster_BL.Database;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CategorieMaster_BL.CategorieMasterFeatures
{
    // Abstract class wrapper
    public abstract class AddCategorieMasterWrapper
    {
        public abstract Task<ActionResult> CreateCategorieMaster(Categories CategorieMaster);
    }
    public class AddCategorieMasterFeatures : AddCategorieMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<AddCategorieMasterFeatures> _logger;

        public AddCategorieMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<AddCategorieMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Create Categorie Master
        /// <summary>
        /// CreateCategorieMaster
        /// </summary>
        /// <param name="CategorieMaster"></param>
        /// <returns></returns>
        public override async Task<ActionResult> CreateCategorieMaster(Categories CategorieMaster)
        {
            _logger.LogInformation("AddCategorieMasterFeatures: CreateCategorieMaster Started.");
           

            var createCategorieMaster = new Categories
            {
                CategoryId = CategorieMaster.CategoryId,
                Name = CategorieMaster.Name,
            };

            _dbContext.Categories.Add(createCategorieMaster);
            await _dbContext.SaveChangesAsync();
            CategorieMaster.CategoryId = createCategorieMaster.CategoryId;

            var response = new DataResponse
            {
                Message = ValidationMessages.RECORD_ADDED_SUCESSFULLY,
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true
            };

            _logger.LogInformation("AddCategorieMasterFeatures: CreateCategorieMaster end.");
            return new OkObjectResult(response);
        }
        #endregion
    }
}
