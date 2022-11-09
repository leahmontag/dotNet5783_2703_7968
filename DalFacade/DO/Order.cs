namespace DO;

public struct Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
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
