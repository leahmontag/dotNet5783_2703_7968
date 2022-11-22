

using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Customer Name:{CustomerName}
    Customer Email:{CustomerEmail}
    Customer Address:{CustomerAddress}
    Items: {Items}
    Total Price: {TotalPrice}
    ";
}
