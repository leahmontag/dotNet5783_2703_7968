// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using do;
using Dal;
namespace Dal:

internal class Program { 
    private Product myProduct=new Product();
    private Order myOrder=new Order();
    private OrderItem OrderItem=new OrderItem();
void Main()
{
    Console.WriteLine("enter your choice:" +"0 to exit" +"1 -product"+"2 -order"+"3 -order items");
    int choice=Console.ReadLine();
    string yourChoice;
    switch (choice)
	{
        case 0:
            break;
        case 1://product
                #region product
            yourChoice=Console.ReadLine();
            switch (yourChoice)
            {
                case 'a':
                        #region add product
                    Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                    myProduct.ID = Console.Read();
                    myProduct.Name = Console.ReadLine();
                    myProduct.Price = Console.Read();
                    myProduct.InStock = Console.Read();
                    int productID=Product.Create(myProduct);
                    Console.WriteLine(${ "קוד המוצר הינו:"+productID});
                        #endregion
                    break;
                case 'b':
                        #region get product
                    Console.WriteLine("הכנס מספר מזהה של המוצר ");
                    int num=Console.ReadLine();
                    Console.WriteLine(Product.Get(num));
                        #endregion
                    break;
                case 'c':
                    Console.WriteLine(Product.GetAll());
                    break;
                case 'd':
                        #region Update product
                    Console.WriteLine("הזן עדכון פרטי המוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                    Product myProduct=new Product();
                    myProduct.ID = Console.Read();
                    myProduct.Name = Console.Read();
                    myProduct.Price = Console.Read();
                    myProduct.InStock = Console.Read();
                    Product.Update(myProduct);
                        #endregion
                    break;
                case 'e':
                        #region Delete
                    Console.WriteLine("הכנס מספר מזהה של המוצר ");
                    int ProductId= Console.Read();
                    Product.Delete(ProductId);
	                    #endregion
                    break;
            }
	           #endregion
            break;
        case 2://order
                #region order
                 yourChoice=Console.ReadLine();
            switch (yourChoice)
            {
                case 'a':
                        #region add order
                    Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                    myOrder.ID = Console.Read();
                    myOrder.CustomerName = Console.ReadLine();
                    myOrder.CustomerEmail = Console.Read();
                    myOrder.CustomerAdress = Console.Read();
                    int productID=Order.Create(myOrder);
                    Console.WriteLine(${ "קוד המוצר הינו:"+productID});
                        #endregion
                    break;
                case 'b':
                        #region get order
                    Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                    int OrderId=Console.ReadLine();
                    Console.WriteLine(Order.Get(OrderId));
                        #endregion
                    break;
                case 'c':
                    Console.WriteLine(Order.GetAll());
                    break;
                case 'd':
                        #region Update order
                   Console.WriteLine("הזן פרטי מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                    myOrder.ID = Console.Read();
                    myOrder.CustomerName = Console.ReadLine();
                    myOrder.CustomerEmail = Console.Read();
                    myOrder.CustomerAdress = Console.Read();
                    Order.Update(myOrder);
                        #endregion
                    break;
                case 'e':
                        #region Delete order
                    Console.WriteLine("הכנס מספר מזהה של הזמנה ");
                    int orderId= Console.Read();
                    Order.Delete(orderId);
	                    #endregion
                    break;
            }
                #endregion
            break;
        case 3://order item
                 #region order item
                  yourChoice=Console.ReadLine();
            switch (yourChoice)
            {
                case 'a':
                        #region add orderItem
                    Console.WriteLine("הזן פרטי הזמנת מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                    myOrderItem.OrderItemID = Console.Read();
                    myOrderItem.ProductID = Console.ReadLine();
                    myOrderItem.OrderID = Console.Read();
                    myOrderItem.Price = Console.Read();
                    myOrderItem.Amount = Console.Read();
                    int OrderItemID=OrderItem.Create(myOrderItem);
                    Console.WriteLine(${ "קוד הזמנת מוצר הינו:"+OrderItemID});
                        #endregion
                    break;
                case 'b':
                        #region get orderItem
                    Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                    int orderItemId=Console.ReadLine();
                    Console.WriteLine(OrderItem.Get(orderItemId));
                        #endregion
                    break;
                case 'c':
                    Console.WriteLine(OrderItem.GetAll());
                    break;
                case 'd':
                        #region Update OrderItem
                   Console.WriteLine("הזן פרטי הזמנת מוצר עם ירידת שורה בין השדות: ID,NAME,PRICE,INSTOCK");            
                     myOrderItem.OrderItemID = Console.Read();
                    myOrderItem.ProductID = Console.ReadLine();
                    myOrderItem.OrderID = Console.Read();
                    myOrderItem.Price = Console.Read();
                    myOrderItem.Amount = Console.Read();
                    OrderItem.Update(myOrderItem);
                        #endregion
                    break;
                case 'e':
                        #region Delete orderItem
                    Console.WriteLine("הכנס מספר מזהה של הזמנת מוצר ");
                    int orderItemId= Console.Read();
                    OrderItem.Delete(orderItemId);
	                    #endregion
                    break;
            }
                #endregion
            break;
		default:
	}
}
}

