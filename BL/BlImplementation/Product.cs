using BO;
using DalApi;
using DO;

namespace BlImplementation;

/// <summary>
/// class Product
/// </summary>
internal class Product : BlApi.IProduct
{
    private IDal _dal = new Dal.DalList();
    /// <summary>
    /// create function
    /// </summary>
    /// <param name="productBL"></param>
    /// <returns>int</returns>
    /// <exception cref="Exception"></exception>
    #region Add new product
    public int Create(BO.Product productBL)
    {
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
            id = _dal.Product.Create(productDal);
        }
        catch (DO.OperationFailedException exp)
        {
            throw new BO.FailedAddingProductException("Failed adding product", exp);
        }
        catch (DO.DuplicatesException exp)
        {
            throw new BO.FailedAddingProductException("Failed adding product", exp);
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
        IEnumerable<DO.Order> ordersList = _dal.Order.GetAll();
        //checking that item not exist in any orders.
        foreach (DO.Order item in ordersList)
        {
            if (item.ID == productID)
                throw new cannotDeletedItemException("An existing item in an order cannot be deleted");
        }
        try
        {
            _dal.Product.Delete(productID);
        }

        catch (DO.NotFoundException exp)
        {
            throw new BO.cannotDeletedItemException("canot delete item ", exp);
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
        try
        {
            IEnumerable<DO.Product> productsList = _dal.Product.GetAll();
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
        catch (DO.NotFoundException exp)
        {
            throw new BO.FailedAddingProductException("Failed to display all items", exp);
        }
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
        DO.Product productDal;
        try
        {
            productDal = _dal.Product.Get(productID);
            productBL = new BO.Product()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Category = (BO.Enums.Category)productDal.Category,
                InStock = productDal.InStock,
                Price = productDal.Price,

            };
        }
        catch (DO.NotFoundException exp)
        {
            throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
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
        DO.Product productDal;
        int amount = 0;
        if (cartBL.Items != null)
        {
            foreach (var item in cartBL.Items)
            {
                if (item.ProductID == productID)
                {
                    amount = item.Amount;
                }
            }
        }
        try
        {
            bool boolInstock = false;
            productDal = _dal.Product.Get(productID);
            if (productDal.InStock > 0)
                boolInstock = true;
            productItemBL = new BO.ProductItem()
            {
                ID = productDal.ID,
                Amount = amount,
                Category = (BO.Enums.Category)productDal.Category,
                Name = productDal.Name,
                Price = productDal.Price,
                InStock = boolInstock
            };
        }
        catch (DO.NotFoundException exp)
        {
            throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
        }
        return productItemBL;
    }
    #endregion

    #region Get all products by category
    public IEnumerable<BO.ProductForList> GetAllByCategory(string category)
    {
        try
        {
            IEnumerable<DO.Product> productsList = _dal.Product.GetAll();
            List<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
            foreach (var item in productsList)
            {
                if (item.Category.ToString() == category)
                {
                    ProductForList.Add(new ProductForList()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Price = item.Price,
                        Category = (BO.Enums.Category)item.Category
                    });
                }
            }
            return ProductForList;
        }
        catch (DO.NotFoundException exp)
        {
            throw new BO.FailedAddingProductException("Failed to display all items", exp);
        }
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
        //Before moving to DAL which is not possible directly, we will go through DO.
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
            _dal.Product.Update(productDal);
        }
        catch (DO.NotFoundException exp)
        {
            throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
        }
    }
    #endregion
}

