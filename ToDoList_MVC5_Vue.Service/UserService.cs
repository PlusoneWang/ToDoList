namespace ToDoList_MVC5_Vue.Service
{
    using System;
    using System.Linq;

    using Po.Helper;
    using Po.Result;

    using ToDoList_MVC5_Vue.Library.Models;
    using ToDoList_MVC5_Vue.Library.ViewModels.Users;

    public class UserService
    {
        public PoResult<User> GetUser(string account)
        {
            try
            {
                using (var db = new ToDoListEntities())
                {
                    // TODO
                    return PoResult<User>.PoSuccess(new User());
                }
            }
            catch (Exception e)
            {
                return PoResult<User>.Exception(e);
            }
        }

        /// <summary>
        /// 使用使用者物件，新增使用者
        /// </summary>
        /// <param name="userModel">使用者物件</param>
        /// <returns>新增結果</returns>
        public PoResult CreateUser(UserCreateVm userModel)
        {
            try
            {
                using (var db = new ToDoListEntities())
                {
                    var isUserExist = db.Users.Any(o => o.Account == userModel.Account);
                    if (isUserExist)
                        return PoResult.Fail("帳號重複");
                    db.Users.Add(new User
                    {
                        Id = Ci.Sequential.Guid.Create(),
                        Account = userModel.Account,
                        Password = PasswordHash(userModel.Password),
                        CreateTime = DateTime.Now
                    });
                    db.SaveChanges();
                    return PoResult.PoSuccess();
                }
            }
            catch (Exception e)
            {
                return PoResult.Exception(e);
            }
        }

        /// <summary>
        /// 使用帳號、密碼，驗證使用者
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns>驗證結果</returns>
        public PoResult VerifyUser(string account, string password)
        {
            try
            {
                using (var db = new ToDoListEntities())
                {
                    var user = db.Users.First(o => o.Account == account);
                    return user.Password == PasswordHash(password) ? PoResult.PoSuccess() : PoResult.Fail("密碼錯誤");
                }
            }
            catch (Exception e)
            {
                return PoResult.Exception(e);
            }
        }

        /// <summary>
        /// 使用原始密碼，產生其雜湊值
        /// </summary>
        /// <param name="originalPassword">原始密碼</param>
        /// <returns>雜湊結果</returns>
        private static string PasswordHash(string originalPassword) => originalPassword.ToSha256("03402829-DE59-4868-8A0A-E33926F6A4EE");
    }
}
