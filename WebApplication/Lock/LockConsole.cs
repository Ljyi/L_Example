namespace NetCoreApplication.Lock
{
    public class LockConsole
    {
        private static Object _obj = new object();
        /// <summary>
        /// 获取自增
        /// </summary>
        public static void GetIncrement()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            //10个并发执行
            Parallel.For(0, 10, (i) =>
            {
                for (int j = 0; j < 10000; j++)
                {
                    result++;
                }
            });
            Console.WriteLine("结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();
        }
        #region 1.基于Lock实现
        /// <summary>
        /// 原子操作基于Lock实现
        /// </summary>
        public static void AtomicityForLock()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            //10个并发执行
            Parallel.For(0, 10, (i) =>
            {
                //lock锁
                lock (_obj)
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        result++;
                    }
                }
            });
            Console.WriteLine("结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();

        }
        #endregion

        #region 2、基于CAS实现
        /// <summary>
        /// 自增CAS实现
        /// </summary>
        public static void AtomicityForInterLock()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            Parallel.For(0, 10, (i) =>
            {
                for (int j = 0; j < 10000; j++)
                {
                    //自增
                    Interlocked.Increment(ref result);
                }
            });
            Console.WriteLine($"结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();
        }

        /// <summary>
        /// 基于CAS原子操作自己写
        /// </summary>
        public static void AtomicityForMyCalc()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            Parallel.For(0, 10, (i) =>
            {
                long init = 0;
                long incrementNum = 0;
                for (int j = 0; j < 10000; j++)
                {
                    do
                    {
                        init = result;
                        incrementNum = result + 1;
                        incrementNum = incrementNum > 10000 ? 1 : incrementNum; //自增到10000后初始化成1

                    }
                    //如果result=init,则result的值被incrementNum替换,否则result不变,返回的是result的原始值
                    while (init != Interlocked.CompareExchange(ref result, incrementNum, init));
                    if (incrementNum == 10000)
                    {
                        Console.WriteLine($"自增到达10000啦!值被初始化为1");
                    }
                }
            });
            Console.WriteLine($"结束计算");

            Console.WriteLine($"result正确值应为：{10000}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();

        }

        #endregion

        #region 3.自旋锁SpinLock
        //创建自旋锁
        private static SpinLock spin = new SpinLock();
        public static void Spinklock()
        {
            Action action = () =>
            {
                bool lockTaken = false;
                try
                {
                    //申请获取锁
                    spin.Enter(ref lockTaken);
                    //临界区
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"当前线程{Thread.CurrentThread.ManagedThreadId.ToString()},输出:1");
                    }
                }
                finally
                {
                    //工作完毕，或者产生异常时，检测一下当前线程是否占有锁，如果有了锁释放它
                    //避免出行死锁
                    if (lockTaken)
                    {
                        spin.Exit();
                    }
                }
            };

            Action action2 = () =>
            {
                bool lockTaken = false;
                try
                {
                    //申请获取锁
                    spin.Enter(ref lockTaken);
                    //临界区
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"当前线程{Thread.CurrentThread.ManagedThreadId.ToString()},输出:2");
                    }
                }
                finally
                {
                    //工作完毕，或者产生异常时，检测一下当前线程是否占有锁，如果有了锁释放它
                    //避免出行死锁
                    if (lockTaken)
                    {
                        spin.Exit();
                    }
                }
            };
            //并行执行2个action
            Parallel.Invoke(action, action2);
        }
        #endregion

        #region 4.读写锁ReaderWriterLockSlim
        //读写锁， //策略支持递归
        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static int index = 0;
        static void read()
        {
            try
            {
                //进入读锁
                rwl.EnterReadLock();
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},读数据,读到index:{index}");
                }
            }
            finally
            {
                //退出读锁
                rwl.ExitReadLock();
            }
        }
        static void write()
        {
            try
            {
                //尝试获写锁
                while (!rwl.TryEnterWriteLock(50))
                {
                    Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},等待写锁");
                }
                Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},获取到写锁");
                for (int i = 0; i < 5; i++)
                {
                    index++;
                    Thread.Sleep(50);
                }
                Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},写操作完成");
            }
            finally
            {
                //退出写锁
                rwl.ExitWriteLock();
            }
        }

        /// <summary>
        /// 执行多线程读写
        /// </summary>
        public static void test()
        {
            var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            Task[] task = new Task[6];
            task[1] = taskFactory.StartNew(write); //写
            task[0] = taskFactory.StartNew(read); //读
            task[2] = taskFactory.StartNew(read); //读
            task[3] = taskFactory.StartNew(write); //写
            task[4] = taskFactory.StartNew(read); //读
            task[5] = taskFactory.StartNew(read); //读
            for (var i = 0; i < 6; i++)
            {
                task[i].Wait();
            }
        }
        #endregion
    }
}

/*
 参考文献：https://www.cnblogs.com/wei325/p/16065342.html#autoid-0-0-0
 */
