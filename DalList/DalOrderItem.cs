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
        if (_orderItemList.Exists(item => item?.OrderItemID == myOrderItem.OrderItemID))
            throw new DuplicatesException("exist orderItem");
        _orderItemList.Add(myOrderItem);
        return myOrderItem.OrderItemID;
    }
    #endregion 

    /// <summary>
    /// update order item.
    /// </summary>
    #region Update
    public void Update(OrderItem? myOrderItem)
    {

        if (_orderItemList.Exists(item => item?.OrderItemID == myOrderItem?.OrderItemID))
        {
            _orderItemList.Remove(_orderItemList.FirstOrDefault(item => item?.OrderItemID == myOrderItem?.OrderItemID));
            _orderItemList.Add(myOrderItem);
            return;
        }
        else
            throw new NotFoundException("not exist OrderItem");


        //for (int i = 0; i < _orderItemList.Count; i++)
        //{
        //    if (_orderItemList[i] != null && _orderItemList[i]!?.OrderItemID == myOrderItem?.OrderItemID)
        //    {
        //        //Checking inputs from the user.
        //        // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.

        //        _orderItemList[i] = myOrderItem;
        //        //if (myOrderItem.OrderID != 0)
        //        //    _orderItemList[i].OrderID = myOrderItem.OrderID;
        //        //if (myOrderItem.ProductID != 0)
        //        //    _orderItemList[i].ProductID = myOrderItem.ProductID;
        //        //if (myOrderItem.Amount != 0)
        //        //    _orderItemList[i].Amount = myOrderItem.Amount;
        //        //if (myOrderItem.Price != 0.0)
        //        //    _orderItemList[i].Price = myOrderItem.Price;
        //        return;
        //    }
        //}
        //throw new NotFoundException("not exist OrderItem");
    }
    #endregion

    /// <summary>
    /// delete order item.
    /// </summary>
    #region Delete
    public void Delete(int OrderItemId)
    {
        _orderItemList.Remove(_orderItemList.FirstOrDefault(item => item?.OrderItemID == OrderItemId)
        ?? throw new NotFoundException("not exist OrderItem"));
    }
    #endregion

    /// <summary>
    /// Get order item
    /// </summary>
    #region Get
    public OrderItem Get(Func<OrderItem?, bool>? d)
    {
        try
        {
            var orderItem = _orderItemList.Where(item => item != null && d != null && d(item) == true).First();
            return new OrderItem()
            {
                OrderID = orderItem?.OrderID ?? 0,
                OrderItemID = orderItem?.OrderItemID ?? 0,
                ProductID = orderItem?.ProductID ?? 0,
                Name = orderItem?.Name ?? "",
                Amount = orderItem?.Amount ?? 0,
                Price = orderItem?.Price ?? 0
            };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist OrderItem");
        }
    }
    #endregion

    /// <summary>
    /// Get all orders items.
    /// </summary>
    #region GetAll
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? d = null)
    {
        List<OrderItem?> _newOrderItemList;
        if (d == null)
        {
            try
            {
                _newOrderItemList = _orderItemList;
                return _newOrderItemList;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all products");
            }
        }
        else
            return _orderItemList.Where(item => item != null && d != null && d(item) == true).ToList();
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
        if (_orderItemList.Exists(item => item?.OrderItemID == num))
            return true;
        return false;
    }
    #endregion


}
