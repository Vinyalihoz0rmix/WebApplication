using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Catalog
{
    public class EditCatalogViewModel
    {
        public int ProviderId { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите информацию")]
        [Display(Name = "Информация")]
        public string Info { get; set; }
    }
}
