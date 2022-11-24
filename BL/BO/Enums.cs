namespace BO;

public class Enums
{
    /// <summary>
    /// enum Category. 
    /// </summary>
    public enum Category
    {
        eyeMakeup,
        lipMakeup,
        facialMmakeup,
        brushes,
        cultivation
    }
    public enum OrderStatus
    {
        confirmed,
        send,
        provided
    }

    /// <summary>
    /// enum Choice. 
    /// </summary>
    public enum Choice
    {
        exit,
        product,
        order,
        cart
    }

    /// <summary>
    /// enum ProductEnum. 
    /// </summary>
    public enum ProductEnum
    {
        getAllProducts=1,
        getProductByManager,
        getProductFromCatalog,
        addProduct,
        removeProduct,
        updateProduct
    }

    /// <summary>
    /// enum OrderEnum. 
    /// </summary>
    public enum OrderEnum
    {
        getAllOrders=1,
        getOrder,
        updateShip,
        updateDelivery,
        trackingOfOrder,
        updateOrder
    }

    /// <summary>
    /// enum CartEnum. 
    /// </summary>
    public enum CartEnum
    {
        addProductToCart=1,
        updateCart,
        confirmCart,

    }
}
