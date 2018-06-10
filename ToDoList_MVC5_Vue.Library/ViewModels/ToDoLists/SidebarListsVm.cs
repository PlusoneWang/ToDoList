namespace ToDoList_MVC5_Vue.Library.ViewModels.ToDoLists
{
    using System;

    /// <summary>
    /// Sidebar的待辦清單View Model
    /// </summary>
    public class SidebarListsVm
    {
        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 資料夾
        /// </summary>
        public Guid? FolderId { get; set; }

        /// <summary>
        /// 待辦事項總數
        /// </summary>
        public int TaskCount { get; set; }
    }
}
