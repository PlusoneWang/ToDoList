﻿namespace ToDoList_MVC5_Vue.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Po.Result;

    using ToDoList_MVC5_Vue.Library.ViewModels.ToDoLists;
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
        public ActionResult GetListAndFolders()
        {
            var userListsResult = this.toDoListService.GetUserToDoLists(this.CurrentUser.Id);
            if (userListsResult.Success)
            {
                var sidebarLists = new List<SidebarListsVm>();
                var sidebarFolders = new List<SidebarFolderVm>();
                userListsResult.Data.ForEach(o =>
                    {
                        sidebarLists.Add(new SidebarListsVm
                        {
                            Id = o.Id,
                            FolderId = o.FolderId,
                            Name = o.Name,
                            Sort = o.Sort,
                            TaskCount = o.ToDoTasks.Count,
                        });
                        if (o.Folder != null && sidebarFolders.All(c => c.Id != o.FolderId))
                        {
                            sidebarFolders.Add(new SidebarFolderVm { Id = o.Folder.Id, Name = o.Folder.Name });
                        }
                    });

                return this.Json(
                    new PoResult<object>
                    {
                        Success = true,
                        Message = userListsResult.Message,
                        Data = new { Lists = sidebarLists, Folders = sidebarFolders }
                    },
                    JsonRequestBehavior.AllowGet);
            }

            return this.Json(userListsResult, JsonRequestBehavior.AllowGet);
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
            if (!createResult.Success) return this.Json(createResult);

            var toDoList = createResult.Data;
            return this.Json(new PoResult<SidebarListsVm>
            {
                Success = true,
                Data = new SidebarListsVm
                {
                    Id = toDoList.Id,
                    Name = toDoList.Name,
                    Sort = toDoList.Sort,
                    TaskCount = 0
                }
            });

        }

        /// <summary>
        /// 更新待辦清單的排序
        /// </summary>
        /// <param name="listIds">待辦清單Id的陣列，陣列索引即為排序</param>
        /// <returns>更新結果</returns>
        [HttpPost]
        public JsonResult SortList(Guid[] listIds)
        {
            var updateResult = this.toDoListService.UpdateListSort(listIds, this.CurrentUser.Id);
            return this.Json(updateResult);
        }
    }
}