using static BO.Enums;
namespace BO;

/// <summary>
/// class OrderTracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// uniqe id for OrderTracking.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// status for OrderTracking.
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// list of OrderTrackingDates for OrderTracking.
    /// </summary>
    public List<OrderTrackingDates?> OrderTrackingDateAndDesc { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string orderItems = "";
        foreach (OrderTrackingDates /*?*/ item in OrderTrackingDateAndDesc)
        {
            orderItems += item;
        }
        return (
        $@"ID: {ID}
        Status: {Status}
        Order Tracking Status:
        {orderItems}");
    }
}
