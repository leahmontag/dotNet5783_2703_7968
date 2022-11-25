using static BO.Enums;
namespace BO;

/// <summary>
///  class ProductItem
/// </summary>
public class ProductItem
{
    /// <summary>
    /// uniqe id for ProductItem.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// uniqe name for ProductItem.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// uniqe price for ProductItem.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// category for ProductItem.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// uniqe amount for ProductItem.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///if ProductItem in stock.
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Product ID:{ID}
    Name:{Name}
    Price: {Price}
    category:{Category}
    Amount:{Amount}
    InStock: {InStock}
    ";
}
