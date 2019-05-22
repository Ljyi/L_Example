using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    public class Order : SqlRepository
    {
        private DBBase _ida;//定义一个私有变量保存抽象

        //构造函数注入
        public Order(DBBase ida)
        {
            _ida = ida;//传递依赖
        }
    }
}
