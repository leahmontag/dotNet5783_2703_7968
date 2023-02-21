using DalApi;
using DO;
using System.Runtime.CompilerServices;
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    }
    #endregion

    /// <summary>
    /// delete order item.
    /// </summary>
    #region Delete
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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

}
