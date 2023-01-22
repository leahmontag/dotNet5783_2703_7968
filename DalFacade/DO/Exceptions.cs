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
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

[Serializable]
public class XMLFileLoadCreateException : Exception
{
    public string xmlFilePath;
    public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
    public XMLFileLoadCreateException(string xmlPath, string message) :
        base(message)
    { xmlFilePath = xmlPath; }
    public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
        base(message, innerException)
    { xmlFilePath = xmlPath; }
    public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
}

