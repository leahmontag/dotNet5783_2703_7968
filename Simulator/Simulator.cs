using BlApi;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{
    private static string? previousState;
    private static string? afterState;
    static bool finishFlag = false;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static void DoStop()
    {
        finishFlag = true;
        if (StopSimulator != null)
            StopSimulator("", EventArgs.Empty);
    }
    /// <summary>
    /// the function runs the program using maun thread
    /// </summary>
    public static void run()
    {
        Thread mainThreads = new Thread(new ThreadStart(chooseOrder));
        mainThreads.Start();
        return;
    }
    /// <summary>
    /// the function choose the order that has to be cared now.
    /// </summary>
    public static void chooseOrder()
    {
      //  IBl bl = new BlApi.IBl();
        BlApi.IBl? bl = BlApi.Factory.Get();
        int? id;
        while (!finishFlag)
        {
            id = bl.Order.SelectingOrderForTreatment();
            if (id == null)
                DoStop();
            else
            {
                BO.Order o = bl.Order.Get(item => item?.ID == id);
                previousState = o.Status.ToString();
                Random rand = new Random();
                int num = rand.Next(1000, 5000);
                Details details = new Details(o, num);
                if (ProgressChange != null)
                {
                    ProgressChange(null, details);
                }
                Thread.Sleep(num);
                afterState = (previousState == "Payed" ? bl.Order.UpdateShip((int)id) : bl.Order.UpdateDelivery((int)id)).Status.ToString();
            }
        }
        return;
    }
}

/// <summary>
/// class to define the things that are sended from the Simulator.cs to the window.
/// </summary>
public class Details : EventArgs
{
    public BO.Order order;
    public int seconds;
    public Details(BO.Order ord, int sec)
    {
        order = ord;
        seconds = sec;
    }
}
//using Amazon.DynamoDBv2.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DO;


//namespace Simulator;

//internal static class Simulator
//{
//    public static BlApi.IBl? bl = BlApi.Factory.Get();

//    private static Random rand = new(DateTime.Now.Second);

//    private static volatile bool isRunning = false;

//    public static BO.Order order;
//    public static BO.OrderTracking lastStatus;
//    public static BO.OrderTracking nextStatus;

//    public delegate void stopRunningEvent();
//    public static event stopRunningEvent? StopRunning;

//    public static void Run()
//    {
//        isRunning = true;
//        new Thread(() =>
//        {
//            while (isRunning)
//            {
//                int? orderId = bl.Order.SelectingOrderForTreatment();
//                if (orderId != null)
//                {
//                    order = bl.Order.Get(item => item?.ID == (int)orderId);
//                    int time = rand.Next(2, 8);
//                    if (order.ShipDate == null)
//                    {
//                        bl?.Order.UpdateShip((int)orderId);
//                    }
//                    else if (order.DeliveryDate == null)
//                    {
//                        bl?.Order.UpdateDelivery((int)orderId);
//                    }
//                }
//                else
//                {
//                    StopRunning();
//                    Thread.Sleep(1000);
//                }
//            }
//        }).Start();
//    }
//    public static void Stop() => isRunning = false;
//}








































////using Amazon.DynamoDBv2.Model;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using DO;


////namespace Simulator
////{

////    internal static class Simulator
////    {
////        static BlApi.IBl? bl = BlApi.Factory.Get();
////        static volatile bool _shouldStop;
////        static int? id=null;
////        public static void run()
////        {
////            while (_shouldStop)
////            {
////                id = bl.Order.SelectingOrderForTreatment();
////                if(id == null)
////                {
////                    Thread.Sleep(1000);
////                    continue;
////                }
////                //שיעשה את הפעולה שעליו לעשות!

////            }

////        }
////        public static void stop()
////        {

////        }
////        //Thread.CurrentThread.Name = "Main Thread";
////        //    Thread t1 = new Thread(run);
////        //t1.Name = "Second Thread";
////        //    t1.Start();
////    }
////}

