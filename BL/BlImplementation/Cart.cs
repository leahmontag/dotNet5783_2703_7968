using BlApi;
using BO;
using DalApi;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BlImplementation;
internal class Cart : ICart
{
    private IDal Dal = new Dal.DalList();
    #region Confirm order
    public int ConfirmOrder(BO.Cart cartBL)
    {
        try
        {
            bool flag = false;
            IEnumerable<DO.Product> listOfProducts = Dal.Product.GetAll();
            foreach (BO.OrderItem item in cartBL.Items)
            {
                foreach (DO.Product item2 in listOfProducts)
                {
                    if (item.Name == item2.Name && item.Amount > 0 && item.Amount < item2.InStock)
                    {
                        flag = true;
                    }
                }
            }
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-
         9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);
            bool isValidEmail = regex.IsMatch(cartBL.CustomerEmail);
            if (!flag || cartBL.CustomerName == null || cartBL.CustomerAddress == null || !isValidEmail && cartBL.CustomerEmail != null)
                throw new Exception();
            DO.Order order = new DO.Order();
            order.CustomerName = cartBL.CustomerName;
            order.CustomerEmail = cartBL.CustomerEmail;
            order.CustomerAdress = cartBL.CustomerAddress;
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.MinValue;
            order.DeliveryDate = DateTime.MinValue;
            int id = Dal.Order.Create(order);
            foreach (BO.OrderItem item in cartBL.Items)
            {
                DO.OrderItem orderItem = new DO.OrderItem();
                orderItem.OrderID = id;
                orderItem.OrderItemID = item.OrderItemID;
                orderItem.ProductID = item.ProductID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
                orderItem.Name = item.Name;
                Dal.OrderItem.Create(orderItem);
                foreach(DO.Product item2 in listOfProducts)
                {
                    if(item.Name == item2.Name)
                    {
                        DO.Product productToUpdate = new DO.Product();
                        productToUpdate.InStock -= item.Amount;
                        Dal.Product.Update(productToUpdate);
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        throw new NotImplementedException();
    }
    #endregion
    #region Add new item to cart
    public BO.Cart Create(BO.Cart CartBL, int OrderItemID)
    {
        IEnumerable<DO.Product> productList = Dal.Product.GetAll();
        // הקניות בסל הפריט את מחפש//
        foreach (var item in CartBL.Items)
        {
            if (item.OrderItemID == OrderItemID)//found item
            {
                //     במלאי כמות למצוא כדי הנתונים בשכבת הפריט את מחפש//
                foreach (DO.Product productItem in productList)
                {
                    if (productItem.ID == OrderItemID)//fount product in dal
                        if (productItem.InStock > 0)//cheaking if instock
                        {
                            item.Amount++;//update current cart.
                            item.Price = item.Price + productItem.Price;

                        }
                        else

                            throw new Exception();
                    return CartBL;
                }
            }
        }

        //if item not in cart:
        foreach (DO.Product productItem in productList)//loop to find product in dal
        {
            if (productItem.ID == OrderItemID)//fount product in dal
                if (productItem.InStock > 0)//cheaking if instock
                {
                    //create a new order item

                    BO.OrderItem tempOrderItem = new BO.OrderItem()

                    {
                        Amount = 1,
                        Name = productItem.Name,
                        OrderItemID = OrderItemID,
                        Price = productItem.Price,
                        ProductID = productItem.ID,
                        TotalPrice = productItem.Price

                    };

                    //update CartBL in BO, add he product and update total price
                    CartBL.Items.Add(tempOrderItem);
                    CartBL.TotalPrice = CartBL.TotalPrice + productItem.Price;
                }
                else
                    throw new Exception();
            return CartBL;
        }
        return CartBL;
    }
    #endregion
    #region Update cart
    public BO.Cart Update(BO.Cart CartBL, int OrderItemID, int newAmount = 0)
    {
        IEnumerable<DO.OrderItem> OrderItemList = Dal.OrderItem.GetAll();
        var price = 0.0;
        int oldAmount = 0;
        //fount product in dal and cheack his amount.
        foreach (var item in CartBL.Items)
        {
            if (item.OrderItemID == OrderItemID)
            {
                if (newAmount == 0)
                {
                    #region delete item

                    CartBL.TotalPrice -= (item.Price * item.Amount);//updatcart price
                    CartBL.Items.Remove(item);//delete item from cart

                    return CartBL;
                    #endregion

                }
                else if (item.Amount < newAmount)
                {
                    #region update amount in dal
                    //fount product in dal and cheack his amount.
                    foreach (var itemInOrderItemList in OrderItemList)

                    {
                        if (itemInOrderItemList.OrderItemID == OrderItemID &&
                        itemInOrderItemList.Amount >= newAmount)
                        {
                            price = itemInOrderItemList.Price;

                            oldAmount = item.Amount;
                            CartBL.TotalPrice = CartBL.TotalPrice + newAmount

                            * price - price * oldAmount;
                            return CartBL;
                        }
                        else
                        {

                            throw new Exception();
                        }
                    }
                }
                #endregion
                else if (item.Amount > newAmount)
                {
                    #region update cart and order item

                    item.Amount = newAmount;
                    CartBL.TotalPrice = CartBL.TotalPrice - item.Price *

                    (item.Amount - newAmount);//update cart price
                    return CartBL;
                    #endregion

                }
            }
        }
        return CartBL;
    }
    #endregion
}