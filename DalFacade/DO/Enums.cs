namespace DO;

/// <summary>
/// structure for enums. 
/// </summary>
public struct Enums
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

    /// <summary>
    /// enum Choice. 
    /// </summary>
    public enum Choice
    {
        exit,
        product,
        order,
        orderItem
    }

    /// <summary>
    /// enum Crud. 
    /// </summary>
    public enum Crud
    {
        create = 1,
        get,
        getAll,
        update,
        delete,
        GetByProductIDAndOrderID,
        GetOrderItemsByOrderID
    }
}
