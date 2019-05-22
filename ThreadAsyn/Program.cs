using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAsyn
{
    class Program
    {
        public static string Gets { get { return "sdfsds"; } }
        static void Main(string[] args)
        {

            string id = "123,123";
            Console.WriteLine("shuj" + id.TrimEnd(','));
            //定义
            Thread thread = new Thread(new ThreadStart(Test));
            thread.IsBackground = true;
            thread.Start();
            for (var i = 0; i < 1000000; i++)
            {
                Console.WriteLine("主线程计数" + i);
            }
            //匿名
            Thread anonymousThread = new Thread(new ThreadStart(() =>
            {
                for (var i = 0; i < 1000000; i++)
                {
                    Console.WriteLine("后台线程计数" + i);
                    Thread.Sleep(100);
                }
            }));
            anonymousThread.IsBackground = true;//设置为后台线程
            anonymousThread.Start();
            List<int> list = new List<int>() { 1, 2, 3 };
            Parallel.ForEach(list, num =>
            {
                num--;
            });
        }
        //Thread 
        private static void Test()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Console.WriteLine("后台线程计数" + i);
                //Thread.Sleep(100);
            }
        }

        private static void TaskTest()
        {
            Task t = Task.Run(() =>
            {
                int ctr = 0;
                for (ctr = 0; ctr <= 1000000; ctr++)
                {
                    Console.WriteLine("Finished {0} loop iterations", ctr);
                }
            });
            t.Wait();
            Task t2 = Task.Factory.StartNew(() =>
            {
                int ctr = 0;
                for (ctr = 0; ctr <= 1000000; ctr++)
                {
                    Console.WriteLine("Finished {0} loop iterations", ctr);
                }
            });
            t2.Wait();
        }
    }
}
