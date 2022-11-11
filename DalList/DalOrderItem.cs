using DO;
namespace Dal;
/// <summary>
/// class DalOrderItem.
/// </summary>
public class DalOrderItem
{
    /// <summary>
    /// add new order item.
    /// </summary>
    #region Create
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

    /// <summary>
    /// update order item.
    /// </summary>
    #region Update
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

    /// <summary>
    /// delete order item.
    /// </summary>
    #region Delete
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

    /// <summary>
    /// Get order item by order item id.
    /// </summary>
    #region Get by order item id
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

    /// <summary>
    /// Get by product id and order id.
    /// </summary>
    #region Get by product id and order id
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

    /// <summary>
    /// Get order items by order id.
    /// </summary>
    #region Get order items by order id
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

    /// <summary>
    /// Get all orders items.
    /// </summary>
    #region GetAll
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

    /// <summary>
    /// function checking if order item ID is exis.
    /// </summary>
    /// <param name="num"></param>
    /// <returns>bool</returns>
    #region if order item ID is exis
    public bool exisOrderItemID(int num)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].OrderItemID == num)
                return true;
        }
        return false;
    }
    #endregion


}
