using BlApi;
namespace BlImplementation;

/// <summary>
/// class Bl
/// </summary>
sealed public class Bl : IBl
{
    /// <summary>
    /// IProduct Product
    /// </summary>
    public IProduct Product => new Product();

    /// <summary>
    /// IOrder Order
    /// </summary>
    public IOrder Order => new Order();

    /// <summary>
    /// ICart Cart
    /// </summary>
    public ICart Cart =>new Cart();
}
