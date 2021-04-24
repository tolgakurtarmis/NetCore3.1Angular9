using Microsoft.AspNetCore.Mvc;
using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;
using System.Collections.Generic;

namespace NetCore3._1Angular9.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusinessEngine _orderBusinessEngine;
        public OrderController(IOrderBusinessEngine orderBusinessEngine)
        {
            _orderBusinessEngine = orderBusinessEngine;
        }
       
        [HttpGet("GetOrders")]
        public List<GetOrderDto> GetOrders()
        {
            var result = _orderBusinessEngine.GetOrders();
            return result.Data;

        }


        [HttpPost("SaveOrder")]
        public  bool SaveOrder([FromBody]OrderDto order)
        {
            var result = _orderBusinessEngine.SaveOrder(order);
            return result.IsSuccess;
        }

        [HttpDelete("DeleteOrder/{orderId}")]
        public bool DeleteOrder(int orderId)
        {
            var data = _orderBusinessEngine.DeleteOrder(orderId);
            return data.IsSuccess;
        }
    }
}
