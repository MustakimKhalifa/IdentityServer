using AutoMapper;
using DemoEmployeeApi.Core.Dtos;
using DemoEmployeeApi.Core.Interface;
using DemoEmployeeApi.Core.Models;
using DemoEmployeeApi.Repo.GenericRepository.Interface;
using DemoEmployeeApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IRepositoryBase<Employee> _repository;
        private IResponseModel _response;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryBase<Employee> repository, IResponseModel response, IMapper mapper)
        {
            _repository = repository;
            _response = response;
            _mapper = mapper;
        }       

        public GetEmployeeDto GetEmployee(int? id)
        {
            var employee = _repository.Get(x => x.Id.Equals(id)).FirstOrDefault();

            if (employee == null)
            {
                throw new ApplicationException("Invalid");
            }
            var empDto = _mapper.Map<GetEmployeeDto>(employee);
            return empDto;
        }

        public IResponseModel GetAllEmployee()
        {
            try
            {
                var employees = _repository.GetAll();

                _response.StatusCode = 200;
                _response.Data = employees;

                if (employees.Count() <= 0)
                {
                    _response.Message = "Employee Not Found";
                    return _response;
                }

                _response.Message = "Employees get successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.StatusCode = 400;
                _response.Data = null;
                _response.Message = "Something is wrong\n" + ex.ToString();
                return _response;
            }
        }

        public IResponseModel AddEmployee(EmployeeDto employee)
        {
            try
            {
                var empData = _mapper.Map<Employee>(employee);
                _repository.Insert(empData);
                _response.StatusCode = 200;
                _response.Data = employee;
                _response.Message = "Employees Add successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.StatusCode = 400;
                _response.Data = null;
                _response.Message = "Something is wrong\n" + ex.ToString();
                return _response;
            }
        }

        public IResponseModel UpdateEmployee(UpdateEmployeeDto employee)
        {
            try
            {
                var emp = _repository.Get(x => x.Id.Equals(employee.Id)).FirstOrDefault();

                _response.StatusCode = 200;
                if (emp == null)
                {
                    _response.Data = employee;
                    _response.Message = "Employee not exist";
                    return _response;
                }
                if (employee.Name != null && employee.Name != "")
                {
                    emp.Name = employee.Name;
                }

                if (employee.Salary != null && employee.Salary != 0)
                {
                    emp.Salary = employee.Salary;
                }

                if (employee.Address != null && employee.Address != "")
                {
                    emp.Address = employee.Address;
                }

                _repository.Update(emp);
                _response.Data = employee;
                _response.Message = "Employees Update successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.StatusCode = 400;
                _response.Data = null;
                _response.Message = "Something is wrong\n" + ex.ToString();
                return _response;
            }
        }

        public IResponseModel DeleteEmployee(int Id)
        {
            try
            {
                var employee = _repository.Get(x => x.Id.Equals(Id)).FirstOrDefault();

                _response.StatusCode = 200;

                if (employee == null)
                {
                    _response.Data = employee;
                    _response.Message = "Employee not exist";
                    return _response;
                }

                _repository.Remove(employee);

                _response.Data = employee;
                _response.Message = "Employees delete successfully";
                return _response;
            }
            catch (Exception ex)
            {
                _response.StatusCode = 400;
                _response.Data = null;
                _response.Message = "Something is wrong\n" + ex.ToString();
                return _response;
            }

        }
    }
}
