using DO;

namespace Dal;

public class DalOrder
{
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

}
