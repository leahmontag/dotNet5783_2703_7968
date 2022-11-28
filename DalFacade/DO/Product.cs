using static DO.Enums;
namespace DO;

/// <summary>
/// structure for Product
/// </summary>
public struct Product
{
    /// <summary>
    /// uniqe id for product. 
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// uniqe name for product. 
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// uniqe price for product. 
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// uniqe enum category for product. 
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// uniqe inStock for product. 
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
    Product ID={ID}: {Name}, 
    category - {Category}
    Price: {Price}
    Amount in stock: {InStock}
    ";

}
