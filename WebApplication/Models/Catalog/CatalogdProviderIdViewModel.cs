using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication.Models.Catalog
{
    public class CatalogdProviderIdViewModel
    {
        public int ProviderId { get; set; }
        public int? MenuId { get; set; }
        public List<CatalogViewModel> Catalogs { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
