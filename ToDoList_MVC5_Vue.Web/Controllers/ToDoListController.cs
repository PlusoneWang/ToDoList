namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    public class ToDoListController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}