using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    /// <summary>
    /// Класс заказа
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Айди заказа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        [StringLength(12)]
        public string UserPhone { get; set; }
        /// <summary>
        /// Комментарий пользователя
        /// </summary>
        [StringLength(128)]
        public string UserComment { get; set; }
        /// <summary>
        /// Дата оформления заказа
        /// </summary>
        [StringLength(20)]
        public string Date { get; set; }
        /// <summary>
        /// Айди пользователя
        /// </summary>
        [StringLength(128)]
        public string UserId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public ApplicationUser User { get; set; }
        /// <summary>
        /// Заказанные товары
        /// </summary>
        [StringLength(100)]
        public string OrderedProducts { get; set; }
    }
}