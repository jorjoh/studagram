using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudaGram.Startup))]
namespace StudaGram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
