using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL.DBBase;
using DAL.DBContext;
using BLL.Quartz;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestRun.Run();
            SendSupplyMailDAL m = new SendSupplyMailDAL();
            var sendSupplyMail = m._p.Find(p => p.IsSend.HasValue);
            Console.WriteLine("123");
        }
    }
    public class SendSupplyMailDAL
    {
        public IBaseRepository<SendSupplyMail> _p = null;
        public SendSupplyMailDAL()
        {
            _p = new BaseRepository<SendSupplyMail>();
        }
        //业务处理
    }
    public class TestRun
    {
        public async static void Run()
        {
            try
            {
                string cronExpression = "0/1 * * * * ?";//这是指每天的9点和16点执行任务
                QuartzUtil.QuartzUtilSet();
                await QuartzUtil.AddJob<QuartzJob>("job1", cronExpression);
                Console.WriteLine("执行成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            // QuartzUtil.ExecuteByCron<QuartzJob>(cronExpression);//这是调用Cron计划方法
        }
    }



}
