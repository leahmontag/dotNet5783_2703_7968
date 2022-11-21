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
        foreach (var item in _orderList)
        {
            if (item.ID == myOrder.ID)
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
        for (int i = 0; i < _orderList.Count; i++)
        {
            if (_orderList[i].ID == myOrder.ID)
            {
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.

                _orderList[i] = myOrder;
                //if (myOrder.CustomerName != " ")
                //    _orderList[i].CustomerName = myOrder.CustomerName;
                //if (myOrder.CustomerEmail != " ")
                //    _orderList[i].CustomerEmail = myOrder.CustomerEmail;
                //if (myOrder.CustomerAdress != " ")
                //    _orderList[i].CustomerAdress = myOrder.CustomerAdress;
                return;
            }
        }
        throw new NotFoundException("not exist order");
    }
    #endregion

    /// <summary>
    /// delete order.
    /// </summary>
    #region Delete
    public void Delete(int OrderId)
    {
        for (int i = 0; i < _orderList.Count; i++)
        {
            if (_orderList[i].ID == OrderId)
            {
                _orderList[i] = _orderList[_orderList.Count];
                return;
            }
        }
        throw new NotFoundException("not exist order");
    }
    #endregion

    /// <summary>
    /// get order.
    /// </summary>
    #region Get
    public Order Get(int OrderId)
    {
        foreach (var item in _orderList)
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
    public IEnumerable<Order> GetAll()
    {
        List<Order> _newOrderList;
        _newOrderList = _orderList;
        return _newOrderList;
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
            if (item.ID == num)
                return true;
        }
        return false;
    }
    #endregion
}
