using DO;
namespace Dal;

/// <summary>
/// class DalOrder.
/// </summary>
public class DalOrder
{
    /// <summary>
    /// add new order.
    /// </summary>
    #region Create
    public int Create(Order myOrder)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == myOrder.ID)
                throw new Exception("exist order");
        }
        DataSource.OrderArr[DataSource.Config.OrderIndex] = myOrder;
        DataSource.Config.OrderIndex++;
        return myOrder.ID;
    }

    #endregion

    /// <summary>
    /// update order.
    /// </summary>
    #region Update
    public void Update(Order myOrder)
    {

        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == myOrder.ID)
            {
                DataSource.OrderArr[i] = myOrder;
                return;
            }
        }
        throw new Exception("not exist order");
    }
    #endregion

    /// <summary>
    /// delete order.
    /// </summary>
    #region Delete
    public void Delete(int OrderId)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == OrderId)
            {
                DataSource.OrderArr[i] = DataSource.OrderArr[DataSource.Config.OrderIndex];
                DataSource.Config.OrderIndex--;
                return;
            }
        }
        throw new Exception("not exist order");
    }
    #endregion

    /// <summary>
    /// get order.
    /// </summary>
    #region Get
    public Order Get(int OrderId)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            Order CurrentOrder = DataSource.OrderArr[i];
            if (CurrentOrder.ID == OrderId)
                return CurrentOrder;
        }
        throw new Exception("not exist order");
    }
    #endregion

    /// <summary>
    /// get all orders.
    /// </summary>
    #region GetAll
    public Order[] GetAll()
    {
        int size = DataSource.Config.OrderIndex;
        Order[] newOrderArr = new Order[size];
        for (int i = 0; i < size; i++)
        {
            newOrderArr[i] = DataSource.OrderArr[i];
        }
        return newOrderArr;
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
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == num)
                return true;
        }
        return false;
    }
    #endregion
}
