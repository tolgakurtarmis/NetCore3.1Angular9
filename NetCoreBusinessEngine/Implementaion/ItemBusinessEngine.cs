using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using NetCoreData.DataContracts;
using System.Linq;
using NetCoreCommon.ResultConstant;

namespace NetCoreBusinessEngine.Implementaion
{
    public class ItemBusinessEngine : IItemBusinessEngine
    {
        public readonly IUnitOfWorks _uow;
        public ItemBusinessEngine(IUnitOfWorks uow)
        {
            _uow = uow;

        }
        public Result<List<ItemDto>> GetItems()
        {
            List<ItemDto> items = new List<ItemDto>();
            var data = _uow.items.GetAll().ToList();
 
            if (data != null)
            {
                foreach (var item in data)
                {
                    items.Add(new ItemDto()
                    {
                        ItemId = item.ItemId,
                        Name = item.Name,
                        Price = item.Price,

                    });
                }
                return new Result<List<ItemDto>>(true, ResultConstant.RecordCreated,items);
            }
            return new Result<List<ItemDto>>(false, ResultConstant.RecordNotFound, items);

        }
    }
}
