using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    /// <summary>
    /// Контроллер для работы с главными страницами сайта
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns>Представление</returns>
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId() != null)
                return RedirectToAction("Account", "Home");

            return View();
        }

        /// <summary>
        /// Страница аккаунта
        /// </summary>
        /// <returns>Представление</returns>
        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        /// <summary>
        /// Страница каталога товаров
        /// </summary>
        /// <returns>Представление</returns>
        [Authorize]
        public ActionResult Shop()
        {
            return View();
        }
    }
}