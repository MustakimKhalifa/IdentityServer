using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Core.Dtos
{
    public class UpdateEmployeeDto : EmployeeDto
    {
        public int Id { get; set; }
    }
}
