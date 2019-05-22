
using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    class Program
    {
        private static ILog log = Common.Logging.LogManager.GetLogger(typeof(TestJob));
        static void Main(string[] args)
        {
            try
            {
                LogHelper.WriteLog("开始");
                //log.Debug("debug");
                //log.Info("info");
               // log.Error("error");
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = schedulerFactory.GetScheduler();
                scheduler.Start();
                Console.WriteLine("执行成功");
                LogHelper.WriteLog("结束");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
