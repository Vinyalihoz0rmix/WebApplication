using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class UserFilterListViewModel
    {
        public SelectList SearchSelection { get; set; }
        public ListUserViewModel ListUsers { get; set; }

        [Display(Name = "Поиск")]
        public string SearchString { get; set; }

        [Display(Name = "Выбор для поиска")]
        public string SearchSelectionString { get; set; }
    }
}
