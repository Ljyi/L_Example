namespace NetCoreApplication
{
    public interface IHelloService
    {
        string SayHello();
    }
    public class HelloService : IHelloService
    {
        public string SayHello()
        {
            Console.WriteLine("Hello");
            return "Hello";
        }
    }
}
