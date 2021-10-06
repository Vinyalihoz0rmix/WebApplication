using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.MenuDishes
{
    public class ListMenuDishViewModel
    {
        public int MenuId { get; set; }
        public int ProviderId { get; set; }
        public List<MenuDishesViewModel> MenuDishes { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
        public SelectList FilterCategorySelection { get; set; }
        public string FilterCatalog { get; set; }
    }
}
