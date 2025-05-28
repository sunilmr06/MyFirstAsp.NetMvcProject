

using System.ComponentModel.DataAnnotations;

namespace Employee1.api.Models.Entities
{
    public class  Employee
    {
        public int EmployeeId { get; set; }
        public string EmpCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public  string Email { get; set; }
        public string Contact { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public long? DesignationId { get; set; }
        public virtual Designation Designation { get; set; }


        //  public int SlNo { get; set; }
    }
}
