namespace BO;

/// <summary>
///  class Cart
/// </summary>
public class Cart
{
    /// <summary>
    /// customer name for Cart.
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// customer email for Cart.
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// customer address for Cart.
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// list of order items for Cart.
    /// </summary>
    public List<OrderItem?> Items { get; set; }

    /// <summary>
    ///  total price for Cart.
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string orderItems = "";
        foreach (OrderItem item in Items)
        {
            orderItems += item;
        }
        return (
        $@"
        customer name: {CustomerName}
        customerEmail: {CustomerEmail}
        customerAddress: {CustomerAddress}
        order items list:
        {orderItems}
        TotalPrice: {TotalPrice}");
    }


}
