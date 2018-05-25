using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoList_MVC5_Vue.Web.Startup))]
namespace ToDoList_MVC5_Vue.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}