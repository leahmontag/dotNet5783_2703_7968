using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Data;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

internal class Order : IOrder
{
    string orderPath = @"OrdersXml.xml";
    string configPath = @"configXml.xml";

    public int Create(DO.Order order)
    {
        XElement ordersRootElem = XMLTools.LoadListFromXMLElement(orderPath);
        XElement config = XMLTools.LoadAutoNumFromXMLElement(configPath);
        int autoNumber = (int)config.Element("OrderID");
        order.ID = autoNumber;
        autoNumber++;
        config.Element("orderID")!.SetValue(autoNumber);
        XMLTools.SaveAutoNumToXMLElement(config, configPath);
        XElement order1 = (from o in ordersRootElem.Elements()
                           where int.Parse(o.Element("ID").Value) == order.ID
                           select o).FirstOrDefault();

        if (order1 != null)
            throw new DuplicatesException("exist order");

        XElement orderElem = new XElement("order", new XElement("ID", order.ID),
                              new XElement("CustomerAdress", order.CustomerAdress?.ToString()),
                              new XElement("CustomerEmail", order.CustomerEmail?.ToString()),
                              new XElement("CustomerName", order.CustomerName?.ToString()),
                              new XElement("DeliveryDate", order.DeliveryDate),
                              new XElement("OrderDate", order.OrderDate),
                              new XElement("ShipDate", order.ShipDate));
        ordersRootElem.Add(orderElem);
        XMLTools.SaveListToXMLElement(ordersRootElem, orderPath);
        return order.ID;
    }

    public void Delete(int ID)
    {
        XElement ordersRootElem = XMLTools.LoadListFromXMLElement(orderPath);

        XElement order1 = (from o in ordersRootElem.Elements()
                           where int.Parse(o.Element("ID").Value) == ID
                           select o).FirstOrDefault();
        if (order1 != null)
        {
            order1.Remove();

            XMLTools.SaveListToXMLElement(ordersRootElem, orderPath);
        }
        else
            throw new NotFoundException("not exist order");
    }

    public DO.Order Get(Func<DO.Order?, bool>? d)
    {

        XElement ordersRootElem = XMLTools.LoadListFromXMLElement(orderPath);
        try
        {
            DO.Order order1 = (from singleOrder in ordersRootElem.Elements()
                               let p1 = new DO.Order()
                               {
                                   ID = Int32.Parse(singleOrder.Element("ID").Value),
                                   CustomerAdress = singleOrder.Element("CustomerAdress").Value,
                                   CustomerEmail = singleOrder.Element("CustomerEmail").Value,
                                   CustomerName = singleOrder.Element("CustomerName").Value,
                                   DeliveryDate = DateTime.Parse(singleOrder.Element("DeliveryDate").Value),
                                   OrderDate = DateTime.Parse(singleOrder.Element("OrderDate").Value),
                                   ShipDate = DateTime.Parse(singleOrder.Element("ShipDate").Value),
                               }
                               where d(p1)
                               select p1
                             ).FirstOrDefault();
            return order1;
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist order");
        }
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? d = null)
    {
        XElement ordersRootElem = XMLTools.LoadListFromXMLElement(orderPath);

        IEnumerable<DO.Order> w = from o in ordersRootElem.Elements()
                                  let o1 = new DO.Order()
                                  {
                                      ID = Int32.Parse(o.Element("ID").Value),
                                      CustomerAdress = o.Element("CustomerAdress").Value,
                                      CustomerEmail = o.Element("CustomerEmail").Value,
                                      CustomerName = o.Element("CustomerName").Value,
                                      DeliveryDate = DateTime.Parse(o.Element("DeliveryDate").Value),
                                      OrderDate = DateTime.Parse(o.Element("OrderDate").Value),
                                      ShipDate = DateTime.Parse(o.Element("ShipDate").Value),
                                  }
                                  where d(o1)
                                  select o1;
        List<DO.Order?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        return ListOrderItems.Where(item => (item != null && d(item) == true)).ToList();

    }

    public void Update(DO.Order? order)
    {
        XElement ordersRootElem = XMLTools.LoadListFromXMLElement(orderPath);


        XElement order1 = (from o in ordersRootElem.Elements()
                           where int.Parse(o.Element("ID").Value) == order.Value.ID
                           select o).FirstOrDefault();


        if (order1 != null)
        {
            order1.Element("ID").Value = order?.ID.ToString();
            order1.Element("CustomerAdress").Value = order?.CustomerAdress.ToString();
            order1.Element("CustomerEmail").Value = order?.CustomerEmail.ToString();
            order1.Element("CustomerName").Value = order?.CustomerName.ToString();
            order1.Element("DeliveryDate").Value = order?.DeliveryDate.ToString();
            order1.Element("OrderDate").Value = order?.OrderDate.ToString();
            order1.Element("ShipDate").Value = order?.ShipDate.ToString();

            XMLTools.SaveListToXMLElement(ordersRootElem, orderPath);
        }
        else
            throw new NotFoundException("not exist OrderItem");
    }
}

