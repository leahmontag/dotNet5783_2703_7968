namespace BO;

/// <summary>
/// class cannotDeletedItemException
/// </summary>
public class cannotDeletedItemException : Exception//can't delete this item.
{
    public cannotDeletedItemException(string msg) : base(msg) { }
    public cannotDeletedItemException(string msg, Exception exp) : base(msg, exp) { }

}

/// <summary>
/// class ProductIsNotAvailableException
/// </summary>

public class ProductIsNotAvailableException : Exception//can't found this product.
{
    public ProductIsNotAvailableException(string msg) : base(msg) { }
    public ProductIsNotAvailableException(string msg, Exception exp) : base(msg, exp) { }

}

/// <summary>
/// class FailedAddingProductException 
/// </summary>
public class FailedAddingProductException : Exception//can't add this product.
{
    public FailedAddingProductException(string msg) : base(msg) { }
    public FailedAddingProductException(string msg, Exception exp) : base(msg, exp) { }

}

/// <summary>
/// class FailedToDisplayAllItemsException
/// </summary>
public class FailedToDisplayAllItemsException : Exception//can't display all items.
{
    public FailedToDisplayAllItemsException(string msg) : base(msg) { }
    public FailedToDisplayAllItemsException(string msg, Exception exp) : base(msg, exp) { }

}

/// <summary>
/// class OperationFailedException
/// </summary>
public class OperationFailedException : Exception//operation failed.
{
    public OperationFailedException(string msg) : base(msg) { }
    public OperationFailedException(string msg, Exception exp) : base(msg, exp) { }

}

