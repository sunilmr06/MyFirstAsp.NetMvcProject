using System.ComponentModel.DataAnnotations;

namespace EmployeeModelViewUI.Models
{
    public class DepartmentViewModel
    {
        [Key]
        public int DepartmentId { get; set; }

        [Display (Name ="Department Name")]
        [Required]
        public string DepartmentName { get; set; }

        [Display(Name = "Description")]
        public string? Discrtipion { get; set; }
        [Display (Name ="SL NO")]
        public int SlNo { get; set; }
    }
}
