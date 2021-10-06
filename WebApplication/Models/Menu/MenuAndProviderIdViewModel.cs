using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication.Models.Menu
{
    public class MenuAndProviderIdViewModel
    {
        public int ProviderId { get; set; }
        public List<MenuViewModel> Menus { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
