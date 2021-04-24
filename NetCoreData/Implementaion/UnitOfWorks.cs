using NetCoreData.DataContext;
using NetCoreData.DataContracts;

namespace NetCoreData.Implementaion
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly NetCoreDbContext _db;

        public UnitOfWorks(NetCoreDbContext db)
        {
            _db = db;
            items = new ItemRepository(_db);
            customer = new CustomerRepository(_db);
            order = new OrderRepository(_db);
            orderItem = new OrderItemRepository(_db);

        }
        public IItemRepository items { get; private set; }
        public IOrderRepository order { get; private set; }
        public IOrderItemRepository orderItem { get; private set; }
        public ICustomerRepository customer { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
