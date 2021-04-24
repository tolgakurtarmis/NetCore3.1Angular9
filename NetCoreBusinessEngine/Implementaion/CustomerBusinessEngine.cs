using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;
using NetCoreData.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreBusinessEngine.Implementaion
{
    public class CustomerBusinessEngine : ICustomerBusinessEngine
    {
        public readonly IUnitOfWorks _uow;
        public CustomerBusinessEngine(IUnitOfWorks uow)
        {
            _uow = uow;

        }
        public Result<List<CustomerDto>> GetCustomers()
        {
            List<CustomerDto> items = new List<CustomerDto>();
            var data = _uow.customer.GetAll().ToList();

            if (data != null)
            {
                foreach (var item in data)
                {
                    items.Add(new CustomerDto()
                    {
                        CustomerId = item.CustomerId,
                        Name = item.Name,

                    });
                }
                return new Result<List<CustomerDto>>(true, ResultConstant.RecordCreated, items);
            }
            return new Result<List<CustomerDto>>(false, ResultConstant.RecordNotFound, items);

        }
    }
}