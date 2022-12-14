using BO;
using DalApi;
using System.Xml.Linq;

namespace BlImplementation;

/// <summary>
/// class Order
/// </summary>
internal class Order : BlApi.IOrder
{
    private IDal _dal = new Dal.DalList();

    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>list of orders</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region get all orders
    public IEnumerable<BO.OrderForList?> GetAll(Func<DO.Order?, bool>? d = null)
    {

        try
        {
            IEnumerable<DO.Order?> orders = _dal.Order.GetAll(d != null ? d : null);
            List<BO.Order?> BoOrders = new List<BO.Order?>();
            List<BO.OrderForList?> orderForList = new List<BO.OrderForList?>();

            foreach (var item in orders)
            {
                BoOrders.Add(Get(x => x?.ID == item?.ID));
            }

            foreach (BO.Order? item in BoOrders)
            {
                if (item != null)
                {
                    item.Items ??= new List<OrderItem?>();
                    orderForList.Add(new BO.OrderForList()
                    {
                        ID = item.ID,
                        CustomerName = item.CustomerName,
                        Status = item.Status,
                        AmountOfItems = item.Items.Count,
                        TotalPrice = item.TotalPrice
                    });
                }
            }

            return orderForList;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.FailedToDisplayAllItemsException("Failed to display all items", exp);
        }
    }
    #endregion

    /// <summary>
    /// get order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region get an order
    public BO.Order Get(Func<DO.Order?, bool>? d)
    {
        try
        {
            if (d == null)
            {
                throw new NotImplementedException();
            }
            DO.Order DoOrder = _dal.Order.Get(x => d(x));
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            return BoOrder;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.ProductIsNotAvailableException("product is not available", exp);
        }
    }
    #endregion

    /// <summary>
    /// update ship date
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order after update</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region update ship
    public BO.Order UpdateShip(int ID)
    {
        try
        {
            DO.Order DoOrder = _dal.Order.Get(x => x?.ID == ID);
            if (DoOrder.ShipDate == null)
                DoOrder.ShipDate = DateTime.Now;
            else
                throw new Exception();
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            _dal.Order.Update(DoOrder);
            return BoOrder;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.OperationFailedException("update ship was failed", exp);
        }
    }
    #endregion

    /// <summary>
    /// update delivery date
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order after update</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region  update delivery
    public BO.Order UpdateDelivery(int ID)
    {
        try
        {
            DO.Order DoOrder = _dal.Order.Get(x => x?.ID == ID);
            if (DoOrder.ShipDate != null && DoOrder.DeliveryDate == null)
                DoOrder.DeliveryDate = DateTime.Now;
            else
                throw new Exception();
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            _dal.Order.Update(DoOrder);
            return BoOrder;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.OperationFailedException("update delivery was failed", exp);
        }
    }
    #endregion

    /// <summary>
    /// tracking of order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order tracking</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region tracking of order
    public OrderTracking TrackingOfOrder(int ID)
    {
        try
        {
            DO.Order DoOrder = _dal.Order.Get(x => x?.ID == ID);
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            OrderTracking orderTracking = new OrderTracking();
            orderTracking.ID = BoOrder.ID;
            orderTracking.Status = BoOrder.Status;
            List<OrderTrackingDates> orderTrackingDates = new List<OrderTrackingDates>();
            for (int i = 0; i < 3; i++)
            {
                OrderTrackingDates tracking = new OrderTrackingDates();
                if (i == 0)
                {
                    tracking.Date = BoOrder.OrderDate;
                    tracking.Description = "The order was created";
                }
                else if (i == 1)
                {
                    tracking.Date = BoOrder.ShipDate;
                    if (BoOrder.ShipDate != null)
                        tracking.Description = "The order was sent";
                    else
                        tracking.Description = "The order was not sent";
                }
                else
                {
                    tracking.Date = BoOrder.DeliveryDate;
                    if (BoOrder.DeliveryDate != null)
                        tracking.Description = "The order was fulfilled";
                    else
                        tracking.Description = "The order was not fulfilled";
                }
                orderTrackingDates.Add(tracking);
            }
            orderTracking.OrderTrackingDateAndDesc = orderTrackingDates;
            return orderTracking;
            throw new NotImplementedException();
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.OperationFailedException("find tracking order was failed", exp);
        }
    }
    #endregion

