using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProjectMain
{
    public class AttributeUsageTest
    {
        //  [Obsolete("Don't use OldMethod, use NewMethod instead", true)]
        static void OldMethod()
        {
            Console.WriteLine("It is the old method");
        }
        static void NewMethod()
        {
            Console.WriteLine("It is the new method");
        }
        public static void Main()
        {
            OldMethod();
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
    public class DeBugInfo : System.Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;

        public DeBugInfo(int bg, string dev, string d)
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }
        public string Developer
        {
            get
            {
                return developer;
            }
        }
        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }
    [DeBugInfo(45, "Zara Ali", "12/8/2012", Message = "Return type mismatch")]
    public class Rectangle
    {
        // 成员变量
        protected double length;
        protected double width;
        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }
        [DeBugInfo(55, "Zara Ali", "19/10/2012",
        Message = "Return type mismatch")]
        public double GetArea()
        {
            return length * width;
        }
        [DeBugInfo(56, "Zara Ali", "19/10/2012")]
        public void Display()
        {
            Console.WriteLine("Length: {0}", length);
            Console.WriteLine("Width: {0}", width);
            Console.WriteLine("Area: {0}", GetArea());
        }
    }

    public class BugInfo
    {
        public int bugNo;
        public string developer;
        public string lastReview;
        public string message;

    }
}
