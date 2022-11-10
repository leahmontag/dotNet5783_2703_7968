using DO;
namespace Dal;
/// <summary>
/// class DalOrderItem.
/// </summary>
public class DalOrderItem
{
    #region Create
    /// <summary>
    /// add new order item.
    /// </summary>
    public int Create(OrderItem myOrderItem)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].OrderItemID == myOrderItem.OrderItemID)
                throw new Exception("exist OrderItem");
        }
        DataSource.OrderItemArr[DataSource.Config.OrderItemIndex] = myOrderItem;
        DataSource.Config.OrderItemIndex++;
        return myOrderItem.OrderItemID;
    }
    #endregion
    #region Update
    /// <summary>
    /// update order item.
    /// </summary>
    public void Update(OrderItem myOrderItem)
    {

        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].OrderItemID == myOrderItem.OrderItemID)
            {
                DataSource.OrderItemArr[i] = myOrderItem;
                return;
            }
        }
        throw new Exception("not exist OrderItem");
    }
    #endregion
    #region Delete
    /// <summary>
    /// delete order item.
    /// </summary>
    public void Delete(int OrderItemId)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].OrderItemID == OrderItemId)
            {
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[DataSource.Config.OrderItemIndex];
                DataSource.Config.OrderItemIndex--;
                return;
            }
        }
        throw new Exception("not exist OrderItem");
    }
    #endregion
    #region Get by order item id
    /// <summary>
    /// Get order item by order item id.
    /// </summary>
    public OrderItem Get(int OrderItemId)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            OrderItem CurrentOrder = DataSource.OrderItemArr[i];
            if (CurrentOrder.OrderItemID == OrderItemId)
                return CurrentOrder;
        }
        throw new Exception("not exist OrderItem");
    }
    #endregion
    #region Get by product id and order id
    /// <summary>
    /// Get by product id and order id.
    /// </summary>
    public OrderItem GetByProductIDAndOrderID(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            OrderItem CurrentOrder = DataSource.OrderItemArr[i];
            if (CurrentOrder.OrderID == orderId && CurrentOrder.ProductID == productId)
                return CurrentOrder;
        }
        throw new Exception("not exist OrderItem");
    }
    #endregion
    #region Get order items by order id
    /// <summary>
    /// Get order items by order id.
    /// </summary>
    public OrderItem[] GetOrderItemsByOrderID(int orderId)
    {
        OrderItem[] OrdetItemsArr = new OrderItem[4];
        int j = 0;
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            OrderItem CurrentOrder = DataSource.OrderItemArr[i];
            if (CurrentOrder.OrderID == orderId)
                OrdetItemsArr[j++] = CurrentOrder;
        }
        return OrdetItemsArr;
    }
    #endregion
    #region GetAll
    /// <summary>
    /// Get all orders items.
    /// </summary>
    public OrderItem[] GetAll()
    {
        int size = DataSource.Config.OrderItemIndex;
        OrderItem[] newOrderItemArr = new OrderItem[size];
        for (int i = 0; i < size; i++)
        {
            newOrderItemArr[i] = DataSource.OrderItemArr[i];
        }
        return newOrderItemArr;
    }
    #endregion

}
