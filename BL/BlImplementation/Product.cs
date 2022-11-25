using BO;
using DalApi;
namespace BlImplementation;
internal class Product : BlApi.IProduct
{
    private IDal Dal = new Dal.DalList();
    /// <summary>
    /// create function
    /// </summary>
    /// <param name="productBL"></param>
    /// <returns>int</returns>
    /// <exception cref="Exception"></exception>
    #region Add new product
    public int Create(BO.Product productBL)
    {
        //cheacking values in BO
        if (productBL.Name == " " || productBL.Price <=
        0 || productBL.InStock <= 0 /*|| productBL.Category == null*/)
            throw new Exception();
        // דו דרך נעבור ישירות אפשרי שאינו לדל המעבר לפני//
        DO.Product productDal = new DO.Product()
        {
            //ID = productBL.ID,
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

    /// <summary>
    /// delete function
    /// </summary>
    /// <param name="productID"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// get all product function
    /// </summary>
    /// <returns>IEnumerable<BO.ProductForList></returns>
    #region Get all products
    public IEnumerable<BO.ProductForList> GetAll()
    {
        IEnumerable<DO.Product> productsList = Dal.Product.GetAll();
        List<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
        foreach (var item in productsList)
            ProductForList.Add(new ProductForList()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = (BO.Enums.Category)item.Category
            });
        return ProductForList;
    }
    #endregion

    /// <summary>
    /// get by manager function
    /// </summary>
    /// <param name="productID"></param>
    /// <returns>BO.Product</returns>
    /// <exception cref="Exception"></exception>
    #region Get by manager
    public BO.Product GetByManager(int productID)
    {
        BO.Product productBL;
        if (productID <= 0)
            throw new Exception();
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
        return productBL;
    }
    #endregion

    /// <summary>
    /// Get Product From Catalog function
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cartBL"></param>
    /// <returns>ProductItem</returns>
    /// <exception cref="Exception"></exception>
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
            foreach (var item in cartBL.Items)
            {
                if (item.ProductID == productID)
                {
                    amount = item.Amount;
                }
            }
            try
            {
                bool boolInstock=false;
                productDal = Dal.Product.Get(productID);
                if (productDal.InStock > 0)
                    boolInstock =true;
                productItemBL = new BO.ProductItem()
                {
                    ID = productDal.ID,
                    Amount = amount,
                    Category = (BO.Enums.Category)productDal.Category,
                    Name = productDal.Name,
                    Price = productDal.Price,
                    InStock= boolInstock
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

    /// <summary>
    /// update function
    /// </summary>
    /// <param name="productBL"></param>
    /// <exception cref="Exception"></exception>
    #region Update product
    public void Update(BO.Product productBL)
    {
        // BOהלוגית בשכבה תקינות בדיקת//
        if (productBL.Name == "" || productBL.ID <= 0 || productBL.Price <= 0
        || productBL.InStock <= 0 /*|| productBL.Category == null*/)
            throw new Exception();
        //  דו דרך נעבור ישירות אפשרי שאינו לדל המעבר לפני//
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