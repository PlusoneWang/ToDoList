namespace ToDoList_MVC5_Vue.Web
{
    using System.Configuration;

    /// <summary>
    /// 用於取得AppSettings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Alert樣式設定
        /// </summary>
        private static string alertMode;

        /// <summary>
        /// Alert樣式設定
        /// </summary>
        public static string AlertMode => alertMode ?? (alertMode = ConfigurationManager.AppSettings["AlertMode"]);
    }
}