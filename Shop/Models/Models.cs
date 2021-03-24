using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    /// <summary>
    /// Модель авторизации
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Модель регистрации
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Email\"")]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Имя\"")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Фамилия\"")]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Отчество\"")]
        public string MiddleName { get; set; }
    }

    /// <summary>
    /// Модель редактирования аккаунта
    /// </summary>
    public class EditModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
    }

    /// <summary>
    /// Модель оформления заказа
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required(ErrorMessage = "Введите номер, по которому наш менеджер свяжется с вами.")]
        [Phone]
        public string UserPhone { get; set; }
        /// <summary>
        /// Комментарий пользователя
        /// </summary>
        public string UserComment { get; set; }
    }
}