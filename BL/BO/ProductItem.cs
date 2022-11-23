using static BO.Enums;
namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int Amount { get; set; }
    public int InStock { get; set; }


    public override string ToString() => $@"
    Product ID:{ID}
    Name:{Name}
    Price: {Price}
    category:{Category}
    Amount:{Amount}
    InStock: {InStock}
    ";
}
