using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeModelViewUI.Models
{
    public class EmployeViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Designation")]
        public long? DesignationId { get; set; }
       
        public string DesignationName { get; set; }

        [Display(Name = "Employee Code")]
        [Required]
        
        public string EmpCode { get; set; }
        [Display (Name ="First Name")]
        [Required]
        public string Firstname { get; set; }
        [Display (Name ="Last Name")]
        [Required]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Only Gmail accounts are allowed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be exactly 10 digits.")]
        public string Contact { get; set; }
        public string DepartmentName { get; set; }

        public virtual DepartmentViewModel Department { get; set; }
        public virtual Designation Designation { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        public IEnumerable<SelectListItem> DesignationList { get; set; }
        [Display (Name ="SL No")]

        public int SlNo { get; set; }
    }
}
