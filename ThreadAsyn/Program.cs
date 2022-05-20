using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// 参考文献 https://www.cnblogs.com/whuanle/category/1756558.html

namespace ThreadAsyn
{
    class Program
    {
        public static string Gets { get { return "sdfsds"; } }
        static void Main(string[] args)
        {
            ResetEventRun();
            //SemaphoreAddOne();
            //  SemaphoreTest();
            return;
            ThreadJoinTest();
            #region Thread
            Thread threadinfo = new Thread(ThreadInfo);
            threadinfo.Name = "Test";
            threadinfo.Start();
            Console.WriteLine("-------------------------------------------------------------------");


            //----------------------------------------------定义方式
            // Thread
            Thread thread = new Thread(Test);
            thread.Name = "Test1";
            thread.Start();
            Console.WriteLine("-------------------------------------------------------------------");

            // Thread1
            Thread thread1 = new Thread(new ThreadStart(Test));
            thread1.IsBackground = true;
            thread1.Start();
            Console.WriteLine("-------------------------------------------------------------------");

            //ThreadStart
            ThreadStart threadStart = DelegateThread;
            Thread thread2 = new Thread(threadStart);
            thread2.Name = "Test";
            thread2.Start();
            Console.WriteLine("-------------------------------------------------------------------");

            //anonymous 匿名
            Thread thread3 = new Thread(() =>
            {
                Test();
            });
            thread3.Name = "Test";
            thread3.Start();
            Console.WriteLine("-------------------------------------------------------------------");

            //匿名
            //Thread anonymousThread = new Thread(new ThreadStart(() =>
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        Console.WriteLine("后台线程计数" + i);
            //        Thread.Sleep(100);
            //    }
            //}));
            //anonymousThread.IsBackground = true;//设置为后台线程
            //anonymousThread.Start();
            //Console.WriteLine("-------------------------------------------------------------------");

            //// 线程池
            //List<int> list = new List<int>() { 1, 2, 3 };
            //Parallel.ForEach(list, num =>
            //{
            //    num--;
            //});

            Console.WriteLine("-------------------------------------------------------------------");
            //暂停与阻塞
            ThreadJoinSleep();
            #endregion
        }
        //Thread 
        private static void Test()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("后台线程计数" + i);
                //  Thread.Sleep(100);
            }
        }
        public static void DelegateThread()
        {
            Test();
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
        #region 线程信息
        /// <summary>
        /// 
        /// </summary>
        private static void ThreadInfo()
        {
            Thread thisTHread = Thread.CurrentThread;
            Console.WriteLine("线程标识：" + thisTHread.Name);
            Console.WriteLine("当前地域：" + thisTHread.CurrentCulture.Name);  // 当前地域
            Console.WriteLine("线程执行状态：" + thisTHread.IsAlive);
            Console.WriteLine("是否为后台线程：" + thisTHread.IsBackground);
            Console.WriteLine("是否为线程池线程" + thisTHread.IsThreadPoolThread);
        }
        #endregion

        #region 线程阻塞
        /*
        Thread.Sleep(); 会阻塞线程，使得线程交出时间片，然后处于休眠状态，直至被重新唤醒；适合用于长时间的等待；
        Thread.SpinWait(); 使用了自旋等待，等待过程中会进行一些的运算，线程不会休眠，用于微小的时间等待；长时间等待会影响性能；
        Task.Delay(); 用于异步中的等待
        */

        /// <summary>
        /// 打游戏
        /// </summary>
        public static void PlayGames()
        {
            Console.WriteLine(Thread.CurrentThread.Name + "开始打游戏");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{DateTime.Now}:第几局：" + i);
                // Sleep方法可以将当前线程挂起一段时间
                Thread.Sleep(TimeSpan.FromSeconds(2));      // 休眠 2 秒
            }
            Console.WriteLine(Thread.CurrentThread.Name + "打完了");
        }
        /// <summary>
        /// 线程状态
        /// Unstarted	即未开始（就绪）
        /// WaitSleepJoin	阻塞
        /// Running		运行中
        /// Stopped		死亡
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static ThreadState GetThreadState(ThreadState ts)
        {
            return ts & (ThreadState.Unstarted |
                ThreadState.WaitSleepJoin |
                ThreadState.Stopped);
        }
        /// <summary>
        /// 暂停与阻塞
        /// 阻塞的定义：当线程由于特点原因暂停执行，那么它就是阻塞的。
        /// 如果线程处于阻塞状态，线程就会交出他的 CPU 时间片，并且不会消耗 CPU 时间，直至阻塞结束。
        /// 阻塞会发生上下文切换。
        /// </summary>
        public static void ThreadJoinSleep()
        {
            Thread thread = new Thread(PlayGames);
            thread.Name = "小弟弟";

            Console.WriteLine($"{DateTime.Now}:大家在吃饭，吃完饭后要带小弟弟逛街");
            Console.WriteLine("吃完饭了");
            Console.WriteLine($"{DateTime.Now}:小弟弟开始玩游戏");
            Console.WriteLine("弟弟在干嘛？(线程状态)：" + Enum.GetName(typeof(ThreadState), GetThreadState(thread.ThreadState)));
            thread.Start();
            Console.WriteLine("弟弟在干嘛？(线程状态)：" + Enum.GetName(typeof(ThreadState), GetThreadState(thread.ThreadState)));
            // 化妆 5 s
            Console.WriteLine("不管他，大姐姐化妆先"); Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("弟弟在干嘛？(线程状态)：" + Enum.GetName(typeof(ThreadState), GetThreadState(thread.ThreadState)));
            Console.WriteLine($"{DateTime.Now}:化完妆，等小弟弟打完游戏");
            //Join()  方法可以阻塞其余线程，使得它们一直等待，直到当前 线程运行结束
            thread.Join();
            Console.WriteLine("弟弟在干嘛？(线程状态)：" + Enum.GetName(typeof(ThreadState), GetThreadState(thread.ThreadState)));
            Console.WriteLine("打完游戏了嘛？" + (!thread.IsAlive ? "true" : "false"));
            Console.WriteLine($"{DateTime.Now}:走，逛街去");
            Console.ReadKey();
        }
        public static void ThreadJoinTest()
        {
            Thread thread1 = new Thread(new ThreadStart(() =>
            {
                Thread thread11 = new Thread(new ThreadStart(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(100);
                        Console.WriteLine("我是第1-1个线程1打印的！");
                    }
                }));
                thread11.Start();
                thread11.Join();
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("我是第1个线程打印的！");
                }
            }));

            thread1.Start();
            Console.WriteLine("我是主线程打印的！");
            Console.Read();
        }
        #endregion

        #region 线程锁
        //lock、

        //Monitor

        //读写锁 ReaderWriterLockSlim 


        private static ReaderWriterLockSlim tool = new ReaderWriterLockSlim();   // 读写锁
        private static int MaxId = 1;
        public static List<DoWorkModel> orders = new List<DoWorkModel>();       // 订单表
        public static void ReaderWriterLockMain()
        {
            // 5个线程读
            for (int i = 0; i < 5; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        var result = DoSelect(1, MaxId);
                        if (result is null)
                        {
                            Console.WriteLine("获取失败");
                            continue;
                        }
                        foreach (var item in result)
                        {
                            Console.Write($"{item.Id}|");
                        }
                        Console.WriteLine("\n");
                        Thread.Sleep(1000);
                    }
                }).Start();
            }

            for (int i = 0; i < 2; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        var result = DoCreate((new Random().Next(0, 100)).ToString(), DateTime.Now);      // 模拟生成订单
                        if (result is null)
                            Console.WriteLine("创建失败");
                        else Console.WriteLine("创建成功");
                    }

                }).Start();
            }
        }
        // 创建订单
        private static DoWorkModel DoCreate(string userName, DateTime time)
        {
            try
            {
                tool.EnterUpgradeableReadLock();        // 升级
                try
                {
                    tool.EnterWriteLock();              // 获取写入锁

                    // 写入订单
                    MaxId += 1;                         // Interlocked.Increment(ref MaxId);

                    DoWorkModel model = new DoWorkModel
                    {
                        Id = MaxId,
                        UserName = userName,
                        DateTime = time
                    };
                    orders.Add(model);
                    return model;
                }
                catch { }
                finally
                {
                    tool.ExitWriteLock();               // 释放写入锁
                }
            }
            catch { }
            finally
            {
                tool.ExitUpgradeableReadLock();         // 降级
            }
            return default;
        }

        // 分页查询订单
        private static DoWorkModel[] DoSelect(int pageNo, int pageSize)
        {

            try
            {
                DoWorkModel[] doWorks;
                tool.EnterReadLock();           // 获取读取锁
                doWorks = orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArray();
                return doWorks;
            }
            catch { }
            finally
            {
                tool.ExitReadLock();            // 释放读取锁
            }
            return default;
        }
        #endregion

        #region 线程型号量
        /*
        Semaphore、SemaphoreSlim
        Semaphore,是负责协调各个线程, 以保证它们能够正确、合理的使用公共资源。也是操作系统中用于控制进程同步互斥的量
        initialCount 表示一开始允许几个线程进入资源池，如果设置为0，所有线程都不能进入，要一直等资源池放通。
        maximumCount 表示最大允许几个线程进入资源池。
        Release() 表示退出信号量并返回前一个计数。这个计数指的是资源池还可以进入多少个线程。
        */

        static Semaphore sema = new Semaphore(2, 5);
        private static void SemaphoreTest()
        {
            const int cycleNum = 9;
            for (int i = 0; i < cycleNum; i++)
            {
                Thread td = new Thread(new ParameterizedThreadStart(testFun));
                td.Name = string.Format("编号{0}", i.ToString());
                td.Start(td.Name);
            }
            Console.ReadKey();
        }
        public static void testFun(object obj)
        {
            bool su = sema.WaitOne();
            Console.WriteLine(obj.ToString() + "进洗手间：" + DateTime.Now.ToString());
            Thread.Sleep(2000);
            Console.WriteLine(obj.ToString() + "出洗手间：" + DateTime.Now.ToString());
            int num = sema.Release();
        }


        // 求和
        private static int sum = 0;
        private static Semaphore _pool;
        // 判断十个线程是否结束了。
        private static int isComplete = 0;
        private static void SemaphoreAddOne()
        {
            Console.WriteLine("执行程序");
            // 设置允许最大三个线程进入资源池
            // 一开始设置为0，就是初始化时允许几个线程进入
            // 这里设置为0，后面按下按键时，可以放通三个线程
            _pool = new Semaphore(0, 3);
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(AddOne));
                thread.Start(i + 1);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("任意按下键(不要按关机键)，可以打开资源池");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            // 准许三个线程进入
            _pool.Release(3);

            // 这里没有任何意义，就单纯为了演示查看结果。
            // 等待所有线程完成任务
            while (true)
            {
                if (isComplete >= 10)
                    break;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("sum = " + sum);

            // 释放池
            _pool.Close();
        }


        public static void AddOne(object n)
        {
            Console.WriteLine($"    线程{(int)n}启动，进入队列");
            // 进入队列等待
            _pool.WaitOne();
            Console.WriteLine($"第{(int)n}个线程进入资源池");
            // 进入资源池
            for (int i = 0; i < 10; i++)
            {
                Interlocked.Add(ref sum, 1);
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            // 解除占用的资源池
            int numberThread = _pool.Release();
            Console.WriteLine("在此线程退出资源池前，资源池还有多少线程可以进入？" + numberThread);
            isComplete += 1;
            Console.WriteLine($"                     第{(int)n}个线程退出资源池");
        }
        #endregion

        #region 线程通知
        /*
         AutoRestEvent 
        AutoResetEvent 对象有终止和非终止状态。Set() 设置终止状态，Reset() 重置非终止状态。

        这个终止状态，可以理解成信号已经通知；非终止状态则是信号还没有通知。
        注意，注意终止状态和非终止状态指的是 AutoResetEvent 的状态，不是指线程的状态
        需要注意的是，如果 AutoResetEvent 已经处于终止状态，那么线程调用 WaitOne() 不会再起作用。除非调用Reset() 。
        构造函数中的参数，正是设置这个状态的。true 代表终止状态，false 代表非终止状态。如果使用 new AutoResetEvent(true); ，则线程一开始是无需等待信号的。
        在使用完类型后，您应直接或间接释放类型，显式调用 Close()/Dispose() 或 使用 using。 当然，也可以直接退出程序。
        需要注意的是，如果多次调用 Set() 的时间间隔过短，如果第一次 Set() 还没有结束(信号发送需要处理时间)，那么第二次 Set() 可能无效(不起作用)。
        */
        // 控制第一个线程
        // 第一个线程开始时，AutoResetEvent 处于终止状态，无需等待信号
        private static AutoResetEvent oneResetEvent = new AutoResetEvent(true);

        // 控制第二个线程
        // 第二个线程开始时，AutoResetEvent 处于非终止状态，需要等待信号
        private static AutoResetEvent twoResetEvent = new AutoResetEvent(false);

        public static void ResetEventRun()
        {
            new Thread(DoOne).Start();
            new Thread(DoTwo).Start();

            Console.ReadKey();
        }
        public static void DoOne()
        {
            while (true)
            {
                Console.WriteLine("\n① 按一下键，我就让DoTwo运行");
                Console.ReadKey();
                twoResetEvent.Set();//设置终止状态
                oneResetEvent.Reset();// 重置非终止状态
                // 等待 DoTwo() 给我信号
                oneResetEvent.WaitOne();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n     DoOne() 执行");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void DoTwo()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                // 等待 DoOne() 给我信号
                twoResetEvent.WaitOne();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n     DoTwo() 执行");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n② 按一下键，我就让DoOne运行");
                Console.ReadKey();
                oneResetEvent.Set();
                twoResetEvent.Reset();
            }
        }


        #endregion

        #region  线程池
        /*
         ThreadPool 
        线程池全称为托管线程池，线程池受 .NET 通用语言运行时(CLR)管理，线程的生命周期由 CLR 处理，因此我们可以专注于实现任务，而不需要理会线程管理。
        线程池的应用场景：任务并行库 (TPL)操作、异步 I/O 完成、计时器回调、注册的等待操作、使用委托的异步方法调用和套接字连接

        */
        #endregion

        #region 任务
        static void Main()
        {
            #region  创建任务
            // Task，Task.Factory，Task.Run
            //-------------------------------Task ---------------------------
            // 定义两个任务
            Task task1 = new Task(() =>
            {
                Console.WriteLine("① 开始执行");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行中");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行即将结束");
            });
            Task task2 = new Task(MyTask);
            // 开始任务
            task1.Start();
            task2.Start();

            // 重载方法 1
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("① 开始执行");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行中");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行即将结束");
            });

            //-------------------------------Task.Factory ---------------------------
            // 重载方法 1
            Task.Factory.StartNew(MyTask);

            // 重载方法 2
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("① 开始执行");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行中");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行即将结束");
            }, TaskCreationOptions.LongRunning);


            //-------------------------------Task.Run ---------------------------
            Task.Run(() =>
            {
                Console.WriteLine("① 开始执行");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行中");
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.WriteLine("① 执行即将结束");
            });
            Task.Run(MyTask);
            #endregion

            #region  取消任务
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Factory.StartNew(MyTask, cts.Token);
            Console.ReadKey();
            cts.Cancel();       // 取消任务
            Console.ReadKey();
            #endregion

            #region  捕获任务异常
            //进行中的任务发生了异常，不会直接抛出来阻止主线程执行，当获取任务处理结果或者等待任务完成时，异常会重新抛出

            Task<string> task = new Task<string>(() =>
            {
                try
                {
                    throw new Exception("反正就想弹出一个异常");
                }
                catch
                {
                    return "返回结果";
                }
            });
            task.Start();

            var result = task.Result;
            if (result is null)
                Console.WriteLine("任务执行失败");
            else Console.WriteLine("任务执行成功");

            Console.ReadKey();

            //------------------------------------------------------------全局捕获任务异常------------------------
            TaskScheduler.UnobservedTaskException += MyTaskException;
            Task.Factory.StartNew(() =>
            {
                throw new ArgumentNullException();
            });
            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("Done");
            Console.ReadKey();

            #endregion
        }
        public static void MyTaskException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            // eventArgs.SetObserved();
            ((AggregateException)eventArgs.Exception).Handle(ex =>
            {
                Console.WriteLine("Exception type: {0}", ex.GetType());
                return true;
            });
        }
        private static void MyTask()
        {
            Console.WriteLine("② 开始执行");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine("② 执行中");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine("② 执行即将结束");
        }

        #region 父子任务
        static void AttachedToParentMain()
        {
            // 父子任务
            Task<int> task = new Task<int>(() =>
            {
                // 子任务
                Task task1 = new Task(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("     内层任务1");
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }
                }, TaskCreationOptions.AttachedToParent);// 标识
                task1.Start();
                Console.WriteLine("最外层任务");
                return 666;
            });

            task.Start();
            Console.WriteLine($"任务运算结果是：{task.Result}");
            Console.WriteLine("\n-------------------\n");

            Console.ReadKey();
        }

        #endregion

        #region 组合任务
        static void ContinueWithMain()
        {
            Task task = new Task(() =>
            {
                Console.WriteLine("     第一个任务");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            });
            // 任务①
            task.ContinueWith(t =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"    任务① ");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            });
            // 任务②
            task.ContinueWith(t =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"     任务②");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            });
            // 任务① 和 任务② 属于同级并行任务
            task.Start();
        }

        #endregion

        #region 延续任务
        private static void ContinueWithParent()
        {
            // 父子任务
            Task<int> task = new Task<int>(() =>
            {
                // 子任务
                Task task1 = new Task(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine("     内层任务1");
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }, TaskCreationOptions.AttachedToParent);

                task1.ContinueWith(t =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine("内层延续任务，也属于子任务");
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }, TaskContinuationOptions.AttachedToParent);

                task1.Start();

                Console.WriteLine("最外层任务");
                return 666;
            });

            task.Start();
            Console.WriteLine($"任务运算结果是：{task.Result}");
            Console.WriteLine("\n-------------------\n");

            Console.ReadKey();
        }
        #endregion

        #region 并行 (异步)处理任务 WhenAll
        // Task.WhenAll() ：等待提供的所有 Task 对象完成执行过程
        static void TaskWhenAllMain()
        {
            List<Task<int>> tasks = new List<Task<int>>();

            for (int i = 0; i < 5; i++)
                tasks.Add(Task.Run<int>(() =>
                {
                    Console.WriteLine($"任务开始执行");
                    return new Random().Next(0, 10);
                }));

            Task<int[]> taskOne = Task.WhenAll(tasks);

            foreach (var item in taskOne.Result)
                Console.WriteLine(item);

            Console.ReadKey();
        }
        #endregion

        #region 并行 (同步)处理任务 WaitAll

        // Task.WaitAll()：等待提供的所有 Task 对象完成执行过程。
        static void TaskWhenAllMainT()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine($"任务开始执行");
                }));

            Task.WaitAll(tasks.ToArray());

            Console.ReadKey();
        }
        #endregion

        #region 并行任务的 Task.WhenAny
        // Task.WhenAny() 和 Task.WhenAll() 使用上差不多，Task.WhenAll() 当所有任务都完成时，才算完成，而 Task.WhenAny() 只要其中一个任务完成，都算完成。
        static void TaskWhenAnyMain()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
                tasks.Add(Task.Run(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(0, 5)));
                    Console.WriteLine("     正在执行任务");
                }));
            Task taskOne = Task.WhenAny(tasks);
            taskOne.Wait(); // 任意一个任务完成，就可以解除等待

            Console.WriteLine("有任务已经完成了");

            Console.ReadKey();
        }
        #endregion 

        #endregion
    }
    // 订单模型
    public class DoWorkModel
    {
        public int Id { get; set; }     // 订单号
        public string UserName { get; set; }    // 客户名称
        public DateTime DateTime { get; set; }  // 创建时间
    }

}
/*
1、线程的不确定性
    线程的不确定性是指几个并行运行的线程，不确定在下一刻 CPU 时间片会分配给谁(当然，分配有优先级)。
对我们来说，多线程是同时运行的，但一般 CPU 没有那么多核，不可能在同一时刻执行所有的线程。CPU 会决定某个时刻将时间片分配给多个线程中的一个线程，这就出现了 CPU 的时间片分配调度。
执行下面的代码示例，你可以看到，两个线程打印的顺序是不确定的，而且每次运行结果都不同。
CPU 有一套公式确定下一次时间片分配给谁，但是比较复杂，需要学习计算机组成原理和操作系统。
留着下次写文章再讲。

2、线程优先级
    前台线程的优先级大于后台线程，
并且程序需要等待所有前台线程执行完毕后才能关闭；而当程序关闭是，无论后台线程是否在执行，都会强制退出。

3、Join 
MSDN的解释：阻塞调用线程，直到某个线程终止时为止。
首先明确几个问题：
1、一个进程由一个或者多个线程组成，线程之间有可能会存在一定的先后关系和互斥关系。多线程编程，首先就是要想办法划分线程，减少线程之间的先后关系和互斥关系，这样才能保证线程之间的独立性，
各自工作，不受影响。Google的MapReduce核心思想就是尽量减少线程之间的先后关系和互斥关系。
2、无论如何地想办法，线程之间还是会存在一定的先后关系和互斥关系，这时候可以使用Thread.Join方法。
3、一个线程在执行的过程中，可能调用另一个线程，前者可以称为调用线程，后者成为被调用线程。
4、Thread.Join方法的使用场景：调用线程挂起，等待被调用线程执行完毕后，继续执行。
5、被调用线程执行Join方法，告诉调用线程，你先暂停，我执行完了，你再执行。从而保证了先后关系。
6、考虑一种有意思的情况：在当前线程内调用Thread.CurrentThread.Join() 会出现什么情况？分析：假设当前线程为A，此时调用线程为A，被调用线程也为A，
由于调用线程A暂停，被调用线程A（也就是调用线程A）永远不会执行完毕，造成死锁。

4、自旋和休眠
当线程处于进入休眠状态或解除休眠状态时，会发生上下文切换，这就带来了昂贵的消耗。
而线程不断运行，就会消耗 CPU 时间，占用 CPU 资源。
对于过短的等待，应该使用自旋(spin)方法，避免发生上下文切换；过长的等待应该使线程休眠，避免占用大量 CPU 时间。
我们可以使用最为熟知的 Sleep() 方法休眠线程。有很多同步线程的类型，也使用了休眠手段等待线程。
自旋的意思是，没事找事做。
Thread.SpinWait() 在极少数情况下，避免线程使用上下文切换很有用

5、Lock
lock 用于读一个引用类型进行加锁，同一时刻内只有一个线程能够访问此对象。lock 是语法糖，是通过 Monitor 来实现的。
Lock 锁定的对象，应该是静态的引用类型（字符串除外）。

6、CPU时间片和上下文切换
CPU时间片
    时间片(timeslice)是操作系统分配给每个正在运行的进程微观上的一段 CPU 时间。
首先，内核会给每个进程分配相等的初始时间片，然后每个进程轮番地执行相应的时间，当所有进程都处于时间 片耗尽的状态时，内核会重新为每个进程计算并分配时间片，如此往复。
上下文切换
    上下文切换（Context Switch），也称做进程切换或任务切换，是指 CPU 从一个进程或线程切换到另一个进程或线程
在接受到中断（Interrupt）的时候，CPU 必须要进行上下文交换。进行上下文切换时，会带来性能损失。

7、信号量（Semaphore）
信号量分为两种类型：本地信号量和命名系统信号量。
命名系统信号量在整个操作系统中均可见，可用于同步进程的活动。
局部信号量仅存在于进程内。
当 name 为 null 或者为空时，Mutex 的信号量时局部信号量，否则 Mutex 的信号量是命名系统信号量。
Semaphore 的话，也是两种情况都有。
如果使用接受名称的构造函数创建 Semaphor 对象，则该对象将与该名称的操作系统信号量关联
*/
