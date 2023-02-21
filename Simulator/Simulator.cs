using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{

    internal static class Simulator
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        static volatile bool _shouldStop;
        static int? id=null;
        public static void run()
        {
            while (_shouldStop)
            {
                id = bl.Order.SelectingOrderForTreatment();
                if(id == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                //שיעשה את הפעולה שעליו לעשות!
                
            }

        }
        public static void stop()
        {
      
        }
        //Thread.CurrentThread.Name = "Main Thread";
        //    Thread t1 = new Thread(run);
        //t1.Name = "Second Thread";
        //    t1.Start();
    }
}
