using NetCoreBusinessEngine.Contracts;
using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;
using NetCoreData.DataContracts;
using NetCoreData.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreBusinessEngine.Implementaion
{
    public class OrderBusinessEngine : IOrderBusinessEngine
    {
        public readonly IUnitOfWorks _uow;
        public OrderBusinessEngine(IUnitOfWorks uow)
        {
            _uow = uow;

        }

        public Result<bool> DeleteOrder(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return new Result<bool>(false, $"id => {orderId} Id değeri 0 dan büyük olmalıdır.");

                var data = _uow.order.GetFirstOrDefault(o => o.OrderId == orderId, "OrderItems");


                foreach (var item in data.OrderItems.ToList())
                {
                    _uow.orderItem.Remove(item);
                }
                _uow.order.Remove(data);
                _uow.Save();

                return new Result<bool>(true, ResultConstant.RecordRemovedSuccess);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, ex.Message.ToString());

            }
        }

        public Result<List<GetOrderDto>> GetOrders()
        {
            var data = _uow.order.GetAll(null,null,"Customer");
            if (data != null)
            {
                List<GetOrderDto> returnModel = new List<GetOrderDto>();
                foreach (var item in data)
                {
                    returnModel.Add(new GetOrderDto()
                    { 
                            GrandTotal = item.GrandTotal,
                            OrderId = item.OrderId,
                            OrderNo = item.OrderNo,
                            PaymentMethod = item.PaymentMethod,
                            CustomerName = item.Customer.Name
                        
                    });
                }
                return new Result<List<GetOrderDto>>(true,ResultConstant.RecordFound,returnModel);
            }
            return new Result<List<GetOrderDto>>(true, ResultConstant.RecordNotFound);


        }

        public Result<bool> SaveOrder(OrderDto order)
        {
            try
            {
                Order orderModel = new Order();
                orderModel.CustomerId = Convert.ToInt32(order.OrderSubDto.CustomerId);
                orderModel.GrandTotal = Convert.ToInt32(order.OrderSubDto.GrandTotal);
                orderModel.OrderNo = order.OrderSubDto.OrderNo;
                orderModel.PaymentMethod = order.OrderSubDto.PaymentMethod;

                _uow.order.Add(orderModel);
                _uow.Save();


                foreach (var item in order.OrderItemModelDtos)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = orderModel.OrderId;
                    orderItem.Quantity = item.Quantity;
                    orderItem.ItemId = Convert.ToInt32(item.ItemId);
                    _uow.orderItem.Add(orderItem);
                }

                _uow.Save();

                return new Result<bool>(true, ResultConstant.RecordCreated);

            }
            catch (Exception ex)
            {
                return new Result<bool>(false, ex.Message.ToString());

            }
        }
    }
}
