using BlApi;
using BO;
using static BO.Enums;
namespace BlTest;



internal class Program
{
    private static readonly IBl _bl = Factory.Get();
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
        Category productEnum;
        string numId;
        int num;
        Console.WriteLine("enter your choice:" + "\n1- get all product" + "\n2- get product by manager" + "\n3-  get products from catalog" + "\n4- add product" + "\n5- remove product" + "\n6- update product");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case ProductEnum.getAllProducts:
                #region get all products
                try
                {
                    IEnumerable<ProductForList> printProductList = _bl.Product.GetAll();
                    foreach (var i in printProductList)
                    {
                        Console.WriteLine(i);
                    }
                }
                catch (BO.FailedAddingProductException exp)
                {

                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case ProductEnum.getProductByManager:
                #region get by manager
                try
                {
                    Console.WriteLine("enter id of product ");
                    numId = Console.ReadLine();
                    num = Convert.ToInt32(numId);
                    if (num <= 0)
                        throw new Exception("ID cannot be negative");
                    Console.WriteLine(_bl.Product.GetByManager(num));
                }
                catch (BO.ProductIsNotAvailableException exp)
                {

                    Console.WriteLine(exp);
                }

                #endregion
                break;
            case ProductEnum.getProductFromCatalog:
                #region get product from catalog
                Console.WriteLine("enter id of product ");
                numId = Console.ReadLine();
                num = Convert.ToInt32(numId);
                if (num <= 0)
                    throw new Exception("ID cannot be negative");
                Console.WriteLine(_bl.Product.GetProductFromCatalog(num, _cartBL));
                #endregion
                break;
            case ProductEnum.addProduct:
                #region add product
                bool flag = false;
                BO.Product myProductToAdd = new BO.Product();
                do
                {
                    flag = false;
                    Console.WriteLine("enter your product iteam:");
                    Console.WriteLine("name");
                    myProductToAdd.Name = Console.ReadLine();
                    Console.WriteLine("price");
                    myProductToAdd.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("InStock");
                    myProductToAdd.InStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("Category");
                    Console.WriteLine("enter your choice:" + "0- eyeMakeup, " + "1- lipMakeup, " + "2- facialMmakeup, " + "3- brushes, " + "4- cultivation");
                    Enum.TryParse(Console.ReadLine(), out productEnum);

                    //cheacking values 
                    #region cheacking values 
                    if (productEnum == Category.eyeMakeup || productEnum == Category.lipMakeup || productEnum == Category.facialMmakeup || productEnum == Category.cultivation || productEnum == Category.brushes)
                        myProductToAdd.Category = productEnum;
                    else
                    {
                        flag = true;
                        Console.WriteLine("not exist category, enter your product iteam again ");
                    }
                    if (myProductToAdd.Name == " " || myProductToAdd.Price <=
                   0 || myProductToAdd.InStock <= 0)
                    {
                        flag = true;
                        Console.WriteLine("Incorrect product details, enter your product iteam again ");
                    }
                    #endregion

                } while (flag);
                try
                {
                    int productID = _bl.Product.Create(myProductToAdd);
                    Console.WriteLine("id:" + productID + "\n");
                }
                catch (BO.FailedAddingProductException exp)
                {

                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case ProductEnum.removeProduct:
                #region remove product
                try
                {
                    Console.WriteLine("enter product id:");
                    int productId = int.Parse(Console.ReadLine());
                    _bl.Product.Delete(productId);
                }
                catch (BO.cannotDeletedItemException exp)
                {

                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case ProductEnum.updateProduct:
                #region update product
                BO.Product myProductToUpdate = new BO.Product();
                flag = false;
                Console.WriteLine("enter your product iteams:");
                do
                {
                    flag = false;
                    Console.WriteLine("id:");
                    myProductToUpdate.ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("name:");
                    myProductToUpdate.Name = Console.ReadLine();
                    Console.WriteLine("price:");
                    myProductToUpdate.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine("instock:");
                    myProductToUpdate.InStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("category:");
                    Console.WriteLine("enter your choice:" + "0- eyeMakeup, " + "1- lipMakeup, " + "2- facialMmakeup, " + "3- brushes, " + "4- cultivation");
                    Enum.TryParse(Console.ReadLine(), out productEnum);

                    //cheacking values 
                    #region cheacking values 
                    if (productEnum == Category.eyeMakeup || productEnum == Category.lipMakeup || productEnum == Category.facialMmakeup || productEnum == Category.cultivation || productEnum == Category.brushes)
                        myProductToUpdate.Category = productEnum;
                    else
                    {
                        flag = true;
                        Console.WriteLine("not exist category, enter your product iteam again ");
                    }
                    if (myProductToUpdate.Name == " " || myProductToUpdate.Price <=
                   0 || myProductToUpdate.InStock <= 0)
                    {
                        flag = true;
                        Console.WriteLine("Incorrect product details, enter your product iteam again ");
                    }
                    #endregion

                } while (flag);
                try
                {
                    _bl.Product.Update(myProductToUpdate);
                }
                catch (BO.ProductIsNotAvailableException exp)
                {

                    Console.WriteLine(exp);
                }
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
        Console.WriteLine("enter your choice:" + "\n1- show all orders" + "\n2- get an order" + "" + "\n3- update ship order" + "\n4- update delivery order" + "\n5-order tracking " + "\n6-update order by manager ");
        Enum.TryParse(Console.ReadLine(), out yourOrderChoice);
        switch (yourOrderChoice)
        {
            case OrderEnum.getAllOrders:
                #region get all orders
                try
                {
                    IEnumerable<OrderForList> printOrders = _bl.Order.GetAll();
                    foreach (OrderForList i in printOrders)
                    {
                        Console.WriteLine(i);
                    }
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case OrderEnum.getOrder:
                #region get order
                try
                {
                    Console.WriteLine("enter your order id: ");
                    int orderId = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Order.Get(orderId));
                }
                catch (BO.ProductIsNotAvailableException exp)
                {
                    Console.WriteLine(exp);
                }

                #endregion
                break;
            case OrderEnum.updateShip:
                #region update ship
                try
                {
                    Console.WriteLine("enter your order id: ");
                    int orderIdToUpdateShip = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Order.UpdateShip(orderIdToUpdateShip));
                }
                catch (BO.OperationFailedException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case OrderEnum.updateDelivery:
                #region update delivery
                try
                {
                    Console.WriteLine("enter your order id: ");
                    int orderIdToUpdateDelivery = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Order.UpdateDelivery(orderIdToUpdateDelivery));
                }
                catch (BO.OperationFailedException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case OrderEnum.trackingOfOrder:
                #region tracking of order
                try
                {
                    Console.WriteLine("enter your order id: ");
                    int orderIdToTracking = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Order.TrackingOfOrder(orderIdToTracking));
                }
                catch (BO.OperationFailedException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case OrderEnum.updateOrder:
                #region update order by manager
                try
                {
                    Console.WriteLine("enter your order id to update: ");
                    int IDOfOrder = int.Parse(Console.ReadLine());
                    BO.Order order = new BO.Order();
                    order = _bl.Order.Get(IDOfOrder);
                    Console.WriteLine("enter 1 to remove product, 2 to add new product, 3 to change amount of this product");
                    int managerChoice = int.Parse(Console.ReadLine());
                    switch (managerChoice)
                    {
                        case 1:
                            Console.WriteLine("enter the orderitem id you want to remove:");
                            int IDRemove = int.Parse(Console.ReadLine());
                            Console.WriteLine(_bl.Order.UpdateOrder(order, IDRemove, "1", 0));
                            break;
                        case 2:
                            Console.WriteLine("enter the product id you want to add:");
                            int IDProduct = int.Parse(Console.ReadLine());
                            Console.WriteLine(_bl.Order.UpdateOrder(order, IDProduct, "2", 0));
                            break;
                        case 3:
                            Console.WriteLine("enter the orderitem id you want to update:");
                            int IDOrderItem = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter the new amount:");
                            int amount = int.Parse(Console.ReadLine());
                            Console.WriteLine(_bl.Order.UpdateOrder(order, IDOrderItem, "3", amount));
                            break;
                        default:
                            throw new Exception();
                    }
                }
                catch (BO.OperationFailedException exp)
                {
                    Console.WriteLine(exp);
                }
                catch (BO.ProductIsNotAvailableException exp)
                {
                    Console.WriteLine(exp);
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {
                    Console.WriteLine(exp);
                }
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
        Console.WriteLine("enter your choice:" + "\n1- add product to cart" + "\n2- update cart" + "\n3- confirm cart" + "\n4- Emptying shopping cart");
        Enum.TryParse(Console.ReadLine(), out yourCrud);
        switch (yourCrud)
        {
            case CartEnum.updateCart:
                #region update cart
                if (_cartBL.Items == null)
                {
                    Console.WriteLine("can't update empty cart");
                    break;
                }
                try
                {
                    Console.WriteLine("enter order item ID ");
                    int OrderItemID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order new amount ");
                    int amount = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Cart.Update(_cartBL, OrderItemID, amount));
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {

                    Console.WriteLine(exp);
                }
                catch (BO.ProductIsNotAvailableException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case CartEnum.confirmCart:
                #region confirm cart 
                if (_cartBL.Items == null)
                    throw new Exception("empty cart");
                try
                {
                    Console.WriteLine("enter customer name ");
                    _cartBL.CustomerName = Console.ReadLine();
                    Console.WriteLine("enter customer email ");
                    _cartBL.CustomerEmail = Console.ReadLine();
                    Console.WriteLine("enter customer address ");
                    _cartBL.CustomerAddress = Console.ReadLine();
                    _bl.Cart.ConfirmOrder(_cartBL);
                    _cartBL = new BO.Cart();//empty cart.
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {
                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case CartEnum.addProductToCart:
                #region add product to cart
                try
                {
                    Console.WriteLine("enter product ID ");
                    int productID = int.Parse(Console.ReadLine());
                    Console.WriteLine(_bl.Cart.Create(_cartBL, productID));
                }
                catch (BO.ProductIsNotAvailableException exp)
                {

                    Console.WriteLine(exp);
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {

                    Console.WriteLine(exp);
                }
                #endregion
                break;
            case CartEnum.emptyingShoppingCart:
                #region emptying shopping cart
                _cartBL = new BO.Cart();
                Console.WriteLine("The cart has been successfully emptied!");
                #endregion
                break;

        }
    }
    #endregion
}



