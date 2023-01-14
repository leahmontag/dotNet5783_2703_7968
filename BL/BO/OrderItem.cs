namespace BO;

/// <summary>
/// class OrderItem
/// </summary>
public class OrderItem
{
    /// <summary>
    ///Name of order item for order item. 
    /// </summary>
    public string? Name { get; set; }

    ///// <summary>
    ///// uniqe id of order item for order item. 
    ///// </summary>
    //public int OrderItemID { get; set; }

    /// <summary>
    /// uniqe id of product for order item. 
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// uniqe double price for order item. 
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// uniqe id for order item. 
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// TotalPrice for order item. 
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Name: {Name}
    Product ID: {ProductID}
    Price: {Price}
    Amount in order: {Amount}
    Total Price: {TotalPrice}
    ";
}
