using System.Web;
using System.Web.Mvc;

namespace Shop
{
    /// <summary>
    /// Фильтры
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Регистрация фильтров
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
