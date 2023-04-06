using DemoEmployeeApi.Core.Dtos;
using DemoEmployeeApi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoEmployeeApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet(nameof(Get))]
        public GetEmployeeDto Get(int Id) 
        {
            var response = _employeeService.GetEmployee(Id);         
            return response;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var response = _employeeService.GetAllEmployee();
            return Ok(response);
        }

        [HttpPost(nameof(Add))]
        public IActionResult Add(EmployeeDto employee) 
        {
            var response = _employeeService.AddEmployee(employee);
            return Ok(response);
        }

        [HttpPost(nameof(Modify))]
        public IActionResult Modify(UpdateEmployeeDto employee) 
        {
            var response = _employeeService.UpdateEmployee(employee);
            return Ok(response);
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id) 
        {
            var response = _employeeService.DeleteEmployee(Id);
            return Ok(response);
        }
    }
}
