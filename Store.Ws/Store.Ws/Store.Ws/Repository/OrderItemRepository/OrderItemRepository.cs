using Store.Data.Models;
using Store.Data.Repository.Base;

namespace StoreWS.Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(StoreContext context) : base(context)
        {

        }
    }
}