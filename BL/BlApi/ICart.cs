
using BO;

namespace BlApi;

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
    public void Update(Cart val);

    /// <summary>
    /// func Update
    /// </summary>
    /// <param name="val"></param>
    /// <returns>void</returns>
    public int ConfirmOrder(Cart val);
}
