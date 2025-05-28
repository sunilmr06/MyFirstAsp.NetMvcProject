namespace Employee1.api.Models.Entities
{
    public class Designation
    {
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        // public string? DesignationCode { get; set; }
    }
}
