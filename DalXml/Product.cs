using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Globalization;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Xml.Serialization;


internal class Product : IProduct
{
    string productPath = @"XMLProduct.xml";
    public int Create(DO.Product myProduct)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);

        XElement pro1 = (from p in productRootElem.Elements()
                         where int.Parse(p.Element("ID").Value) == myProduct.ID
                         select p).FirstOrDefault();

        if (pro1 != null)
            throw new DO.XMLFileLoadCreateException("Duplicate products ID");

        XElement productElem = new XElement("Product", new XElement("ID", myProduct.ID),
                              new XElement("Name", myProduct.Name),
                              new XElement("ID", myProduct.ID),
                              new XElement("Price", myProduct.Price),
                              new XElement("InStock", myProduct.InStock),
                              new XElement("Category", myProduct.Category));
        productRootElem.Add(productElem);

        XMLTools.SaveListToXMLElement(productRootElem, productPath);

        return myProduct.ID;
    }

    public void Delete(int ProductID)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);

        XElement pro = (from p in productRootElem.Elements()
                        where int.Parse(p.Element("ID").Value) == ProductID
                        select p).FirstOrDefault();

        if (pro != null)
        {
            pro.Remove(); //<==>   Remove pro from productRootElem
            XMLTools.SaveListToXMLElement(productRootElem, productPath);
        }
        else
            throw new DO.XMLFileLoadCreateException($"bad product id: {ProductID}");

    }

    public DO.Product Get(Func<DO.Product?, bool>? d)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);

        DO.Product p = (from per in productRootElem.Elements()
                        let p1 = new DO.Product()
                        {
                            ID = Int32.Parse(per.Element("ID").Value),
                            Name = per.Element("Name").Value,
                            Price = Int32.Parse(per.Element("Price").Value),
                            InStock = Int32.Parse(per.Element("InStock").Value),
                            Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), per.Element("Category").Value),
                        }
                        where d(p1)
                        select p1).FirstOrDefault();

        if (p.ID == null)
            throw new DO.XMLFileLoadCreateException("not exist product id");
        return p;
    }


    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? d = null)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
        var products = from p in productRootElem.Elements()
                       select new DO.Product()
                       {
                           ID = Int32.Parse(p.Element("ID").Value),
                           Name = p.Element("Name").Value,
                           Price = Int32.Parse(p.Element("Price").Value),
                           InStock = Int32.Parse(p.Element("InStock").Value),
                           Category = (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), p.Element("Category").Value)
                       }
                      ;
        return d != null ? products.Cast<DO.Product?>().Where(d) : products.Cast<DO.Product?>();
    }

    public void Update(DO.Product? myProduct)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(productPath);
        XElement per = (from p in productRootElem.Elements()
                        where int.Parse(p.Element("ID").Value) == myProduct?.ID
                        select p).FirstOrDefault();

        if (per != null)
        {
            per.Element("ID")!.Value = myProduct?.ID.ToString();
            per.Element("Name")!.Value = myProduct?.Name;
            per.Element("Price")!.Value = myProduct?.Price.ToString();
            per.Element("InStock")!.Value = myProduct?.InStock.ToString();
            per.Element("Category")!.Value = myProduct?.Category.ToString();


            XMLTools.SaveListToXMLElement(productRootElem, productPath);
        }
        else
            throw new DO.XMLFileLoadCreateException($"bad person id: {myProduct?.ID.ToString()}");
    }
}
