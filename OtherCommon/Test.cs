using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtherCommon
{
    public class Test
    {
        public void ListTest(List<string> listStr, string str)
        {
            listStr.Add("1");
            listStr.Add("2");
            listStr.Add("3");
            listStr[0] = "123";
            listStr[1] = "223";
            listStr[2] = "323";
            str = "tes";
        }



    }
}
