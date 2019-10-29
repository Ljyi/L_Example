using System;

namespace NolgConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Console.WriteLine("---------------------------begin");
            //"helper logs debug".Debug();
            //"helper logs info".Info();
            //"helper logs warn".Warn();
            //"helper logs error".Error();
            //"helper logs fatal".Fatal();
            //Console.WriteLine("---------------------------end");

            LogHelper.Trace("Hello World! Trace");
            LogHelper.Info("Hello World! Info");
            LogHelper.Warn("Hello World! Warn");
            LogHelper.Debug("Hello World! Debug");
            LogHelper.Error("Hello World! Error");
            Console.ReadKey();
        }
    }
}
