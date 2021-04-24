using NetCoreData.DataContext;
using NetCoreData.DataContracts;
using NetCoreData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreData.Implementaion
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly NetCoreDbContext _db;

        public OrderRepository(NetCoreDbContext db) : base(db)
        {
            _db = db;

        }
    }
}
