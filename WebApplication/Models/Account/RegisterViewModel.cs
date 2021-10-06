using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}
