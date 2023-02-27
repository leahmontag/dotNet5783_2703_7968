using Amazon.Runtime.Internal;
using BO;
using DalApi;
using DO;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BlImplementation;

/// <summary>
/// class Order
/// </summary>
internal class Order : BlApi.IOrder
{
    DalApi.IDal? _dal = DalApi.Factory.Get();


    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>list of orders</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region get all orders
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList?> GetAll(Func<DO.Order?, bool>? d = null)
    {
        lock (_dal)
        {
            try
            {

                IEnumerable<DO.Order?> orders = _dal.Order.GetAll();
                IEnumerable<BO.Order?> BoOrders = from DO.Order order in orders
                                                  let newItem = Get(x => x?.ID == order.ID)
                                                  select newItem;
                return (from BO.Order order in BoOrders
                        select new BO.OrderForList()
                        {
                            ID = order.ID,
                            CustomerName = order.CustomerName,
                            Status = order.Status,
                            AmountOfItems = order.Items.Count,
                            TotalPrice = order.TotalPrice
                        });
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.FailedToDisplayAllItemsException("Failed to display all items", exp);
            }
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order Get(Func<DO.Order?, bool>? d)
    {
        lock (_dal)
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
    }
    #endregion

    /// <summary>
    /// update ship date
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order after update</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region update ship
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateShip(int ID)
    {
        lock (_dal)
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
                _dal?.Order.Update(DoOrder);
                return BoOrder;
            }
            catch (DO.NotFoundException exp)
            {

                throw new BO.OperationFailedException("update ship was failed", exp);
            }
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateDelivery(int ID)
    {
        lock (_dal)
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
    }
    #endregion

    /// <summary>
    /// tracking of order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>order tracking</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region tracking of order
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderTracking TrackingOfOrder(int ID)
    {
        lock (_dal)
        {
            try
            {
                DO.Order DoOrder = _dal.Order.Get(x => x?.ID == ID);
                BO.Order BoOrder = new BO.Order();
                BoOrder = ConvertDoOrderToBoOrder(DoOrder);
                OrderTracking orderTracking = new OrderTracking();
                orderTracking.ID = BoOrder.ID;
                orderTracking.Status = BoOrder.Status;
                List<OrderTrackingDates?> orderTrackingDates = new List<OrderTrackingDates?>();
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
        lock (_dal)
        {
            IEnumerable<DO.OrderItem?> DoItemsOfOrder = _dal.OrderItem.GetAll(x => x?.OrderID == DoOrder.ID);
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
            IEnumerable<BO.OrderItem?> BoOrderItems = from DO.OrderItem itemInOrder in DoItemsOfOrder
                                                      let it = new BO.OrderItem()
                                                      {
                                                          Name = itemInOrder.Name,
                                                          OrderID = itemInOrder.OrderID,
                                                          Price = itemInOrder.Price,
                                                          ProductID = itemInOrder.ProductID,
                                                          Amount = itemInOrder.Amount,
                                                          TotalPrice = (itemInOrder.Price) * (itemInOrder.Amount)
                                                      }
                                                      select it;
            var sumOfTotalPrice = from BO.OrderItem BoItemInOrder in BoOrderItems
                                  select BoItemInOrder.TotalPrice;

            BoOrder.Items = BoOrderItems.ToList();
            BoOrder.TotalPrice = sumOfTotalPrice.Sum();
            return BoOrder;
        }
    }
    #endregion


    /// <summary>
    /// Selecting Order For Treatment() 
    /// </summary>
    /// <returns></returns>
    #region
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? SelectingOrderForTreatment()
    {
        lock (_dal)
        {
            IEnumerable<DO.Order?> orders = _dal.Order.GetAll();
            try
            {
                var newOrders1 = (from DO.Order o in orders
                                  where o.DeliveryDate == null
                                  orderby o.ShipDate
                                  select o).FirstOrDefault();

                var newOrders2 = (from DO.Order o in orders
                                  where o.ShipDate == null
                                  orderby o.OrderDate
                                  select o).FirstOrDefault();
                if (newOrders2.OrderDate != null)
                {
                    if (newOrders1.ShipDate < newOrders2.OrderDate)
                        return newOrders1.ID;
                    else
                        return newOrders2.ID;
                }
                else
                    return newOrders1.ID;
            }
            catch
            {
                return null;

            }

        }
    }

    #endregion

}

