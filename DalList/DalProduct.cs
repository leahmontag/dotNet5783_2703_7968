using DalApi;
using DO;
namespace Dal;

/// <summary>
/// class DalProduct.
/// </summary>
internal class DalProduct//:IProduct
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
        DataSource._productArr[DataSource.Config.ProductIndex++] = myProduct;
        return myProduct.ID;
    }
    #endregion

    /// <summary>
    /// update product.
    /// </summary>
    #region Update
    public void Update(Product myProduct)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource._productArr[i].ID == myProduct.ID)
            {
                //Checking inputs from the user.
                // In case the input is 0, null or " "(depending on the type) the field will remain the same as the delay and will not change.
                if (myProduct.Name != "")
                    DataSource._productArr[i].Name = myProduct.Name;
                if (myProduct.InStock != 0)
                    DataSource._productArr[i].InStock = myProduct.InStock;
                if (myProduct.Price != 0.0)
                    DataSource._productArr[i].Price = myProduct.Price;
                if (myProduct.Category != null)
                    DataSource._productArr[i].Category = myProduct.Category;
                return;
            }
        }
        throw new Exception("not exist product");
    }
    #endregion

    /// <summary>
    /// delete product.
    /// </summary>
    #region Delete
    public void Delete(int ProductId)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource._productArr[i].ID == ProductId)
            {
                DataSource._productArr[i] = DataSource._productArr[DataSource.Config.ProductIndex];
                DataSource.Config.ProductIndex--;
                return;
            }
        }
        throw new Exception("not exist product");
    }
    #endregion

    /// <summary>
    /// get product.
    /// </summary>
    #region Get
    public Product Get(int ProductId)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            Product CurrentProduct = DataSource._productArr[i];
            if (CurrentProduct.ID == ProductId)
                return CurrentProduct;
        }
        throw new Exception("not exist product");
    }
    #endregion

    /// <summary>
    /// get all products.
    /// </summary>
    #region GetAll
    public IEnumerable<Product> GetAll()
    {
        int size = DataSource.Config.ProductIndex;
        Product[] newProductArr = new Product[size];
        for (int i = 0; i < size; i++)
        {
            newProductArr[i] = DataSource._productArr[i];
        }
        return newProductArr;
    }
    #endregion

    /// <summary>
    /// checking if product is exist  (by ID).
    /// </summary>
    #region checking if product is exist
    public Product existProductID(int num)
    {
        Product p = new Product();
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            p = DataSource._productArr[i];
            if (p.ID == num)
                return p;
        }
        p.ID = 0;
        return p;
    }
    #endregion
}