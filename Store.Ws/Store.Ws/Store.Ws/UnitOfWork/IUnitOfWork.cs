using StoreWS.Repository;

namespace StoreWS.UnitOfWork
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IOrderItemRepository OrderItemRepository { get; set; }

        void Commit();
    }
}