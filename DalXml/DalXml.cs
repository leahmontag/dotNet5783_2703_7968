using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }
       // private readonly string orderPath = "Order.xml";
        private readonly string orderItemPath = "OrderItem.xml";
      //  private readonly string productPath = "Product.xml";

        #region DalXml


        //private static object syncRoot = new object();
        //public static IDal Instance { get; } = new DalXml();

        //private DalXml() { }
        //static DalXml()
        //{
        //    if (Instance == null)
        //    {
        //        lock (syncRoot)
        //        {
        //            if (Instance == null)
        //                Instance = new DalXml();
        //        }
        //    }
        //}
        #endregion

        #region PRODUCT
        public IProduct Product { get; } = new Dal.Product();
        #endregion

        #region order
        public IOrder Order { get; } = new Dal.Order();
        #endregion

        #region orderitem
        public IOrderItem OrderItem { get; } = new Dal.OrderItem();
        #endregion

       

    }
}




