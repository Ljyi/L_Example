using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class TestJob : IJob
    {
        #region IJob 成员
        private readonly ILog log = LogManager.GetLogger(typeof(TestJob));
        public void Execute(IJobExecutionContext context)
        {
            LogHelper.WriteLog("任务运行");
            log.Info("任务运行");
        }
        #endregion
    }
}
