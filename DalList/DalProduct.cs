using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;

/// <summary>
/// class DalProduct.
/// </summary>
internal class DalProduct : IProduct
{

    /// <summary>
    /// add a new product.
    /// </summary>
    #region Create
    public int Create(Product myProduct)
    {
        try
        {
            foreach (var item in _productList)
            {
                if (myProduct.ID == item.ID)
                    throw new DuplicatesException("id of product is exist");
            }
            _productList.Add(myProduct);
            return myProduct.ID;
        }
        catch (DuplicatesException exc)
        {

            throw  exc;

        }
        catch (Exception)
        {

            throw new OperationFailedException("operation failed");
        }
    }
    #endregion

    /// <summary>
    /// update product.
    /// </summary>
    #region Update
    public void Update(Product myProduct)
    {
        for (int i = 0; i < _productList.Count; i++)
        {
            if (_productList[i].ID == myProduct.ID)
            {
                _productList[i] = myProduct;
                return;
            }
        }
        throw new NotFoundException("not exist product");
    }
    #endregion

    /// <summary>
    /// delete product.
    /// </summary>
    #region Delete
    public void Delete(int ProductId)
    {
        try
        {
            for (int i = 0; i < _productList.Count; i++)
            {
                foreach (var item in _productList)
                {
                    if (item.ID == ProductId)
                    {
                        _productList.Remove(item);
                        return;
                    }
                }
            }
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist product");
        }
    }
    #endregion

    /// <summary>
    /// get product.
    /// </summary>
    #region Get
    public Product Get(int ProductId)
    {
        foreach (var item in _productList)
        {
            if (item.ID == ProductId)
                return item;
        }
        throw new NotFoundException("not exist product");
    }
    #endregion

    /// <summary>
    /// get all products.
    /// </summary>
    #region GetAll
    public IEnumerable<Product> GetAll()
    {
        try
        {
            List<Product> _newProductList;
            _newProductList = _productList;
            return _newProductList;
        }
        catch (Exception)
        {

            throw new NotFoundException("can't display all products");

        }

    }
    #endregion

    /// <summary>
    /// checking if product is exist  (by ID).
    /// </summary>
    #region checking if product is exist
    public Product existProductID(int num)
    {
        foreach (var item in _productList)
        {
            //if (item?.ID == num)
               // return item;
        }
        Product p = new Product() { ID = 0 };
        return p;
    }
    #endregion
}