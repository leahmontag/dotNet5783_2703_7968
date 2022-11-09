namespace DO;

public struct Enums
{
    public enum Category
    {
        eyeMakeup,
        lipMakeup,
        facialMmakeup,
        brushes,
        cultivation
    }

    public enum Choice
    {
        exit,
        product,
        order,
        orderItem
    }

    public enum crud
    {
        create = 1,
        get,
        getAll,
        update,
        delete
    }
}
