using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Product : IProduct
{
    public int Create(DO.Product val)
    {
        throw new NotImplementedException();
    }

    public void Delete(int val)
    {
        throw new NotImplementedException();
    }

    public DO.Product existProductID(int num)
    {
        throw new NotImplementedException();
    }

    public DO.Product Get(Func<DO.Product?, bool>? d)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? d = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product? val)
    {
        throw new NotImplementedException();
    }
}
