using BlApi;
using BO;
using DalApi;
using System.Collections;

namespace BlImplementation;

internal class Cart : ICart
{

    private IDal Dal = new Dal.DalList();

    #region Confirm order
    public int ConfirmOrder(BO.Cart cartBL)
    {
       foreach (var item in cartBL.Items)
        {
        }
        //copy to new obj with instock-1
        DO.Product tempProduct = new DO.Product()
        {
            //ID = productItem.ID,
            //Name = productItem.Name,
            //Price = productItem.Price,
            //Category = (DO.Enums.Category)productItem.Category,
            //InStock = productItem.InStock - 1,//dropping  1
        };
        Dal.Product.Update(tempProduct);//update product in dal
        throw new NotImplementedException();
    }
    #endregion


    #region Add new item to cart
    public BO.Cart Create(BO.Cart CartBL, int OrderItemID)
    {
        IEnumerable<DO.Product> productList = Dal.Product.GetAll();

        //מחפש את הפריט בסל הקניות
        foreach (var item in CartBL.Items)
        {
            if (item.OrderItemID == OrderItemID)//found item
            {
                //מחפש את הפריט בשכבת הנתונים כדי למצוא כמות במלאי
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
                    CartBL.TotalPrice -= (item.Price * item.Amount);//update cart price
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
                        if (itemInOrderItemList.OrderItemID == OrderItemID && itemInOrderItemList.Amount >= newAmount)
                        {
                            price = itemInOrderItemList.Price;
                            oldAmount = item.Amount;
                            CartBL.TotalPrice = CartBL.TotalPrice + newAmount * price - item.Price * oldAmount;
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
                    CartBL.TotalPrice = CartBL.TotalPrice - item.Price * (item.Amount - newAmount);//update cart price
                    return CartBL;
                    #endregion
                }
            }
        }

        return CartBL;
    }
    #endregion
}
