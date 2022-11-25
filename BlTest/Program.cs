using Amazon.DynamoDBv2.Model;
using BL;
using BlApi;
using BlImplementation;
using BO;
using DalApi;
using System;
using static BO.Enums;
namespace BlTest;



internal class Program
{
    private static readonly IBl _bl = new Bl();
    private static BO.Cart _cartBL = new BO.Cart();
    public static void Main()
    {

        try
        {
            Choice yourChoice;
            do
            {
                Console.WriteLine("enter your choice:" + "\n0-exit" + "\n1-product" + "\n2-order" + "\n3-cart ");
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
                    case Choice.cart:
                        CartFunction();//order item
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
        ProductEnum yourCrud;
        string numId;
        int num;
        Console.WriteLine("enter your choice:" + "\n1- get all product" + "\n2- get product by manager" + "\n3-  get products from catalog" + "\n4- add product" + "\n5- remove product" + "\n6- update product");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case ProductEnum.getAllProducts:
                #region get all products
                IEnumerable<ProductForList> printProductList = _bl.Product.GetAll();
                foreach (var i in printProductList)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case ProductEnum.getProductByManager:
                #region get by manager
                Console.WriteLine("enter id of product ");
                numId = Console.ReadLine();
                num = Convert.ToInt32(numId);
                Console.WriteLine(_bl.Product.GetByManager(num));
                #endregion
                break;
            case ProductEnum.getProductFromCatalog:
                #region get product from catalog
                Console.WriteLine("enter id of product ");
                numId = Console.ReadLine();
                num = Convert.ToInt32(numId);
                Console.WriteLine(_bl.Product.GetProductFromCatalog(num, _cartBL));
                #endregion
                break;
            case ProductEnum.addProduct:
                #region add product
                Console.WriteLine("enter your product iteam:");
                BO.Product myProductToAdd = new BO.Product();
                Console.WriteLine("ID");
                myProductToAdd.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("name");
                myProductToAdd.Name = Console.ReadLine();
                Console.WriteLine("price");
                myProductToAdd.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Category");
                //  myProductToAdd.Category = int.Parse(Console.ReadLine());//how to get this enum?
                Console.WriteLine("InStock");
                myProductToAdd.InStock = int.Parse(Console.ReadLine());
                int productID = _bl.Product.Create(myProductToAdd);
                Console.WriteLine("id:" + productID + "\n");
                #endregion
                break;
            case ProductEnum.removeProduct:
                #region remove product
                Console.WriteLine("enter product id:");
                int productId = int.Parse(Console.ReadLine());
                _bl.Product.Delete(productId);
                #endregion
                break;

            case ProductEnum.updateProduct:
                #region update product
                BO.Product myProductToUpdate = new BO.Product();
                Console.WriteLine("enter your product iteams:");
                Console.WriteLine("id:");
                myProductToUpdate.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("name:");
                myProductToUpdate.Name = Console.ReadLine();
                Console.WriteLine("price:");
                myProductToUpdate.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("category:");
                //  myProductToAdd.Category = int.Parse(Console.ReadLine());//how to get this enum?
                Console.WriteLine("instock:");
                myProductToUpdate.InStock = int.Parse(Console.ReadLine());
                _bl.Product.Update(myProductToUpdate);
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
        //להוסיף בונוס כאן של עדכון הזמנה להוסיף לאופציה שביעית
        OrderEnum yourOrderChoice;
        Console.WriteLine("enter your choice:" + "\n1- show all orders" + "\n2- get an order" + "" + "\n3- update ship order" + "\n4- update delivery order" + "\n5-order tracking ");
        Enum.TryParse(Console.ReadLine(), out yourOrderChoice);
        switch (yourOrderChoice)
        {
            case OrderEnum.getAllOrders:
                #region get all orders
                IEnumerable<OrderForList> printOrders = _bl.Order.GetAll();
                foreach (OrderForList i in printOrders)
                {
                    Console.WriteLine(i);
                }
                #endregion
                break;
            case OrderEnum.getOrder:
                #region get order
                Console.WriteLine("enter your order id: ");
                int orderId = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Order.Get(orderId));
                #endregion
                break;
            case OrderEnum.updateShip:
                #region update ship
                Console.WriteLine("enter your order id: ");
                int orderIdToUpdateShip = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Order.UpdateShip(orderIdToUpdateShip));
                #endregion
                break;
            case OrderEnum.updateDelivery:
                #region update delivery
                Console.WriteLine("enter your order id: ");
                int orderIdToUpdateDelivery = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Order.UpdateDelivery(orderIdToUpdateDelivery));
                #endregion
                break;
            case OrderEnum.trackingOfOrder:
                #region tracking of order
                Console.WriteLine("enter your order id: ");
                int orderIdToTracking = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Order.TrackingOfOrder(orderIdToTracking));
                #endregion
                break;
        }
    }
    #endregion

    /// <summary>
    /// Cart function.
    /// </summary>
    #region CartFunction
    public static void CartFunction()
    {
        CartEnum yourCrud;
        Cart cartBL = new Cart();
        Console.WriteLine("enter your choice:" + "\n1- update cart" + "\n2- confirm cart" + "\n3- add product to cart");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case CartEnum.updateCart:
                #region update cart
                Console.WriteLine("enter order item ID ");
                int OrderItemID = int.Parse(Console.ReadLine());

                Console.WriteLine("enter order  new amount ");
                int amount = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Cart.Update(_cartBL, OrderItemID, amount));
                #endregion
                break;
            case CartEnum.confirmCart:
                #region confirm cart 
                //Console.WriteLine("enter your cart items ");
                Console.WriteLine("enter customer name ");
                _cartBL.CustomerName = Console.ReadLine();
                Console.WriteLine("enter customer email ");
                _cartBL.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter customer address ");
                _cartBL.CustomerAddress = Console.ReadLine();
                Console.WriteLine(_bl.Cart.ConfirmOrder(_cartBL));
                _cartBL = new BO.Cart();//איפוס סל
                #endregion
                break;
            case CartEnum.addProductToCart:
                #region add product to cart
                Console.WriteLine("enter order item ID ");
                OrderItemID = int.Parse(Console.ReadLine());
                Console.WriteLine(_bl.Cart.Create(_cartBL, OrderItemID));
                #endregion
                break;
        }
    }
    #endregion
}



