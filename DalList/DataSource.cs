using DO;
using static DO.Enums;
namespace Dal;
internal static class DataSource
{
    internal static List<Product?> _productList = new();
    internal static List<Order?> _orderList = new();
    internal static List<OrderItem?> _orderItemList = new();
    internal static readonly Random _rand = new Random();
    static DataSource()
    {
        s_Initialize();
    }

    /// <summary>
    /// Initializing the entity arrays and sending to the add functions.
    /// </summary>
    private static void s_Initialize()
    {
        #region AddProduct
        Product newProduct = new Product();
        string[] name = { "eye shadow palette", "rubber", "mascara", "facial soap", "blush", "makeup", "makeup brush", "eyeshadow brush", "moisturizer", "lipstick" };
        double[] price = { 250, 80, 68, 73, 69, 189, 75, 45, 55, 35 };
        int[] inStock = { 17, 78, 3, 0, 26, 47, 212, 269, 0, 10 };
        Category[] categoryArr = {
            Category.eyeMakeup,
            Category.lipMakeup,
            Category.eyeMakeup,
            Category.cultivation,
            Category.facialMmakeup,
            Category.facialMmakeup,
            Category.brushes,
            Category.brushes,
            Category.cultivation,
            Category.lipMakeup
        };
        for (int i = 0; i < 10; i++)
        {
            int id = _rand.Next(100000, 999999);
            {
                for (int j = 0; j < _productList.Count; j++)
                {
                    if (_productList[j]!=null && _productList[j]!?.ID == id)
                    {
                        id = _rand.Next(100000, 999999);
                        j = 0;
                    }
                }
            }
            newProduct.ID = id;
            newProduct.Name = name[i];
            newProduct.Price = price[i];
            newProduct.InStock = inStock[i];
            newProduct.Category = categoryArr[i];
            addProduct(newProduct);
        }
        #endregion
        #region AddOrder
        Order newOrder = new Order();
        string[] customerName = { "Efrat", "Elisheva", "Leah", "Rachel", "Esther", "Batya", "Tamar", "Miriam", "Hadassah", "Shira", "Dini", "Ila", "Mali", "Ruti", "Naama", "Gila", "Yael", "Penina", "Tzipi", "Tova" };
        string[] customerEmail = { "r33197903@gmail.com", "et3367903@gmail.com", "p33197903@gmail.com", "o33197903@gmail.com", "m33197903@gmail.com", "b33197903@gmail.com", "v33197903@gmail.com", "l33197903@gmail.com", "a33197903@gmail.com", "d33197903@gmail.com", "r33198903@gmail.com", "et8367903@gmail.com", "n33197903@gmail.com", "x33197903@gmail.com", "c33197903@gmail.com", "j33197903@gmail.com", "k33197903@gmail.com", "f33197903@gmail.com", "g33197903@gmail.com", "p33197903@gmail.com" };
        string[] customerAdress = { "SHOREK River", "Nahal Dolev", "Nahal Noam", "Nahal Ayalon", "Nahal Oriya", "Nahal Micah", "Nahal Akziv", "Jordan river", "Levi Eshkol", "Levi Eshkol", "SHOREK River", "Nahal Dolev", "Nahal Noam", "Nahal Ayalon", "Nahal Oriya", "Nahal Micah", "Nahal Akziv", "Jordan river", "Levi Eshkol", "Levi Eshkol" };
        for (int i = 0; i < 20; i++)
        {
            newOrder.ID = Config.AutoNumOrder;
            newOrder.CustomerName = customerName[i % 20];
            newOrder.CustomerEmail = customerEmail[i % 20];
            newOrder.CustomerAdress = customerAdress[i % 20];
            /// <summary>
            /// For order date we rand a date from the last 25 days.
            ///And then we rand a random date for shipdate and delivery
            ///So on average 80 % of the orders have a shipdate date
            ///And 60 % will have a delivery date and the rest we have set to a min value.
            /// </summary>
            #region rand of date
            var start = DateTime.Now.AddDays(-25);
            int range = (DateTime.Today - DateTime.Today.AddDays(-25)).Days;
            var result = start.AddDays(_rand.Next(range));
            newOrder.OrderDate = result;
            DateTime? randOfShipDate = newOrder.OrderDate + new TimeSpan(_rand.Next(0, 10), _rand.Next(2, 10), _rand.Next(0, 59), _rand.Next(0, 59));
        if (randOfShipDate > DateTime.Now)
        {
                newOrder.ShipDate = null;
                newOrder.DeliveryDate = null;
            }
            else
            {
                newOrder.ShipDate = randOfShipDate;
                DateTime? randOfDeliveryDate = newOrder.ShipDate + new TimeSpan(_rand.Next(0, 10), _rand.Next(2, 10), _rand.Next(0, 59), _rand.Next(0, 59));
        if (randOfDeliveryDate > DateTime.Now)
                    newOrder.DeliveryDate = null;
                else
                    newOrder.DeliveryDate = randOfDeliveryDate;
            }
            #endregion
            addOrder(newOrder);
        }
        #endregion
        #region AddOrderItem
        OrderItem newOrderItem = new OrderItem();
        for (int i = 0; i < 40; i++)
        {
            newOrderItem.OrderItemID = Config.AutoNumOrderItem;
            int rng = _rand.Next(0, _productList.Count);
            if (_productList[rng]!= null)
            {
                //היה כאן 3 סימני שאלה לבדוק אם צריך אותם
                newOrderItem.ProductID = _productList[rng]?.ID ?? 0;
                newOrderItem.OrderID = _orderList[i % 20]?.ID ?? 0;
                newOrderItem.Price = _productList[rng]?.Price ?? 0;
                newOrderItem.Amount = _rand.Next(1, 10);
                newOrderItem.Name = _productList[rng]!?.Name ?? "";
                addOrderItem(newOrderItem);
            }

        }
        #endregion
    }
    #region AddFunctions
    /// <summary>
    /// adding new product.
    /// </summary>
    private static void addProduct(Product newProduct)
    {
        _productList.Add(newProduct);
    }
    /// <summary>
    /// adding new order.
    /// </summary>
    private static void addOrder(Order newOrder)
    {
        _orderList.Add(newOrder);
    }
    /// <summary>
    /// adding new order item.
    /// </summary>
    private static void addOrderItem(OrderItem newOrderItem)
    {
        _orderItemList.Add(newOrderItem);
    }
    #endregion


    #region class Config
    /// <summary>
    /// class config.
    /// </summary>
    internal static class Config
    {
        /// <summary>
        /// static autoNumOrder.
        /// </summary>
        private static int _autoNumOrder = 99999;
        /// <summary>
        /// static autoNumOrderItem.
        /// </summary>
        private static int _autoNumOrderItem = 99999;
        /// <summary>
        /// static auto NumOrder.
        /// </summary>
        public static int AutoNumOrder { get { _autoNumOrder++; return _autoNumOrder; } }
        /// <summary>
        /// static auto NumOrder items.
        /// </summary>
        public static int AutoNumOrderItem { get { _autoNumOrderItem++; return _autoNumOrderItem; } }
    }
    #endregion
}
