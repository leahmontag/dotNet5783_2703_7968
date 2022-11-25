using static BO.Enums;
namespace BO;

/// <summary>
/// class Product
/// </summary>
public class Product
{
    /// <summary>
    /// uniqe id for product. 
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// uniqe name for product. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// uniqe price for product. 
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// uniqe enum category for product. 
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// uniqe inStock for product. 
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Product name: {Name}
    Product ID:{ID}: 
    category:{Category}
    Price: {Price}
    Amount in stock: {InStock}
    ";
}
