namespace Employee1.api.Models.Entities
{
    public class AddEmployeeDto
    {
        public string EmpCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int? DepartmentId { get; set; }
        public long? DesignationId { get; set; }


    }
}
