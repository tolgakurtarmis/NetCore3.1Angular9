using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreData.DataContracts
{
    public interface IUnitOfWorks :IDisposable
    {
        IOrderRepository order { get; }
        IOrderItemRepository orderItem { get; }

        IItemRepository items { get; }
        ICustomerRepository customer { get; }
        void Save();
            
    }
}
