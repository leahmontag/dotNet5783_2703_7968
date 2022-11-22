using DO;
namespace DalApi;

public interface IOrder : ICrud<Order>
{
    bool exisOrderID(int checkID);
}
