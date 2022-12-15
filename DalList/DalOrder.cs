using DalApi;
using DO;
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
    public int Create(Order myOrder)
    {
        myOrder.ID = Config.AutoNumOrder;
        foreach (var item in _orderList)
        {
            if (item?.ID == myOrder.ID)
                throw new DuplicatesException("exist order");
        }
        _orderList.Add(myOrder);
        return myOrder.ID;
    }
    #endregion

    /// <summary>
    /// update order.
    /// </summary>
    #region Update
    public void Update(Order? myOrder)
    {
        try
        {
            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i]!=null && _orderList[i]!?.ID == myOrder?.ID)
                {
                    _orderList[i] = myOrder;
                    return;
                }
            }
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist order");
        }

    }
    #endregion

    /// <summary>
    /// delete order.
    /// </summary>
    #region Delete
    public void Delete(int OrderId)
    {
        try
        {
            foreach (var item in _orderList)
            {
                if (item?.ID == OrderId)
                {
                    _orderList.Remove(item);
                    return;
                }
            }
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist order");
        }
    }
    #endregion

    /// <summary>
    /// get order.
    /// </summary>
    #region Get
    public Order Get(Func<Order?, bool>? d)
    {
        foreach (Order? item in _orderList)
        {
            if (item != null && d != null && d(item) == true)
                return new Order()
                {
                    ID = item?.ID ?? 0,
                    CustomerAdress = item?.CustomerAdress ?? "",
                    DeliveryDate = item?.DeliveryDate ?? null,
                    OrderDate = item?.OrderDate ?? null,
                    ShipDate = item?.ShipDate ?? null,
                    CustomerEmail = item?.CustomerEmail ?? "",
                    CustomerName = item?.CustomerName ?? ""
                };
        }
        throw new NotFoundException("not exist order");
    }
    #endregion

    /// <summary>
    /// get all orders.
    /// </summary>
    #region GetAll
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
            _newOrderList = _orderList.FindAll(item => (item != null && d(item) == true));
            return _newOrderList;
        }
    }
    #endregion

    /// <summary>
    /// function checking if order ID is exis.
    /// </summary>
    /// <param name="num"></param>
    /// <returns>bool</returns>
    #region if order ID is exis
    public bool exisOrderID(int num)
    {
        foreach (var item in _orderList)
        {
            if (item?.ID == num)
                return true;
        }
        return false;
    }
    #endregion
}
