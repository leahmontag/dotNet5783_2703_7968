
using static BO.Enums;

namespace BO;

public class OrderTrackingDates
{
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }

    public override string ToString() => $@"
    Date:{Date}
    Status: {Status}
    ";
}
