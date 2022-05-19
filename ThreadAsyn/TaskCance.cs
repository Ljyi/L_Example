using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAsyn
{
    public class TaskCance
    {
        public static void Test()
        {
            // 正常任务
            Task task1 = new Task(() =>
            {
            });
            task1.Start();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            GetResult(task1.IsCanceled, task1.IsFaulted);
            Console.WriteLine("任务是否完成：" + task1.IsCompleted);
            Console.WriteLine("-------------------");

            // 异常任务
            Task task2 = new Task(() =>
            {
                throw new Exception();
            });
            task2.Start();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            GetResult(task2.IsCanceled, task2.IsFaulted);
            Console.WriteLine("任务是否完成：" + task2.IsCompleted);
            Console.WriteLine("-------------------");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            CancellationTokenSource cts = new CancellationTokenSource();
            // 取消任务
            Task task3 = new Task(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }, cts.Token);
            task3.Start();
            cts.Cancel();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            GetResult(task3.IsCanceled, task3.IsFaulted);
            Console.WriteLine("任务是否完成：" + task3.IsCompleted);
            Console.ReadKey();
        }

        public static void GetResult(bool isCancel, bool isFault)
        {
            if (isCancel == false && isFault == false)
                Console.WriteLine("没有异常发生");
            else if (isCancel == true)
                Console.WriteLine("任务被取消");
            else
                Console.WriteLine("任务引发了未经处理的异常");
        }
    }
}
