using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CMSBlogMaster_BL.Domain.Common;
using CMSBlogMaster_BL.Domain.Extension;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using CMSBlogMaster_BL.Database;
using CMSBlogMaster_BL.Domain;
using System.Runtime.Serialization;
using System.Linq;
using CMSBlog_Api_Contracts.Model;

namespace CMSBlogMaster_BL.CategorieMasterFeatures
{
    // Abstract class wrapper
    public abstract class GetCategorieMasterWrapper
    {
        public abstract Task<DataResponse> GetCategorieMaster();

        public abstract Task<CategorieMasterModel> GetCategorieMasterById(int id);
    }
    public class GetCategorieMasterFeatures : GetCategorieMasterWrapper
    {
        private readonly CMSBlogMasterDbContext _dbContext;
        private readonly ILogger<GetCategorieMasterFeatures> _logger;

        public GetCategorieMasterFeatures(CMSBlogMasterDbContext dbContext, ILogger<GetCategorieMasterFeatures> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Get Categorie Master
        /// <summary>
        /// Get data From Categorie Master 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public override async Task<DataResponse> GetCategorieMaster()
        {
            _logger.LogInformation("GetCategorieMasterFeatures: GetCategorieMaster Started.");

            var CategorieMasterDetails = from dm in _dbContext.Categories
                                         select new CategorieMasterModel()
                                         {
                                             CategoryId = dm.CategoryId,
                                             Name = dm.Name,
                                         };
            var response = new DataResponse
            {
                StatusCode = ErrorStatusCode.STATUS_SUCCESS,
                IsSuccess = true,
                CategorieMasterCollection = CategorieMasterDetails
            };

            _logger.LogInformation("GetCategorieMasterFeatures: GetCategorieMaster End.");
            return await Task.FromResult(response);
        }
        #endregion


        #region Get Categorie Master By ID
        /// <summary>
        ///  Get data From Categorie Master by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public override async Task<CategorieMasterModel> GetCategorieMasterById(int id)
        {
            _logger.LogInformation("GetCategorieMasterFeatures: GetCategorieMasterById Started.");
            var CategorieMasterById = await _dbContext.Categories.Where(s => s.CategoryId == id).Select(s =>
                                    new CategorieMasterModel()
                                    {
                                        CategoryId = s.CategoryId,
                                        Name = s.Name,
                                    }).ToListAsync();

            if (CategorieMasterById.FirstOrDefault() == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            _logger.LogInformation("GetCategorieMasterFeatures: GetCategorieMasterById End.");

            return await Task.FromResult(CategorieMasterById.First());
        }
        #endregion

    }

}
