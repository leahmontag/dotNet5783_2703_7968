using BlApi;
using DalApi;

namespace BlImplementation;

internal class Order :BlApi.IOrder
{
    private IDal Dal = new Dal.DalList();

    public BO.Order Get(int val)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public void UpdateDelivery(BO.Order val)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(BO.Order val)
    {
        throw new NotImplementedException();
    }

    public void UpdateShip(BO.Order val)
    {
        throw new NotImplementedException();
    }
}
