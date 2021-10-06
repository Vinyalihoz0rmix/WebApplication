using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Account
{
    public class ProfileVIewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты не указан")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
    }
}
