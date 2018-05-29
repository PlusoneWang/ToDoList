namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Po.Helper;

    using ToDoList_MVC5_Vue.Library.ViewModels.Users;
    using ToDoList_MVC5_Vue.Service;

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }

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
        /// <param name="loginVm">登入資料</param>
        /// <returns>登入結果</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm loginVm)
        {
            this.SignOutCurrentUser();

            if (this.ModelState.IsValid)
            {
                var verifyResult = this.userService.VerifyUser(loginVm.Account, loginVm.Password);
                if (verifyResult.Success)
                {
                    IdentityTool.Authentication(this.AuthenticationManager, loginVm.RememberMe, loginVm.Account);
                    var returnUrl = this.TempData["returnUrl"]?.ToString();
                    if (returnUrl == null || !this.Url.IsLocalUrl(returnUrl))
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    return this.Redirect(returnUrl);
                }

                this.TempData["alert"] = "帳號或密碼錯誤";
            }
            else
            {
                this.SetModelStateError();
            }

            return this.View(loginVm);
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
            if (this.ModelState.IsValid)
            {
                var result = this.userService.CreateUser(userCreateVm);
                if (result.Success)
                {
                    this.TempData["alert"] = "註冊成功，請登入以開始使用網站功能。";
                    return this.RedirectToAction("Login");
                }

                this.TempData["alert"] = result.Message.ReplaceContent();
            }
            else
            {
                this.SetModelStateError();
            }

            return this.View(userCreateVm);
        }
    }
}