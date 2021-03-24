using Microsoft.AspNet.Identity;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    /// <summary>
    /// Контроллер для работы с корзиной
    /// </summary>
    public class CartController : Controller
    {
        //Контекст
        ApplicationContext db = new ApplicationContext();

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <returns>Страница каталога</returns>
        public RedirectToRouteResult AddToCart()
        {
            return RedirectToAction("Shop", "Home");
        }

        /// <summary>
        /// Удаление продукта из корзины
        /// </summary>
        /// <returns>Страница корзины</returns>
        public RedirectToRouteResult RemoveFromCart()
        {
            return RedirectToAction("Cart", "Cart");
        }

        /// <summary>
        /// Отображение страницы корзины
        /// </summary>
        /// <returns>Страница корзины</returns>
        [Authorize]
        public ActionResult Cart()
        {
            return View();
        }

        /// <summary>
        /// GET метод оформления заказа
        /// </summary>
        /// <returns>Страница оформления заказа</returns>
        [Authorize]
        [HttpGet]
        public ActionResult MakeOrder()
        {
            return View();
        }

        /// <summary>
        /// POST метод оформления заказа
        /// </summary>
        /// <param name="model">Модель оформления заказа</param>
        /// <returns>Запись в БД и переадресация на страницу завершения оформления заказа, если нет ошибок, иначе текст ошибок</returns>
        [Authorize]
        [HttpPost]
        public ActionResult MakeOrder(OrderModel model)
        {
            //Проверяем поле "Номер телефона"
            if (String.IsNullOrEmpty(model.UserPhone))
            {
                ModelState.AddModelError("", "Введите номер телефона!");
                return View(model);
            }

            Order order = new Order
            {
                UserPhone = model.UserPhone,
                UserComment = model.UserComment,
                Date = DateTime.Now.ToString(),
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                OrderedProducts = "Товар 1|1, Товар 2|2,"
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("EndOrder", "Cart");
        }

        /// <summary>
        /// Страница успешного оформления заказа
        /// </summary>
        /// <returns>Представление</returns>
        [Authorize]
        public ActionResult EndOrder()
        {
            return View();
        }
    }
}