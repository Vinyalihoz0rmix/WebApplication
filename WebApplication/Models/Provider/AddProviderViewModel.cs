using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Provider
{
    public class AddProviderViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите время работы с")]
        [Display(Name = "Время работы с")]
        public DateTime TimeWorkWith { get; set; }

        [Required(ErrorMessage = "Введите время работы до")]
        [Display(Name = "Время работы до")]
        public DateTime TimeWorkTo { get; set; }
        public bool IsActive { get; set; }
        public string Path { get; set; }
        public string WorkingDays { get; set; }
        public bool IsFavorite { get; set; }
        public string Info { get; set; }
    }
}
