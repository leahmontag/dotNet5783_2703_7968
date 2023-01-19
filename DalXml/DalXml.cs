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
        private static object syncRoot = new object();
        public static IDal Instance { get; } = new DalXml();

        private DalXml() { }
        static DalXml()
        {
            if (Instance == null)
            {
                lock (syncRoot)
                {
                    if (Instance == null)
                        Instance = new DalXml();
                }
            }
        }
        public IProduct Product { get; } = new Dal.Product();
        public IOrder Order { get; } = new Dal.Order();
        public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    }
}




