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
        if (_productList.Exists(item => item?.ID == myProduct.ID))
            throw new DuplicatesException("id of product is exist");
        _productList.Add(myProduct);
        return myProduct.ID;
        //    try
        //    {
        //        if (_productList.Exists(item => item?.ID == myProduct.ID))
        //            throw new DuplicatesException("id of product is exist");
        //        _productList.Add(myProduct);
        //        return myProduct.ID;
        //    }
        //    catch (DuplicatesException exc)
        //    {
        //        throw exc;
        //    }
        //    catch (Exception)
        //    {
        //        throw new OperationFailedException("operation failed");
        //    }
    }
    #endregion

    /// <summary>
    /// update product.
    /// </summary>
    #region Update
    public void Update(Product? myProduct)
    {
        for (int i = 0; i < _productList.Count; i++)
        {
            if (_productList[i]!?.ID == myProduct?.ID)
            {
                Product myProductTemp = new();
                myProductTemp.ID = _productList[i]!?.ID ?? 0;
                if (myProduct?.Name != "")
                    myProductTemp.Name = myProduct?.Name;
                else
                    myProductTemp.Name = _productList[i]?.Name;
                if (myProduct?.Price > 0)
                    myProductTemp.Price = myProduct?.Price ?? 0;
                else
                    myProductTemp.Price = _productList[i]?.Price ?? 0;
                if (myProduct?.InStock >= 0)
                    myProductTemp.InStock = myProduct?.InStock ?? 0;
                else
                    myProductTemp.InStock = _productList[i]?.InStock ?? 0;
                myProductTemp.Category = myProduct?.Category;
                _productList[i] = myProductTemp;
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
    public void Delete(int ProductID)
    {
        _productList.Remove(_productList.FirstOrDefault(item => item?.ID == ProductID)
        ?? throw new NotFoundException("not exist product"));
    }
    #endregion

    /// <summary>
    /// get product.
    /// </summary>
    #region Get
    public Product Get(Func<Product?, bool>? a)
    {
        try
        {
            var product = _productList.Where(item => item != null && a != null && a(item) == true).First();
            return new Product() { Category = product?.Category ?? null, ID = product?.ID ?? 0, Name = product?.Name ?? "", Price = product?.Price ?? 0, InStock = product?.InStock ?? 0 };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist product");
        }
    }
    #endregion

    /// <summary>
    /// get all products.
    /// </summary>
    #region GetAll
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? d = null)
    {
        List<Product?> _newProductList;
        if (d == null)
        {
            try
            {
                _newProductList = _productList;
                return _newProductList;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all products");
            }
        }
        else
            return _productList.Where(item => (item != null && d(item) == true)).ToList();

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
                return new Product() { Category = item?.Category ?? null, ID = item?.ID ?? 0, Name = item?.Name ?? "", Price = item?.Price ?? 0, InStock = item?.InStock ?? 0 };
        }
        Product p = new Product() { ID = 0 };
        return p;
    }
    #endregion
}