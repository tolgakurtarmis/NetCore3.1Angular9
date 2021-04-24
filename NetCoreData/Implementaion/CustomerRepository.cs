using NetCoreData.DataContext;
using NetCoreData.DataContracts;
using NetCoreData.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreData.Implementaion
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly NetCoreDbContext _db;
        public CustomerRepository(NetCoreDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
