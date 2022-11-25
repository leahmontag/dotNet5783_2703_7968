using static BO.Enums;
namespace BO;

/// <summary>
/// class OrderForList
/// </summary>
public class OrderForList
{
    /// <summary>
    ///uniqe id for OrderForList. 
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    ///customer name for OrderForList. 
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    ///status for OrderForList. 
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    ///amount of items for OrderForList. 
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    ///total price for OrderForList. 
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Product ID:{ID}
    Customer Name:{CustomerName}
    Status:{Status}
    AmountOfItems: {AmountOfItems}
    TotalPrice:{TotalPrice}
    ";
}
