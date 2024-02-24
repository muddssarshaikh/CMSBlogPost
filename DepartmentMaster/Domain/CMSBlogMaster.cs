using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSBlogMaster_BL.Domain
{
    public class DepartmentMasteres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public inIDId { get; set; }
        [Required]
        [MaxLength055, ErrorMessage = "Maximum Characte2055!")]        [RegularExpression(@"^[a-zA-Z0-9\.\-]+$", ErrorMessage = "Space & Special Characters are not allowed (except “.”& “-“)")]
        [DefaultValue("")]

        public strigCodeme { get; set; }
      [Required]
        [MaxLength(50, ErrorMessage = "Maximum Character 50!")]
        public string Display { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Character 100!")]
        public string Definition { get; set; }
        public int GroupDepartmentID { get; set; }
        public Int16 Sequence { get; set; }
        public string? Prefix{ get; set; }
        public string? SequenceNumber { get; set; }
        public string? SequenceMonth { get; set; }
        public string? SequenceYear { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }  
    }
}

