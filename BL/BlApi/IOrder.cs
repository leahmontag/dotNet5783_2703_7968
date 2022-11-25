using BO;
namespace BlApi;

/// <summary>
/// interface IOrder
/// </summary>
public interface IOrder
{
    /// <summary>
    /// func GetAll
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<OrderForList> GetAll();

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order</returns>
    public Order Get(int ID);

    /// <summary>
    /// func UpdateShip
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>void</returns>
    public BO.Order UpdateShip(int ID);

    /// <summary>
    /// func UpdateDelivery
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>void</returns>
    public BO.Order UpdateDelivery(int ID);

    /// <summary>
    /// func TrackingOfOrder
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>void</returns>
    public OrderTracking TrackingOfOrder(int ID);

    /// <summary>
    /// func UpdateOrder
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>void</returns>
    public BO.Order UpdateOrder(int orderId, int orderItemId, string whatToDO, int Amount, BO.OrderItem newOrderItem);
}
