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
    /// <param name="val"></param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<OrderForList> GetAll();


    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>order</returns>
    public Order Get(int val);


    /// <summary>
    /// func UpdateShip
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public BO.Order UpdateShip(int val);

    /// <summary>
    /// func UpdateDelivery
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public BO.Order UpdateDelivery(int val);

    /// <summary>
    /// func TrackingOfOrder
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public OrderTracking TrackingOfOrder(int val);





    /// <summary>
    /// func UpdateOrder
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void UpdateOrder(Order val);
}
