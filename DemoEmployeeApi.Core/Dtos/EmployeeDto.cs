using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Core.Dtos
{
    public class EmployeeDto
    {           
        public string? Name { get; set; }        
        public int? Salary { get; set; }
        public string? Address { get; set; }
    }
}
