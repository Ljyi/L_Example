using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCE.Service;
namespace WCF.MyServiceHost
{
    public class MyHelloHost : IDisposable
    {
        /// <summary>  
        /// 定义一个服务对象  
        /// </summary>  
        private ServiceHost _myHelloHost;
        public const string BaseAddress = "net.pipe://localhost"; //基地址  
        public const string HelloServiceAddress = "Hello"; //可选地址  
        public static readonly Type ServiceType = typeof(HelloService);  //服务契约实现类型  
        public static readonly Type ContractType = typeof(IHelloService);  //服务契约接口  
        public static readonly Binding HelloBinding = new NetNamedPipeBinding(); //服务定义一个绑定  

        /// <summary>  
        /// 构造方法  
        /// </summary>  
        public MyHelloHost()
        {
            CreateHelloServiceHost();
        }

        /// <summary>  
        /// 构造服务对象  
        /// </summary>  
        protected void CreateHelloServiceHost()
        {
            _myHelloHost = new ServiceHost(ServiceType, new Uri[] { new Uri(BaseAddress) });//创建服务对象  
            _myHelloHost.AddServiceEndpoint(ContractType, HelloBinding, HelloServiceAddress); //添加终结点  
        }

        /// <summary>  
        /// 打开服务方法  
        /// </summary>  
        public void Open()
        {
            Console.WriteLine("开始启动服务...");
            _myHelloHost.Open();
            Console.WriteLine("服务已启动");
        }

        /// <summary>  
        /// 销毁服务宿主对象实例  
        /// </summary>  
        public void Dispose()
        {
            if (_myHelloHost != null)
                (_myHelloHost as IDisposable).Dispose();
        }
    }

}
