using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите Адрес электронной почты")]
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
    }
}
