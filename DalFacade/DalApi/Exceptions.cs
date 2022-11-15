using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal class Exceptions
    {
        public string NotFoundExceptions { get; set; }//עבור עדכון, מחיקה או בקשה
        public string DuplicatesException { get; set; }//עבור הוספה של אובייקט עם מזהה שכבר קיים

        public Exceptions(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}
