using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectMain
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test = "121312";
#if Release
            test = "DEBUG:222222222222";
#else
            test = "Release:222222222222";
#endif
            Console.WriteLine("Length: {0}", 123);
            Rectangle rectangle = new Rectangle(1, 2);
            rectangle.GetArea();
            rectangle.Display();
        }
    }
}
