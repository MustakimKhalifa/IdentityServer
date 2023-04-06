using DemoEmployeeApi.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoEmployeeApi.Core.Models
{
    public class ResponseModel : IResponseModel
    {
        public int StatusCode { get; set; } 
        public string? Message { get; set; }
        public object? Data { get; set; }        
    }
}
