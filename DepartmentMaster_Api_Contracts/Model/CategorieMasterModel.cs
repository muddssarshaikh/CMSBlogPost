using System.ComponentModel.DataAnnotations;

namespace CMSBlog_Api_Contracts.Model
{
    public class CategorieMasterModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
