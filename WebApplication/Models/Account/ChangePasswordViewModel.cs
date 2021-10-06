using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Account
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Электроннаая почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
