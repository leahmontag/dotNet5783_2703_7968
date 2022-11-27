using DalApi;
using DO;
using static Dal.DataSource;
namespace Dal;

/// <summary>
/// class DalOrderItem.
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// add new order item.
    /// </summary>
    #region Create
    public int Create(OrderItem myOrderItem)
    {
        myOrderItem.OrderItemID = Config.AutoNumOrderItem;
        foreach (var item in _orderItemList)
        {
            if (item.OrderItemID == myOrderItem.OrderItemID)
                throw new DuplicatesException("exist orderItem");
        }

        _orderItemList.Add(myOrderItem);
        return myOrderItem.OrderItemID;
    }
    #endregion 

    /// <summary>
    /// update order item.
    /// </summary>
    #region Update
    public void Update(OrderItem myOrderItem)
    {

        for (int i = 0; i < _orderItemList.Count; i++)
        {
            if (_orderItemList[i].OrderItemID == myOrderItem.OrderItemID)
            {
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.

                _orderItemList[i] = myOrderItem;
                //if (myOrderItem.OrderID != 0)
                //    _orderItemList[i].OrderID = myOrderItem.OrderID;
                //if (myOrderItem.ProductID != 0)
                //    _orderItemList[i].ProductID = myOrderItem.ProductID;
                //if (myOrderItem.Amount != 0)
                //    _orderItemList[i].Amount = myOrderItem.Amount;
                //if (myOrderItem.Price != 0.0)
                //    _orderItemList[i].Price = myOrderItem.Price;
                return;
            }
        }
        throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// delete order item.
    /// </summary>
    #region Delete
    public void Delete(int OrderItemId)
    {
        foreach (var item in _orderItemList)
        {
            if (item.OrderItemID == OrderItemId)
            {
                _orderItemList.Remove(item);
                return;
            }
        }
        throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// Get order item by order item id.
    /// </summary>
    #region Get by order item id
    public OrderItem Get(int OrderItemId)
    {
        foreach (var item in _orderItemList)
        {
            if (item.OrderItemID == OrderItemId)
                return item;
        }
        throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// Get by product id and order id.
    /// </summary>
    #region Get by product id and order id
    public OrderItem GetByProductIDAndOrderID(int orderId, int productId)
    {
        foreach (var item in _orderItemList)
        {
            if (item.OrderID == orderId && item.ProductID == productId)
                return item;
        }
        throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// Get order items by order id.
    /// </summary>
    #region Get order items by order id
    public IEnumerable<OrderItem> GetOrderItemsByOrderID(int orderId)///מה טיפוס ערך המוחזר?
    {
        List<OrderItem> _newOrderItemList = new();
        foreach (var item in _orderItemList)
        {
            if (item.OrderID == orderId)
                _newOrderItemList.Add(item);
        }
        return _newOrderItemList;
    }
    #endregion

    /// <summary>
    /// Get all orders items.
    /// </summary>
    #region GetAll
    public IEnumerable<OrderItem> GetAll()
    {
        try
        {
        List<OrderItem> _newOrderItemList;
        _newOrderItemList = _orderItemList;
        return _newOrderItemList;
        }
        catch (Exception)
        {

            throw new NotFoundException("can't display all order items");

        }

    }
    #endregion

    /// <summary>
    /// function checking if order item ID is exis.
    /// </summary>
    /// <param name="num"></param>
    /// <returns>bool</returns>
    #region if order item ID is exis
    public bool exisOrderItemID(int num)
    {
        foreach (var item in _orderItemList)
        {
            if (item.OrderItemID == num)
                return true;
        }
        return false;
    }
    #endregion


}
