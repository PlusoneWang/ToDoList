namespace ToDoList_MVC5_Vue.Library.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 登入ViewModel
    /// </summary>
    public class LoginVm
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Display(Name = "帳號")]
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 記住我?
        /// </summary>
        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }
    }
}
