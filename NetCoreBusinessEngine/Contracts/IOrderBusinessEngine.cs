using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;
using System.Collections.Generic;

namespace NetCoreBusinessEngine.Contracts
{
    public interface IOrderBusinessEngine
    {
        Result<bool> SaveOrder(OrderDto order);
        Result<List<GetOrderDto>> GetOrders();
        Result<bool> DeleteOrder(int orderId);
    }
}
