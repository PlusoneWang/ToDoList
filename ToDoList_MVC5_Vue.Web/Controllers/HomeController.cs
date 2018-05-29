namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}