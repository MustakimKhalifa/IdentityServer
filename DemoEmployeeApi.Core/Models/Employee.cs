using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Core.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Employee Name is Required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Salary is Required")]
        public int? Salary { get; set; }
        public string? Address { get; set; }
    }
}
