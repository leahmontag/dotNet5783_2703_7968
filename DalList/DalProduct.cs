using DO;
namespace Dal;

/// <summary>
/// class DalProduct.
/// </summary>
public class DalProduct
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
        } while (existProductID(myID).ID!=0);

        myProduct.ID = myID;
        DataSource.ProductArr[DataSource.Config.ProductIndex++] = myProduct;
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
            if (DataSource.ProductArr[i].ID == myProduct.ID)
            {
                DataSource.ProductArr[i] = myProduct;
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
            if (DataSource.ProductArr[i].ID == ProductId)
            {
                DataSource.ProductArr[i] = DataSource.ProductArr[DataSource.Config.ProductIndex];
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
            Product CurrentProduct = DataSource.ProductArr[i];
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
    public Product[] GetAll()
    {
        int size = DataSource.Config.ProductIndex;
        Product[] newProductArr = new Product[size];
        for (int i = 0; i < size; i++)
        {
            newProductArr[i] = DataSource.ProductArr[i];
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
            p=DataSource.ProductArr[i];
            if (p.ID == num)
                return p;
        }
        p.ID = 0;
        return p;
    }
    #endregion
}