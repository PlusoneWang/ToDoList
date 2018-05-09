namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    public class AccountController : BaseController
    {
        /// <summary>
        /// 登入Get
        /// </summary>
        /// <param name="returnUrl">登入成功後的前往位址</param>
        /// <returns>登入頁</returns>
        [AllowAnonymous]
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password, bool rememberMe)
        {
            return this.View();
        }
    }
}