using CMSBlogMaster_BL.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMSBlogMaster_BL.Domain;
using CMSBlogMaster_BL.Domain.Common;
using System.Linq;


namespace CMSBlogMaster_BL.CategorieMasterFeatures
{
    // Abstract class wrapper
    public abstract class UpdateCategorieMasterWrapper
    {
        public abstract Task<IActionResult> UpdateCategorieMaster(Categories CategorieMaster);
    }
    public class UpdateCategorieMasterFeatures : UpdateCategorieMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<UpdateCategorieMasterFeatures> _logger;

        public UpdateCategorieMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<UpdateCategorieMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Update Categorie Master
        /// <summary>
        /// UpdateCategorieMaster
        /// </summary>
        /// <param name="CategorieMaster"></param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateCategorieMaster(Categories CategorieMaster)
        {
            _logger.LogInformation("UpdateCategorieMasterFeatures: UpdateCategorieMaster Started.");
           
            var existCategorieMaster = await _dbContext.Categories.FindAsync(CategorieMaster.CategoryId);

            if (existCategorieMaster == null)
            {
                var badResponse = new DataResponse
                {
                    Message = ValidationMessages.RECORD_NOT_FOUND,
                    StatusCode = ErrorStatusCode.STATUS_BadRequest,
                    IsSuccess = false
                };
                return new BadRequestObjectResult(badResponse);
            }
            else
            {
                existCategorieMaster.CategoryId = CategorieMaster.CategoryId;
                existCategorieMaster.Name = CategorieMaster.Name;
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation("UpdateCategorieMasterFeatures: UpdateCategorieMaster end.");

            var successResponse = new DataResponse
            {
                Message = ValidationMessages.RECORD_UPDATED_SUCESSFULLY,
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true
            };
            return new OkObjectResult(successResponse);
        }
        #endregion
    }
}