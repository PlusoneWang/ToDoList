namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    using ToDoList_MVC5_Vue.Library.ViewModels.Users;

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        /// <summary>
        /// 登入Get
        /// </summary>
        /// <param name="returnUrl">登入成功後的前往位址</param>
        /// <returns>登入頁</returns>
        public ActionResult Login(string returnUrl = null)
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                if (returnUrl == null || !this.Url.IsLocalUrl(returnUrl))
                {
                    return this.RedirectToAction("Index", "Home");
                }

                return this.Redirect(returnUrl);
            }

            this.TempData["returnUrl"] = returnUrl;

            return this.View();
        }

        /// <summary>
        /// 登入Post
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <param name="rememberMe">記住我</param>
        /// <returns>登入結果</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password, bool rememberMe)
        {
            return this.View();
        }

        /// <summary>
        /// 註冊頁面Get
        /// </summary>
        /// <returns>註冊頁面</returns>
        [HttpGet]
        public ActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// 註冊Post
        /// </summary>
        /// <param name="userCreateVm">新建使用者ViewModel</param>
        /// <returns>註冊結果</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserCreateVm userCreateVm)
        {
            return this.View();
        }
    }
}