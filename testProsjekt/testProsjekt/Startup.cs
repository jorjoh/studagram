using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testProsjekt.Startup))]
namespace testProsjekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
