using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.MyServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyHelloHost host = new MyHelloHost())
            {
                host.Open();
                Console.ReadLine();
            }  
        }
    }
}
