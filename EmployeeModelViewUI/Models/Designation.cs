using System.ComponentModel.DataAnnotations;

namespace EmployeeModelViewUI.Models
{
    public class Designation
    {
        public long? DesignationId { get; set; }
        [Display(Name ="Designation Name")]
        public string DesignationName { get; set; }
        public IEnumerable<EmployeViewModel> Employees { get; set; }
        public int SlNo { get; set; }
    }
}
