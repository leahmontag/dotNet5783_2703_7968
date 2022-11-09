using DO;
using static DO.Enums;

namespace Dal;
public static class DataSource
{
    internal static Product[] ProductArr = new Product[50];
    internal static Order[] OrderArr = new Order[100];
    internal static OrderItem[] OrderItemArr = new OrderItem[200];
    internal static Category[] categoriesArr = new Category[5];
    internal static readonly Random rand = new Random();
    static DataSource()
    {
        Console.Write("in datas");
        s_Initialize();
    }

    private static void s_Initialize()
    {
        #region AddProduct
        Product newProduct = new Product();
        string[] Name = { "פלטת צלליות", "אודם", "מסקרה", "סבון פנים", "סומק", "מייקאפ", "מברשת למיקאפ", "מברשת לצלליות", "קרם לחות", "שפתון" };
        double[] Price = { 250, 80, 68, 73, 69, 189, 75, 45, 55, 35 };
        int[] InStock = { 17, 78, 3, 0, 26, 47, 212, 269, 0, 10 };
        Category[] CategoryArr = {
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
            int id = rand.Next(100000, 999999);
            for (int j = 0; j < Config.ProductIndex; j++)
            {
                if (ProductArr[j].ID == id)
                {
                    id = rand.Next(100000, 999999);
                    j = 0;
                }
            }
            newProduct.ID = id;
            newProduct.Name = Name[i];
            newProduct.Price = Price[i];
            newProduct.InStock = InStock[i];
            newProduct.Category = CategoryArr[i % 5];
            addProduct(newProduct);
            Console.Write("Config.ProductIndex");//testing
            Console.Write(Config.ProductIndex);//testing

        }
        #endregion
        #region AddOrder
        Order newOrder = new Order();
        string[] CustomerName = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        string[] CustomerEmail = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        string[] CustomerAdress = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        DateTime[] OrderDate = { };
        DateTime[] ShipDate = { };
        DateTime[] DeliveryDate = { };

        for (int i = 0; i < 20; i++)
        {
            newOrder.ID = Config.AutoNumOrder;
            newOrder.CustomerName = CustomerName[i % 10];
            newOrder.CustomerEmail = CustomerEmail[i % 10];
            newOrder.CustomerAdress = CustomerAdress[i % 10];
            newOrder.OrderDate = DateTime.MinValue;
            newOrder.ShipDate = newOrder.OrderDate.Add(new TimeSpan(rand.Next(2, 10), rand.Next(0, 59), rand.Next(0, 59)));
            newOrder.DeliveryDate = newOrder.ShipDate.Add(new TimeSpan(rand.Next(0, 10), rand.Next(2, 10), rand.Next(0, 59), rand.Next(0, 59)));
            addOrder(newOrder);
        }
        #endregion
        #region AddOrderItem
        OrderItem newOrderItem = new OrderItem();
        for (int i = 0; i < 40; i++)
        {
            newOrderItem.OrderItemID = Config.AutoNumOrderItem;
            newOrderItem.ProductID = ProductArr[rand.Next(0, Config.ProductIndex)].ID;
            newOrderItem.OrderID = OrderArr[rand.Next(0, Config.OrderIndex)].ID;
            newOrderItem.Price = rand.Next(100, 900);
            newOrderItem.Amount = rand.Next(1, 10);
            addOrderItem(newOrderItem);
        }
        #endregion
    }
    #region AddFunctions
    private static void addProduct(Product newProduct)
    {
        ProductArr[Config.ProductIndex++] = newProduct;
    }
    private static void addOrder(Order newOrderArr)
    {
        OrderArr[Config.OrderIndex++] = newOrderArr;
    }
    private static void addOrderItem(OrderItem newOrderItem)
    {
        OrderItemArr[Config.OrderItemIndex++] = newOrderItem;
    }
    #endregion


    #region class Config
    internal static class Config
    {
        internal static int ProductIndex = 0;
        internal static int OrderIndex = 0;
        internal static int OrderItemIndex = 0;
        private static int _autoNumOrder = 99999;
        private static int _autoNumOrderItem = 99999;

        public static int AutoNumOrder { get { _autoNumOrder++; return _autoNumOrder; } }
        public static int AutoNumOrderItem { get { _autoNumOrderItem++; return _autoNumOrderItem; } }

    }
    #endregion

    public static void CallData()
    {
        //return;
    }
}
