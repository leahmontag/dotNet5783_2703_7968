namespace DO;

public class NotFoundException : Exception
{
    public string NotFoundExceptions { get; set; }//עבור עדכון, מחיקה או בקשה
    public NotFoundException(string msg) : base(msg) {}
}

public class DuplicatesException : Exception
{
    public string Duplicates { get; set; }//עבור הוספה של אובייקט עם מזהה שכבר קיים
    public DuplicatesException(string msg) : base(msg){}
}
