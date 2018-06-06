namespace ToDoList_MVC5_Vue.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Po.Result;

    using ToDoList_MVC5_Vue.Library.Models;

    using Guid = Ci.Sequential.Guid;

    public class ToDoListService : BaseService
    {
        /// <summary>
        /// 使用清單名稱、帳號，新增清單
        /// </summary>
        /// <param name="listName">清單名稱</param>
        /// <param name="account">帳號</param>
        /// <returns>新增結果</returns>
        public PoResult CreateList(string listName, string account)
        {
            try
            {
                var user = this.Database.Users.FirstOrDefault(o => o.Account == account);
                if (user == null)
                    return PoResult.DbNotFound();

                this.Database.ToDoLists.Add(new ToDoList
                {
                    Id = Guid.Create(),
                    Name = listName,
                    UserId = user.Id,
                    Sort = user.ToDoLists.Count
                });

                this.Database.SaveChanges();
                return PoResult.PoSuccess();
            }
            catch (Exception e)
            {
                return PoResult.Exception(e);
            }
        }

        /// <summary>
        /// 使用使用者帳號，取得使用者的待辦清單
        /// </summary>
        /// <param name="userAccount">使用者帳號</param>
        /// <returns>使用者的待辦清單</returns>
        public PoResult<List<ToDoList>> GetUserToDoLists(string userAccount)
        {
            try
            {
                var toDoLists = this.Database.ToDoLists.Where(o => o.User.Account == userAccount).Include(o => o.Folder).OrderBy(o => o.Sort).ToList();
                return PoResult<List<ToDoList>>.PoSuccess(toDoLists);
            }
            catch (Exception e)
            {
                return PoResult<List<ToDoList>>.Exception(e);
            }
        }
    }
}
