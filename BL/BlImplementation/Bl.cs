using BlApi;
namespace BlImplementation;

/// <summary>
/// class Bl
/// </summary>
sealed internal class Bl : IBl
{
    /// <summary>
    /// IProduct Product
    /// </summary>
    public IProduct Product { get; } = new Product();

    /// <summary>
    /// IOrder Order
    /// </summary>
    public IOrder Order { get; } = new Order();

    /// <summary>
    /// ICart Cart
    /// </summary>
    public ICart Cart { get; } = new Cart();

    //public IProduct Product => new Product();

    //public IOrder Order => new Order();

    //public ICart Cart => new Cart();
}
