using DO;

namespace Dal;

public class DalOrderItem
{
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
    #region Get
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

}
