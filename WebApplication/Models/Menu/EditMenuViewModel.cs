using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Menu
{
    public class EditMenuViewModel
    {
        public int ProviderId { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите информацию")]
        [Display(Name = "Информация")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}
