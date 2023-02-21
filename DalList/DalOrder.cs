using DalApi;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;
namespace Dal;

/// <summary>
/// class DalOrder.
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// add new order.
    /// </summary>
    #region Create
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Order myOrder)
    {
        myOrder.ID = Config.AutoNumOrder;
        if (_orderList.Exists(item => item?.ID == myOrder.ID))
            throw new DuplicatesException("exist order");
        _orderList.Add(myOrder);
        return myOrder.ID;
    }
    #endregion

    /// <summary>
    /// update order.
    /// </summary>
    #region Update
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order? myOrder)
    {
        if (_orderList.Exists(item => item?.ID == myOrder?.ID))
        {
            _orderList.Remove(_orderList.FirstOrDefault(item => item?.ID == myOrder?.ID));
            _orderList.Add(myOrder);
            return;
        }
        else
            throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// delete order.
    /// </summary>
    #region Delete
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int OrderId)
    {
        _orderList.Remove(_orderList.FirstOrDefault(item => item?.ID == OrderId)
            ?? throw new NotFoundException("not exist order"));
    }
    #endregion

    /// <summary>
    /// get order.
    /// </summary>
    #region Get
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Func<Order?, bool>? d)
    {
        try
        {
            var order = _orderList.Where(item => item != null && d != null && d(item) == true).First();
            return new Order()
            {
                ID = order?.ID ?? 0,
                CustomerAdress = order?.CustomerAdress ?? "",
                DeliveryDate = order?.DeliveryDate ?? null,
                OrderDate = order?.OrderDate ?? null,
                ShipDate = order?.ShipDate ?? null,
                CustomerEmail = order?.CustomerEmail ?? "",
                CustomerName = order?.CustomerName ?? ""
            };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist order");

        }
    }
    #endregion

    /// <summary>
    /// get all orders.
    /// </summary>
    #region GetAll
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? d = null)
    {
        List<Order?> _newOrderList;
        if (d == null)
        {
            try
            {
                _newOrderList = _orderList;
                return _newOrderList;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all products");
            }
        }
        else
        {
            return _orderList.Where(item => item != null && d != null && d(item) == true).ToList();
        }
    }
    #endregion
}
