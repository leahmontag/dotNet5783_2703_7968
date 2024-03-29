﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    string orderItemPath = @"XMLOrderItem.xml";
    string configPath = @"Config.xml";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(DO.OrderItem myOrderItem)
    {
        XElement config = XMLTools.LoadAutoNumFromXMLElement(configPath);
        int autoNumber = (int)config.Element("_autoNumOrderItem");
        myOrderItem.OrderItemID = autoNumber;
        autoNumber++;
        config.Element("_autoNumOrderItem")!.SetValue(autoNumber);
        XMLTools.SaveAutoNumToXMLElement(config, configPath);
        List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        if (ListOrderItems.Exists(item => item?.OrderItemID == myOrderItem.OrderItemID))
            throw new DuplicatesException("exist orderItem");
        ListOrderItems.Add(myOrderItem);
        XMLTools.SaveListToXMLSerializer(ListOrderItems, orderItemPath);
        return myOrderItem.OrderItemID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int OrderItemId)
    {
        List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        ListOrderItems.Remove(ListOrderItems.FirstOrDefault(item => item?.OrderItemID == OrderItemId)
        ?? throw new NotFoundException("not exist OrderItem"));
        XMLTools.SaveListToXMLSerializer(ListOrderItems, orderItemPath);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(Func<DO.OrderItem?, bool>? d)
    {
        List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);

        try
        {
            var orderItem = ListOrderItems.Where(item => item != null && d != null && d(item) == true).First();
            return new DO.OrderItem()
            {
                //OrderID = orderItem?.OrderID ?? 0,
                OrderItemID = orderItem?.OrderItemID ?? 0,
                ProductID = orderItem?.ProductID ?? 0,
                Name = orderItem?.Name ?? "",
                Amount = orderItem?.Amount ?? 0,
                Price = orderItem?.Price ?? 0
            };
        }
        catch (Exception)
        {
            throw new NotFoundException("not exist OrderItem");
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? d = null)
    {
        List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);

        if (d == null)
        {
            try
            {
                return from OrderItem in ListOrderItems
                       select OrderItem;
            }
            catch (Exception)
            {
                throw new NotFoundException("can't display all OrderItem");
            }
        }
        else
            return ListOrderItems.Where(item => (item != null && d(item) == true)).ToList();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem? myOrdetItem)
    {
        List<DO.OrderItem?> ListOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);

        if (ListOrderItems.Exists(item => item?.OrderItemID == myOrdetItem?.OrderItemID))
        {
            ListOrderItems.Remove(ListOrderItems.FirstOrDefault(item => item?.OrderItemID == myOrdetItem?.OrderItemID));
            ListOrderItems.Add(myOrdetItem);
            return;
        }
        else
            throw new NotFoundException("not exist product");
        XMLTools.SaveListToXMLSerializer(ListOrderItems, orderItemPath);
    }
}
