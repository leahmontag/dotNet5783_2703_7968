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
        int myID;
        Random rand = new Random();
        do
        {
            myID = rand.Next(100000, 999999);
        } while (existProductID(myID).ID != 0);

        myProduct.ID = myID;
        _productList.Add(myProduct);

        return myProduct.ID;
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
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.
                _productList[i] = myProduct;

                //if (myProduct.Name != "")
                //    _productList[i].Name = "fdvfdvv";
                //if (myProduct.InStock != 0)
                //   _productList[i].InStock = myProduct.InStock;
                //if (myProduct.Price != 0.0)
                //   _productList[i].Price = myProduct.Price;
                //if (myProduct.Category != null)
                //   _productList[i].Category = myProduct.Category;
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
        for (int i = 0; i < _productList.Count; i++)
        {
            if (_productList[i].ID == ProductId)
            {
                _productList[i] = _productList[_productList.Count];
                return;
            }
        }
        throw new NotFoundException("not exist product");
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
        List<Product> _newProductList;
        _newProductList = _productList;
        return _newProductList;
        //int size = DataSource.Config.ProductIndex;
        //Product[] newProductArr = new Product[size];
        //for (int i = 0; i < size; i++)
        //{
        //    newProductArr[i] = DataSource._productArr[i];
        //}
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
            if (item.ID == num)
                return item;
        }
        Product p = new Product() { ID = 0 };
        return p;
    }
    #endregion
}