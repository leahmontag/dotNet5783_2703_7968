using BlApi;
using DalApi;
using System.Net.Mail;

namespace BlImplementation;

/// <summary>
/// class Cart
/// </summary>
internal class Cart : ICart
{
    DalApi.IDal? _dal = DalApi.Factory.Get();

    /// <summary>
    /// function Add new item to cart
    /// </summary>
    /// <param name="CartBL"></param>
    /// <param name="orderItemID"></param>
    /// <returns> BO.Cart</returns>
    /// <exception cref="Exception"></exception>
    #region Add new item to cart
    public BO.Cart Create(BO.Cart CartBL, int productID)
    {
        try
        {
            IEnumerable<DO.Product?> productList = _dal.Product.GetAll();

            // Looking for the item in the shopping cart
            #region search item in cart
            if (CartBL.Items != null)
            {
                foreach (var item in CartBL.Items)
                {
                    if (item.ProductID == productID)//found item
                    {
                        //Searches for the item in the data layer to find an in-stock quantity.
                        foreach (DO.Product productItem in productList)
                        {
                            if (productItem.ID == productID)
                            { //fount product in dal
                                if (productItem.InStock > 0)//cheaking if instock
                                {
                                    item.Amount++;//update current cart.
                                    item.TotalPrice = item.Amount * productItem.Price;
                                    CartBL.TotalPrice = CartBL.TotalPrice + productItem.Price;
                                }
                                else

                                    throw new BO.ProductIsNotAvailableException("Product is not in stock");
                                return CartBL;
                            }
                        }
                    }
                }
            }
            #endregion

            //if item not in cart:
            #region add a new item to cart
            foreach (DO.Product productItem in productList)//loop to find product in dal
            {
                if (productItem.ID == productID)//fount product in dal
                {
                    if (productItem.InStock > 0)//cheaking if instock
                    {
                        //updateing CartBL in BO, and adding his product and update total price
                        #region cart is empty
                        if (CartBL.Items == null)
                        {
                            List<BO.OrderItem> newOrderItems = new List<BO.OrderItem>() {new BO.OrderItem(){
                            Amount = 1,
                            Name = productItem.Name,
                            Price = productItem.Price,
                            ProductID = productItem.ID,
                            TotalPrice = productItem.Price
                        }};

                            CartBL.Items = newOrderItems;
                        }
                        #endregion
                        #region cart is not empty
                        else
                        {
                        
                                CartBL.Items.Add(new BO.OrderItem()
                                {
                                    Amount = 1,
                                    Name = productItem.Name,
                                    Price = productItem.Price,
                                    ProductID = productItem.ID,
                                    TotalPrice = productItem.Price
                                });
                            
                        }
                        #endregion

                        CartBL.TotalPrice = CartBL.TotalPrice + productItem.Price;
                        return CartBL;
                    }
                    else
                        throw new BO.ProductIsNotAvailableException("not in stock");
                }
            }
            #endregion
            return CartBL;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.FailedToDisplayAllItemsException("Failed to display all items", exp);
        }
        catch (Exception)
        {

            throw new BO.FailedToDisplayAllItemsException("Operation failed");
        }
    }
    #endregion

    /// <summary>
    /// function Update cart
    /// </summary>
    /// <param name="CartBL"></param>
    /// <param name="OrderItemID"></param>
    /// <param name="newAmount"></param>
    /// <returns>BO.cart</returns>
    /// <exception cref="Exception"></exception>
    #region Update cart
    public BO.Cart Update(BO.Cart CartBL, int OrderItemID, int newAmount = 0)
    {
        try
        {
            IEnumerable<DO.OrderItem?> OrderItemList = _dal.OrderItem.GetAll();
            IEnumerable<DO.Product?> productList = _dal.Product.GetAll();

            var price = 0.0;
            int oldAmount = 0;
            bool flag = false;
            //fount product in dal and cheack his amount.
            if (CartBL.Items != null)
            {
                foreach (var item in CartBL.Items)
                {
                    if (item.OrderItemID == OrderItemID)
                    {
                        flag = true;
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
                            foreach (DO.Product productItem in productList)
                            {
                                if (productItem.ID == OrderItemID)
                                {
                                    if (productItem.InStock <= newAmount)
                                        throw new Exception();
                                    price = productItem.Price;
                                    oldAmount = item.Amount;
                                    item.Amount = newAmount;
                                    item.TotalPrice = price * newAmount;
                                    CartBL.TotalPrice = CartBL.TotalPrice + newAmount * price - price * oldAmount;
                                    return CartBL;
                                }
                            }
                        }
                        #endregion
                        else if (item.Amount > newAmount)
                        {
                            #region update cart and order item
                            CartBL.TotalPrice -= item.TotalPrice;
                            item.Amount = newAmount;
                            item.TotalPrice = item.Amount * item.Price;
                            CartBL.TotalPrice += item.TotalPrice;
                            return CartBL;
                            #endregion

                        }
                    }
                }
                if (flag == false)
                    throw new BO.ProductIsNotAvailableException(" product is not available");
            }
            return CartBL;
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.FailedToDisplayAllItemsException("Failed to get all items", exp);
        }
    }
    #endregion

    /// <summary>
    /// function Confirm order
    /// </summary>
    /// <param name="cartBL"></param>
    /// <returns>int</returns>
    /// <exception cref="NotImplementedException"></exception>
    #region Confirm order
    public void ConfirmOrder(BO.Cart cartBL)
    {
        try
        {
            bool flag = false;
            IEnumerable<DO.Product?> listOfProducts = _dal.Product.GetAll();
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
            var mail = new MailAddress(cartBL.CustomerEmail);
            bool isValidEmail = mail.Host.Contains(".");
            if (!flag || cartBL.CustomerName == null || cartBL.CustomerAddress == null || !isValidEmail && cartBL.CustomerEmail != null)
                throw new Exception();
            int id = _dal.Order.Create(new DO.Order()
            {
                CustomerName = cartBL.CustomerName,
                CustomerEmail = cartBL.CustomerEmail,
                CustomerAdress = cartBL.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null
            });
            foreach (BO.OrderItem item in cartBL.Items)
            {
                _dal.OrderItem.Create(new DO.OrderItem()
                {
                    OrderID = id,
                    OrderItemID = item.OrderItemID,
                    ProductID = item.ProductID,
                    Price = item.Price,
                    Amount = item.Amount,
                    Name = item.Name
                });
                foreach (DO.Product item2 in listOfProducts)
                {
                    if (item.Name == item2.Name)
                    {
                        DO.Product productToUpdate = new DO.Product();
                        productToUpdate = item2;
                        productToUpdate.InStock -= item.Amount;
                        _dal.Product.Update(productToUpdate);
                        return;
                    }
                }
            }
        }
        catch (DO.NotFoundException exp)
        {

            throw new BO.FailedToDisplayAllItemsException("Operation failed", exp);
        }
        catch (Exception)
        {

            throw new BO.FailedToDisplayAllItemsException("Operation failed");
        }
    }
    #endregion
}
