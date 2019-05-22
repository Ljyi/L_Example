using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProjectMain.WhereFS
{
    public interface IBaseEntity
    {
    }
    public class User
    {
    }
    public class BaseEntity : IBaseEntity
    {
    }
    public class Order : BaseEntity
    {
    }
    public class OP<T> where T : class
    {
        public void add(T t)
        {
        }
    }
    public class OP1<T> where T : BaseEntity
    {
        public void add(T t)
        {
        }
    }
    public class OP2<T> where T : Order
    {
        public void add(T t)
        {
        }
    }
    public class OP3<T, U> where T : class///约束T参数必须为“引用 类型{ }”
        where U : BaseEntity///约束T参数必须为“引用 类型{ }”
    {
        public void add(T t)
        {
        }
    }
    public class OP4<T> where T : IBaseEntity
    {
        public void add(T t)
        {
        }
    }
    public class OP5<T, U> : OP3<T, U> where T : class where U : BaseEntity
    {
        public void add(T t)
        {
        }
    }
    public class OP
    {
        OP<Order> oP = new OP<Order>();
        OP2<Order> oP1 = new OP2<Order>();
        OP3<Order, BaseEntity> oP3 = new OP3<Order, BaseEntity>();
        OP4<IBaseEntity> oP4 = new OP4<IBaseEntity>();
        public void OP1(Order order)
        {
            oP.add(order);
        }
        public void OP2(Order order)
        {
            oP1.add(order);
        }
        public T GetData<T>(T test)
        {
            return test;
        }
        public void GetT<T>()
        {

        }
    }
}
