using static BO.Enums;
namespace BO;

/// <summary>
/// class ProductForList
/// </summary>
public class ProductForList
{
    /// <summary>
    /// uniqe id for ProductForList.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name for ProductForList.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// price for ProductForList.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// category for ProductForList.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// function toString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $@"
    Product ID:{ID}
    Name:{Name}
    Price: {Price}
    category:{Category}
    ";
}
