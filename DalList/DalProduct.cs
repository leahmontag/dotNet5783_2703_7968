using DalApi;
using DO;
using System.Runtime.CompilerServices;
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Product myProduct)
    {
        if (_productList.Exists(item => item?.ID == myProduct.ID))
            throw new DuplicatesException("id of product is exist");
        _productList.Add(myProduct);
        return myProduct.ID;
    }
    #endregion

    /// <summary>
    /// update product.
    /// </summary>
    #region Update
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product? myProduct)
    {

        if (_productList.Exists(item => item?.ID == myProduct?.ID))
        {
            _productList.Remove(_productList.FirstOrDefault(item => item?.ID == myProduct?.ID));
            _productList.Add(myProduct);
            return;
        }
        else
            throw new NotFoundException("not exist product");
    }
    #endregion

    /// <summary>
    /// delete product.
    /// </summary>
    #region Delete
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Func<Product?, bool>? a)
    {
        try
        {

            var product = _productList.Where(item => item != null && a != null && a(item) == true).FirstOrDefault();
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
}