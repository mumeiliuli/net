using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Email.Web.security;

[assembly: OwinStartup(typeof(Email.Web.App_Start.Startup))]
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace Email.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAuthCookie("/Account/Login");
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
