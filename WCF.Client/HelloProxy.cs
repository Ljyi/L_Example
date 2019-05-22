using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCE.Service;

namespace WCF.Client
{
    class HelloProxy : ClientBase<IHelloService>, IService
    {
        public static readonly Binding HelloBinding = new NetNamedPipeBinding();  //硬编码定义绑定  
        //硬编码定义地址 注意：这里要和之前服务定义的地址保持一直  
        public static readonly EndpointAddress HelloAddress = new EndpointAddress(new Uri("net.pipe://localhost/Hello"));
        public HelloProxy() : base(HelloBinding, HelloAddress) { } //构造方法  

        public string Say(string name)
        {
            //使用Channel属性对服务进行调用  
            return Channel.SayHello(name);
        }
    }
    [ServiceContract]
    interface IService
    {
        [OperationContract]
        string Say(string name);
    }
}
