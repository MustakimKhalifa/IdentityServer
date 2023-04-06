using DemoEmployeeApi.Core.Dtos;
using DemoEmployeeApi.Core.Interface;
using DemoEmployeeApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Service.Interfaces
{
    public interface IEmployeeService
    {
        GetEmployeeDto GetEmployee(int? id);
        IResponseModel GetAllEmployee();
        IResponseModel AddEmployee(EmployeeDto employee);
        IResponseModel UpdateEmployee(UpdateEmployeeDto employee);
        IResponseModel DeleteEmployee(int Id);
    }
}
