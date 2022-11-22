using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    bool exisOrderItemID(int checkID);
    OrderItem GetByProductIDAndOrderID(int orderItId, int productId);
    IEnumerable<OrderItem> GetOrderItemsByOrderID(int orderItId);
}
