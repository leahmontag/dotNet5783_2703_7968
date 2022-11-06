﻿using DO;


namespace Dal;

internal static class DataSource
{
    internal static Product[] ProductArr = new Product[50];
    internal static Order[] OrderArr = new Order[100];
    internal static OrderItem[] OrderItemArr = new OrderItem[200];
    internal static readonly Random rand=new Random();
     static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize()
    {
        #region AddProduct
        Product newProduct = new Product();
        string[] Name = {"A","B","C","D","E","F","G","H","I","J"};
        double[] Price= { 99,88,77,66,55,44,33,22,11,10};
        int[] InStock = {1,2,3,4,5,6,7,8,9,10};

        for (int i = 0; i <10; i++) {
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
            //  newProduct.Category
            AddProduct(newProduct);
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
            newOrder.CustomerName = CustomerName[i];
            newOrder.CustomerEmail = CustomerEmail[i];
            newOrder.CustomerAdress = CustomerAdress[i];
            newOrder.OrderDate = OrderDate[i];
            newOrder.ShipDate = ShipDate[i];
            newOrder.DeliveryDate = DeliveryDate[i];
            AddOrder(newOrder);
        }
        #endregion
        #region AddOrderItem
        OrderItem newOrderItem = new OrderItem();
        double[] OrderPrice = { 1,2,3,4,5,6,7,8,9,10 };
        int[] Amount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        for (int i = 0; i < 40; i++)
        {
            newOrderItem.OrderItemID = Config.AutoNumOrderItem;
            newOrderItem.ProductID = ProductArr[rand.Next(0,Config.ProductIndex)].ID;
            newOrderItem.OrderID = OrderArr[rand.Next(0,Config.OrderIndex)].ID;
            newOrderItem.Price = OrderPrice[i];
            newOrderItem.Amount=Amount[i];
            AddOrderItem(newOrderItem); 
        }
        #endregion
    }
    #region AddFunctions
    private static void AddProduct(Product newProduct)//random
    {
        ProductArr[Config.ProductIndex] = newProduct;
        Config.ProductIndex++;
    }
    private static void AddOrder(Order newOrderArr)
    {
        OrderArr[Config.OrderIndex] = newOrderArr;
        Config.OrderIndex++;
    }
    private static void AddOrderItem(OrderItem newOrderItem)
    {
        OrderItemArr[Config.OrderItemIndex] = newOrderItem;
        Config.OrderItemIndex++;
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

}
