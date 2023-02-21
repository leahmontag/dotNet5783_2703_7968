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
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Xml.Linq;

internal class Order : IOrder
{
    string orderPath = @"XMLOrder.xml";
    string configPath = @"Config.xml";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(DO.Order order)
    {
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        XElement config = XMLTools.LoadAutoNumFromXMLElement(configPath);
        int autoNumber = (int)config.Element("_autoNumOrder");
        order.ID = autoNumber;
        autoNumber++;
        ListOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
        return order.ID;
        return order.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ID)
    {
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        ListOrders.Remove(ListOrders.FirstOrDefault(item => item?.ID == ID)
      ?? throw new NotFoundException("not exist order"));
        XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(Func<DO.Order?, bool>? d)
    {
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        try
        {

            var order = ListOrders.Where(item => item != null && d != null && d(item) == true).FirstOrDefault();
            return new DO.Order()
            {
                ID = order?.ID ?? 0,
                CustomerEmail = order?.CustomerEmail ?? "",
                CustomerAdress = order?.CustomerAdress ?? "",
                CustomerName = order?.CustomerName ?? "",
                DeliveryDate = order?.DeliveryDate ?? null,
                OrderDate = order?.OrderDate ?? null,
                ShipDate = order?.ShipDate ?? null
            };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist order");
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? d = null)
    {
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);

        if (d == null)
        {
            try
            {
                return from Order in ListOrders
                       select Order;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all Orders");
            }
        }
        else
            return ListOrders.Where(item => (item != null && d(item) == true)).ToList();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order? order)
    {
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);

        if (ListOrders.Exists(item => item?.ID == order?.ID))
        {
            ListOrders.Remove(ListOrders.FirstOrDefault(item => item?.ID == order?.ID));
            ListOrders.Add(order);
            XMLTools.SaveListToXMLSerializer(ListOrders, orderPath);
            return;
        }
        else
            throw new NotFoundException("not exist product");
    }
}

