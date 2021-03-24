using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтом
    /// </summary>
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        /// <summary>
        /// GET метод регистрации
        /// </summary>
        /// <returns>Представление для регистрации если пользователь не авторизован, иначе перенаправляет на страницу аккаунта</returns>
        public ActionResult Register()
        {
            //Если пользователь авторизован
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Account", "Home");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// POST метод авторизации
        /// </summary>
        /// <param name="model">Модель регистрации</param>
        /// <param name="check">Соглашение на обработку персональных данных</param>
        /// <returns>Если регистрация прошла успешно, то перенаправляет на страницу авторизации, иначе возвращает представление регистрации с указанием ошибок</returns>
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model, bool check)
        {
            //Если не согласен с обработкой персональных данных
            if (!check)
            {
                ModelState.AddModelError("", "Вы не дали согласие на обработку персональных данных");
            }

            //Если нет ошибок
            if (ModelState.IsValid)
            {
                //Создаем нового пользователя
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName };
                //Проверяем результат попытки создания нового пользователя
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                //Успешна ли регистрация
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// GET метод авторизации
        /// </summary>
        /// <returns>Страница аккаунта, если авторизация пройдена, иначе страница авторизации с текстом ошибок</returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Account", "Home");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// POST метод авторизации
        /// </summary>
        /// <param name="model">Модель авторизации</param>
        /// <returns>Страница аккаунта, если авторизация прошла, иначе страница авторизации с текстом ошибок</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //Если состояние модели в норме
            if (ModelState.IsValid)
            {
                //Ищем пользователя в БД
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                //Если не найден
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                //Если найден
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Account", "Home");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Деавторизация
        /// </summary>
        /// <returns>Главная страница сайта</returns>
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        /// <summary>
        /// GET метод редактирования аккаунта
        /// </summary>
        /// <returns>Страница редактирования аккаунта, если пользователь авторизован, иначе страница авторизации</returns>
        [Authorize]
        public async Task<ActionResult> Edit()
        {
            //Ищем пользователя
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                //Если найден, передаем модель
                EditModel model = new EditModel { FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// POST метод редактирования аккаунта
        /// </summary>
        /// <param name="model">Модель редактирования аккаунта</param>
        /// <returns>Страницу аккаунта, если успешно, иначе текст ошибок</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            //Ищем юзера
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                //Перезаписываем поля
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                //Обновляем данные
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Account", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        /// <summary>
        /// GET метод удаления аккаунта
        /// </summary>
        /// <returns>Страница подтверждения удаления аккаунта</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// POST метод удаления аккаунта
        /// </summary>
        /// <returns>Главная страница, если успешно, иначе страница аккаунта</returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            //Ищем пользователя
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                //Удаляем
                IdentityResult result = await UserManager.DeleteAsync(user);
                //Если удаление прошло успешно
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Account", "Home");
        }
    }
}