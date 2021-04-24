using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;

namespace NetCore3._1Angular9.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemBusinessEngine _itemBusiness;
        public ItemController(IItemBusinessEngine itemBusiness)
        {
            _itemBusiness = itemBusiness;
        }
        [HttpGet("GetItems")]
        public List<ItemDto> GetItems()
        {
            return _itemBusiness.GetItems().Data; 
        }
        
    }
}