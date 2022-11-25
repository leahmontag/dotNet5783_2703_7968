using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Xml.Linq;
using static BO.Enums;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = new Dal.DalList();

    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>list of orders</returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<BO.OrderForList> GetAll()
    {
        IEnumerable<DO.Order> orders = Dal.Order.GetAll();
        List<BO.Order> BoOrders = new List<BO.Order>();
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>();

        foreach (var item in orders)
        {
            BoOrders.Add(Get(item.ID));
        }
        foreach (var item in BoOrders)
            orderForList.Add(new BO.OrderForList() { ID = item.ID, CustomerName = item.CustomerName, Status = item.Status, AmountOfItems = item.Items.Count, TotalPrice = item.TotalPrice });
        return orderForList;
        throw new NotImplementedException();
    }
    /// <summary>
    /// get order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order Get(int ID)
    {
        try
        {
            if (ID <= 0)
            {
                throw new NotImplementedException();
            }
            DO.Order DoOrder = Dal.Order.Get(ID);
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            return BoOrder;
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// update ship date
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order after update</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateShip(int ID)
    {
        try
        {
            DO.Order DoOrder = Dal.Order.Get(ID);
            if (DoOrder.ShipDate == DateTime.MinValue)
                DoOrder.ShipDate = DateTime.Now;
            else
                throw new Exception();
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            Dal.Order.Update(DoOrder);
            return BoOrder;
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// update delivery date
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order after update</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateDelivery(int ID)
    {
        try
        {
            DO.Order DoOrder = Dal.Order.Get(ID);
            if (DoOrder.ShipDate != DateTime.MinValue && DoOrder.DeliveryDate == DateTime.MinValue)
                DoOrder.DeliveryDate = DateTime.Now;
            else
                throw new Exception();
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            Dal.Order.Update(DoOrder);
            return BoOrder;
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// tracking of order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order tracking</returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderTracking TrackingOfOrder(int ID)
    {
        try
        {
            DO.Order DoOrder = Dal.Order.Get(ID);
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
                    if (BoOrder.ShipDate > DateTime.MinValue)
                        tracking.Description = "The order was sent";
                    else
                        tracking.Description = "The order was not sent";
                }
                else
                {
                    tracking.Date = BoOrder.DeliveryDate;
                    if (BoOrder.DeliveryDate > DateTime.MinValue)
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
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// Convert DoOrder To BoOrder
    /// </summary>
    /// <param name="DoOrder"></param>
    /// <returns>return the BO order after convert</returns>
    private BO.Order ConvertDoOrderToBoOrder(DO.Order DoOrder)
    {
        IEnumerable<DO.OrderItem> itemsOfOrder = Dal.OrderItem.GetOrderItemsByOrderID(DoOrder.ID);
        BO.Order BoOrder = new BO.Order();
        BoOrder.ID = DoOrder.ID;
        BoOrder.CustomerName = DoOrder.CustomerName;
        BoOrder.CustomerEmail = DoOrder.CustomerEmail;
        BoOrder.CustomerAdress = DoOrder.CustomerAdress;
        BoOrder.OrderDate = DoOrder.OrderDate;
        if (DoOrder.DeliveryDate > DateTime.MinValue)
            BoOrder.Status = BO.Enums.OrderStatus.provided;
        else if (DoOrder.ShipDate > DateTime.MinValue)
            BoOrder.Status = BO.Enums.OrderStatus.send;
        else
            BoOrder.Status = BO.Enums.OrderStatus.confirmed;
        //    PaymentDate: { PaymentDate}
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



    //לבונוס:

    public BO.Order UpdateOrder(int orderId,int orderItemId,string whatToDO, int Amount, BO.OrderItem newOrderItem)
    {
        try
        {
            DO.Order DoOrder = Dal.Order.Get(orderId);
            if (DoOrder.ShipDate != DateTime.MinValue)//הזמנה נשלחה ואז אין טעם לעדכן אותה
                throw new NotImplementedException();
            BO.Order BoOrder = new BO.Order();
            BoOrder = ConvertDoOrderToBoOrder(DoOrder);
            foreach (BO.OrderItem item in BoOrder.Items)
            {
                if (item.OrderItemID == orderItemId)
                {
                  // DO.Product product = Dal.Product.Get(orderId);

                    if (whatToDO == "remove")
                        BoOrder.Items.Remove(item);
                    if(whatToDO == "add")
                        BoOrder.Items.Add(newOrderItem);
                    if (whatToDO == "+")
                        item.Amount += Amount;
                    if(whatToDO == "-")
                        item.Amount -= Amount;
                }
            }
            throw new NotImplementedException();
        }
        catch (Exception)
        {
            throw;
        }


    }
}
