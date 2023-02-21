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
    public IEnumerable<BO.OrderForList?> GetAll(Func<DO.Order?, bool>? d = null);

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order</returns>
    public BO.Order Get(Func<DO.Order?, bool>? d);

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
    public BO.OrderTracking TrackingOfOrder(int ID);

    /// <summary>
    /// update order by manager
    /// </summary>
    /// <param name="BOorder"></param>
    /// <param name="ID"></param>
    /// <param name="whatToDO"></param>
    /// <param name="Amount"></param>
    /// <returns></returns>
    public BO.Order UpdateOrder(BO.Order BOorder, int ID, string whatToDO, int Amount);

    /// <summary>
    /// Selecting an order for treatment
    /// </summary>
    /// <returns></returns>
    public int? SelectingOrderForTreatment();

}
