using BO;
namespace BlApi;

/// <summary>
/// interface IOrder
/// </summary>
public interface IOrder
{
    /// <summary>
    /// func UpdateShip
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void UpdateShip(Order val);

    /// <summary>
    /// func UpdateDelivery
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void UpdateDelivery(Order val);

    /// <summary>
    /// func UpdateOrder
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void UpdateOrder(Order val);

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>order</returns>
    public Order Get(int val);

    /// <summary>
    /// func GetAll
    /// </summary>
    /// <param name="val"></param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Order> GetAll();
}