    /// <summary>
    /// update order
    /// </summary>
    /// <param name="BOorder"></param>
    /// <param name="ID"></param>
    /// <param name="whatToDO"></param>
    /// <param name="Amount"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    #region update order-bunus
    public BO.Order UpdateOrder(BO.Order BOorder, int ID, string whatToDO, int Amount)
    {
        try
        {
            double sum = 0;
            IEnumerable<DO.Product?> productList = _dal.Product.GetAll();
            IEnumerable<DO.OrderItem?> orderItems = _dal.OrderItem.GetAll();
            DO.Order DoOrder = _dal.Order.Get(x => x?.ID == BOorder.ID);
            if (DoOrder.ShipDate != null)//הזמנה נשלחה ואז אין טעם לעדכן אותה
                throw new NotImplementedException();
            switch (whatToDO)
            {
                case "1"://delete order item and update the stock
                    #region delete order item and update the stock

                    foreach (BO.OrderItem item in BOorder.Items)
                    {
                        if (ID == item.OrderItemID)
                        {
                            foreach (DO.Product product in productList)
                            {
                                if (item.Name == product.Name)//עדכון מלאי במחיקה פריט בהזמנה
                                {
                                    DO.Product newProduct = new DO.Product();
                                    newProduct = product;
                                    newProduct.InStock += item.Amount;
                                    _dal.Product.Update(newProduct);
                                    break;
                                }
                            }
                            BOorder.Items.Remove(item);
                            foreach (DO.OrderItem orderItem in orderItems)//מחיקת פריט בהזמנה משכבת הנתונים
                            {
                                if (orderItem.OrderItemID == ID)
                                {
                                    _dal.OrderItem.Delete(ID);
                                    break;
                                }
                            }
                            foreach (BO.OrderItem item1 in BOorder.Items)
                                sum += item1.TotalPrice;
                            BOorder.TotalPrice = sum;
                            return BOorder;
                        }

                    }
                    #endregion
                    break;
                case "2":
                    #region add order item
                    foreach (BO.OrderItem item in BOorder.Items)
                    {
                        if (ID == item.OrderItemID)//לעשות כאן שגיאה של מוצר כבר קיים ולכן לא נוכל להוסיף אותו להזמנה
                            throw new NotImplementedException();
                    }
                    foreach (DO.Product product in productList)
                    {
                        if (product.ID == ID)
                        {
                            if (product.InStock > 1)//נבדוק האם קיים כזה מוצר והאם יש ממנו מספיק במלאי)
                            {
                                BOorder.Items.Add(new BO.OrderItem
                                {
                                    ProductID = product.ID,
                                    Amount = 1,
                                    Name = product.Name,
                                    Price = product.Price,
                                    TotalPrice = product.Price
                                }); ;
                                DO.Product newProduct = new DO.Product();//עדכון מלאי פחות 1 בגלל שהוספנו מהמוצר הזה חדש
                                newProduct = product;
                                newProduct.InStock -= 1;
                                _dal.Product.Update(newProduct);
                                _dal.OrderItem.Create(new DO.OrderItem()//הוספת פריט בהזמנה לשכבת הנתונים
                                {
                                    OrderID = BOorder.ID,
                                    ProductID = product.ID,
                                    Amount = 1,
                                    Name = product.Name,
                                    Price = product.Price
                                });
                                foreach (BO.OrderItem item in BOorder.Items)
                                    sum += item.TotalPrice;
                                BOorder.TotalPrice = sum;
                                return BOorder;
                            }
                            else
                                throw new NotImplementedException();//לעשות שגיאה של לא קיים במלאי

                        }
                    }
                    #endregion
                    break;
                case "3":
                    #region update order
                    foreach (BO.OrderItem item in BOorder.Items)
                    {
                        if (item.OrderItemID == ID)
                        {
                            foreach (DO.Product product in productList)
                            {
                                if (product.ID == item.ProductID && (product.InStock + item.Amount) - Amount > 0)//בדיקה האם יש מספיק במלאי
                                {
                                    item.Amount = Amount;//עדכון הכמות בהזמנה בשכבה הלוגית
                                    item.TotalPrice = Amount * item.Price;
                                    DO.Product newProduct = new DO.Product();//עדכון מלאי
                                    newProduct = product;
                                    newProduct.InStock = newProduct.InStock + item.Amount - Amount;
                                    _dal.Product.Update(newProduct);
                                    _dal.OrderItem.Update(new DO.OrderItem()//עדכון פריט בהזמנה לשכבת הנתונים
                                    {
                                        OrderID = BOorder.ID,
                                        ProductID = product.ID,
                                        OrderItemID = item.OrderItemID,
                                        Amount = Amount,
                                        Name = product.Name,
                                        Price = product.Price
                                    });
                                    foreach (BO.OrderItem item1 in BOorder.Items)
                                        sum += item1.TotalPrice;
                                    BOorder.TotalPrice = sum;
                                    return BOorder;
                                }
                            }
                        }
                    }
                    throw new BO.ProductIsNotAvailableException("find product was faild");//יזרוק שגיאה כי הוא לא מצא את הפריט שאותו נראה לעדכן
                    #endregion
                    break;
                default:
                    throw new BO.OperationFailedException("update order was failed");
            }
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.FailedToDisplayAllItemsException("update order was failed", exp);
        }
        return BOorder;
    }
    #endregion

