using static BO.Enums;
namespace BO;

/// <summary>
/// class Order
/// </summary>
public class Order
{
    /// <summary>
    /// uniqe id for order. 
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// uniqe Customer name for order. 
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// uniqe Customer email for order. 
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// uniqe Customer adress for order. 
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// Status for order. 
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Items for order. 
    /// </summary>
    public List<OrderItem> Items { get; set; }

    /// <summary>
    /// TotalPrice for order. 
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// uniqe Order date for order. 
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// uniqe Ship date for order. 
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// uniqe Delivery date for order. 
    /// </summary>
    public DateTime DeliveryDate { get; set; }

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
        $@"Order ID:{ID}
        customer name: {CustomerName}
        customerEmail: {CustomerEmail}
        customerAddress: {CustomerAdress}
        Order Date: {OrderDate.ToShortDateString()}
        Status:{Status}
        Ship Date: {ShipDate.ToShortDateString()}
        DeliveryDate: {DeliveryDate.ToShortDateString()}
        order items list:
        {orderItems}
        TotalPrice: {TotalPrice}");
    }
}
