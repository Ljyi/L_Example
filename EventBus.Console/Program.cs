using EventBus.Core;
using EventBus.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sendEmailHandler = new UserAddedEventHandlerSendEmail();
            var sendMessageHandler = new UserAddedEventHandlerSendMessage();
            var sendRedbagsHandler = new UserAddedEventHandlerSendRedbags();
            EventBus.Core.EventBus.Instance.Subscribe(sendEmailHandler);
            EventBus.Core.EventBus.Instance.Subscribe(sendMessageHandler);
            //Weiz.EventBus.Core.EventBus.Instance.Subscribe<UserGeneratorEvent>(sendRedbagsHandler);
            EventBus.Core.EventBus.Instance.Subscribe<OrderGeneratorEvent>(sendRedbagsHandler);

            var userGeneratorEvent = new UserGeneratorEvent { UserId = Guid.NewGuid() };

            System.Console.WriteLine("{0}注册成功", userGeneratorEvent.UserId);

            EventBus.Core.EventBus.Instance.Publish(userGeneratorEvent, CallBack);

            var orderGeneratorEvent = new OrderGeneratorEvent { OrderId = Guid.NewGuid() };

            System.Console.WriteLine("{0}下单成功", orderGeneratorEvent.OrderId);

            EventBus.Core.EventBus.Instance.Publish(orderGeneratorEvent, CallBack);


            // Core.EventBus.InstanceForXml();
            // Core.EventBus.Instance.Publish(new Events.UserGeneratorEvent { UserId = Guid.NewGuid() }, CallBack);

            System.Console.ReadKey();
        }

        private static void CallBack(OrderGeneratorEvent orderGeneratorEvent, bool result, Exception ex)
        {
            System.Console.WriteLine("用户下单订阅事件执行成功");
        }

        public static void CallBack(UserGeneratorEvent userGenerator, bool result, Exception ex)
        {
            System.Console.WriteLine("用户注册订阅事件执行成功");
        }
    }
}
