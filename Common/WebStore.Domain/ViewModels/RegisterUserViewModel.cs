using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(250)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
