using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Catalog
{
    public class AddCatalogViewModel
    {
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите информацию")]
        [Display(Name = "Информация")]
        public string Info { get; set; }
    }
}
