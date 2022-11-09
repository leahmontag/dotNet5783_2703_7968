using DO;
using static DO.Enums;

namespace Dal;
public static class DataSource //or internal to cheak
{
    internal static Product[] ProductArr = new Product[50];
    internal static Order[] OrderArr = new Order[100];
    internal static OrderItem[] OrderItemArr = new OrderItem[200];
    internal static Category[] categoriesArr = new Category[5];
    internal static readonly Random rand = new Random();
    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        #region AddProduct
        Product newProduct = new Product();
        string[] Name = { "eye shadow palette", "rubber", "mascara", "facial soap", "blush", "makeup", "makeup brush", "eyeshadow brush", "moisturizer", "lipstick" };
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
        }
        #endregion
        #region AddOrder
        Order newOrder = new Order();
        string[] CustomerName = { "Efrat", "Elisheva", "Leah", "Rachel", "Esther", "Batya", "Tamar", "Miriam", "Hadassah", "Shira", "Dini", "Ila", "Mali" ,"Ruti", "Naama", "Gila", "Yael", "Penina","Tzipi", "Tova" };
        string[] CustomerEmail = { "r33197903@gmail.com", "et3367903@gmail.com", "p33197903@gmail.com", "o33197903@gmail.com", "m33197903@gmail.com", "b33197903@gmail.com", "v33197903@gmail.com", "l33197903@gmail.com", "a33197903@gmail.com", "d33197903@gmail.com", "r33198903@gmail.com", "et8367903@gmail.com", "n33197903@gmail.com", "x33197903@gmail.com", "c33197903@gmail.com", "j33197903@gmail.com", "k33197903@gmail.com", "f33197903@gmail.com", "g33197903@gmail.com", "p33197903@gmail.com" };
        string[] CustomerAdress = { "SHOREK River","Nahal Dolev","Nahal Noam","Nahal Ayalon","Nahal Oriya","Nahal Micah","Nahal Akziv","Jordan river", "Levi Eshkol", "Levi Eshkol", "SHOREK River", "Nahal Dolev", "Nahal Noam", "Nahal Ayalon", "Nahal Oriya", "Nahal Micah", "Nahal Akziv", "Jordan river", "Levi Eshkol", "Levi Eshkol" };
        DateTime[] OrderDate = { };
        DateTime[] ShipDate = { };
        DateTime[] DeliveryDate = { };

        for (int i = 0; i < 20; i++)
        {
            newOrder.ID = Config.AutoNumOrder;
            newOrder.CustomerName = CustomerName[i % 20];
            newOrder.CustomerEmail = CustomerEmail[i % 20];
            newOrder.CustomerAdress = CustomerAdress[i % 20];
            Random gen = new Random();
            var start = DateTime.Now.AddDays(-15);
            int range = (DateTime.Today - DateTime.Today.AddDays(-15)).Days;
            var result = start.AddDays(gen.Next(range));
            newOrder.OrderDate = result;
           // Console.WriteLine("timer"+new TimeSpan(rand.Next(2, 10), rand.Next(0, 59), rand.Next(0, 59)));
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
            int rng = rand.Next(0, Config.ProductIndex);
            newOrderItem.ProductID = ProductArr[rng].ID;
            newOrderItem.OrderID = OrderArr[rand.Next(0, Config.OrderIndex)].ID;
            newOrderItem.Price = ProductArr[rng].Price;
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

    #region CallData function
    public static void CallData()
    {
    }
    #endregion
}
