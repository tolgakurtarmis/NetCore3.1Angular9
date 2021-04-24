using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;

namespace NetCore3._1Angular9.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusinessEngine _customerBusinessEngine;

        public CustomerController(ICustomerBusinessEngine customerBusinessEngine)
        {
            _customerBusinessEngine = customerBusinessEngine;
        }
        [HttpGet("GetCustomersList")]
        public List<CustomerDto> GetCustomerList()
        {
            return _customerBusinessEngine.GetCustomers().Data;
        }
    }
}