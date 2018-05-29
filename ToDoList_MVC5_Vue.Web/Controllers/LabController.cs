namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// 實驗用Controller
    /// </summary>
    [AllowAnonymous]
    public class LabController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}