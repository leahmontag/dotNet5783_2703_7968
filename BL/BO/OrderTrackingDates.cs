namespace BO;

/// <summary>
/// class OrderTrakingDates
/// </summary>
public class OrderTrackingDates
{
    /// <summary>
    /// date for OrderTrackingDates.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Description for OrderTrackingDates.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Date:{Date}
    Description: {Description}
    ";
}
