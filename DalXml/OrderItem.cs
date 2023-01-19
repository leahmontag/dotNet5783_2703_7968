using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
internal class OrderItem : IOrderItem
{
    public int Create(DO.OrderItem val)
    {
        throw new NotImplementedException();
    }

    public void Delete(int val)
    {
        throw new NotImplementedException();
    }

    public bool exisOrderItemID(int checkID)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Get(Func<DO.OrderItem?, bool>? d)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? d = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem? val)
    {
        throw new NotImplementedException();
    }
}
