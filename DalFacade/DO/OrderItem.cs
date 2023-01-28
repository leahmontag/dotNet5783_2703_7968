namespace DO;

/// <summary>
/// structure for order item . 
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// uniqe id of order item for order item. 
    /// </summary>
    public int OrderItemID { get; set; }
    /// <summary>
    /// uniqe id of product for order item. 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// uniqe id of order for order item. 
    /// </summary>
    //  public int OrderID { get; set; }
    /// <summary>
    /// uniqe double price for order item. 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount from item 
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Name of item 
    /// </summary>
    public string? Name { get; set; }

    public override string ToString() => $@"
    OrderItem ID: {OrderItemID}
    Product ID: {ProductID}
    Price: {Price}
    Amount in order: {Amount}
    Name of item:{Name}
    ";
    // Order ID: {OrderID}
}
