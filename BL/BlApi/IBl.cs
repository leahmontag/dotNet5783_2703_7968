namespace BlApi;

/// <summary>
/// interface IBl
/// </summary>
public interface IBl
{
    /// <summary>
    /// IProduct Product
    /// </summary>
    public IProduct Product { get; }

    /// <summary>
    ///  IOrder Order
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// ICart Cart 
    /// </summary>
    public ICart Cart { get; }
}
