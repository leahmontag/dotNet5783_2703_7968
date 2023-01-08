namespace BlApi;

/// <summary>
/// interface IProduct
/// </summary>
public interface IProduct
{
    /// <summary>
    /// func Create
    /// </summary>
    /// <param name="val"></param>
    /// <returns>int</returns>
    public int Create(BO.Product val);

    /// <summary>
    /// func Delete
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void Delete(int val);

    /// <summary>
    /// func Update
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void Update(BO.Product val);

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Product</returns>
    public BO.Product GetByManager(Func<DO.Product?, bool>? d);

    /// <summary>
    /// func get all productsitem
    /// </summary>
    /// <param name="cartBL"></param>
    /// <param name="d"></param>
    /// <returns>IEnumerable<BO.ProductItem?></returns>
    public IEnumerable<BO.ProductItem?> GetAllProductsItemFromCatalog(BO.Cart cartBL, Func<DO.Product?, bool>? d = null);

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Product</returns>
    public BO.ProductItem GetProductFromCatalog(BO.Cart cart, Func<DO.Product?, bool>? d);

    /// <summary>
    /// func GetAll
    /// </summary>
    /// <param name="val"></param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<BO.ProductForList?> GetAll(Func<DO.Product?, bool>? d = null);
}