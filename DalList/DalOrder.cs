using DalApi;
using DO;
namespace Dal;

/// <summary>
/// class DalOrder.
/// </summary>
internal class DalOrder:IOrder
{
    /// <summary>
    /// add new order.
    /// </summary>
    #region Create
    public int Create(Order myOrder)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource._orderArr[i].ID == myOrder.ID)
                throw new Exception("exist order");
        }
        DataSource._orderArr[DataSource.Config.OrderIndex++] = myOrder;
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
            if (DataSource._orderArr[i].ID == myOrder.ID)
            {
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.
                if (myOrder.CustomerName != " ")
                    DataSource._orderArr[i].CustomerName = myOrder.CustomerName;
                if (myOrder.CustomerEmail != " ")
                    DataSource._orderArr[i].CustomerEmail = myOrder.CustomerEmail;
                if (myOrder.CustomerAdress != " ")
                    DataSource._orderArr[i].CustomerAdress = myOrder.CustomerAdress;
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
            if (DataSource._orderArr[i].ID == OrderId)
            {
                DataSource._orderArr[i] = DataSource._orderArr[DataSource.Config.OrderIndex--];
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
            Order CurrentOrder = DataSource._orderArr[i];
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
            newOrderArr[i] = DataSource._orderArr[i];
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
            if (DataSource._orderArr[i].ID == num)
                return true;
        }
        return false;
    }
    #endregion
}
