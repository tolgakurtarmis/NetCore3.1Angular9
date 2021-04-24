using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreBusinessEngine.Contracts
{
    public interface ICustomerBusinessEngine
    {
        Result<List<CustomerDto>> GetCustomers();
    }
}
