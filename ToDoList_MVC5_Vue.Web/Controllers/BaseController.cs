namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Owin.Security;

    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 當前的使用者
        /// </summary>
        public AppUser CurrentUser => new AppUser(this.User);

        /// <summary>
        /// 目前Controller
        /// </summary>
        public string CurrentController => Convert.ToString(this.ControllerContext.RouteData.Values["controller"]);

        /// <summary>
        /// 目前Action
        /// </summary>
        public string CurrentAction => Convert.ToString(this.ControllerContext.RouteData.Values["action"]);

        /// <summary>
        /// 驗證管理員
        /// </summary>
        protected IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        /// <summary>
        /// 將ModelState中的驗證失敗錯誤訊息加到TempData["alert"]裡
        /// </summary>
        protected void SetModelStateError()
        {
            var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

            foreach (var error in errors)
            {
                this.TempData["alert"] += error.ErrorMessage + "<br/>";
            }
        }
    }
}