// See https://aka.ms/new-console-template for more information
using DalApi;
using DO;
using DalList;
using static DO.Enums;
namespace Dal;

/// <summary>
/// class Program.
/// </summary>
internal class Program
{
    private static readonly IDal? _dal = DalApi.Factory.Get();
    /// <summary>
    /// public void main.
    /// </summary>
    public static void Main()
    {
        try
        {
            Choice yourChoice;
            do
            {
                Console.WriteLine("enter your choice:" + "\n0-exit" + "\n1-product" + "\n2-order" + "\n3-order items");
                Enum.TryParse(Console.ReadLine(), out yourChoice);
                switch (yourChoice)
                {
                    case Choice.exit:
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
            } while (yourChoice != Choice.exit);
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc);
        }
    }
    /// <summary>
    /// product function.
    /// </summary>
        #region ProductFunctions
    public static void ProductFunction()
    {
        Crud yourCrud;
        string numId;
        int num;
        Console.WriteLine("enter your choice:" + "\n1- add a new product" + "\n2- get product" + "\n3- get all products" + "\n4- update product" + "\n5- delete product");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case Crud.create:
                #region add product
                Console.WriteLine("enter your product iteam:");
                Product myProductToAdd = new Product();
                Console.WriteLine("name");
                myProductToAdd.Name = Console.ReadLine();
                Console.WriteLine("price");
                myProductToAdd.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("InStock");
                myProductToAdd.InStock = int.Parse(Console.ReadLine());
                int productID = _dal.Product.Create(myProductToAdd);
                Console.WriteLine("id:" + productID + "\n");
                #endregion
                break;
            case Crud.get:
                #region get product
                Console.WriteLine("enter id of product ");
                numId = Console.ReadLine();
                num = Convert.ToInt32(numId);
                Console.WriteLine(_dal?.Product.Get(x=>x?.ID==num));
                #endregion
                break;
            case Crud.getAll:
                #region getAll product
                IEnumerable<Product?> printProducts = _dal.Product.GetAll();
                foreach (Product i in printProducts)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case Crud.update:
                #region Update product
                Console.WriteLine("enter your product id:");
                int checkID = int.Parse(Console.ReadLine());
                Product myProductToUpdate = new Product();
             //   myProductToUpdate = _dal.Product.existProductID(checkID);
                if (myProductToUpdate.ID == 0)
                    throw new Exception("not exist product id");
                Console.WriteLine(myProductToUpdate);
                Console.WriteLine("enter your product iteams:");
                Console.WriteLine("name:");
                myProductToUpdate.Name = Console.ReadLine();
                Console.WriteLine("price:");
                myProductToUpdate.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("instock:");
                myProductToUpdate.InStock = int.Parse(Console.ReadLine());
                _dal.Product.Update(myProductToUpdate);
                #endregion
                break;
            case Crud.delete:
                #region Delete product
                Console.WriteLine("enter product id:");
                int productId = int.Parse(Console.ReadLine());
                _dal.Product.Delete(productId);
                #endregion
                break;
        }
    }
    #endregion

    /// <summary>
    /// order function.
    /// </summary>
    #region OrderFunctions
    public static void OrderFunction()
    {
        Crud yourCrud;
        Console.WriteLine("enter your choice:" + "\n1- add a new order" + "\n2- get an order" + "\n3- get all orders" + "\n4- update order" + "\n5- delete order");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case Crud.create:
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
                Console.WriteLine("OrderDate");
                DateTime.TryParse(Console.ReadLine(), out dateResult);
                myOrderToAdd.OrderDate = dateResult;
                Console.WriteLine("ShipDate");
                DateTime.TryParse(Console.ReadLine(), out dateResult);
                myOrderToAdd.ShipDate = dateResult;
                Console.WriteLine("DeliveryDate");
                DateTime.TryParse(Console.ReadLine(), out dateResult);
                myOrderToAdd.DeliveryDate = dateResult;
                int orderID = _dal.Order.Create(myOrderToAdd);
                Console.WriteLine("id:" + orderID + "\n");

                #endregion
                break;
            case Crud.get:
                #region get order
                Console.WriteLine("enter your order id: ");
                int orderId = int.Parse(Console.ReadLine());
                Console.WriteLine(_dal?.Order.Get(x=>x?.ID== orderId));
                #endregion
                break;
            case Crud.getAll:
                #region getAll order
                IEnumerable<Order?> printOrders = _dal.Order.GetAll();
                foreach (Order i in printOrders)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case Crud.update:
                #region Update order
                Console.WriteLine("enter your order id:");
                int checkID = int.Parse(Console.ReadLine());
             //   if (_dal?.Order.exisOrderID(checkID) == false)
                 //   throw new Exception("not exist order id");
                Order myOrderToUpdate = new Order();
                myOrderToUpdate.ID = checkID;
                Console.WriteLine("enter your order items:");
                Console.WriteLine("name:");
                myOrderToUpdate.CustomerName = Console.ReadLine();
                Console.WriteLine("email:");
                myOrderToUpdate.CustomerEmail = Console.ReadLine();
                Console.WriteLine("address:");
                myOrderToUpdate.CustomerAdress = Console.ReadLine();
                _dal?.Order.Update(myOrderToUpdate);
                #endregion
                break;
            case Crud.delete:
                #region Delete order
                Console.WriteLine("enter your order id: ");
                orderId = int.Parse(Console.ReadLine());
                _dal?.Order.Delete(orderId);
                #endregion
                break;
        }
    }
    #endregion

    /// <summary>
    /// order item function.
    /// </summary>
    #region OrderItemFunctions
    public static void OrderItemFunction()
    {
        Crud yourCrud;
        Console.WriteLine("enter your choice:" + "\n1- add a new order item" + "\n2- get an order item by order item ID" + "\n3- get all orders items" + "\n4- update order item" + "\n5- delete order item" + "\n6- Get order item by Product ID and Order ID" + "\n7- Get Order Items By Order ID");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case Crud.create:
                #region add orderItem         
                Console.WriteLine(" enter your order items:");
                OrderItem myOrderItemToAdd = new OrderItem();
                Console.WriteLine(" OrderItemID:");
                myOrderItemToAdd.OrderItemID = int.Parse(Console.ReadLine());
                Console.WriteLine(" ProductID:");
                myOrderItemToAdd.ProductID = int.Parse(Console.ReadLine());
                //Console.WriteLine(" OrderID:");
                //myOrderItemToAdd.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine(" Price:");
                myOrderItemToAdd.Price = double.Parse(Console.ReadLine());
                Console.WriteLine(" Amount:");
                myOrderItemToAdd.Amount = int.Parse(Console.ReadLine());
                Console.WriteLine(" Name:");
                myOrderItemToAdd.Name = Console.ReadLine();
                int orderItemID = _dal.OrderItem.Create(myOrderItemToAdd);
                Console.WriteLine("id:" + orderItemID + "\n");
                #endregion
                break;
            case Crud.get:
                #region get orderItem
                Console.WriteLine("enter your order item id: ");
                int orderItemId = int.Parse(Console.ReadLine());
                Console.WriteLine(_dal?.OrderItem.Get(x=>x?.OrderItemID==orderItemId));
                #endregion
                break;
            case Crud.getAll:
                #region getAll orderItem
                IEnumerable<OrderItem?> printOrdersItems = _dal.OrderItem.GetAll();
                foreach (OrderItem i in printOrdersItems)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case Crud.update:
                #region Update OrderItem
                Console.WriteLine("enter your order item id:");
                int checkID = int.Parse(Console.ReadLine());
               // if (_dal.OrderItem.exisOrderItemID(checkID) == false)
                //    throw new Exception("not exist order item id");
                OrderItem myOrderItemToUpdate = new OrderItem();
                myOrderItemToUpdate.OrderItemID = checkID;
                Console.WriteLine(" enter your order items:");
                Console.WriteLine(" ProductID:");
                myOrderItemToUpdate.ProductID = int.Parse(Console.ReadLine());
                //Console.WriteLine("OrderID:");
                //myOrderItemToUpdate.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine("Price:");
                myOrderItemToUpdate.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Amount:");
                myOrderItemToUpdate.Amount = int.Parse(Console.ReadLine());
                _dal.OrderItem.Update(myOrderItemToUpdate);
                #endregion
                break;
            case Crud.delete:
                #region Delete orderItem
                Console.WriteLine("enter your product id: ");
                int orderItId = int.Parse(Console.ReadLine());
                _dal.OrderItem.Delete(orderItId);
                #endregion
                break;
            case Crud.GetByProductIDAndOrderID:
                #region Get orderitem by ProductID and OrderID 
                Console.WriteLine("enter your order id: ");
                orderItId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter your product id: ");
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine(_dal?.OrderItem.Get(x=>x?.ProductID==productId));
                #endregion
                break;
            case Crud.GetOrderItemsByOrderID:
                #region Get Order Items By Order ID
                Console.WriteLine("enter your order id: ");
                orderItId = int.Parse(Console.ReadLine());
                printOrdersItems= _dal.OrderItem.GetAll(x=>x?.OrderItemID == orderItId);
                foreach (OrderItem i in printOrdersItems)
                {
                    if (i.OrderItemID != 0)
                        Console.WriteLine(i);
                }
                #endregion
                break;
        }

    }
    #endregion
}
