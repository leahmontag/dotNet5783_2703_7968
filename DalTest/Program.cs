// See https://aka.ms/new-console-template for more information
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using DO;
using static DO.Enums;

namespace Dal;

internal class Program
{
    private static DalProduct _dalProduct = new DalProduct();
    private static DalOrder _dalOrder = new DalOrder();
    private static DalOrderItem _dalOrderItem = new DalOrderItem();

    public static void Main()
    {
        DataSource.CallData();
        Choice t;
        do
        {
            Choice yourChoice;
            Console.WriteLine("enter your choice:" + "0 to exit" + "1 -product" + "2 -order" + "3 -order items");
            Enum.TryParse(Console.ReadLine(), out yourChoice);
            Console.Write("yourChoice: " + yourChoice);
            switch (yourChoice)
            {
                case Choice.exit:
                    Console.Write("exit????????????? ");
                    break;
                case Choice.product:
                    ProductFunction();//product
                    break;
                case Choice.order:
                    OrderFunction();//order
                    break;
                case Choice.orderItem:
                    OrderItemFunction();//order item
                    break;
            }
            t = yourChoice;
        } while (t != Choice.exit);

    }
    #region ProductFunction
    public static void ProductFunction()
    {
        crud yourCrud;
        string numId;
        int num;
        Console.WriteLine("enter your choice");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case crud.create:
                #region add product
                Console.WriteLine("enter your product iteam:");
                Product myProductToAdd = new Product();
                Console.WriteLine("name");
                myProductToAdd.Name = Console.ReadLine();
                Console.WriteLine("price");
                myProductToAdd.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("InStock");
                myProductToAdd.InStock = int.Parse(Console.ReadLine());
                int productID = _dalProduct.Create(myProductToAdd);
                Console.WriteLine("id:" + productID);
                #endregion
                break;
            case crud.get:
                #region get product
                Console.WriteLine("enter id of product ");
                numId = Console.ReadLine();
                num = Convert.ToInt32(numId);
                Console.WriteLine("num" + num);
                Product p = _dalProduct.Get(num);
                Console.WriteLine(p.ID);
                #endregion
                break;
            case crud.getAll:
                #region getAll product
                Product[] PrintProducts = _dalProduct.GetAll();
                foreach (Product i in PrintProducts)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case crud.update:
                #region Update product
                Console.WriteLine("enter your product iteam:");
                Product myProductToUpdate = new Product();
                Console.WriteLine("id");
                myProductToUpdate.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("name");
                myProductToUpdate.Name = Console.ReadLine();
                Console.WriteLine("price");
                myProductToUpdate.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("instock");
                myProductToUpdate.InStock = int.Parse(Console.ReadLine());
                _dalProduct.Update(myProductToUpdate);
                #endregion
                break;
            case crud.delete:
                #region Delete product
                Console.WriteLine("enter product id:");
                int ProductId = int.Parse(Console.ReadLine());
                _dalProduct.Delete(ProductId);
                #endregion
                break;
        }
    }
    #endregion

    #region OrderFunction
    public static void OrderFunction()
    {
        crud yourCrud;
        Console.WriteLine("enter your choice");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case crud.create:
                #region add order
                Console.WriteLine(" enter your order items:");
                Order myOrderToAdd = new Order();
                Console.WriteLine("id");
                myOrderToAdd.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("name");
                myOrderToAdd.CustomerName = Console.ReadLine();
                Console.WriteLine("email");
                myOrderToAdd.CustomerEmail = Console.ReadLine();
                Console.WriteLine("address");
                myOrderToAdd.CustomerAdress = Console.ReadLine();
                DateTime dateResult;
                DateTime.TryParse(Console.ReadLine(), out dateResult);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                myOrderToAdd.OrderDate = dateResult;
                DateTime.TryParse(Console.ReadLine(), out dateResult);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                myOrderToAdd.ShipDate = dateResult;
                DateTime.TryParse(Console.ReadLine(), out dateResult);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                myOrderToAdd.DeliveryDate = dateResult;//צריך להמיר גם מספרים?
                int orderID = _dalOrder.Create(myOrderToAdd);
                Console.WriteLine("id:" + orderID);
                #endregion
                break;
            case crud.get:
                #region get order
                Console.WriteLine("enter your order id: ");
                int OrderId = int.Parse(Console.ReadLine());
                Console.WriteLine(_dalOrder.Get(OrderId));
                #endregion
                break;
            case crud.getAll:
                #region getAll order
                Order[] PrintOrders = _dalOrder.GetAll();
                foreach (Order i in PrintOrders)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case crud.update:
                #region Update order
                Console.WriteLine(" enter your order items:");
                Order myOrderToUpdate = new Order();
                Console.WriteLine(" id:");
                myOrderToUpdate.ID = int.Parse(Console.ReadLine());
                Console.WriteLine(" name:");
                myOrderToUpdate.CustomerName = Console.ReadLine();
                Console.WriteLine(" email:");
                myOrderToUpdate.CustomerEmail = Console.ReadLine();
                Console.WriteLine("address:");
                myOrderToUpdate.CustomerAdress = Console.ReadLine();
                _dalOrder.Update(myOrderToUpdate);
                #endregion
                break;
            case crud.delete:
                #region Delete order
                Console.WriteLine("enter your order id: ");
                int orderId = int.Parse(Console.ReadLine());
                _dalOrder.Delete(orderId);
                #endregion
                break;
        }
    }
    #endregion

    #region OrderItemFunction
    public static void OrderItemFunction()
    {
        crud yourCrud;
        Console.WriteLine("enter your choice");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case crud.create:
                #region add orderItem         
                Console.WriteLine(" enter your order items:");
                OrderItem myOrderItemToAdd = new OrderItem();
                Console.WriteLine(" OrderItemID:");
                myOrderItemToAdd.OrderItemID = int.Parse(Console.ReadLine());
                Console.WriteLine(" ProductID:");
                myOrderItemToAdd.ProductID = int.Parse(Console.ReadLine());
                Console.WriteLine(" OrderID:");
                myOrderItemToAdd.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine(" Price:");
                myOrderItemToAdd.Price = double.Parse(Console.ReadLine());
                Console.WriteLine(" Amount:");
                myOrderItemToAdd.Amount = int.Parse(Console.ReadLine());
                int OrderItemID = _dalOrderItem.Create(myOrderItemToAdd);
                Console.WriteLine("id:" + OrderItemID);
                #endregion
                break;
            case crud.get:
                #region get orderItem
                Console.WriteLine("enter your product id: ");
                int orderItemId = int.Parse(Console.ReadLine());
                Console.WriteLine(_dalOrderItem.Get(orderItemId));
                #endregion
                break;
            case crud.getAll:
                #region getAll orderItem
                OrderItem[] PrintOrdersItems = _dalOrderItem.GetAll();
                foreach (OrderItem i in PrintOrdersItems)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case crud.update:
                #region Update OrderItem
                Console.WriteLine(" enter your order item items:");
                OrderItem myOrderItemToUpdate = new OrderItem();
                Console.WriteLine(" OrderItemID:");
                myOrderItemToUpdate.OrderItemID = int.Parse(Console.ReadLine());
                Console.WriteLine(" ProductID:");
                myOrderItemToUpdate.ProductID = int.Parse(Console.ReadLine());
                Console.WriteLine("OrderID:");
                myOrderItemToUpdate.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine("Price:");
                myOrderItemToUpdate.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Amount:");
                myOrderItemToUpdate.Amount = int.Parse(Console.ReadLine());
                _dalOrderItem.Update(myOrderItemToUpdate);
                #endregion
                break;
            case crud.delete:
                #region Delete orderItem
                Console.WriteLine("enter your product id: ");
                int orderItId = int.Parse(Console.ReadLine());
                _dalOrderItem.Delete(orderItId);
                #endregion
                break;
        }

    }
    #endregion

}



