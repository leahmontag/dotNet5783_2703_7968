using BlApi;
using BO;
using DalApi;
namespace BlImplementation;

/// <summary>
/// class Cart 
/// </summary>
internal class Cart : ICart
{
    private IDal Dal = new Dal.DalList();
    /// <summary>
    /// function Add new item to cart
    /// </summary>
    /// <param name="CartBL"></param>
    /// <param name="orderItemID"></param>
    /// <returns> BO.Cart</returns>
    /// <exception cref="Exception"></exception>
    #region Add new item to cart
    public BO.Cart Create(BO.Cart CartBL, int orderItemID)
    {
        IEnumerable<DO.Product> productList = Dal.Product.GetAll();

        // Looking for the item in the shopping cart
        #region search item in cart
        if (CartBL.Items != null)
        {
            foreach (var item in CartBL.Items)
            {
                if (item.OrderItemID == orderItemID)//found item
                {
                    //Searches for the item in the data layer to find an in-stock quantity.
                    foreach (DO.Product productItem in productList)
                    {
                        if (productItem.ID == orderItemID)
                        { //fount product in dal
                            if (productItem.InStock > 0)//cheaking if instock
                            {
                                item.Amount++;//update current cart.
                                item.TotalPrice = item.Amount * productItem.Price;
                                CartBL.TotalPrice = CartBL.TotalPrice + productItem.Price;
                            }
                            else

                                throw new Exception();
                            return CartBL;
                        }
                    }
                }
            }
        }
        #endregion

        //if item not in cart:
        #region add a new item to cart
        foreach (var productItem in productList)//loop to find product in dal
        {
            if (productItem.ID == orderItemID)//fount product in dal
            {
                if (productItem.InStock > 0)//cheaking if instock
                {
                    //updateing CartBL in BO, and adding his product and update total price
                    #region cart is empty
                    if (CartBL.Items == null)
                    {
                        List<OrderItem> newOrderItems = new List<OrderItem>() {new BO.OrderItem(){
                            Amount = 1,
                            Name = productItem.Name,
                            OrderItemID = orderItemID,
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
                            OrderItemID = orderItemID,
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
                    throw new Exception("not in stock");
            }
        }
        #endregion
        return CartBL;
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
        IEnumerable<DO.OrderItem> OrderItemList = Dal.OrderItem.GetAll();
        IEnumerable<DO.Product> productList = Dal.Product.GetAll();

        var price = 0.0;
        int oldAmount = 0;
        //fount product in dal and cheack his amount.
        if (CartBL.Items != null)
        {
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
                        foreach (var productItem in productList)
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
        }
        return CartBL;
    }
    #endregion

    /// <summary>
    /// function Confirm order
    /// </summary>
    /// <param name="cartBL"></param>
    /// <returns>int</returns>
    /// <exception cref="NotImplementedException"></exception>
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
            //   Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-
            //9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            //   RegexOptions.CultureInvariant | RegexOptions.Singleline);
            //   bool isValidEmail = regex.IsMatch(cartBL.CustomerEmail);
            //   if (!flag || cartBL.CustomerName == null || cartBL.CustomerAddress == null || !isValidEmail && cartBL.CustomerEmail != null)
            //       throw new Exception();
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
                foreach (DO.Product item2 in listOfProducts)
                {
                    if (item.Name == item2.Name)
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
}
