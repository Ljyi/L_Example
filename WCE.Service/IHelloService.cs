using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;

namespace WCE.Service
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        string SayHello(string name);
    }
    [ServiceContract]
    public interface IUpLoadService
    {
        [OperationContract(Action = "UploadFile", IsOneWay = true)]
        void UploadFile(FileUploadMessage request);
    }
    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string SavePath;

        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageBodyMember(Order = 1)]
        public Stream FileData;

    }
}
