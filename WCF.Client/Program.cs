using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Client.ServiceReference1;

namespace WCF.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (HelloProxy proxy = new HelloProxy())
            //{
            //    //利用代理调用方法  
            //    Console.WriteLine(proxy.Say("郑少秋"));
            //    Console.ReadLine();
            //}
            Service1Client service1Client = new Service1Client();
            string msg = service1Client.GetData(1);
            Console.WriteLine(msg);
            Console.ReadLine();
            //using (HelloProxy proxy = new HelloProxy())
            //{
            //    //利用代理调用方法  
            //    Console.WriteLine(proxy.Say("郑少秋"));
            //    Console.ReadLine();
            //}
        }
    }
}
