using NetCoreData.DataContext;
using NetCoreData.DataContracts;
using NetCoreData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreData.Implementaion
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly NetCoreDbContext _db;
        public ItemRepository(NetCoreDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