    /// <summary>
    /// Convert DoOrder To BoOrder
    /// </summary>
    /// <param name="DoOrder"></param>
    /// <returns>return the BO order after convert</returns>
    #region Convert doOrder to boOrder
    private BO.Order ConvertDoOrderToBoOrder(DO.Order DoOrder)
    {
        IEnumerable<DO.OrderItem?> itemsOfOrder = _dal.OrderItem.GetAll(x => x?.OrderID == DoOrder.ID);
        BO.Order BoOrder = new BO.Order();
        BoOrder.ID = DoOrder.ID;
        BoOrder.CustomerName = DoOrder.CustomerName;
        BoOrder.CustomerEmail = DoOrder.CustomerEmail;
        BoOrder.CustomerAdress = DoOrder.CustomerAdress;
        BoOrder.OrderDate = DoOrder.OrderDate;
        if (DoOrder.DeliveryDate != null)
            BoOrder.Status = BO.Enums.OrderStatus.provided;
        else if (DoOrder.ShipDate != null)
            BoOrder.Status = BO.Enums.OrderStatus.send;
        else
            BoOrder.Status = BO.Enums.OrderStatus.confirmed;
        BoOrder.ShipDate = DoOrder.ShipDate;
        BoOrder.DeliveryDate = DoOrder.DeliveryDate;
        List<BO.OrderItem> orderItems = new List<BO.OrderItem>();
        double sumOfTotalPrice = 0;
        foreach (DO.OrderItem item in itemsOfOrder)
        {
            orderItems.Add(new BO.OrderItem() { Name = item.Name, OrderItemID = item.OrderItemID, Price = item.Price, ProductID = item.ProductID, Amount = item.Amount, TotalPrice = item.Price * item.Amount });
            sumOfTotalPrice += item.Price * item.Amount;
        }
        BoOrder.Items = orderItems;
        BoOrder.TotalPrice = sumOfTotalPrice;
        return BoOrder;
    }
    #endregion

}

