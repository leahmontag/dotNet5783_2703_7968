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
            if (item?.OrderItemID == myOrderItem.OrderItemID)
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
            if (_orderItemList[i].HasValue && _orderItemList[i]!.Value.OrderItemID == myOrderItem.OrderItemID)
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
            if (item?.OrderItemID == OrderItemId)
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
    public OrderItem Get(Func<OrderItem?, bool>? d)
    {
        foreach (OrderItem? item in _orderItemList)
        {
            if (item != null && d != null && d(item) == true)
                return new OrderItem() 
                { 
                    OrderID=item.Value.OrderID,
                    OrderItemID= item.Value.OrderItemID,
                    ProductID= item.Value.ProductID,
                    Name= item.Value.Name,
                    Amount= item.Value.Amount,
                    Price= item.Value.Price
                };
        }
        throw new NotFoundException("not exist OrderItem");

    }
    #endregion

    /// <summary>
    /// Get all orders items.
    /// </summary>
    #region GetAll
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? d = null)
    {
        if (d == null)
        {
            try
            {
                List<OrderItem?> _newOrderItemList;
                _newOrderItemList = _orderItemList;
                return _newOrderItemList;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all products");
            }
        }
        else
        {
            List<OrderItem?> _newOrderItemList;
            List<OrderItem> _OrderItemListTmp = new List<OrderItem>();

            _newOrderItemList = _orderItemList;
            foreach (OrderItem? item in _newOrderItemList)
            {
                if (item != null && d(item) == true)
                    _OrderItemListTmp.Add(item.Value);
            }
            return _newOrderItemList;
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
            if (item?.OrderItemID == num)
                return true;
        }
        return false;
    }
    #endregion


}
