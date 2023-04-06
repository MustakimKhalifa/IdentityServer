using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Core.Dtos
{
    public class EmployeeResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UpdateEmployeeDto()
            {
                Id = 1,
                Name = "Test",
                Address = "",
                Salary = 000

            };
        }
    }
}
