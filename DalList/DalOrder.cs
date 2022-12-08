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
    public void Update(Order myOrder)
    {
        try
        {
            for (int i = 0; i < _orderList.Count; i++)
            {
                if (_orderList[i].HasValue && _orderList[i]!.Value.ID == myOrder.ID)
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
    public Order Get(int OrderId)
    {
        foreach (Order item in _orderList)
        {
            if (item.ID == OrderId)
                return item;
        }
        throw new NotFoundException("not exist order");
    }
    #endregion

    /// <summary>
    /// get all orders.
    /// </summary>
    #region GetAll
    public IEnumerable<Order?> GetAll()
    {
        try
        {
        List<Order?> _newOrderList;
        _newOrderList = _orderList;
        return _newOrderList;
        }
        catch (Exception)
        {
            throw new NotFoundException("can't display all products");
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
