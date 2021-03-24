using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Data.Entity;

namespace Shop.Models
{
    /// <summary>
    /// Класс пользователя ASP.NET Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Заказы
        /// </summary>
        public ICollection<Order> Orders { get; set; }
        /// <summary>
        /// Конструктор класса ApplicationUser
        /// </summary>
        public ApplicationUser()
        {
            Orders = new List<Order>();
        }
    }

    /// <summary>
    /// Контекст
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Контекст данных для сущности заказа
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// Контекст
        /// </summary>
        public ApplicationContext() : base("DefaultConnection") { }

        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <returns>Контекст</returns>
        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }

    /// <summary>
    /// Менеджер аккаунта
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Метод ASP.NET Identity
        /// </summary>
        /// <param name="store">API UserManager'а</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }

        /// <summary>
        /// Создание менеджера аккаунта
        /// </summary>
        /// <param name="options">Настройки конфигурации</param>
        /// <param name="context">контекст</param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}