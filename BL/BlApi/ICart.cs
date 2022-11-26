using BO;
namespace BlApi;

/// <summary>
/// interface ICart
/// </summary>
public interface ICart
{
    /// <summary>
    /// func Create
    /// </summary>
    /// <param name="val"></param>
    /// <returns>int</returns>
    public BO.Cart Create(BO.Cart CartBL, int OrderItemID);

    /// <summary>
    /// func Update
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public BO.Cart Update(BO.Cart CartBL, int OrderItemID, int newAmount);

    /// <summary>
    /// func Update
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public int ConfirmOrder(Cart cartBL);
}