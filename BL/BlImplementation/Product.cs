using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
   private IDal Dal = new Dal.DalList();
    public int Create(BO.Product productBL)
    {
        //בדיקת תקינות בשכבה הלוגיתBO
        if (productBL.Name == " " || productBL.ID <= 0 || productBL.Price <= 0 || productBL.InStock <= 0 || productBL.Category == null)
            throw new Exception();

        //לפני המעבר לדל שאינו אפשרי ישירות נעבור דרך דו
        DO.Product productDal = new DO.Product()
        {
            ID = productBL.ID,
            Name = productBL.Name,
            Price = productBL.Price,
            Category = (DO.Enums.Category)productBL.Category,
            InStock = productBL.InStock,
        };

        int id = 0;
        try
        {
            id = Dal.Product.Create(productDal);
        }
        catch (Exception)
        {
            throw;
        }
        return id;
    }



    public void Delete(int productID)
    {
        IEnumerable<BO.Order> ordersList = BO.Order.Get();
        foreach (BO.Order product in ordersList)
        {
            if (product.ID == productID)
                throw new Exception();
        }
        try
        {
            Dal.Product.Delete(productID);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public IEnumerable<BO.Product> GetAll()
    {
        throw new Exception();

    }

    public BO.Product GetByManagger(int val)
    {

        throw new Exception();

    }

    public ProductItem GetProductFromCatalog(int val, BO.Cart cart)
    {



        throw new Exception();

    }

    public void Update(BO.Product productBL)
    {

        //בדיקת תקינות בשכבה הלוגיתBO
        if (productBL.Name == "" || productBL.ID <= 0 || productBL.Price <= 0 || productBL.InStock <= 0 || productBL.Category == null)
            throw new Exception();

        //לפני המעבר לדל שאינו אפשרי ישירות נעבור דרך דו
        DO.Product productDal = new DO.Product()
        {
            ID = productBL.ID,
            Name = productBL.Name,
            Price = productBL.Price,
            InStock = productBL.InStock,
            Category = (DO.Enums.Category)productBL.Category
        };

        try
        {
            Dal.Product.Update(productDal);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
