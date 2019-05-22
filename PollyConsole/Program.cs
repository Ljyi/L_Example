using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var retryTwoTimesPolicy = Policy.Handle<DivideByZeroException>().Retry(3, (ex, count) =>
                          {
                              Console.WriteLine("执行失败! 重试次数 {0}", count);
                              Console.WriteLine("异常来自 {0}", ex.GetType().Name);
                          });
                retryTwoTimesPolicy.Execute(() =>
                {
                    Compute();
                });
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"Excuted Failed,Message: ({e.Message})");
            }

            var fallBackPolicy = Policy<string>.Handle<Exception>().Fallback("执行失败，返回Fallback");
            var fallBack = fallBackPolicy.Execute(() =>
            {
                return ThrowException();
            });
            Console.WriteLine(fallBack);

            var politicaWaitAndRetry = Policy<string>.Handle<Exception>().Retry(3, (ex, count) =>
                     {
                         Console.WriteLine("执行失败! 重试次数 {0}", count);
                         Console.WriteLine("异常来自 {0}", ex.GetType().Name);
                     });

            var mixedPolicy = Policy.Wrap(fallBackPolicy, politicaWaitAndRetry);
            var mixedResult = mixedPolicy.Execute(ThrowException);
            Console.WriteLine($"执行结果: {mixedResult}");
            Console.WriteLine();
        }
        static int Compute()
        {
            int a = 0;
            return 1 / a;
        }
        static string ThrowException()
        {
            throw new Exception();
        }
    }
}
