using NetCoreData.DataContext;
using NetCoreData.DataContracts;
using NetCoreData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreData.Implementaion
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly NetCoreDbContext _db;

        public OrderItemRepository(NetCoreDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
