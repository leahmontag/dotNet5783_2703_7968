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
                if (myProduct.ID == item?.ID)
                    throw new DuplicatesException("id of product is exist");
            }
            _productList.Add(myProduct);
            return myProduct.ID;
        }
        catch (DuplicatesException exc)
        {

            throw exc;

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
            if (_productList[i]!.Value.ID == myProduct.ID)
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
            if (_productList != null)
            {
                for (int i = 0; i < _productList.Count; i++)
                {

                    foreach (Product? item in _productList)
                    {
                            if (item?.ID == ProductId)
                        {
                            _productList.Remove(item);
                            return;
                        }
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
    public Product Get(Func<Product?, bool>? a )
    {
            foreach (Product? item in _productList)
            {
                if (item!=null && a!=null && a(item)==true)
                    return new Product() { Category=item.Value.Category,ID=item.Value.ID,Name=item.Value.Name,Price=item.Value.Price,InStock=item.Value.InStock};
            }
        throw new NotFoundException("not exist product");
    }
    #endregion

    /// <summary>
    /// get all products.
    /// </summary>
    #region GetAll
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? a = null)
    {
        if (a == null)
        {
            try
            {
                List<Product?> _newProductList;
                _newProductList = _productList;
                return _newProductList;
            }
            catch (Exception)
            {

                throw new NotFoundException("can't display all products");

            }
        }
        else
        {
            List<Product?> _newProductList;
            List<Product> _newProductList1 =new List<Product>();

            _newProductList = _productList;
            foreach (Product? item in _newProductList)
            {
                if (item!=null && a(item) == true)
                    _newProductList1.Add(item.Value);
            }
            return _newProductList;
        }

    }
    #endregion

    /// <summary>
    /// checking if product is exist  (by ID).
    /// </summary>
    #region checking if product is exist
    public Product existProductID(int num)
    {
            foreach (Product? item in _productList)
            {
                if (item?.ID == num)
                    return new Product() { Category = item.Value.Category, ID = item.Value.ID, Name = item.Value.Name, Price = item.Value.Price, InStock = item.Value.InStock };
            }
        Product p = new Product() { ID = 0 };
        return p;
    }
    #endregion
}