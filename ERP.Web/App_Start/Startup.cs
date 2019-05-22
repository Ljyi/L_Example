using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//[assembly: OwinStartupAttribute(typeof(ERP.Web.App_Start.Startup))]
namespace ERP.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration()
        {
            ConfigureAuth();
        }
        
    }
}