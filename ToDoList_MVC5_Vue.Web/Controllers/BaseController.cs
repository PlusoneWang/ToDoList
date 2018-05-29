namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
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
        /// 複寫: 在叫用動作方法之前呼叫
        /// </summary>
        /// <param name="filterContext">目前要求和動作的相關資訊</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // 用於設定TempData["alert"]要在前端使用的彈出視窗樣式，預設值:default。
            // 參考: ~/Shared/_TempDataAlert.cshtml
            this.ViewBag.AlertMode = AppSettings.AlertMode;

            // 取得當前的User
            this.ViewBag.CurrentUser = this.CurrentUser;
        }

        /// <summary>
        /// 將ModelState中的驗證失敗錯誤訊息加到TempData["alert"]裡
        /// </summary>
        protected void SetModelStateError()
        {
            var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

            foreach (var error in errors)
            {
                this.TempData["alert"] += error.ErrorMessage + "\n";
            }
        }

        /// <summary>
        /// 登出當前的使用者
        /// </summary>
        protected void SignOutCurrentUser()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
        }
    }
}