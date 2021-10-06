using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Dish
{
    public class AddDishViewModel
    {
        public int CatalogId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите информацию")]
        [Display(Name = "Информация")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Введите вес")]
        [Display(Name = "Вес")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public string Path { get; set; }
    }
}
