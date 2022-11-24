
using static BO.Enums;

namespace BO;

public class OrderTrackingDates
{
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public override string ToString() => $@"
    Date:{Date}
    Description: {Description}
    ";
}
