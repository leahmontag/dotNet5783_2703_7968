using Amazon.Runtime.Internal.Util;
using BlApi;
using BO;
using DO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BlImplementation;

/// <summary>
/// class Product
/// </summary>
internal class Product : BlApi.IProduct
{
    DalApi.IDal? _dal = DalApi.Factory.Get();

    /// <summary>
    /// create function
    /// </summary>
    /// <param name="productBL"></param>
    /// <returns>int</returns>
    /// <exception cref="Exception"></exception>
    #region Add new product
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(BO.Product productBL)
    {
        lock (_dal)
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
    }
    #endregion

    /// <summary>
    /// delete function
    /// </summary>
    /// <param name="productID"></param>
    /// <exception cref="Exception"></exception>
    #region Delete product
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int productID)
    {
        lock (_dal)
        {
            IEnumerable<DO.Order?> ordersList = _dal.Order.GetAll();
            ordersList.ToList();
            if (ordersList.ToList().Exists(item => item?.ID == productID))
                throw new cannotDeletedItemException("An existing item in an order cannot be deleted");
            try
            {
                _dal.Product.Delete(productID);
            }

            catch (DO.NotFoundException exp)
            {
                throw new BO.cannotDeletedItemException("canot delete item ", exp);
            }
        }
    }
    #endregion

    /// <summary>
    /// get all product function
    /// </summary>
    /// <returns>IEnumerable<BO.ProductForList></returns>
    #region Get all products
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetAll(Func<DO.Product?, bool>? d = null)
    {
        lock (_dal)
        {
            try
            {
                IEnumerable<DO.Product?> productsList = _dal.Product.GetAll();
                IEnumerable<BO.ProductForList?> ProductForList = from DO.Product productsListDO in productsList
                                                                 where d == null || d(productsListDO)
                                                                 orderby productsListDO.ID
                                                                 select new BO.ProductForList()
                                                                 {
                                                                     ID = productsListDO.ID,
                                                                     Name = productsListDO.Name,
                                                                     Price = productsListDO.Price,
                                                                     Category = (BO.Enums.Category?)productsListDO.Category
                                                                 };
                return ProductForList;
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.FailedAddingProductException("Failed to display all items", exp);
            }
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetByManager(Func<DO.Product?, bool>? d)
    {
        lock (_dal)
        {
            try
            {
                DO.Product? productDal;
                productDal = _dal?.Product.Get(d);
                return new BO.Product()
                {
                    ID = productDal?.ID ?? 0,
                    Name = productDal?.Name ?? "",
                    Category = (BO.Enums.Category?)productDal?.Category,
                    InStock = productDal?.InStock ?? 0,
                    Price = productDal?.Price ?? 0
                };
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
            }
        }
    }
    #endregion

    /// <summary>
    /// func GetProductForList
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    /// <exception cref="BO.ProductIsNotAvailableException"></exception>
    #region Get ProductForList
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductForList GetProductForList(Func<DO.Product?, bool>? d)
    {
        lock (_dal)
        {
            try
            {
                DO.Product? productDal;
                productDal = _dal?.Product.Get(d);
                return new BO.ProductForList()
                {
                    ID = productDal?.ID ?? 0,
                    Name = productDal?.Name ?? "",
                    Category = (BO.Enums.Category?)productDal?.Category,
                    Price = productDal?.Price ?? 0
                };
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
            }
        }
    }
    #endregion

    /// <summary>
    /// func get all productsitem
    /// </summary>
    /// <param name="cartBL"></param>
    /// <param name="d"></param>
    /// <returns>IEnumerable<BO.ProductItem?></returns>
    #region Get All ProductsItem From Catalog
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem?> GetAllProductsItemFromCatalog(BO.Cart cartBL, Func<DO.Product?, bool>? d = null)
    {
        lock (_dal)
        {
            try
            {

                IEnumerable<DO.Product?> productsList = _dal.Product.GetAll();
                IEnumerable<BO.ProductItem?> ProductItemForList = new List<BO.ProductItem>();
                if (d == null)
                {
                    ProductItemForList = from DO.Product productsListDO in productsList
                                         let x = productsListDO.ID
                                         select GetProductFromCatalog(cartBL, x => x?.ID.ToString() == productsListDO.ID.ToString());
                }
                else
                {
                    ProductItemForList = from DO.Product productsListDOWithCondtion in productsList
                                         where d(productsListDOWithCondtion)
                                         let item = productsListDOWithCondtion.ID
                                         select GetProductFromCatalog(cartBL, item => item?.ID.ToString() == productsListDOWithCondtion.ID.ToString());
                }





                return ProductItemForList;
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.FailedAddingProductException("Failed to display all items", exp);
            }
        }
    }
    #endregion


    /// <summary>
    /// Get Product From Catalog function
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cartBL"></param>
    /// <returns>ProductItem</returns>
    /// <exception cref="Exception"></exception>
    #region Get  ProductsItem from Catalog
    [MethodImpl(MethodImplOptions.Synchronized)]
    public ProductItem GetProductFromCatalog(BO.Cart cartBL, Func<DO.Product?, bool>? d = null)
    {
        lock (_dal)
        {
            cartBL.Items ??= new List<BO.OrderItem?>() { };
            BO.ProductItem productItemBL = new();
            try
            {
                if (d != null)
                {
                    DO.Product? productDal = _dal?.Product.Get(d);
                    int productIndex = cartBL.Items.FindIndex(x => x?.ProductID == productDal?.ID);
                    if (cartBL != null && cartBL.Items != null)
                    {
                        int amount = productIndex == -1 ? 0 : cartBL.Items[productIndex].Amount;
                        productItemBL = new BO.ProductItem()
                        {
                            ID = productDal?.ID ?? 0,
                            Amount = amount > 0 ? amount : 0,
                            Category = (BO.Enums.Category?)productDal?.Category,
                            Name = productDal?.Name,
                            Price = productDal?.Price ?? 0,
                            InStock = productDal?.InStock > 0
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
    }
    #endregion

    /// <summary>
    /// update function
    /// </summary>
    /// <param name="productBL"></param>
    /// <exception cref="Exception"></exception>
    #region Update product
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product productBL)
    {
        lock (_dal)
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
                _dal?.Product.Update(productDal);
            }
            catch (DO.NotFoundException exp)
            {
                throw new BO.ProductIsNotAvailableException("Finding this product details failed due to not finding an item with such an ID", exp);
            }
        }
    }
    #endregion

}


