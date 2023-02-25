namespace BO;

/// <summary>
/// class Enums 
/// </summary>
public class Enums
{
    /// <summary>
    /// enum Category. 
    /// </summary>
    #region enum Category
    public enum Category
    {
        eyeMakeup,
        lipMakeup,
        facialMmakeup,
        brushes,
        cultivation
    }
    #endregion

    /// <summary>
    /// enum OrderStatus
    /// </summary>
    #region enum OrderStatus
    public enum OrderStatus
    {
        confirmed,
        send,
        provided
    }
    #endregion


    public enum OrderStatus2
    {
        Payed = 1,
        Shiped,
        Delivered,
    }

    /// <summary>
    /// enum Choice. 
    /// </summary>
    #region enum Choice
    public enum Choice
    {
        exit,
        product,
        order,
        cart
    }
    #endregion

    /// <summary>
    /// enum ProductEnum. 
    /// </summary>
    #region enum ProductEnum
    public enum ProductEnum
    {
        getAllProducts = 1,
        getProductByManager,
        getProductFromCatalog,
        addProduct,
        removeProduct,
        updateProduct
    }
    #endregion

    /// <summary>
    /// enum OrderEnum. 
    /// </summary>
    #region enum OrderEnum
    public enum OrderEnum
    {
        getAllOrders = 1,
        getOrder,
        updateShip,
        updateDelivery,
        trackingOfOrder,
        updateOrder
    }
    #endregion


    /// <summary>
    /// enum CartEnum. 
    /// </summary>
    #region enum CartEnum
    public enum CartEnum
    {
        addProductToCart = 1,
        updateCart,
        confirmCart,
        emptyingShoppingCart

    }
    #endregion
}
