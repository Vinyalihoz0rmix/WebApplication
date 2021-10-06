using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FLP { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты не указан")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
    }
}
