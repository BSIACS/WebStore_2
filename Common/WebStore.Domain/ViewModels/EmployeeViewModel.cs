using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        /// <summary>Идентификатор</summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>Имя</summary>
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(20, ErrorMessage = "Превышено допустимое количество символов (20)")]
        public string Name { get; set; }

        /// <summary>Фамилия</summary>
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(20, ErrorMessage = "Превышено допустимое количество символов (20)")]
        public string Surename { get; set; }

        /// <summary>Отчество</summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>Пол</summary>
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(7, ErrorMessage = "Превышено допустимое количество символов")]
        public string Gender { get; set; }

        /// <summary>Возраст</summary>
        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Допустимое значение находится в диапазоне 18 - 80")]
        public int? Age { get; set; }

        /// <summary>Профессия</summary>
        [Display(Name = "Профессия")]
        public int ProfessionId { get; set; }

        /// <summary>Профессия</summary>
        public Profession Profession { get; set; }
    }
}
