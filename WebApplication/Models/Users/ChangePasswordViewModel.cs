using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Новый Пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Старый Пароль")]
        public string OldPassword { get; set; }
    }
}
