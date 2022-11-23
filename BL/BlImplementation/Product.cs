using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal Dal = new Dal.DalList();

    #region Add new product
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
    #endregion

    #region Delete product 
    public void Delete(int productID)
    {
        IEnumerable<DO.Order> ordersList = Dal.Order.GetAll();

        //checking that item not exist in any orders.
        foreach (DO.Order item in ordersList)
        {
            if (item.ID == productID)
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
    #endregion

    #region Get all products
    public IEnumerable<BO.ProductForList> GetAll()
    {
        IEnumerable<DO.Product> productsList = Dal.Product.GetAll();
        List<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
        int i = 0;
        foreach (var item in productsList)
        {
            ProductForList[i].ID = item.ID;
            ProductForList[i].Name = item.Name;
            ProductForList[i].Price = item.Price;
            ProductForList[i].Category = (BO.Enums.Category)item.Category;
            i++;
        }
        return ProductForList;
    }
    #endregion

    #region Get by managger
    public BO.Product GetByManagger(int productID)
    {
        BO.Product productBL;
        if (productID <= 0)
            throw new Exception();
        else
        {
            DO.Product productDal;
            try
            {
                productDal = Dal.Product.Get(productID);
                productBL = new BO.Product()
                {
                    ID = productDal.ID,
                    Name = productDal.Name,
                    Category = (BO.Enums.Category)productDal.Category,
                    InStock = productDal.InStock,
                    Price = productDal.Price,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        return productBL;
    }
    #endregion

    #region Get product fromCatalog
    public ProductItem GetProductFromCatalog(int productID, BO.Cart cartBL)
    {
        BO.ProductItem productItemBL;

        if (productID <= 0)
            throw new Exception();
        else
        {
            DO.Product productDal;
            int amount = 0;
            //int inStock = 0;

            foreach (var item in cartBL.Items)
            {
                if (item.ProductID == productID)
                {
                    amount = item.Amount;
                }
            }
            try
            {
                productDal = Dal.Product.Get(productID);
                productItemBL = new BO.ProductItem()
                {
                    ID = productDal.ID,
                    Amount = amount,
                    Category = (BO.Enums.Category)productDal.Category,
                    Name = productDal.Name,
                    Price = productDal.Price,
                    InStock = productDal.InStock

                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        return productItemBL;
    }
    #endregion

    #region Update product 
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
    #endregion

}
