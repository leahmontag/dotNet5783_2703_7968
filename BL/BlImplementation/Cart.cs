using BlApi;
using DalApi;

namespace BlImplementation;

internal class Cart : ICart
{

    IDal Dal = new Dal.DalList();

    public int ConfirmOrder(BO.Cart val)
    {
        throw new NotImplementedException();
    }

    public int Create(BO.Cart val)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Cart val)
    {
        throw new NotImplementedException();
    }
}
