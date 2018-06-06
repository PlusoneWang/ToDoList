namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    using Newtonsoft.Json;

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
            return this.View();
        }

        /// <summary>
        /// 取得使用者待辦清單
        /// </summary>
        /// <returns>使用者待辦清單</returns>
        [HttpGet]
        public ActionResult GetLists()
        {
            var userListsResult = this.toDoListService.GetUserToDoLists(this.CurrentUser.Id);
            var authorData = JsonConvert.SerializeObject(
                userListsResult,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });

            return this.Content(authorData, "application/json");
        }

        /// <summary>
        /// 建立清單
        /// </summary>
        /// <param name="listName">清單名稱</param>
        /// <returns>建立結果</returns>
        [HttpPost]
        public JsonResult CreateList(string listName)
        {
            var createResult = this.toDoListService.CreateList(listName, this.CurrentUser.Id);
            return this.Json(createResult);
        }
    }
}