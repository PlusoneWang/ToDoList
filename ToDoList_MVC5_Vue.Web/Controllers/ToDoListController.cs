namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    using ToDoList_MVC5_Vue.Service;

    public class ToDoListController : BaseController
    {
        private readonly ToDoListService toDoListService;

        public ToDoListController()
        {
            this.toDoListService = new ToDoListService();
        }

        /// <summary>
        /// ToDoList主頁面
        /// </summary>
        /// <returns>主頁面</returns>
        public ActionResult Index()
        {
            // TODO model
            return this.View();
        }

        /// <summary>
        /// 建立清單
        /// </summary>
        /// <param name="listName">清單名稱</param>
        /// <returns>建立結果</returns>
        public JsonResult CreateList(string listName)
        {
            var createResult = this.toDoListService.CreateList(listName, this.CurrentUser.Id);
            return this.Json(createResult);
        }
    }
}