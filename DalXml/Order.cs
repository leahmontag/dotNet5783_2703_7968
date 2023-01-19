using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
internal class Order : IOrder
{
    public int Create(DO.Order val)
    {
        throw new NotImplementedException();
    }

    public void Delete(int val)
    {
        throw new NotImplementedException();
    }

    public bool exisOrderID(int checkID)
    {
        throw new NotImplementedException();
    }

    public DO.Order Get(Func<DO.Order?, bool>? d)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? d = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order? val)
    {
        throw new NotImplementedException();
    }
}

