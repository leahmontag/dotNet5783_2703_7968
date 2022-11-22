using BO;
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
    public int Create(Product val);

    /// <summary>
    /// func Delete
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void Delete(Product val);

    /// <summary>
    /// func Update
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public void Update(Product val);
    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Product</returns>
    public Product GetByManagger(int val);

    /// <summary>
    /// func Get
    /// </summary>
    /// <param name="val"></param>
    /// <returns>Product</returns>
    public Product GetCatalog(int val);

    /// <summary>
    /// func GetAll
    /// </summary>
    /// <param name="val"></param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Product> GetAll();










}
