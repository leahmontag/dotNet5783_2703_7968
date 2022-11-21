using DalApi;
using DO;
namespace Dal;

/// <summary>
/// class DalOrderItem.
/// </summary>
internal class DalOrderItem: IOrderItem
{
    /// <summary>
    /// add new order item.
    /// </summary>
    #region Create
    public int Create(OrderItem myOrderItem)
    {
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource._orderItemArr[i].OrderItemID == myOrderItem.OrderItemID)
                throw new Exception("exist OrderItem");
        }
        DataSource._orderItemArr[DataSource.Config.OrderItemIndex] = myOrderItem;
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
            if (DataSource._orderItemArr[i].OrderItemID == myOrderItem.OrderItemID)
            {
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.
                if (myOrderItem.OrderID != 0)
                    DataSource._orderItemArr[i].OrderID = myOrderItem.OrderID;
                if (myOrderItem.ProductID != 0)
                    DataSource._orderItemArr[i].ProductID = myOrderItem.ProductID;
                if (myOrderItem.Amount != 0)
                    DataSource._orderItemArr[i].Amount = myOrderItem.Amount;
                if (myOrderItem.Price != 0.0)
                    DataSource._orderItemArr[i].Price = myOrderItem.Price;
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
            if (DataSource._orderItemArr[i].OrderItemID == OrderItemId)
            {
                DataSource._orderItemArr[i] = DataSource._orderItemArr[DataSource.Config.OrderItemIndex];
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
            OrderItem CurrentOrder = DataSource._orderItemArr[i];
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
            OrderItem CurrentOrder = DataSource._orderItemArr[i];
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
            OrderItem CurrentOrder = DataSource._orderItemArr[i];
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
    public IEnumerable<OrderItem> GetAll()
    {
        int size = DataSource.Config.OrderItemIndex;
        OrderItem[] newOrderItemArr = new OrderItem[size];
        for (int i = 0; i < size; i++)
        {
            newOrderItemArr[i] = DataSource._orderItemArr[i];
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
            if (DataSource._orderItemArr[i].OrderItemID == num)
                return true;
        }
        return false;
    }
    #endregion


}
