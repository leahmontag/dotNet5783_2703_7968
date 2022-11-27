namespace DO;

/// <summary>
/// class NotFoundException
/// </summary>
public class NotFoundException : Exception//עבור עדכון, מחיקה או בקשה
{
    public NotFoundException(string msg) : base(msg) { }
}

/// <summary>
/// class DuplicatesException
/// </summary>
public class DuplicatesException : Exception//עבור הוספה של אובייקט עם מזהה שכבר קיים
{
    public DuplicatesException(string msg) : base(msg) { }
}

/// <summary>
/// class OperationFailedException
/// </summary>
public class OperationFailedException : Exception//פעולה נכשלה
{
    public OperationFailedException(string msg) : base(msg) { }
}
