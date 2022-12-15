using BO;
using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

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
            Category = (DO.Enums.Category?)productBL.Category,
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
        IEnumerable<DO.Order?> ordersList = _dal.Order.GetAll();
        //checking that item not exist in any orders.
        foreach (DO.Order? item in ordersList)
        {
            if (item?.ID == productID)
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
    public IEnumerable<BO.ProductForList?> GetAll(Func<DO.Product?, bool>? d = null)
    {
        IEnumerable<DO.Product?> productsList = _dal.Product.GetAll(d != null ? d : null);
        
        List<BO.ProductForList?> ProductForList = new List<BO.ProductForList?>();
        try
        {
            foreach (DO.Product? item in productsList)
            {
                    ProductForList.Add(new ProductForList()
                    {
                        ID = item?.ID ?? 0,
                        Name = item?.Name ?? "",
                        Price = item?.Price ?? 0,
                        Category = (BO.Enums.Category?)item?.Category
                    });
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
    /// get by manager function
    /// </summary>
    /// <param name="productID"></param>
    /// <returns>BO.Product</returns>
    /// <exception cref="Exception"></exception>
    #region Get by manager
    public BO.Product GetByManager(Func<DO.Product?, bool>? d)
    {
        BO.Product productBL;
        DO.Product productDal;
        try
        {
            productDal = _dal.Product.Get(d);
            productBL = new BO.Product()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Category = (BO.Enums.Category?)productDal.Category,
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
    public ProductItem GetProductFromCatalog(BO.Cart cartBL, Func<DO.Product?, bool>? d = null)
    {
        cartBL.Items ??= new List<BO.OrderItem?>() { };
        BO.ProductItem productItemBL = new();
        try
        {
            if (d != null)
            {
                DO.Product productDal = _dal.Product.Get(d);
                int productIndex = cartBL.Items.FindIndex(x => x?.ProductID == productDal.ID);
                if (cartBL!= null&&cartBL.Items!=null)
                {
                    int amount = productIndex == -1 ? 0 : cartBL.Items[productIndex].Amount;
                    productItemBL = new BO.ProductItem()
                    {
                        ID = productDal.ID,
                        Amount = amount > 0 ? amount : 0,
                        Category = (BO.Enums.Category?)productDal.Category,
                        Name = productDal.Name,
                        Price = productDal.Price,
                        InStock = productDal.InStock > 0
                    };
                    return productItemBL;
                }
            }
        }
        catch (DO.NotFoundException exp)
        {
            throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
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
        //Before moving to DAL which is not possible directly, we will go through DO.
        DO.Product productDal = new DO.Product()
        {
            ID = productBL.ID,
            Name = productBL.Name,
            Price = productBL.Price,
            InStock = productBL.InStock,
            Category = (DO.Enums.Category?)productBL.Category
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

