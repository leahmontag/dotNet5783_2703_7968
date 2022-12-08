using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    bool exisOrderItemID(int checkID);
}
