// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using DO;
using Dal;
//namespace DalTest;
//using static Dal.DalProduct;
//namespace Dal;

public class Program
{
    private Product myProduct = new Product();
    private Order myOrder = new Order();
    private Product MyProduct = new Product();

    private OrderItem myOrderItem = new OrderItem();
    
    void Main()
    {
        Console.WriteLine("enter your choice:" +
            "0 to exit" +
            "1 -product" +
            "2 -order" + 
            "3 -order items");
        int choice = Console.Read();
        String yourChoice;
        switch (choice)
        {
            case 0:
                break;
            case 1://product
                #region product
                Console.WriteLine("הכנס בחירה ");
                yourChoice = Console.ReadLine();
                //DalProduct product = new DalProduct();
                switch (yourChoice)
                {
                    case "a":
                        #region add product
                        Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        MyProduct.ID = Console.Read();
                        MyProduct.Name = Console.ReadLine();
                        MyProduct.Price = Console.Read();
                        MyProduct.InStock = Console.Read();
                        int productID = DalProduct.Create(MyProduct);
                        Console.WriteLine("קוד המוצר הינו:" + productID);
                        #endregion
                        break;
                    case "b":
                        #region get product
                        Console.WriteLine("הכנס מספר מזהה של המוצר ");
                        int num = Console.Read();
                        Console.WriteLine(Product.Get(num));
                        #endregion
                        break;
                    case "c":
                        Console.WriteLine(DalProduct.GetAll());
                        break;
                    case "d":
                        #region Update product
                        Console.WriteLine("הזן עדכון פרטי המוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        Product myProduct = new Product();
                        myProduct.ID = Console.Read();
                        myProduct.Name = Console.ReadLine();
                        myProduct.Price = Console.Read();
                        myProduct.InStock = Console.Read();
                        DalProduct.Update(myProduct);
                        #endregion
                        break;
                    case "e":
                        #region Delete
                        Console.WriteLine("הכנס מספר מזהה של המוצר ");
                        int ProductId = Console.Read();
                        DalProduct.Delete(ProductId);
                        #endregion
                        break;
                }
                #endregion
                break;
            case 2://order
                #region order
                yourChoice = Console.ReadLine();
                switch (yourChoice)
                {
                    case "a":
                        #region add order
                        Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        myOrder.ID = Console.Read();
                        myOrder.CustomerName = Console.ReadLine();
                        myOrder.CustomerEmail = Console.ReadLine();
                        myOrder.CustomerAdress = Console.ReadLine();
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrder.OrderDate = d;
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrder.ShipDate = d;
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrder.DeliveryDate = d;//צריך להמיר גם מספרים?

                        int productID = Order.Create(myOrder);
                        Console.WriteLine("קוד המוצר הינו:" + productID);
                        #endregion
                        break;
                    case "b":
                        #region get order
                        Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                        int OrderId = Console.Read();
                        Console.WriteLine(Order.Get(OrderId));
                        #endregion
                        break;
                    case "c":
                        //foreach???????????????????
                        Console.WriteLine(Order.GetAll());
                        break;
                    case "d":
                        #region Update order
                        Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        myOrder.ID = Console.Read();
                        myOrder.CustomerName = Console.ReadLine();
                        myOrder.CustomerEmail = Console.ReadLine();
                        myOrder.CustomerAdress = Console.ReadLine();
                        Order.Update(myOrder);
                        #endregion
                        break;
                    case "e":
                        #region Delete order
                        Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                        int orderId = Console.Read();
                        Order.Delete(orderId);
                        #endregion
                        break;
                }
                #endregion
                break;
            case 3://order item
                #region order item
                yourChoice = Console.ReadLine();
                switch (yourChoice)
                {
                    case "a":
                        #region add orderItem
                        Console.WriteLine("הזן פרטי הזמנת מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        myOrderItem.OrderItemID = Console.Read();
                        myOrderItem.ProductID = Console.Read();
                        myOrderItem.OrderID = Console.Read();
                        myOrderItem.Price = Console.Read();
                        myOrderItem.Amount = Console.Read();
                        int OrderItemID = OrderItem.Create(myOrderItem);
                        Console.WriteLine("קוד הזמנת מוצר הינו:" + OrderItemID);
                        #endregion
                        break;
                    case "b":
                        #region get orderItem
                        Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                        int orderItemId = Console.Read();
                        Console.WriteLine(OrderItem.Get(orderItemId));
                        #endregion
                        break;
                    case "c":
                        Console.WriteLine(OrderItem.GetAll());
                        break;
                    case "d":
                        #region Update OrderItem
                        Console.WriteLine("הזן פרטי הזמנת מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        myOrderItem.OrderItemID = Console.Read();
                        myOrderItem.ProductID = Console.Read();
                        myOrderItem.OrderID = Console.Read();
                        myOrderItem.Price = Console.Read();
                        myOrderItem.Amount = Console.Read();
                        OrderItem.Update(myOrderItem);
                        #endregion
                        break;
                    case "e":
                        #region Delete orderItem
                        Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                        int orderItId = Console.Read();
                        OrderItem.Delete(orderItId);
                        #endregion
                        break;
                }
                #endregion
                break;
        }
    }
}

