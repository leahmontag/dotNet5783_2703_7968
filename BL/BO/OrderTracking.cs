using static BO.Enums;
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderTrackingDates> OrderTrackingDateAndDesc { get; set; }

    public override string ToString()
    {
        string orderItems = "";
        foreach (OrderTrackingDates item in OrderTrackingDateAndDesc)
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
