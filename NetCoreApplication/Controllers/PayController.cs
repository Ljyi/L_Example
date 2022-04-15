using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApplication.Service;

namespace NetCoreApplication.Controllers
{
    /// <summary>
    /// 支付
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private IPayService _wxPayService;
        private IPayService _aliPayService;
        private IComponentContext _componentContext;//Autofac上下文
        //通过构造函数注入Service
        public PayController(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            //解释组件
            _wxPayService = _componentContext.ResolveNamed<IPayService>(typeof(WxPayService).Name);
            _aliPayService = _componentContext.ResolveNamed<IPayService>(typeof(AliPayService).Name);
        }
        /// <summary>
        /// 1213
        /// </summary>
        /// <returns></returns>
        //public IActionResult Index()
        //{
        //    string wxPay = _wxPayService.Pay();
        //    string aliPay = _aliPayService.Pay();
        //    return Content(wxPay + aliPay);
        //}
    }
}
