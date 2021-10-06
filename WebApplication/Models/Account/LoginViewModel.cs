using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
