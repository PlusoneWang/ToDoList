namespace ToDoList_MVC5_Vue.Library.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserCreateVm
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "帳號為必填項")]
        [StringLength(20, ErrorMessage = "帳號不得大於 {1} 個字")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "密碼為必填項")]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z]\\w{5,23}$", ErrorMessage = "密碼必須以英文字母開頭，由英文或數字或 _ 組成，長度介於6~24個字")]
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = "確認密碼為必填項")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "確認密碼與密碼不相符")]
        public string ConfirmPassword { get; set; }
    }
}
