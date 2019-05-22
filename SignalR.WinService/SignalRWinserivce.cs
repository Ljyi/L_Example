using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
namespace SignalR.WinService
{
    public partial class SignalRWinserivce : ServiceBase
    {
        public SignalRWinserivce()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            string url = "http://10.4.24.196:8080";
            WebApp.Start(url);
            Log.WriteLog("开启服务");
        }

        protected override void OnStop()
        {
            Log.WriteLog("关闭服务");
        }
    }
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        //发送指令
        public void Send(string name, string message)
        {
            Log.WriteLog(name + ":" + message);
            Clients.All.addMessage(name, message);
        }
        /// <summary>
        /// 刷新服务（状态）
        /// </summary>
        public void RefrashService(string serviceName)
        {
            if (OprateService.IsServiceExisted(serviceName))
            {
               string serviceStatus = OprateService.ServiceStatus(serviceName);
                Clients.All.addMessage(serviceStatus);
            }
            else
            {
                Clients.All.addMessage(serviceName + "服务不存在");
            }
        }
        /// <summary>
        /// 卸载服务
        /// </summary>
        public void UninstallService()
        {

        }
        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartService()
        {

        }
        /// <summary>
        /// 暂停服务
        /// </summary>
        public void StopService()
        {

        }
    }
    public class Log
    {
        public static void WriteLog(string msg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }


    public class OprateService
    {

        /// <summary>
        /// 判断服务是否存在
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="serviceFilePath"></param>

        public static void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="serviceFilePath"></param>
        public static void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static string ServiceStatus(string serviceName)
        {
            string serviceStatus = "";
            ServiceController control = new ServiceController(serviceName);
            switch (control.Status)
            {
                case ServiceControllerStatus.ContinuePending:
                    serviceStatus = "ContinuePending";
                    break;
                case ServiceControllerStatus.Paused:
                    serviceStatus = "Paused";
                    break;
                case ServiceControllerStatus.PausePending:
                    serviceStatus = "PausePending";
                    break;
                case ServiceControllerStatus.Running:
                    serviceStatus = "Running";
                    break;
                case ServiceControllerStatus.StartPending:
                    serviceStatus = "StartPending";
                    break;
                case ServiceControllerStatus.Stopped:
                    serviceStatus = "Stopped";
                    break;
                case ServiceControllerStatus.StopPending:
                    serviceStatus = "StopPending";
                    break;
                default:
                    break;
            }
            return serviceStatus;
        }
    }
}
