using System.ComponentModel.DataAnnotations;

namespace DemoEmployeeClient.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }
        public string? Address { get; set; }
    }
}
