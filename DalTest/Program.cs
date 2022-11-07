// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using DO;
//using Dal;
//namespace DalTest;
//using static Dal.DalProduct;
namespace Dal;

internal class Program
{
    private static DalProduct _dalProduct = new DalProduct();
    private static DalOrder _dalOrder = new DalOrder();
    private static DalOrderItem _dalOrderItem = new DalOrderItem();
    
    private static void Main()
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
                        Product myProductToAdd = new Product();
                        myProductToAdd.ID = Console.Read();
                        myProductToAdd.Name = Console.ReadLine();
                        myProductToAdd.Price = Console.Read();
                        myProductToAdd.InStock = Console.Read();
                        int productID = _dalProduct.Create(myProductToAdd);
                        Console.WriteLine("קוד המוצר הינו:" + productID);
                        #endregion
                        break;
                    case "b":
                        #region get product
                        Console.WriteLine("הכנס מספר מזהה של המוצר ");
                        int num = Console.Read();
                        Console.WriteLine(_dalProduct.Get(num));
                        #endregion
                        break;
                    case "c":
                        Console.WriteLine(_dalProduct.GetAll());
                        break;
                    case "d":
                        #region Update product
                        Console.WriteLine("הזן עדכון פרטי המוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        Product myProductToUpdate = new Product();
                        myProductToUpdate.ID = Console.Read();
                        myProductToUpdate.Name = Console.ReadLine();
                        myProductToUpdate.Price = Console.Read();
                        myProductToUpdate.InStock = Console.Read();
                        _dalProduct.Update(myProductToUpdate);
                        #endregion
                        break;
                    case "e":
                        #region Delete
                        Console.WriteLine("הכנס מספר מזהה של המוצר ");
                        int ProductId = Console.Read();
                        _dalProduct.Delete(ProductId);
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
                        Order myOrderToAdd = new Order();
                        myOrderToAdd.ID = Console.Read();
                        myOrderToAdd.CustomerName = Console.ReadLine();
                        myOrderToAdd.CustomerEmail = Console.ReadLine();
                        myOrderToAdd.CustomerAdress = Console.ReadLine();
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrderToAdd.OrderDate = d;
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrderToAdd.ShipDate = d;
                        DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        myOrderToAdd.DeliveryDate = d;//צריך להמיר גם מספרים?

                        int productID = _dalOrder.Create(myOrderToAdd);
                        Console.WriteLine("קוד המוצר הינו:" + productID);
                        #endregion
                        break;
                    case "b":
                        #region get order
                        Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                        int OrderId = Console.Read();
                        Console.WriteLine(_dalOrder.Get(OrderId));
                        #endregion
                        break;
                    case "c":
                        //foreach???????????????????
                        Console.WriteLine(_dalOrder.GetAll());
                        break;
                    case "d":
                        #region Update order
                        Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        Order myOrderToUpdate = new Order();
                        myOrderToUpdate.ID = Console.Read();
                        myOrderToUpdate.CustomerName = Console.ReadLine();
                        myOrderToUpdate.CustomerEmail = Console.ReadLine();
                        myOrderToUpdate.CustomerAdress = Console.ReadLine();
                        _dalOrder.Update(myOrderToUpdate);
                        #endregion
                        break;
                    case "e":
                        #region Delete order
                        Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                        int orderId = Console.Read();
                        _dalOrder.Delete(orderId);
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
                        OrderItem myOrderItemToAdd = new OrderItem();
                        myOrderItemToAdd.OrderItemID = Console.Read();
                        myOrderItemToAdd.ProductID = Console.Read();
                        myOrderItemToAdd.OrderID = Console.Read();
                        myOrderItemToAdd.Price = Console.Read();
                        myOrderItemToAdd.Amount = Console.Read();
                        int OrderItemID = _dalOrderItem.Create(myOrderItemToAdd);
                        Console.WriteLine("קוד הזמנת מוצר הינו:" + OrderItemID);
                        #endregion
                        break;
                    case "b":
                        #region get orderItem
                        Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                        int orderItemId = Console.Read();
                        Console.WriteLine(_dalOrderItem.Get(orderItemId));
                        #endregion
                        break;
                    case "c":
                        Console.WriteLine(_dalOrderItem.GetAll());
                        break;
                    case "d":
                        #region Update OrderItem
                        Console.WriteLine("הזן פרטי הזמנת מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");
                        OrderItem myOrderItemToUpdate = new OrderItem();
                        myOrderItemToUpdate.OrderItemID = Console.Read();
                        myOrderItemToUpdate.ProductID = Console.Read();
                        myOrderItemToUpdate.OrderID = Console.Read();
                        myOrderItemToUpdate.Price = Console.Read();
                        myOrderItemToUpdate.Amount = Console.Read();
                        _dalOrderItem.Update(myOrderItemToUpdate);
                        #endregion
                        break;
                    case "e":
                        #region Delete orderItem
                        Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                        int orderItId = Console.Read();
                        _dalOrderItem.Delete(orderItId);
                        #endregion
                        break;
                }
                #endregion
                break;
        }
    }
}

