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
        bool Flag = true;
        Choice yourChoice;
        do
        {
            Console.WriteLine("enter your choice:" + "0 to exit" + "1 -product" + "2 -order" + "3 -order items");
            Enum.TryParse(Console.ReadLine(), out yourChoice);
            Flag = true;
            switch (yourChoice)
            {
                case Choice.exit:
                    Flag = false;
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
        } while (Flag);

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
                myProductToAdd.Price = Console.Read();
                Console.WriteLine("InStock");
                myProductToAdd.InStock = Console.Read();
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
                    Console.WriteLine("in c");
                }
                #endregion
                break;
            case crud.update:
                #region Update product
                Console.WriteLine("enter your product iteam:");
                Product myProductToUpdate = new Product();
                Console.WriteLine("id");
                myProductToUpdate.ID = Console.Read();
                Console.WriteLine("name");
                myProductToUpdate.Name = Console.ReadLine();
                Console.WriteLine("price");
                myProductToUpdate.Price = Console.Read();
                Console.WriteLine("instock");
                myProductToUpdate.InStock = Console.Read();
                _dalProduct.Update(myProductToUpdate);
                #endregion
                break;
            case crud.delete:
                #region Delete product
                Console.WriteLine("enter product id:");
                int ProductId = Console.Read();
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
                myOrderToAdd.ID = Console.Read();
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
                int OrderId = Console.Read();
                Console.WriteLine(_dalOrder.Get(OrderId));
                #endregion
                break;
            case crud.getAll:
                #region getAll order
                //foreach???????????????????
                Console.WriteLine(_dalOrder.GetAll());
                #endregion
                break;
            case crud.update:
                #region Update order
                Console.WriteLine(" enter your order items:");
                Order myOrderToUpdate = new Order();
                Console.WriteLine(" id:");
                myOrderToUpdate.ID = Console.Read();
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
                int orderId = Console.Read();
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
                myOrderItemToAdd.OrderItemID = Console.Read();
                Console.WriteLine(" ProductID:");
                myOrderItemToAdd.ProductID = Console.Read();
                Console.WriteLine(" OrderID:");
                myOrderItemToAdd.OrderID = Console.Read();
                Console.WriteLine(" Price:");
                myOrderItemToAdd.Price = Console.Read();
                Console.WriteLine(" Amount:");
                myOrderItemToAdd.Amount = Console.Read();
                int OrderItemID = _dalOrderItem.Create(myOrderItemToAdd);
                Console.WriteLine("id:" + OrderItemID);
                #endregion
                break;
            case crud.get:
                #region get orderItem
                Console.WriteLine("enter your product id: ");
                int orderItemId = Console.Read();
                Console.WriteLine(_dalOrderItem.Get(orderItemId));
                #endregion
                break;
            case crud.getAll:
                #region getAll orderItem
                Console.WriteLine(_dalOrderItem.GetAll());
                #endregion
                break;
            case crud.update:
                #region Update OrderItem
                Console.WriteLine(" enter your order item items:");
                OrderItem myOrderItemToUpdate = new OrderItem();
                Console.WriteLine(" OrderItemID:");
                myOrderItemToUpdate.OrderItemID = Console.Read();
                Console.WriteLine(" ProductID:");
                myOrderItemToUpdate.ProductID = Console.Read();
                Console.WriteLine("OrderID:");
                myOrderItemToUpdate.OrderID = Console.Read();
                Console.WriteLine("Price:");
                myOrderItemToUpdate.Price = Console.Read();
                Console.WriteLine("Amount:");
                myOrderItemToUpdate.Amount = Console.Read();
                _dalOrderItem.Update(myOrderItemToUpdate);
                #endregion
                break;
            case crud.delete:
                #region Delete orderItem
                Console.WriteLine("enter your product id: ");
                int orderItId = Console.Read();
                _dalOrderItem.Delete(orderItId);
                #endregion
                break;
        }

    }
    #endregion

}



