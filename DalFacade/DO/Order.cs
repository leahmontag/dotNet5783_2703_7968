namespace DO;

/// <summary>
/// structure for order. 
/// </summary>
    public struct Order
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

    public override string ToString() => $@"
    Order ID={ID},
    Customer Name:{CustomerName},
    Customer Email: {CustomerEmail}
    CustomerAdress: {CustomerAdress}
    Order Date: {OrderDate}
    Ship Date: {ShipDate}
    DeliveryDate: {DeliveryDate}
    ";
}
