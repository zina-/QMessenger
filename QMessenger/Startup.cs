using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(QMessenger.Startup))]

namespace QMessenger
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 응용 프로그램을 구성하는 방법에 대한 자세한 내용은 http://go.microsoft.com/fwlink/?LinkID=316888을 참조하십시오.
            app.MapSignalR();
        }
    }
}
