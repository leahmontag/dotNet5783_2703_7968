using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.IO;
using System.Xml.Serialization;


internal class Product : IProduct
{
    string productPath = @"XMLProduct.xml";
    public int Create(DO.Product myProduct)
    {
        List<DO.Product?> ListProducts = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        if (ListProducts.Exists(item => item?.ID == myProduct.ID))
            throw new DuplicatesException("id of product is exist");
        ListProducts.Add(myProduct);
        XMLTools.SaveListToXMLSerializer(ListProducts, productPath);
        return myProduct.ID;
    }

    public void Delete(int ProductID)
    {
        List<DO.Product?> ListProducts = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        ListProducts.Remove(ListProducts.FirstOrDefault(item => item?.ID == ProductID)
      ?? throw new NotFoundException("not exist product"));

        XMLTools.SaveListToXMLSerializer(ListProducts, productPath);
    }

    public DO.Product Get(Func<DO.Product?, bool>? d)
    {
        List<DO.Product?> ListProducts = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        try
        {

            var product = ListProducts.Where(item => item != null && d != null && d(item) == true).FirstOrDefault();
            return new DO.Product()
            {
                Category = product?.Category ?? null,
                ID = product?.ID ?? 0,
                Name = product?.Name ?? "",
                Price = product?.Price ?? 0,
                InStock = product?.InStock ?? 0
            };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist product");
        }
    }


    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? d = null)
    {
        List<DO.Product?> ListProducts = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        if (d == null)
        {
            try
            {
                return from Product in ListProducts
                       select Product;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all products");
            }
        }
        else
            return ListProducts.Where(item => (item != null && d(item) == true)).ToList();
    }

    public void Update(DO.Product? myProduct)
    {
        List<DO.Product?> ListProducts = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        if (ListProducts.Exists(item => item?.ID == myProduct?.ID))
        {
            ListProducts.Remove(ListProducts.FirstOrDefault(item => item?.ID == myProduct?.ID));
            ListProducts.Add(myProduct);
            return;
        }
        else
            throw new NotFoundException("not exist product");
        XMLTools.SaveListToXMLSerializer(ListProducts, productPath);
    }
}
