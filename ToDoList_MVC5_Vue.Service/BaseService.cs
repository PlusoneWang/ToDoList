namespace ToDoList_MVC5_Vue.Service
{
    using System;

    using ToDoList_MVC5_Vue.Library.Models;

    /// <summary>
    /// 基礎Service，定義了其他Service會用到的共用程式碼
    /// </summary>
    public class BaseService : IDisposable
    {
        protected ToDoListEntities Database { get; set; } = new ToDoListEntities();

        public void Dispose()
        {
            this.Database.Dispose();
        }
    }
}
